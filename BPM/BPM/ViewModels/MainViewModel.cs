using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BPM.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private readonly List<DateTime> tapTimes;
        private double bpm;

        public MainViewModel()
        {
            tapTimes = new List<DateTime>();
            TapCommand = new Command(Tap);
        }

        public double BPM
        {
            get => bpm;
            private set
            {
                if (Equals(bpm, value)) return;
                bpm = value;
                OnPropertyChanged();
            }
        }

        public ICommand TapCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private void Tap()
        {
            tapTimes.Add(DateTime.Now);
            if (tapTimes.Count > 2)
            {
                var oldest = tapTimes.First();
                var newest = tapTimes.Last();
                var duration = newest - oldest;
                var average = new TimeSpan(duration.Ticks / tapTimes.Count);
                BPM = 60 / average.TotalSeconds;
            }
        }
    }
}