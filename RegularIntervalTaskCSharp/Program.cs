using System.ServiceProcess;

namespace RegularIntervalTaskCSharp
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun = new ServiceBase[]
            {
                new IntervalTaskService()
            };

            ServiceBase.Run(ServicesToRun);
        }
    }
}