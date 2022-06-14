using UnityEngine;
using UnityEngine.UI;
using System;
using TimeTestApp.Runtime.Applications;
using TimeTestApp.Runtime.Controllers;

namespace TimeTestApp.Runtime.Alarm.Modules
{
    public class DigitalAlarmInputModule : MonoBehaviour
    {
        [SerializeField] private AlarmApplication _alarm;
        [SerializeField] private InputField _secondsField;
        [SerializeField] private InputField _minutesField;
        [SerializeField] private InputField _hoursField;

        private void Start()
        {
            _secondsField.onValueChanged.AddListener(UpdateAlarmTime);
            _minutesField.onValueChanged.AddListener(UpdateAlarmTime);
            _hoursField.onValueChanged.AddListener(UpdateAlarmTime);
            _alarm.OnAlarmTimeChange += delegate { SetTimeInInputFields(_alarm.AlarmTime); };
        }

        private void UpdateAlarmTime(string str)
        {
            var time = new DateTime();
            var number = 0;

            if (int.TryParse(_secondsField.text, out number))
                time = time.AddSeconds(number);
            if (int.TryParse(_minutesField.text, out number))
                time = time.AddMinutes(number);
            if (int.TryParse(_hoursField.text, out number))
                time = time.AddHours(number);
            _alarm.SetAlarmTime(time);
        }

        private void SetTimeInInputFields(DateTime time)
        {
            _secondsField.text = time.Second.ToString();
            _minutesField.text = time.Minute.ToString();
            _hoursField.text = time.Hour.ToString();
        }

        private void OnDestroy()
        {
            _alarm.OnAlarmTimeChange -= delegate { SetTimeInInputFields(_alarm.AlarmTime); };
        }
    }
}

