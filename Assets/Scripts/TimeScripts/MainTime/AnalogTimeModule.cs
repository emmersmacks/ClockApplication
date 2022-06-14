using UnityEngine;
using TimeTestApp.Runtime.Applications;
using TimeTestApp.Runtime.Controllers;

namespace TimeTestApp.Runtime.Module
{
    public class AnalogTimeModule : MonoBehaviour, ITimeModule
    {
        [SerializeField] TimeApplication timeController;
        [SerializeField] HandleController handController;

        private void Start()
        {
            timeController.OnTick += UpdateTime;
        }

        public void UpdateTime()
        {
            handController.SetTime(timeController.CurrentTime);
        }

        private void OnDestroy()
        {
            timeController.OnTick -= UpdateTime;
        }
    }
}