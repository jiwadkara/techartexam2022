using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Exam.Technical
{
    public class MainMenuHandler : MonoBehaviour
    {
        [SerializeField] private TransitionController[] mainMenuButtons;

        public void ShowAllMenuButtons()
        {
            foreach (TransitionController c in mainMenuButtons)
            {
                c.Show();
            }
        }
        public void HideAllMenuButtons()
        {
            foreach (TransitionController c in mainMenuButtons)
            {
                c.Hide();
            }
        }
    }
}