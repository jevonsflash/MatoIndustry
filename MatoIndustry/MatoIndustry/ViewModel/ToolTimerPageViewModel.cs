using System;
using System.Collections.Generic;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MatoIndustry.Helper;
using MatoIndustry.Interface;
using MatoIndustry.Server;
using Xamarin.Forms;

namespace MatoIndustry.ViewModel
{
    public class ToolTimerPageViewModel : ViewModelBase
    {

        private Interface.IAudioService _audioService = DependencyService.Get<IAudioService>();
        private DispatcherTimer Timer;

        public ToolTimerPageViewModel()
        {

            this.PropertyChanged += ToolTimerPageViewModel_PropertyChanged;
            this.Time = new TimeSpan(0, 0, 0);
            Timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 1), TickInvocked);
            Timer.OnStatusChanged += Timer_OnStatusChanged;
            StartorPauseCommand = new RelayCommand(StartorPauseAction);
            ResetCommand = new RelayCommand(ResetAction);

        }

        private void Timer_OnStatusChanged(object sender, bool e)
        {
            this.IsRunning = e;
        }

        private void ToolTimerPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Time))
            {
                RaisePropertyChanged(nameof(Min));
                RaisePropertyChanged(nameof(Hour));
                RaisePropertyChanged(nameof(Sec));

            }
        }

        private void ResetAction()
        {
            if (Timer.IsRunning)
            {
                Timer.Stop();
                this.Sec = 0;

            }
            else
            {
                this.Time = TimeSpan.Zero;

            }
        }

        private void StartorPauseAction()
        {
            if (Timer.IsRunning)
            {
                Timer.Stop();
                this.Sec = 0;
            }
            else
            {
                if (this.Time == TimeSpan.Zero)
                {
                    this.WarnningText = TpiServer.GetWarnning();

                }
                else
                {
                    this.WarnningText = string.Empty;
                    Timer.Start();

                }
            }
        }
        private string _warnningText;

        public string WarnningText
        {
            get { return _warnningText; }
            set
            {
                _warnningText = value;
                RaisePropertyChanged();
            }
        }


        private void TickInvocked()
        {
            if (Time > TimeSpan.Zero)
            {
                Time -= TimeSpan.FromSeconds(1.0);

            }
            else
            {
                Timer.Stop();
                this._audioService.PlayAudioFile("al.wav");
            }

        }

        private TimeSpan _time;

        public TimeSpan Time
        {
            get { return _time; }
            set
            {

                _time = value;
                RaisePropertyChanged();
            }
        }
        public int Hour
        {
            get { return Time.Hours; }
            set
            {
                this.Time = new TimeSpan(value, Min, Sec);
                RaisePropertyChanged();

            }
        }
        public int Min
        {
            get { return Time.Minutes; }
            set
            {
                this.Time = new TimeSpan(Hour, value, Sec);
                RaisePropertyChanged();
            }
        }
        public int Sec
        {
            get { return Time.Seconds; }
            set
            {
                this.Time = new TimeSpan(Hour, Min, value);
                RaisePropertyChanged();
            }
        }

        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                RaisePropertyChanged();
            }
        }


        public List<int> MinSource
        {
            get
            {

                var result = new List<int>();
                for (int i = 0; i < 60; i++)
                {
                    result.Add(i);
                }
                return result;
            }

        }


        public List<int> HourSource
        {
            get
            {
                var result = new List<int>();
                for (int i = 0; i < 24; i++)
                {
                    result.Add(i);
                }
                return result;
            }

        }


        public RelayCommand StartorPauseCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }

    }
}
