using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Domain;
using MediatR;

namespace MediatRSample
{
    class Program
    {
        private static IMediator _bus = BuildMediator();
        
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            var command = new CreateFooCommand()
            {
                Data = "Foobar",
                IssuedAt = DateTime.UtcNow
            };

            await _bus.Send(command);
            Console.WriteLine("Press any key to simulate a command that throws exception");
            Console.ReadKey();

            var badCommand = new CreateBarThatThrowsCommand()
            {
                Stuff = "Bad Stuff"
            };

            try
            {
                await _bus.Send(badCommand);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static IMediator BuildMediator()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            var mediatrOpenTypes = new[]
            {
                //typeof(IRequestHandler<>),
                //typeof(IRequestHandler<,>),
                typeof(IAsyncRequestHandler<>),
                typeof(IAsyncRequestHandler<,>),
                typeof(ICancellableAsyncRequestHandler<,>),
                typeof(INotificationHandler<>),
                typeof(IAsyncNotificationHandler<>),
                typeof(ICancellableAsyncNotificationHandler<>)
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(typeof(CreateFooCommand).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            builder.RegisterInstance(Console.Out).As<TextWriter>();

            // It appears Autofac returns the last registered types first
            //builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t =>
                {
                    object o;
                    return c.TryResolve(t, out o) ? o : null;
                };
            });

            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            var container = builder.Build();

            // The below returns:
            //  - RequestPreProcessorBehavior
            //  - RequestPostProcessorBehavior
            //  - GenericPipelineBehavior

            //var behaviors = container
            //    .Resolve<IEnumerable<IPipelineBehavior<Ping, Pong>>>()
            //    .ToList();

            var mediator = container.Resolve<IMediator>();

            return mediator;
        }
    }
}
