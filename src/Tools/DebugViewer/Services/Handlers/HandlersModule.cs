using Autofac;

namespace DebugViewer.Services.Handlers;

public class HandlersModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<StartProfilingScopeMessageHandler>().SingleInstance();
        builder.RegisterType<EndProfilingScopeMessageHandler>().SingleInstance();

        builder.RegisterType<IncrementCounterMessageHandler>().SingleInstance();
        builder.RegisterType<DecrementCounterMessageHandler>().SingleInstance();
    }
}