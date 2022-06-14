using UnityEngine;
using System;

namespace TimeTestApp.Runtime.Applications
{
    public class AlarmApplication : MonoBehaviour
    {
        [SerializeField] private TimeApplication _timeApplication;

        internal DateTime AlarmTime;
        public Action OnAlarmTimeChange = default;

        private void Start()
        {
            _timeApplication.OnTick += CheckAlarmTime;
        }

        public void SetAlarmTime(DateTime time)
        {
            AlarmTime = time;
            OnAlarmTimeChange();
        }

        private void CheckAlarmTime()
        {
            if (_timeApplication.CurrentTime.Equals(AlarmTime))
            {
                Debug.Log("ALARM!!!");
            }
        }
    }
}

