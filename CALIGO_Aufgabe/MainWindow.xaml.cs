using CALIGO_Aufgabe.Features;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CALIGO_Aufgabe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // stores all Features
        public ObservableCollection<FeatureBase> Features { get; set; }

        // lookup Map, linking Type to Class
        private Dictionary<Features.Type, System.Type> ClassMap = new Dictionary<Features.Type, System.Type> {
            { CALIGO_Aufgabe.Features.Type.NONE, typeof(FeatureBase) },
            { CALIGO_Aufgabe.Features.Type.PLN, typeof(Plane) },
            { CALIGO_Aufgabe.Features.Type.CIR, typeof(Circle) },
            { CALIGO_Aufgabe.Features.Type.SPH, typeof(Sphere) },
            { CALIGO_Aufgabe.Features.Type.CYL, typeof(Cylinder) },
        };
        
        public MainWindow()
        {
            InitializeComponent();

            // Keep '.' instead of ',' for decimal seperator
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            this.Features = new ObservableCollection<FeatureBase>();

            // Bind Source Items List to Feature List
            var binding = new Binding("")
            {
                Source = Features
            };
            SourceList.SetBinding(ListBox.ItemsSourceProperty, binding);
        }

        // Load CSV File into Feature List
        private void LoadFileContent(string content)
        {
            // counter to increment File appendix for multiple Features of same type
            Dictionary<CALIGO_Aufgabe.Features.Type, int> namingCounter = new Dictionary<Features.Type, int>();

            // all input Lines
            List<string> lines = content.Split("\r\n").ToList();

            this.Features.Clear();
            // Add Header
            this.Features.Add(new Header());

            foreach(string line in lines)
            {
                // ignore empty Lines
                if (line == "")
                    continue;

                // convert line to Feature
                FeatureBase feature = ParseLine(line);
                if(feature == null)
                    continue;

                feature.OriginalIndex = Features.Count;

                // get Type
                Features.Type featureType = (Features.Type)feature.getParameter("Type").Value;

                // add Number to Name according to lookup map
                int number = 1;
                if (namingCounter.ContainsKey(featureType))
                {
                    number = ++namingCounter[featureType];
                }
                else
                    namingCounter.Add(featureType, number);


                feature.getParameter("Name").Value += "_" + number;

                // add Feature to List
                this.Features.Add(feature);
            }

            // Select first Item
            if(Features.Count > 0)
            {
                this.SourceList.SelectedIndex = 0;
            }

            // update output Text
            CreateOutput();
        }

        // Convert CSV Line to Feature
        private FeatureBase ParseLine(string line)
        {
            // extract columns
            List<string> columns = line.Split(";").ToList();

            // get Type
            Features.Type FeatureType = CALIGO_Aufgabe.Features.Type.NONE;
            if (!Enum.TryParse<Features.Type>(columns[0], out FeatureType))
                return null;

            // Get Feature Class from Lookup Map
            System.Type? FeatureTypeClass;
            if (!ClassMap.TryGetValue(FeatureType, out FeatureTypeClass))
                return null;

            

            // Create new Feature according to the Feature Class
            return ((FeatureBase)FeatureTypeClass.GetConstructor(System.Type.EmptyTypes).Invoke(null)).LoadFromCSV(columns);

        }

        // create Text for output File
        private void CreateOutput()
        {
            string targetText = "";

            // Convert all Features to strings representing a line in the output
            foreach(FeatureBase feature in this.Features)
            {
                targetText += feature.GetOutputLine();
            }

            TargetText.Text = targetText;
        }

        // open File Finder Dialog
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Table (*.csv)|*.csv|Text File (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
                LoadFileContent(File.ReadAllText(openFileDialog.FileName));
        }

        // open File Save Dialog
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, TargetText.Text);
        }

        // Show all parameters of Selected Feature
        private void SourceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SourceListDetails.Items.Clear();
            foreach (Features.Parameter parameter in this.Features[SourceList.SelectedIndex].Parameters)
            {
                if (parameter.enabled)
                    this.SourceListDetails.Items.Add(parameter);
            }
        }

        // Update Feature List and Output Text on Text Input
        private void TextChangedHandler(object sender, TextChangedEventArgs args)
        {
            SourceList.Items.Refresh();
            CreateOutput();
        }
    }
}
