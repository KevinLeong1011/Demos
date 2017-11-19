/* ***********************************************
 * Author : Kevin
 * Function : 
 * Created : 2017/11/19 16:50:01
 * ***********************************************/
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoFac.Demo
{
    /// <summary>
    /// 
    /// </summary>
    public class LifetimeTest
    {
        public static void Run()
        {
            SimpleLifetimeScope();
            InstancePerLifetimeScope();
            NamedLifetimeScope();
        }

        public static void SimpleLifetimeScope()
        {
            Console.WriteLine("=================Simple Lifetime Scope Test");

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<Tester>().As<IWorker>();
            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                for (int i = 0; i < 5; i++)
                {
                    var worker = scope.Resolve<IWorker>();
                    worker.Work();
                }
            }
            Console.WriteLine("=================END\n");
        }

        public static void InstancePerLifetimeScope()
        {
            Console.WriteLine("=================Per Lifetime Scope Test");

            ContainerBuilder builder = new ContainerBuilder();
            // only create one instance per lifetime scope. 
            // 在单个生命周期中只会创建一个实例
            builder.RegisterType<Tester>().As<IWorker>().InstancePerLifetimeScope(); 
            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                for (int i = 0; i < 5; i++)
                {
                    var worker = scope.Resolve<IWorker>(); // use scope here instead of container. 这里使用的scope，而不是container
                    worker.Work();
                }
            }

            Console.WriteLine("\nAnother lifetime scope....");
            using (var scope2 = container.BeginLifetimeScope())
            {
                for (int i = 0; i < 5; i++)
                {
                    var worker2 = scope2.Resolve<IWorker>();
                    worker2.Work();
                }
            }

            Console.WriteLine("=================END\n");
        }

        public static void NamedLifetimeScope()
        {
            Console.WriteLine("=================Named Per Lifetime Scope Test");

            ContainerBuilder builder = new ContainerBuilder();
            string scopeName = "NamedScope";
            builder.RegisterType<Tester>().As<IWorker>().InstancePerMatchingLifetimeScope(scopeName); // named scope
            var container = builder.Build();
            // Only one instance will be created in this named lifetime scope.
            // 同一个命名的生命周期中，创建的对象总是一个
            using (var scope = container.BeginLifetimeScope(scopeName))
            {
                for (int i = 0; i < 5; i++)
                {
                    var worker = scope.Resolve<IWorker>();
                    worker.Work();
                    using (var innerScope = container.BeginLifetimeScope())
                    {
                        var worker2 = scope.Resolve<IWorker>();
                        worker2.Work();
                    }
                }
            }

            Console.WriteLine("\nAnother lifetime scope....");
            // Here we see the two instances are different from which created before.
            // 下面创建的对象与前面的不同
            using (var scope = container.BeginLifetimeScope(scopeName))
            {
                for (int i = 0; i < 5; i++)
                {
                    var worker = scope.Resolve<IWorker>();
                    worker.Work();
                    using (var innerScope = container.BeginLifetimeScope())
                    {
                        var worker2 = scope.Resolve<IWorker>();
                        worker2.Work();
                    }
                }
            }

            Console.WriteLine("\nAnother lifetime scope....");
            using (var scope2 = container.BeginLifetimeScope())
            {
                try { 
                    var worker2 = scope2.Resolve<IWorker>();
                    worker2.Work();
                }
                catch
                {
                    // Exception occurred 'cause the method "BeginLifetimeScope" got nothing.
                    Console.WriteLine("Exception thrown."); // 因为该生命周期没有命名，因此抛出异常
                }
            }

            Console.WriteLine("\nAnother lifetime scope....");
            using (var scope2 = container.BeginLifetimeScope("UnknownScope"))
            {
                try
                {
                    var worker2 = scope2.Resolve<IWorker>();
                    worker2.Work();
                }
                catch
                {
                    // Exception will occur again with wrong name.
                    Console.WriteLine("Exception thrown."); // 传入的名称错误，也会抛出异常
                }
            }

            Console.WriteLine("=================END\n");
        }
    }
}
