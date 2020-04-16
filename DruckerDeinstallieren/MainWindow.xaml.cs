using System.Windows;
using System.Diagnostics;
using System.Management;
using System.Windows.Forms;

namespace DruckerDeinstallieren
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DruckerDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DruckerList.SelectedIndex != -1)
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show(DruckerList.SelectedItem.ToString() + " wirklich löschen?", "Löschen bestätigen!", MessageBoxButtons.YesNo);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(@"C:\Windows\System32\cmd.exe");
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                    startInfo.Arguments = @" /c C:\Windows\System32\cscript.exe C:\Windows\System32\Printing_Admin_Scripts\de-DE\prnmngr.vbs -d -p " + DruckerList.SelectedItem.ToString() + " -s " + System.Net.Dns.GetHostName().ToString();

                    Process.Start(startInfo);
                }
            }
        }

        private void DruckerSearch_Click(object sender, RoutedEventArgs e)
        {
            DruckerList.Items.Clear();
            ManagementObjectSearcher printer = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            foreach (ManagementObject result in printer.Get())
            {
                DruckerList.Items.Add(result["Name"].ToString());
            }
        }

        private void Window_Initialized(object sender, System.EventArgs e)
        {
            ManagementObjectSearcher printer = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            foreach (ManagementObject result in printer.Get())
            {
                DruckerList.Items.Add(result["Name"].ToString());
            }
        }
    }
}
