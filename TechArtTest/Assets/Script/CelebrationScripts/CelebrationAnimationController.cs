using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Exam.Technical
{
    public class CelebrationAnimationController : MonoBehaviour
    {
        [SerializeField] TransitionController transitionController;
        [SerializeField] MoveObjectTo moveObjectTo;

        [SerializeField] GameObject trailParticle;
        [SerializeField] GameObject endParticle;
        // Update is called once per frame
        void Update()
        {

            if (moveObjectTo)
            {
                if (moveObjectTo.GetFinishedMovingState())
                {
                    if (endParticle) endParticle.SetActive(true);
                }
            }
        }
        public void CelebrateStart()
        {
            if (trailParticle) trailParticle.SetActive(true);
            if (moveObjectTo) moveObjectTo.StartMove();
            if (transitionController) transitionController.Hide();

        }
    }
}
