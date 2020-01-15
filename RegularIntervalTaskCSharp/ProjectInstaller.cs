using System.Collections;
using System.ComponentModel;

namespace RegularIntervalTaskCSharp
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);

            //The following code starts the services after it is installed.
            using (System.ServiceProcess.ServiceController serviceController = new System.ServiceProcess.ServiceController(IntervalServiceCS.ServiceName))
            {
                serviceController.Start();
            }
        }
    }
}