using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using Tulpep.NotificationWindow;

namespace FileNotifierV2
{
    public partial class Form1 : Form
    {
        private string exePath = string.Empty;
        public string filePath = string.Empty;

        public Form1()
        {
            InitializeComponent();            
        }

        public void LogWrite(string logMessage)
        {
            exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter logFile = File.AppendText(exePath + "\\" + "log.txt"))
                {
                    logFile.Write("\r\n{0}", logMessage);
                }
            }
            catch (Exception ex)
            {
            }
        }   


        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                string time = DateTime.Now.ToString("HH:mm:ss");
                string alert = string.Format("File created {0} - {2}", e.FullPath, e.Name, time);
                //MessageBox.Show(alert);
                listBox1.BeginUpdate();
                listBox1.Items.Add(alert);
                listBox1.EndUpdate();
                LogWrite(alert);
                filePath = e.FullPath;
                popupNotifier1.TitleText = "File Created!";
                popupNotifier1.ContentText = alert;
                popupNotifier1.Popup();
            }
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                string time = DateTime.Now.ToString("HH:mm:ss");
                string alert = string.Format("File deleted {0} - {2}", e.FullPath, e.Name, time);
                //MessageBox.Show(alert);
                listBox1.BeginUpdate();
                listBox1.Items.Add(alert);
                listBox1.EndUpdate();
                LogWrite(alert);
                filePath = e.FullPath;
                popupNotifier1.TitleText = "File Deleted!";
                popupNotifier1.ContentText = alert;
                popupNotifier1.Popup();
            }
        }

        private void fileSystemWatcher1_Renamed(object sender, FileSystemEventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                string time = DateTime.Now.ToString("HH:mm:ss");
                string alert = string.Format("File renamed {0} - {2}", e.FullPath, e.Name, time);
                //MessageBox.Show(alert);
                listBox1.BeginUpdate();
                listBox1.Items.Add(alert);
                listBox1.EndUpdate();
                LogWrite(alert);
                filePath = e.FullPath;
                popupNotifier1.TitleText = "File Renamed!";
                popupNotifier1.ContentText = alert;
                popupNotifier1.Popup();
            }
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                string time = DateTime.Now.ToString("HH:mm:ss");
                string alert = string.Format("File changed {0} - {2}", e.FullPath, e.Name, time);
                //MessageBox.Show(alert);
                listBox1.BeginUpdate();
                listBox1.Items.Add(alert);
                listBox1.EndUpdate();
                LogWrite(alert);
                filePath = e.FullPath;
                popupNotifier1.TitleText = "File Changed!";
                popupNotifier1.ContentText = alert;
                popupNotifier1.Popup();
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            PathSelect();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                fileSystemWatcher1.IncludeSubdirectories = true;
            }
            else
            {
                fileSystemWatcher1.IncludeSubdirectories = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PathSelect();
        }

        private void PathSelect()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    //MessageBox.Show(fbd.SelectedPath.ToString());
                    textBox1.Text = fbd.SelectedPath;
                    fileSystemWatcher1.Path = fbd.SelectedPath;
                    
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.github.com/alperunlu");
        }

        private void popupNotifier1_Click(object sender, EventArgs e)
        {
            string argument = "/select, \"" + filePath + "\"";
            System.Diagnostics.Process.Start("explorer.exe", argument);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string time = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
            if (File.Exists("log.txt")) System.IO.File.Move("log.txt", "log"+time+".txt");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fileSystemWatcher1.EnableRaisingEvents = false;
            fileSystemWatcher1.EnableRaisingEvents = true;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string selected = listBox1.SelectedItem.ToString();
           
            if (selected.Length > 0)
            {
                int i = selected.IndexOf("d ") + 1;
                selected = selected.Substring(i);
                selected = selected.Remove(selected.IndexOf(" -") + 1);
                selected = selected.Substring(1);
                selected = selected.Remove(selected.Length - 1, 1);

            }
            //MessageBox.Show(selected);
            string argument = "/select, \"" + selected + "\"";
           // Console.WriteLine(argument);
            System.Diagnostics.Process.Start("explorer.exe", argument);

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            fileSystemWatcher1.Filter = "*.doc*";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            fileSystemWatcher1.Filter = "*.xls*";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            fileSystemWatcher1.Filter = "*.ppt*";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            fileSystemWatcher1.Filter = "*.pdf*";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            fileSystemWatcher1.Filter = "*.*";
        }
    }
}