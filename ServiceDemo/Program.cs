﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDemo
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        static void Main()
        {
            var ServicesToRun = new ServiceBase[]
            {
                new Agent()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
