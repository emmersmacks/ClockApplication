using UnityEngine;
using System;
using UnityEngine.EventSystems;
using TimeTestApp.Runtime.Controllers;
using TimeTestApp.Runtime.Applications;

namespace TimeTestApp.Runtime.Alarm.Modules
{
    public class AnalogAlarmInputModule : MonoBehaviour
    {
        [SerializeField] private AlarmApplication _alarm;
        [SerializeField] private HandleController _handleController;

        [SerializeField] private EventTrigger _triggerSeconds;
        [SerializeField] private EventTrigger _triggerMinutes;
        [SerializeField] private EventTrigger _triggerHours;

        private Vector2 _vectorRight;
        private Vector2 _touchPoint;
        private Transform _currentHand;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _vectorRight = Vector2.right;

            _alarm.OnAlarmTimeChange += delegate { _handleController.SetTime(_alarm.AlarmTime); };

            AddTriggerToHand(_handleController.secondsHand.gameObject, _triggerSeconds);
            AddTriggerToHand(_handleController.minutesHand.gameObject, _triggerMinutes);
            AddTriggerToHand(_handleController.hoursHand.gameObject, _triggerHours);
        }

        private void AddTriggerToHand(GameObject hand, EventTrigger trigger)
        {
            EventTrigger.Entry dragEntry = new EventTrigger.Entry();
            EventTrigger.Entry dragOut = new EventTrigger.Entry();
            dragEntry.eventID = EventTriggerType.Drag;
            dragOut.eventID = EventTriggerType.EndDrag;
            dragEntry.callback.AddListener(delegate { DragHand(hand); });
            dragOut.callback.AddListener(delegate { DragOut(); });
            trigger.triggers.Add(dragEntry);
            trigger.triggers.Add(dragOut);
        }

        public void DragHand(GameObject hand)
        {
            _currentHand = hand.transform;
            float z = GetHandRotationInZ();
            _currentHand.rotation = Quaternion.Euler(0, 0, z);
        }

        private void DragOut()
        {
            var currentSeconds = Math.Abs(_handleController.GetHandTimeInMinimalDivisions(_handleController.secondsHand.gameObject));
            var currentMinutes = Math.Abs(_handleController.GetHandTimeInMinimalDivisions(_handleController.minutesHand.gameObject));
            var currentHourse = Math.Abs(_handleController.GetHandTimeInMaximumDivisions(_handleController.hoursHand.gameObject));

            var time = new DateTime();

            time = time.AddSeconds(currentSeconds);
            time = time.AddMinutes(currentMinutes);
            time = time.AddHours(currentHourse);

            _alarm.SetAlarmTime(time);
        }

        private float GetHandRotationInZ()
        {
            _touchPoint = _camera.ScreenToWorldPoint(Input.mousePosition) - _currentHand.position;
            float scalarComposition = _vectorRight.x * _touchPoint.x + _vectorRight.y * _touchPoint.y;
            float mudelesComposition = _vectorRight.magnitude * _touchPoint.magnitude;
            float division = scalarComposition / mudelesComposition;
            float angle = Mathf.Acos(division) * Mathf.Rad2Deg * (int)GetSide();
            return angle - 90;
        }

        private Side GetSide()
        {
            Side side = Side.Right;
            if (_touchPoint.y <= _vectorRight.y)
                side = Side.Left;
            return side;
        }

        private void OnDestroy()
        {
            _alarm.OnAlarmTimeChange -= delegate { _handleController.SetTime(_alarm.AlarmTime); };
        }

        private enum TimeHandType
        {
            secons,
            minute,
            hour,
        }

        private enum Side
        {
            Left = -1,
            Right = 1
        }
    }
}

