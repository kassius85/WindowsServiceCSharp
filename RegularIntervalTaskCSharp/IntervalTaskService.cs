using System;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace RegularIntervalTaskCSharp
{
    public partial class IntervalTaskService : ServiceBase
    {
        private bool GuardaLog { get; set; } = (ConfigurationManager.AppSettings["MuestraLog"] == "S");
        //Get the Scheduled Time from AppSettings.
        //private DateTime ScheduledTime { get; set; } = DateTime.MinValue;
        private DateTime ScheduledTime { get; set; } = DateTime.Parse(ConfigurationManager.AppSettings["ScheduledTime"]);
        //private bool EnEjecucion { get; set; } = false;
        private Timer Schedular { get; set; }

        public IntervalTaskService()
        {
            InitializeComponent();
        }

        //INICIO DEL TIMER
        protected override void OnStart(string[] args)
        {
            ScheduleService();
        }

        protected override void OnStop()
        {
            Schedular.Dispose();
        }

        public void ScheduleService()
        {
            try
            {
                //Initialize the Schedular
                Schedular = new Timer(new TimerCallback(SchedularCallback));

                //Get the interval mode
                string mode = ConfigurationManager.AppSettings["Mode"].ToUpper();

                string schedule = string.Empty;
                bool executeTask = true;

                //Get the current minute
                DateTime currentDate = DateTime.Now;
                DateTime tempDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, currentDate.Minute, 0, 0);

                switch (mode)
                {
                    case "DAILY":

                        if (tempDate != ScheduledTime)
                        {
                            //If Scheduled Time is passed set Schedule for the next day.
                            if (DateTime.Now > this.ScheduledTime) ScheduledTime = ScheduledTime.AddDays(1);

                            executeTask = false;
                        }
                        else
                        {
                            ScheduledTime = ScheduledTime.AddDays(1);
                        }

                        break;

                    case "INTERVAL":
                        //Get the Interval in Minutes from AppSettings.
                        int intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"]);

                        if (intervalMinutes > 0)
                        {
                            if (tempDate != ScheduledTime)
                            {
                                if (tempDate > ScheduledTime) ScheduledTime = ScheduledTime.AddDays(1);

                                executeTask = false;
                            }
                            else
                            {
                                //Set the Scheduled Time by adding the Interval to Current Time.
                                ScheduledTime = tempDate.AddMinutes(intervalMinutes);

                                //If Scheduled Time is passed set Schedule for the next Interval.
                                if (DateTime.Now > ScheduledTime) ScheduledTime = ScheduledTime.AddMinutes(intervalMinutes);
                            }
                        }                            
                        else
                        {
                            throw new Exception("El intervalo en minutos debe ser mayor a cero.");
                        }

                        break;

                    default:
                        throw new Exception("El modo definido no es válido.");
                }

                schedule = ScheduledTime.ToString("dd/MM/yyyy hh:mm:ss tt");

                //Update Schedular
                ChangeSchedular(ScheduledTime);

                //Execute task
                //if (executeTask) PRINCIPAL();

                //Save Log
                if (GuardaLog) GuardaLogBitacora(((executeTask) ? "Proceso ejecutado! " : string.Empty) + "Próxima ejecución aproximada: " + schedule);

            }
            catch (Exception ex)
            {
                ChangeSchedular(DateTime.Now.AddHours(1));
                GuardaLogBitacora(ex.Message);
            }
        }

        private void SchedularCallback(object e)
        {
            ScheduleService();
        }

        private void ChangeSchedular(DateTime scheduledTimeTemp = default)
        {
            if (scheduledTimeTemp == DateTime.MinValue) scheduledTimeTemp = ScheduledTime;

            //Get the difference in Minutes between the Scheduled and Current Time.
            TimeSpan timeSpan = scheduledTimeTemp.Subtract(DateTime.Now);
            int dueTime = Convert.ToInt32(timeSpan.TotalMilliseconds);

            //Change the Timer's Due Time.
            Schedular.Change(dueTime, Timeout.Infinite);
        }
        //FIN DEL TIMER

        private void GuardaLogBitacora(string text)
        {
            string path = ConfigurationManager.AppSettings["PathLog"];

            if (!Directory.Exists(path)) path = @"C:\";

            string rutalog = Path.Combine(path, "Log_BitacoraProcesarCorreosGCS.txt");

            File.AppendAllText(rutalog, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " : " + text + Environment.NewLine + Environment.NewLine);
        }
    }
}