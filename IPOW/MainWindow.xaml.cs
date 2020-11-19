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

namespace IPOW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly Dictionary<CheckBox, string> checkboxMappings;

        public MainWindow()
        {
            InitializeComponent();
            runButton.IsEnabled = false;
            checkboxMappings = new Dictionary<CheckBox, string>()
            {
                { peselCheckbox, PatternsModel.PESEL },
                { birthDateCheckbox, PatternsModel.BIRTH_DATE},
                { emailCheckbox, PatternsModel.EMAIL},
                { phoneNoCheckbox, PatternsModel.PHONE_NUMBER},
            };

        }

        private void ChooseButtonClick(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\Development\\C#\\",
                Filter = "All files (*.*)|*.*|Word files (*.docx)|*.docx",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                fileTextbox.Text = fileName;
                runButton.IsEnabled = true;
            }
        }

        private List<string> GetSelectedOptionsPatterns()
        {
            List<string> patterns = new List<string>();
            foreach (KeyValuePair<CheckBox, string> mapping in this.checkboxMappings)
            {
                if (mapping.Key.IsChecked == true)
                {
                    patterns.Add(mapping.Value);
                }
            }
            return patterns;
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            List<string> patterns = this.GetSelectedOptionsPatterns();
            AnonymizationManager.AnonymizeWithSave(fileTextbox.Text, patterns, "C:\\Development\\C#\\test.docx");
            fileTextbox.Text = "";
            runButton.IsEnabled = false;
        }
    }
}
