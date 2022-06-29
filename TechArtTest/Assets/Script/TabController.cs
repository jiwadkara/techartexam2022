using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Exam.Technical
{
    public class TabController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] tabObjects;

        [SerializeField]
        private Button[] buttonObjects;

        void Start()
        {
            ChangeTab(0);
        }

        // Switch tabs
        public void ChangeTab(int number)
        {
            for (int i = 0; i < tabObjects.Length; i++)
            {
                if (i == number)
                {
                    buttonObjects[i].interactable = false;
                    tabObjects[i].SetActive(true);
                }
                else
                {
                    buttonObjects[i].interactable = true;
                    tabObjects[i].SetActive(false);
                }

            }
        }
    }

}