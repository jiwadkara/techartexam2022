using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Exam.Technical
{

    public class CelebrationTaskManager : MonoBehaviour
    {
        [SerializeField] private TransitionController[] startingObjects;
        [SerializeField] private CelebrationAnimationController[] celebrationControllers;
        
        private float introTime;
        private bool finishIntro;

        private void Update()
        {
            if (finishIntro) return;
            if (introTime < 0.50f)
            {
                introTime += Time.deltaTime;
            }
            else
            {
                finishIntro = true;
                foreach (TransitionController t in startingObjects)
                {
                    t.Show();
                }
            }

        }
        public void AllCelebrateStart()
        {
            if (!finishIntro) return;
            foreach (CelebrationAnimationController c in celebrationControllers)
            {
                c.CelebrateStart();
            }
        }
        void OnGUI()
        {
            if (GUI.Button(new Rect(50, 10, 150, 100), "Reload Level"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }
    }
}