using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GranSteL.Chatbot.Services.Extensions
{
    public static class TasksExtensions
    {
        private static readonly ILogger Log;

        static TasksExtensions()
        {
            Log = InternalLoggerFactory.CreateLogger(nameof(TaskExtensions));
        }

        /// <summary>
        /// Fire-and-forget
        /// Allows you not to wait for the task to complete.
        /// In case of an error, an exception will be logged
        /// </summary>
        /// <param name="task"></param>
        public static void Forget(this Task task)
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    await task.ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Log?.LogError(e, "Error while executing the task");
                }
            }).ConfigureAwait(false);
        }
    }
}
