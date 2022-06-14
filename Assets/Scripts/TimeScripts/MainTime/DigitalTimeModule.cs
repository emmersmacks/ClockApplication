using UnityEngine;
using TimeTestApp.Runtime.Applications;
using TimeTestApp.Runtime.Controllers;

namespace TimeTestApp.Runtime.Module
{
    public class DigitalTimeModule : MonoBehaviour, ITimeModule
    {
        [SerializeField] TimeApplication timeController;
        [SerializeField] DigitalTextController digitalTimeText;

        private void Start()
        {
            timeController.OnTick += UpdateTime;
        }

        public void UpdateTime()
        {
            var time = timeController.CurrentTime;
            digitalTimeText.SetTime(time);
        }

        private void OnDestroy()
        {
            timeController.OnTick -= UpdateTime;
        }
    }
}