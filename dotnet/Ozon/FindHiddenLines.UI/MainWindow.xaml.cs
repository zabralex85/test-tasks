using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using FindHiddenLines.Lib;
using FindHiddenLines.UI.Models;
using FindHiddenLines.UI.Utils;
using ScottPlot;

namespace FindHiddenLines.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Random _rand;

        public MainWindow()
        {
            _rand = new Random();
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            DirtyGenerate();
        }

        private void BtnDirtyGenerate_OnClick(object sender, RoutedEventArgs e)
        {
            DirtyGenerate();
        }

        private void BtnLuckGenerate_OnClick(object sender, RoutedEventArgs e)
        {
            var map = new Map(-50, 50, -50, 50);

            IEnumerable<Line> lines;
            int[] initialXPoints;
            int[] initialYPoints;
            int pointCount = 1000;

            do
            {
                initialXPoints = Generator.RandomInts(_rand, pointCount, map.MinXY.X, map.MaxXY.X).ToArray();
                initialYPoints = Generator.RandomInts(_rand, pointCount, map.MinXY.Y, map.MaxXY.Y).ToArray();

                map.Init(initialXPoints, initialYPoints);
                lines = map.GetLinesWithScan();
            } 
            while (lines == null);
            
            ShowPlot(initialXPoints, initialYPoints, lines);
        }

        private void DirtyGenerate()
        {
            var map = new Map(-50, 50, -50, 50);

            int[] initialXPoints = new[] { 1, 3, 4, 1, 3, 4, 6, 10, 14, 1, 3, 4, 5, 6, 10, 14 };
            int[] initialYPoints = new[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 0, 0, 0, 0, -2, -2, -2 };

            map.Init(initialXPoints, initialYPoints);
            var lines = map.GetLinesWithScan();

            ShowPlot(initialXPoints, initialYPoints, lines);
        }

        private void ShowPlot(IEnumerable<int> initialXPoints, IEnumerable<int> initialYPoints, IEnumerable<Line> lines)
        {
            WpfPlot.plt.Clear();

            double[] xs = initialXPoints.Select(x => (double)x).ToArray();
            double[] ys = initialYPoints.Select(x => (double)x).ToArray();

            var settings = WpfPlot.plt.GetSettings(false);

            WpfPlot.plt.PlotScatter(xs, ys, lineWidth: 0, label: "markers only");
            WpfPlot.plt.PlotVLine(0, lineStyle: LineStyle.Dash);
            WpfPlot.plt.PlotHLine(0, lineStyle: LineStyle.Dash);

            foreach (var line in lines)
            {
                settings.plottables.Add
                (
                    new SimpleLine
                    (
                        startPoint: line.StartPoint.Value,
                        endPoint: line.EndPoint.Value,
                        color: Color.Black,
                        lineWidth: 1,
                        lineStyle: LineStyle.Solid
                    )
                );
            }

            WpfPlot.Render();
        }
    }
}
