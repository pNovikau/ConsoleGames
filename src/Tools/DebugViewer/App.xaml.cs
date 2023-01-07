using System.Windows;
using Autofac;
using DebugViewer.Core.Communication.Server;
using DebugViewer.Core.Messages;
using DebugViewer.Services.Handlers;
using DebugViewer.ViewModels;

namespace DebugViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer Container = default!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ContainerBuilder();

            builder.RegisterModule<HandlersModule>();
            builder.RegisterModule<ViewModelsModule>();

            var backgroundInterProcessPipeServer = new BackgroundInterProcessPipeServer("debug_viewer");
            builder.RegisterInstance(backgroundInterProcessPipeServer)
                .SingleInstance();

            Container = builder.Build();

            backgroundInterProcessPipeServer.RegisterHandler<StartProfilingScopeMessageHandler, StartProfilingScopeMessage>(Container.Resolve<StartProfilingScopeMessageHandler>());
            backgroundInterProcessPipeServer.RegisterHandler<EndProfilingScopeMessageHandler, EndProfilingScopeMessage>(Container.Resolve<EndProfilingScopeMessageHandler>());

            backgroundInterProcessPipeServer.RegisterHandler<IncrementCounterMessageHandler, IncrementCounterMessage>(Container.Resolve<IncrementCounterMessageHandler>());
            backgroundInterProcessPipeServer.RegisterHandler<DecrementCounterMessageHandler, DecrementCounterMessage>(Container.Resolve<DecrementCounterMessageHandler>());

            backgroundInterProcessPipeServer.Run();
        }
    }
}