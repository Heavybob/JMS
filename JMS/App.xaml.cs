using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TwitchLib.Logging;

namespace JMS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnMainWindowClose;
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        public bool DoHandle { get; set; }
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (DoHandle)
            {
                //Handling the exception within the UnhandledException handler.
                MessageBox.Show(e.Exception.Message, "Exception Caught", MessageBoxButton.OK, MessageBoxImage.Error);
                File.AppendAllText("crashlog.txt", "\n" + DateTime.Now.ToString() + " - Exception Caught: " + e.Exception.Message);
                e.Handled = true;
            }
            else
            {
                //If you do not set e.Handled to true, the application will close due to crash.
                MessageBox.Show("Application is going to close! ", "Uncaught Exception");
                File.AppendAllText("crashlog.txt", "\n" + DateTime.Now.ToString() + " - " + "Uncaught Exception");
                e.Handled = false;
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show(ex.Message, "Uncaught Thread Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            File.AppendAllText("crashlog.txt", "\n" + DateTime.Now.ToString() + " - Uncaught Thread Exception: " + ex.Message);
        }
    }
}
