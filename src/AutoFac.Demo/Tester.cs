/* ***********************************************
 * Author : Kevin
 * Function : 
 * Created : 2017/11/19 16:30:02
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoFac.Demo
{
    /// <summary>
    /// 
    /// </summary>
    public class Tester : IWorker, IDisposable
    {
        public int Count { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Work()
        {
            Count++;
            Console.WriteLine(string.Format("I'm a tester. And I am testing AutoFac for {0} times.", Count));
        }
    }
}
