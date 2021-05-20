using System.Collections.Generic;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;

namespace GranSteL.Chatbot.Messengers.Tests.Controllers
{
    public abstract class ControllerTests<T> where T : Controller
    {
        protected MockRepository MockRepository;

        protected T Target;

        protected Fixture Fixture;

        protected void InitTestBase()
        {
            MockRepository = new MockRepository(MockBehavior.Default);

            Fixture = new Fixture { OmitAutoProperties = true };
        }

        protected void SetControllerContext()
        {
            var path = string.Join("/", Fixture.CreateMany<string>());

            var pathString = new PathString($"/{path}");

            var host = new HostString(Fixture.Create<string>());

            var request = MockRepository.Create<HttpRequest>();
            request.SetupGet(r => r.Path).Returns(pathString);
            request.SetupGet(r => r.Host).Returns(host);
            request.SetupGet(r => r.PathBase).Returns(pathString);

            var context = MockRepository.Create<HttpContext>();
            context.SetupGet(c => c.Request).Returns(request.Object);

            Target.ControllerContext = Fixture.Build<ControllerContext>()
                .With(c => c.HttpContext, context.Object)
                .OmitAutoProperties()
                .Create();
        }

        protected string GetExpectedWebHookUrl()
        {
            var request = Target.Request;

            var pathBase = request.PathBase.Value;
            var pathSegment = request.Path.Value;

            var expected = $"https://{request.Host}{pathBase}{pathSegment}";

            return expected;
        }

        protected ActionExecutingContext GetActionContext(Dictionary<string, object> actionArguments)
        {
            var httpContext = MockRepository.Create<HttpContext>();

            var actionContext = Fixture.Build<ActionContext>()
                .With(c => c.HttpContext, httpContext.Object)
                .With(c => c.RouteData)
                .With(c => c.ActionDescriptor)
                .Create();

            actionContext.ActionDescriptor.Parameters = new List<ParameterDescriptor>
            {
                new ParameterDescriptor
                {
                    Name = "token"
                }
            };
            
            var filterMetadata = MockRepository.Create<IFilterMetadata>();

            var context = new ActionExecutingContext(actionContext, new List<IFilterMetadata> { filterMetadata.Object }, actionArguments, Target);

            return context;
        }
    }
}
