using UnityEngine;
using System;

namespace TimeTestApp.Runtime.Controllers
{
    public class HandleController : MonoBehaviour
    {
        [SerializeField] internal Transform secondsHand;
        [SerializeField] internal Transform minutesHand;
        [SerializeField] internal Transform hoursHand;

        const float oneMinimalDivisionNumber = -6;
        const float oneMaximumDivisionNumber = -30;

        public void SetTime(DateTime time)
        {
            secondsHand.transform.eulerAngles = new Vector3(0, 0, GetHandRotateInMinimalDivision(time.Second));
            minutesHand.transform.eulerAngles = new Vector3(0, 0, GetHandRotateInMinimalDivision(time.Minute));
            int convetableHour = 0;
            if (time.Hour > 11)
                convetableHour = time.Hour - 12;
            else
                convetableHour = time.Hour;
            hoursHand.transform.eulerAngles = new Vector3(0, 0, GetHandRotateInMaximumDivision(convetableHour));
        }

        private float GetHandRotateInMinimalDivision(int timeValue)
        {
            return timeValue * oneMinimalDivisionNumber;
        }

        private float GetHandRotateInMaximumDivision(int timeValue)
        {
            return timeValue * oneMaximumDivisionNumber;
        }

        public int GetHandTimeInMinimalDivisions(GameObject hand)
        {
            if (hand.transform.eulerAngles.z < 0)
                return (int)(Math.Abs(hand.transform.eulerAngles.z) / oneMinimalDivisionNumber);
            else if (hand.transform.eulerAngles.z == 0)
                return 0;
            else
                return (int)(((180 - hand.transform.eulerAngles.z) + 180) / oneMinimalDivisionNumber);
        }

        public int GetHandTimeInMaximumDivisions(GameObject hand)
        {
            if (hand.transform.eulerAngles.z < 0)
                return (int)(Math.Abs(hand.transform.eulerAngles.z) / oneMaximumDivisionNumber);
            else if (hand.transform.eulerAngles.z == 0)
                return 0;
            else
                return (int)(((180 - hand.transform.eulerAngles.z) + 180) / oneMaximumDivisionNumber);
        }
    }
}

