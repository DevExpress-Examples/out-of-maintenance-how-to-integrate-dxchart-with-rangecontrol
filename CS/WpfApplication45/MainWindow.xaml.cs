using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using DevExpress.Xpf.Charts;
using System.Collections.ObjectModel;

namespace WpfApplication45
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
        ObservableCollection<DataObject> chartDataField;
        public ObservableCollection<DataObject> ChartData
        {
            get
            {
                if (chartDataField == null)
                    chartDataField = CreateData(150);
                return chartDataField;
            }
        }


        private ObservableCollection<DataObject> CreateData(int pointCount)
        {
            string[] names = new string[] { "Aaa", "Bbb", "Ccc", "Ddd", "Eee", "Fff", "Ggg" };
            Random r = new Random();
            ObservableCollection<DataObject> collection = new ObservableCollection<DataObject>();
            double vX = 50;
            double vY = 30;
            for (int i = 0; i < pointCount; i++)
            {
                vX = vX + r.Next(9) - 5;
                vY = vY + r.Next(9) - 4;
                collection.Add( new DataObject( ) { 
                    Name=  names[r.Next(names.Length)], 
                    Date = DateTime.Today.AddDays(i), 
                    ValueX = vX, 
                    ValueY = vY  });
            }
            return collection;
        }

        private void chartControl1_BoundDataChanged(object sender, RoutedEventArgs e)
        {
            XYDiagram2D diagram = ((ChartControl)sender).Diagram as XYDiagram2D;
            Axis2D axisY = diagram.ActualAxisY;
            axisY.VisualRange = new Range();
            double minValue = diagram.Series.Select(s => s.Points.Min(p => p.Value)).Min() * 1.1;
            double maxValue = diagram.Series.Select(s => s.Points.Max(p => p.Value)).Max() * 1.1;
            axisY.VisualRange.SetMinMaxValues(minValue, maxValue);   
            
            
               
            
        }
    }
    public class DataObject
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public double ValueX { get; set; }
        public double ValueY { get; set; }
    }
}
