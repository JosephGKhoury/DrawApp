using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P1DrawJoe
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
        List<Line> lines = new List<Line>();
        List<Polyline> polylines = new List<Polyline>();
        Polyline polyline;
        Color c;
        double numberOfLines;
        double thickness;
        double deg;
        double deg1;

        private void ColorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Ändra färgen med Sliders
            byte r = (byte)RedSlider.Value;
            byte g = (byte)GreenSlider.Value;
            byte b = (byte)BlueSlider.Value;
            c = (Color.FromRgb(r, g, b));
            MainCanvas.Cursor = Cursors.Pen;
            TestLine.Stroke = new SolidColorBrush(c);
        }


        private void MainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(MainCanvas);

            if (combo.SelectedIndex == 0)
            {
                deg = 360 / numberOfLines;
                deg1 = 0;
                for (int i = 0; i < numberOfLines; i++)
                {
                    CreateLine(point, deg1);
                    deg1 += deg;
                }
                //lines.Add(CreateLine(point, 0));
                //MainCanvas.Cursor = Cursors.Pen;

            }
            else
            {
                deg = 360 / numberOfLines;
                deg1 = 0;
                for (int i = 0; i < numberOfLines; i++)
                {
                    CreatePolyline(point, deg1);
                    deg1 += deg;
                }
            }

        }

        private Polyline CreatePolyline(Point point, double angle)
        {
            polyline = new Polyline();
            polyline.Points.Add(point);
            polyline.RenderTransform = TransformAngle(angle);
            polyline.Stroke = new SolidColorBrush(c);
            polyline.StrokeThickness = thickness;
            polyline.StrokeStartLineCap = PenLineCap.Round;
            polyline.StrokeEndLineCap = PenLineCap.Round;
            MainCanvas.Children.Add(polyline);
            polylines.Add(polyline);
            return polyline;
        }

        private Transform TransformAngle(double angle)
        {
            RotateTransform transform = new RotateTransform(angle);
            transform.CenterX = MainCanvas.ActualWidth / 2;
            transform.CenterY = MainCanvas.ActualHeight / 2;
            return transform;
        }

        private Line CreateLine(Point point, double angle)
        {
            Line line = new Line();
            line.RenderTransform = TransformAngle(angle);

            line.X1 = point.X;
            line.Y1 = point.Y;
            line.X2 = point.X;
            line.Y2 = point.Y;
            line.Stroke = new SolidColorBrush(c);
            line.StrokeThickness = thickness;
            line.StrokeStartLineCap = PenLineCap.Round;
            line.StrokeEndLineCap = PenLineCap.Round;
            MainCanvas.Children.Add(line);
            lines.Add(line);
            return line;
        }

        private void MainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(MainCanvas);

            if (combo.SelectedIndex == 0)
            {
                AddPoint(e);
                lines.Clear();

            }
            else
            {
                if (polyline != null)
                {

                    polyline.Points.Add(point);
                }
                

            }
            polylines.Clear();

        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {

            AddPoint(e);

            if (combo.SelectedIndex == 0)
            {
                AddPoint(e);
            }
            else
            {
                Point point = e.GetPosition(MainCanvas);
                foreach (Polyline polyline in polylines)
                {
                    if (polyline != null)
                    {
                        polyline.Points.Add(point);
                    }
                }

            }
        }

        private void AddPoint(MouseEventArgs e)
        {
            Point point = e.GetPosition(MainCanvas);
            foreach (Line line in lines)
            {
                line.X2 = point.X;
                line.Y2 = point.Y;
            }

        }
        private void ThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsLoaded)
            {
                thickness = ThicknessSlider.Value;
                TestLine.StrokeThickness = thickness;
                ThicknessLabel.Content = thickness;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (MainCanvas.Children.Count > 0)
            {
                MainCanvas.Children.RemoveAt(MainCanvas.Children.Count - 1);
            }
        }

        private void LineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            numberOfLines = (int)LineSlider.Value;
            LineCountLabel.Content = numberOfLines;


        }

    }

}
