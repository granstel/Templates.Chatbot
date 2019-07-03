using System;
using System.ServiceModel;
using Autofac;
using Autofac.Builder;
using NLog;

namespace GranSteL.Chatbot.Api.Extensions
{
    /// <summary>
    /// Extend the registration syntax with WCF-specific helpers.
    /// </summary>
    public static class RegistrationExtensions
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Dispose the channel instance in such a way that exceptions aren't thrown
        /// if a faulted channel is closed.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to set release action for.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <remarks>This will eat exceptions generated in the closing of the channel.</remarks>
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle>
            UseWcfSafeRelease<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration)
        {
            // When a channel is closed in WCF, the Dispose method calls Close internally.
            // If the channel is in a faulted state, the Close method will throw, yielding
            // an incorrect exception to be thrown during disposal. This extension fixes
            // that design problem.
            if (registration == null) throw new ArgumentNullException(nameof(registration));
            return registration.OnRelease(CloseChannel);
        }

        static void CloseChannel<T>(T channel)
        {
            var disp = (ICommunicationObject)channel;
            try
            {
                if (disp.State == CommunicationState.Faulted)
                    disp.Abort();
                else
                    disp.Close();
            }
            catch (TimeoutException e)
            {
                _log.Warn(e);

                disp.Abort();
            }
            catch (CommunicationException e)
            {
                _log.Warn(e);

                disp.Abort();
            }
            catch (Exception e)
            {
                _log.Error(e);

                disp.Abort();
                throw;
            }
        }
    }
}
