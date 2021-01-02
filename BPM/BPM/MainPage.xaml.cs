using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BPM
{
    public partial class MainPage : ContentPage
    {
        private readonly List<DateTime> tapTimes;

        public MainPage()
        {
            InitializeComponent();
            tapTimes = new List<DateTime>();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            tapTimes.Add(DateTime.Now);
            if (tapTimes.Count > 2)
            {
                var oldest = tapTimes.First();
                var newest = tapTimes.Last();
                var duration = newest - oldest;
                var average = new TimeSpan(duration.Ticks / tapTimes.Count);
                double bpm = 60 / average.TotalSeconds;
                bpmLabel.Text = Math.Round(bpm).ToString();
            }
        }
    }
}
