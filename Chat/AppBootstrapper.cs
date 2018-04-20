using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace Chat
{
    public class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            Dictionary<string, object> window_settings = new Dictionary<string, object>();

            window_settings.Add("Height", 350);
            window_settings.Add("Width", 300);
            window_settings.Add("SizeToContent", SizeToContent.Manual);
            window_settings.Add("Title", "Service Bus Chat");
            
            DisplayRootViewFor<ChatViewModel>(window_settings);
        }
    }
}
