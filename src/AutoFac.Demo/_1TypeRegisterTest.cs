/* ***********************************************
 * Author : Kevin
 * Function : 
 * Created : 2017/11/19 16:32:02
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
    public class TypeRegisterTest
    {
        public static void Run()
        {
            Console.WriteLine("=================Register Type");

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<Tester>().As<IWorker>();
            var container = builder.Build();

            var worker = container.Resolve<IWorker>();
            worker.Work();

            builder.RegisterType<Developer>().As<IWorker>(); // Not worked. 并不起作用
            worker = container.Resolve<IWorker>();
            worker.Work();

            
            builder = new ContainerBuilder();
            builder.RegisterType<Developer>().As<IWorker>();
            container = builder.Build();
            worker = container.Resolve<IWorker>();
            worker.Work();

            Console.WriteLine("=================END\n");
        }
    }
}
