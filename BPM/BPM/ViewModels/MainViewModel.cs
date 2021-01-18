using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace BPM.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private int seconds;
        public int Seconds
        {
            get => seconds;

            private set
            {
                seconds = value;
                OnPropertyChanged();
            }
        }
        private string moment;
        public string Moment
        {
            get => moment;

            private set
            {
                moment = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand StartCommand { get; }
        public ICommand RestartCommand { get; }
        public ICommand LapCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Thread countingThread;

        public MainViewModel()
        {
            seconds = 0;
            moment = "Start";
            Laps = new ObservableCollection<int>();

            StartCommand = new Command(Start);
            RestartCommand = new Command(Restart);
            LapCommand = new Command(Lap);

            var starter = new ThreadStart(Tick);
            countingThread = new Thread(starter);
            countingThread.Start();

        }

        private void Tick()
        {
            while (true)
            {
                if (Moment != "Stop")
                    continue;

                Seconds++;
                Thread.Sleep(1000);
            }
        }

        private void Start()
        {
            Moment = Moment == "Start" ? "Stop" : "Start";
        }

        private void Restart()
        {
            Moment = "Start";
            Seconds = 0;
            Laps.Clear();
        }
        public ObservableCollection<int> Laps { get; }
        private void Lap()
        {
            Laps.Add(Seconds);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}