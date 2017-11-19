/* ***********************************************
 * Author : Kevin
 * Function : 
 * Created : 2017/11/19 16:58:46
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
    public class SingletonTest
    {
        public static void Run()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<Tester>().As<IWorker>().SingleInstance(); // 单例
            var container = builder.Build();
            IWorker tester1 = container.Resolve<IWorker>();
            tester1.Work();

            IWorker tester2 = container.Resolve<IWorker>();
            tester2.Work();
            Console.WriteLine(tester1 == tester2); // 结果为true，证明它们实际是一个对象
        }
    }
}
