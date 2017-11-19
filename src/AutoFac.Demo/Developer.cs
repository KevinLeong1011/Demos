/* ***********************************************
 * Author : Kevin
 * Function : 
 * Created : 2017/11/19 16:37:04
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoFac.Demo
{
    /// <summary>
    /// 
    /// </summary>
    public class Developer : IWorker
    {
        public void Work()
        {
            Console.WriteLine("I'm a developer. And I am writing demos for AutoFac.");
        }
    }
}
