using UnityEngine;
using System;
using TimeTestApp.Data;
using TimeTestApp.Runtime.Module;

namespace TimeTestApp.Runtime.Applications
{
    public class TimeApplication : MonoBehaviour
    {
        [SerializeField] private AnalogTimeModule _analogTimeController;
        [SerializeField] private DigitalTimeModule _digitalTimeController;

        public Action OnTick = default;

        internal DateTime CurrentTime;
        private float _currentMilliseconds;
        private DateTime _lastTimeCheck;
        const int _oneSecond = 1;

        private void Start()
        {
            SetCurrentTime();
        }

        private void SetCurrentTime()
        {
            var timeString = DataLoader.GetTimeFromWeb("https://www.ntp-servers.net/?ref=xranks");
            timeString = DataLoader.GetTimeFromWeb("https://www.google.ru");
            CurrentTime = DataParser.ParseToDateTime(timeString);
        }

        private void Update()
        {
            UpdateMillisecondsCounter();
            UpdateSecondsCounter();
        }

        private void UpdateMillisecondsCounter()
        {
            _currentMilliseconds += Time.deltaTime;
        }

        private void UpdateSecondsCounter()
        {
            if (_currentMilliseconds > _oneSecond)
            {
                _currentMilliseconds = 0;
                CurrentTime = CurrentTime.AddSeconds(_oneSecond);
                OnTick();
                CheckHourPassed();
            }
        }

        private void CheckHourPassed()
        {
            if(CurrentTime.Subtract(_lastTimeCheck).Hours >= 1)
            {
                SetCurrentTime();
            }
        }
    }
}