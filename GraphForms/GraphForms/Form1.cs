using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows.Forms;
using System.Diagnostics;

namespace SystemChartMonitor
{
    public partial class Form1 : Form
    {
        readonly PerformanceCounter[] CPUCounterDict = new PerformanceCounter[Environment.ProcessorCount];
        readonly PerformanceCounter RAMCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        public Form1()
        {
            InitializeComponent();
            
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Инициализация объектов Chart
            for(int proc = 0; proc < Environment.ProcessorCount; proc++)
            {
                PerformanceCounter proccounter = new PerformanceCounter("Processor", "% Processor Time", proc.ToString());
                CPUCounterDict[proc] = proccounter;
                Series cpuseries = new Series("CPU" + proc, 100);
                cpuseries.ChartType = SeriesChartType.Spline;
                CPUchart.Series.Add(cpuseries);
            }
            
        }

        private void ShowCPUUsage(object sender, EventArgs e)
        {
            for (int proc = 0; proc < Environment.ProcessorCount; proc++)
            {
                CPUchart.Series[proc].Points.AddY(CPUCounterDict[proc].NextValue());

               
            }
            RAMchart.Series[0].Points.AddY(RAMCounter.NextValue());
            
        }
    }
}
