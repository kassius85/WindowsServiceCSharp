namespace RegularIntervalTaskCSharp
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.IntervalServiceCS = new System.ServiceProcess.ServiceInstaller();
            
            // serviceProcessInstaller1
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;

            //IntervalServiceCS
            //Set the ServiceName of the Windows Service.
            this.IntervalServiceCS.Description = "Test service with execution time interval CS";
            this.IntervalServiceCS.DisplayName = "Interval Task Service CS";
            this.IntervalServiceCS.ServiceName = "IntervalServiceCS";

            //Set its StartType to Automatic.
            this.IntervalServiceCS.StartType = System.ServiceProcess.ServiceStartMode.Automatic;

            // ProjectInstaller
            this.Installers.AddRange(new System.Configuration.Install.Installer[] { this.serviceProcessInstaller1, this.IntervalServiceCS});
        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller IntervalServiceCS;
    }
}