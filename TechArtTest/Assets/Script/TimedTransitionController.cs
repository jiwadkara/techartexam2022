using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exam.Technical
{
    public class TimedTransitionController : MonoBehaviour
    {
        [SerializeField] private TransitionController transitionController;

        private float curTime;
        private bool isTimedActive;


        // Update is called once per frame
        void Update()
        {
            if (!isTimedActive) return;
            if (curTime > 0.0f)
            {
                curTime -= Time.deltaTime;
            }
            else
            {
                transitionController.Hide();
                isTimedActive = false;
            }
        }

        public void ShowForXSeconds(float time)
        {
            curTime = time;
            isTimedActive = true;
            transitionController.Show();
        }
    }

}