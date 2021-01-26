using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace BPM.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        Stopwatch stopwatch;
        public string lblStopWatch {get; set;}

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            
            stopwatch = new Stopwatch();
            lblStopWatch = "00:00:00.000";
            StartCommand = new Command(stopwatch.Start);
            RestartCommand = new Command(stopwatch.Restart);
           
        }

        public ICommand StartCommand { get; set; }
        public ICommand RestartCommand { get; set; }
        

        public void Start()
        {
            stopwatch.Start();
            Device.StartTimer(TimeSpan.FromSeconds(100), () =>
            {
                lblStopWatch = stopwatch.Elapsed.ToString();
                return true;
            }
        );
        }

        public void Restart() 
            { 
             stopwatch.Restart();
            }



    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}