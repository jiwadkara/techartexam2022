using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Exam.Technical
{
    public class MoveObjectTo : MonoBehaviour
    {

        [SerializeField] private float dampDuration = 0.5f;

        [SerializeField] private Transform targetPosition;
        private Vector3 dampRef;

        private bool isMoving;
        private bool isFinishedMoving;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isMoving)
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition.position, ref dampRef, dampDuration);

            CheckForDistance();
        }

        public void StartMove()
        {
            isMoving = true;


        }
        public void CheckForDistance()
        {
            if (isFinishedMoving) return;
            if (Vector3.Distance(transform.position, targetPosition.position) < 10.0f)
            {
                isFinishedMoving = true;

            }
        }
        public bool GetFinishedMovingState()
        {
            return isFinishedMoving;
        }


    }
}
