using Microsoft.Win32;
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
using System.Windows.Shapes;
using Xceed.Words.NET;

namespace IPOW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void choose_button_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "All files (*.*)|*.*|Word files (*.docx)|*.docx";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                object readOnly = false;
                object visible = false;
                object save = false;
                string fileName = openFileDialog.FileName;

                /**object newTemplate = false;
                object docType = 0;
                object missing = Type.Missing;
                Microsoft.Office.Interop.Word._Document document;
                Microsoft.Office.Interop.Word._Application application = new Microsoft.Office.Interop.Word.Application() { Visible = false };
                document = application.Documents.Open(ref fileName, ref missing, ref readOnly, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref visible, ref missing, ref missing, ref missing, ref missing);
                document.ActiveWindow.Selection.WholeStory();
                document.ActiveWindow.Selection.Copy();

                IDataObject dataObject = Clipboard.GetDataObject();**/
                string content;

                using (DocX document = DocX.Load(fileName))
                {
                    // Make sure this document has at least one Image.
                    content = document.Text;

                    // Save this document as Output.docx.
                    //document.SaveAs("Output.docx");
                }

                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter("E:/Programowanie/dotnet/WriteLines2.txt"))
                {
                    file.WriteLine(content);
                }

                //application.Quit(ref missing, ref missing, ref missing);
            }
        }
    }
}
