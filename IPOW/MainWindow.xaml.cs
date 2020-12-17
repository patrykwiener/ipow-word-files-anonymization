using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace IPOW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly Dictionary<ComboBoxItem, Func<IAlgorithm>> algorithms;
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

            algorithms = new Dictionary<ComboBoxItem, Func<IAlgorithm>>()
            {
                { algSimplePattern, () => new SimplePatternsAlgorithm(GetSelectedOptionsPatterns()) },
                { algNameScan, () => new NameScanAlgorithm() }
            };

            customTargetCheckbox.IsChecked = false;
            fileTargetTextbox.Visibility = Visibility.Hidden;
            chooseDestButton.Visibility = Visibility.Hidden;
        }

        private void ChooseButtonClick(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
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
            var algorithmProvider = algorithms[(ComboBoxItem) cbAlgorithm.SelectedItem];

            List<string> patterns = this.GetSelectedOptionsPatterns();
            string targetFilename = CreateTargetFilename(fileTextbox.Text);

            AnonymizationManager.AnonymizeWithSave(fileTextbox.Text, algorithmProvider.Invoke(), targetFilename);
            fileTextbox.Text = "";
            runButton.IsEnabled = false;
            MessageBox.Show("File was successfully anonymized", "Word Files Anonymization");
        }

        private void CustomSaveCheckboxClick(object sender, RoutedEventArgs e)
        {
            if (customTargetCheckbox.IsChecked == true)
            {
                fileTargetTextbox.Visibility = Visibility.Visible;
                chooseDestButton.Visibility = Visibility.Visible;
            } 
            else
            {
                fileTargetTextbox.Visibility = Visibility.Hidden;
                chooseDestButton.Visibility = Visibility.Hidden;
            }
        }

        private void ChooseDestButtonClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "All files (*.*)|*.*|Word files (*.docx)|*.docx",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;
                fileTargetTextbox.Text = fileName;
            }
        }

        private string CreateTargetFilename(string source)
        {
            if (customTargetCheckbox.IsChecked == true && fileTargetTextbox.Text != null && fileTargetTextbox.Text.Trim() != "")
            {
                return fileTargetTextbox.Text;
            }

            int lastSlashIndex = source.LastIndexOf('\\');
            string filename = source.Substring(lastSlashIndex + 1);
            string directory = source.Substring(0, lastSlashIndex);
            string filenameWithoutExtension = filename.Substring(0, filename.LastIndexOf('.'));
            string extension = filename.Substring(filename.LastIndexOf('.'));

            return directory + "\\" + filenameWithoutExtension + "-anonymized" + extension;
        }
    }
}
