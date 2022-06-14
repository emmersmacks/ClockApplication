using UnityEngine;
using UnityEngine.UI;
using System;

namespace TimeTestApp.Runtime.Controllers
{
    public class DigitalTextController : MonoBehaviour
    {
        [SerializeField] Text secondsText;
        [SerializeField] Text minutesText;
        [SerializeField] Text hourseText;

        public void SetTime(DateTime time)
        {
            secondsText.text = time.Second.ToString();
            minutesText.text = time.Minute.ToString();
            hourseText.text = time.Hour.ToString();
        }
    }
}

