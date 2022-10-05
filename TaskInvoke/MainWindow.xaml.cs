using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinForms = System.Windows.Forms;

namespace TaskInvoke
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker bw;
        public MainWindow()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;

            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
        }

        private void bw_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
           // throw new NotImplementedException();
        }

        private void bw_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            string fileName = e.UserState as string;
            statusInfo.Text = fileName;
            var pt = Math.Round((double)(e.ProgressPercentage) / 365 * 100);
            progressBar.Value = pt;
            txtPercentage.Text = pt.ToString()+"%";
        }

        private void bw_DoWork(object? sender, DoWorkEventArgs e)
        {
            GetFileName(e.Argument.ToString());
        }

        private void btnFolder_Click(object sender, RoutedEventArgs e)
        {

            WinForms.FolderBrowserDialog openFileDlg = new WinForms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                folderPath.Text = openFileDlg.SelectedPath;
            }

            
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(folderPath.Text.Trim())) return;
            bw.RunWorkerAsync(folderPath.Text.Trim());

        }

        private void GetFileName(string path)
        {
            int x = 1;
            string fileName, pathString;
            for (int i = 1; i <= 12; i++)
                {
                  //string pathString = System.IO.Path.Combine(folderName, $"m{i}");
                  pathString = $"m{i}";
                  //MessageBox.Show(pathString);

                  //files named with month and day
                  for (int d = 1; d <= DateTime.DaysInMonth(2021, i); d++)
                        {
                            fileName = $"{pathString}\\d{d}.txt";
                    //return fileName;
                    bw.ReportProgress(x, fileName);
                    Thread.Sleep(50);
                    x++;
                        }
                       
                    }
        
        }
    }
}
