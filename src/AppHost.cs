using System.Reflection;
using System.Collections.Generic;
using ServiceStack;

namespace ToDoBackEnd
{
    public class AppHost : AppSelfHostBase
    {
        public AppHost()
          : base("HttpListener Self-Host", typeof(TodoService).GetTypeInfo().Assembly) { }

        public override void Configure(Funq.Container container)
        {
            ToDoStore todoStore = new SingletonStore();
            container.Register<ToDoStore>(todoStore);
            var todoSpecPath = "/Users/kylehodgson/projects/servicestack-todo-backend/ToDoBackend/www";
            Plugins.Add(new CorsFeature());
            SetConfig(new HostConfig
            {
                DebugMode = true,
                WebHostPhysicalPath = todoSpecPath
            });

        }
    }
}