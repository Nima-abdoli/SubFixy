using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Runtime.InteropServices;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using Microsoft.AppCenter.Analytics;

namespace SubFixy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string Result;

        string FixedPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            Analytics.TrackEvent("File Droped");
            e.Handled = true;
            MessageShow_TxtBl.Text = "";

            string file = IsSingleFile(e);

            SubFile_TxtBx.Text = file;

            if (file != null)
            {
                if (FileValidation(file))
                {
                    
                    FileDecoding(file);

                    WriteFile(file, Result);

                    SubFixed_TxtBx.Text = FixedPath;

                    MessageShow_TxtBl.Foreground = Brushes.Green;
                    MessageShow_TxtBl.Text = "Completed!!!";
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageShow_TxtBl.Foreground = Brushes.Red;
                MessageShow_TxtBl.Text = "Only Drag on File";
            }

        }

        private void OpenFile_Btn_Click(object sender, RoutedEventArgs e)
        {
            //string argument = "/select, \"" + SubFixed_TxtBx.Text + "\"";
            if (SubFixed_TxtBx.Text != "")
            {
                string p = SubFixed_TxtBx.Text;

                if (File.Exists(p))
                {
                    string args = string.Format("/e, /select, \"{0}\"", p);

                    ProcessStartInfo info = new ProcessStartInfo();
                    info.FileName = "explorer";
                    info.Arguments = args;
                    Process.Start(info);
                }
            }
            
        }

        #region Is Single File
        /// <summary>
        /// check to be only one file in args
        /// </summary>
        /// <param name="args">drag event argument that caller passed </param>
        /// <returns> return file path and when it will pass path when only one file be in args</returns>
        private string IsSingleFile(DragEventArgs args)
        {
            // Check for files in the hovering data object.
            if (args.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                var fileNames = args.Data.GetData(DataFormats.FileDrop, true) as string[];
                // Check for a single file or folder.
                if (fileNames?.Length is 1)
                {
                    // Check for a file (a directory will return false).
                    if (File.Exists(fileNames[0]))
                    {
                        // At this point we know there is a single file.
                        return fileNames[0];
                    }
                }
            }

            return null;

        }// End of IsSingleFile
        #endregion


        #region File Validation
        /// <summary>
        /// check file type validation. only get .srt and .txt
        /// </summary>
        /// <param name="Path"> Path of file</param>
        /// <returns> return bool to show file type valid or not</returns>
        bool FileValidation(string Path)
        {
            FileInfo fileInfo = new FileInfo(Path);
            string ext = fileInfo.Extension;

            if (ext == ".srt" || ext==".txt")
            {
                return true;
            }
            else
            {
                MessageShow_TxtBl.Foreground = Brushes.Red;
                MessageShow_TxtBl.Text = "Your file not valid";
                return false;
            }

        }// End Of FileValidation
        #endregion


        void FileDecoding(string Path)
        {
            MessageShow_TxtBl.Foreground = Brushes.Orange;
            MessageShow_TxtBl.Text = "Start Decoding";

            //await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            //{
            //    Result = new Decoder(Path).Start();
            //}));


            Result = new Decoder(Path).Start();
        }

        void WriteFile(string path,string result)
        {
            MessageShow_TxtBl.Foreground = Brushes.Orange;
            MessageShow_TxtBl.Text = "Start Writing";

            FileInfo fi = new FileInfo(path);
            
            path = fi.DirectoryName + @"\Fixed-" + fi.Name;

            StreamWriter sw = new StreamWriter(path, true);
            sw.Write(result);
            sw.Close();

            FixedPath = path;
        }

        private void Info_Btn_Click(object sender, RoutedEventArgs e)
        {
            string s= " Dev : Nima Abdoli \n\n Telegram : @Abdoli_nima \n\n Code With ❤\n\n version : 0.5.45";
            MessageBox.Show(s);
        }
    }// End of MainWindow Class
}// End of SubFixy NameSpace
