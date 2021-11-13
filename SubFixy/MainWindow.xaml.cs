using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;

using Microsoft.AppCenter.Analytics;

namespace SubFixy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // whole text of fixed subtitle hold in here.
        string Result;

        // path of where fixed subtitled will save(fixed file will save in path that first file is so it's where subtitle is)
        string FixedPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// window drop down event to get file from explorer with drop down.
        /// </summary>
        private void Window_Drop(object sender, DragEventArgs e)
        {
            // don't remember what this do ;)
            Analytics.TrackEvent("File Droped");

            e.Handled = true;
            MessageShow_TxtBl.Text = "";

            string file = IsSingleFile(e);

            SubFile_TxtBx.Text = file;

            if (file != null)
            {
                if (FileValidation(file))
                {
                    // call this function to fixing subtitle
                    FileDecoding(file);

                    // writing fixed subtitle to computer
                    WriteFile(file, Result);

                    // show fixed file path in Gui
                    SubFixed_TxtBx.Text = FixedPath;

                    //show message to indicate task finish.
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
                MessageShow_TxtBl.Text = "Only Drag one File";
            }

        }// end of Window_Drop Event

        /// <summary>
        /// open Fixed file in explorer and highlight fil.
        /// </summary>
        private void OpenFile_Btn_Click(object sender, RoutedEventArgs e)
        {
            // check this to be sure task finish.
            if (SubFixed_TxtBx.Text != "")
            {
                string p = SubFixed_TxtBx.Text;

                //check file exist in computer
                if (File.Exists(p))
                {
                    // this string is set file path for explorer and /select paarametr highlight file in explorer.
                    string args = string.Format("/e, /select, \"{0}\"", p);

                    // start explorer to show file
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.FileName = "explorer";
                    info.Arguments = args;
                    Process.Start(info);
                }
            }
        }// end of OpenFile_Btn_Click event

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

        /// <summary>
        /// changing and fixing wrong format character
        /// </summary>
        /// <param name="Path">subtitle path</param>
        void FileDecoding(string Path)
        {
            MessageShow_TxtBl.Foreground = Brushes.Orange;
            MessageShow_TxtBl.Text = "Start Decoding";

            Result = new Decoder(Path).Start();
        }

        /// <summary>
        /// save file in computer
        /// </summary>
        /// <param name="path">path for save the file in there</param>
        /// <param name="result">subtitle content(all text inside subtitle file)</param>
        void WriteFile(string path,string result)
        {
            MessageShow_TxtBl.Foreground = Brushes.Orange;
            MessageShow_TxtBl.Text = "Start Writing";

            FileInfo fi = new FileInfo(path);
            
            // add 'Fixed' to name of old subtitle file and save it with with same new name
            path = fi.DirectoryName + @"\Fixed-" + fi.Name;

            StreamWriter sw = new StreamWriter(path, true);
            sw.Write(result);
            sw.Close();

            FixedPath = path;
        }// end of WriteFile 

        /// <summary>
        /// show info of app and dev(me)
        /// </summary>
        private void Info_Btn_Click(object sender, RoutedEventArgs e)
        {
            string s= " Dev : Nima Abdoli \n\n Telegram : @Abdoli_nima \n\n Code With ❤\n\n version : 0.5.45";
            MessageBox.Show(s);

        }// End of Info_btn click event

    }// End of MainWindow Class

}// End of SubFixy NameSpace
