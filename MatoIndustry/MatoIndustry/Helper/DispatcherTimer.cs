using System;
using Xamarin.Forms;

namespace MatoIndustry.Helper
{

    public class DispatcherTimer
    {
        public event EventHandler<bool> OnStatusChanged;

        private readonly TimeSpan _timespan;
        private readonly Action _callback;

        private bool _flag = false;

        public DispatcherTimer(TimeSpan timespan, Action callback)
        {
            this._timespan = timespan;
            this._callback = callback;
        }

        public void Start()
        {
            _flag = true;
            OnStatusChanged?.Invoke(this, _flag);
            Device.StartTimer(this._timespan,
                () =>
                {
                    if (_flag)
                    {
                        this._callback.Invoke();

                    }
                    return _flag;
                });
        }

        public void Stop()
        {
            _flag = false;
            OnStatusChanged?.Invoke(this, _flag);
        }

        public bool IsRunning => _flag;


    }

}
