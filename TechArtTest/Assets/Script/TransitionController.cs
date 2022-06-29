using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Exam.Technical
{
    public class TransitionController : MonoBehaviour
    {
        [System.Serializable]
        public class TransitionClass
        {
            public TweenClass position;
            public TweenClass rotation;
            public TweenClass scale;
            public float duration = 1.0f;
        }
        [System.Serializable]
        public class TweenClass
        {
            public bool isEnabled;
            public AnimationCurve curve;
            public Vector3 tarVal;
        }

        [SerializeField]
        private TransitionClass showAnimation;
        [SerializeField]
        private TransitionClass hideAnimation;
        [SerializeField]
        private TransitionClass clickAnimation;

        public bool triggerShow;
        public bool triggerHide;
        public bool triggerClick;

        [SerializeField]
        private bool isHide;
        [SerializeField]
        private bool isShow = true;

        private bool isAvailableForTransition;
        private float lockDuration;


        // Start is called before the first frame update
        void Start()
        {
            if (isHide) transform.localScale = new Vector3(0, 0, 1);
        }
        public void Hide()
        {
            if (!triggerHide) return;
            if (!isAvailableForTransition) return;
            if (!isHide)
            {
                TriggerTransitions(hideAnimation);
                isHide = true;
                isShow = false;
            }
        }
        public void Show()
        {
            if (!triggerShow) return;
            if (!isAvailableForTransition) return;
            if (!isShow)
            {
                TriggerTransitions(showAnimation);
                isShow = true;
                isHide = false;
            }

        }
        public void Click()
        {
            if (!isAvailableForTransition) return;
            if (!triggerClick) return;
            TriggerTransitions(clickAnimation);

        }

        void TriggerTransitions(TransitionClass transitionclass)
        {
            lockDuration = transitionclass.duration;

            if (transitionclass.position.isEnabled)
                StartCoroutine(AnimateMove(transform.localPosition, transitionclass.position.tarVal, transitionclass.duration, transitionclass.position.curve));

            if (transitionclass.scale.isEnabled)
                StartCoroutine(AnimateScale(transform.localScale, transitionclass.scale.tarVal, transitionclass.duration, transitionclass.scale.curve));

            if (transitionclass.rotation.isEnabled)
                StartCoroutine(AnimateRotation(transform.localRotation, transitionclass.rotation.tarVal, transitionclass.duration, transitionclass.rotation.curve));
        }

        void UpdateLock()
        {
            if (lockDuration > 0.0f)
            {
                lockDuration -= Time.deltaTime;
                isAvailableForTransition = false;
            }
            else
                isAvailableForTransition = true;

        }
        // Update is called once per frame
        void Update()
        {
            //Prevents double clicking from triggering the same tranisition
            UpdateLock();
        }


        /* Coroutine functions for animations */
        IEnumerator AnimateMove(Vector3 origin, Vector3 target, float maxtime, AnimationCurve curve)
        {
            float curtime = 0f;
            while (curtime <= maxtime)
            {
                curtime += Time.deltaTime;
                float percent = Mathf.Clamp01(curtime / maxtime);

                float curvePercent = curve.Evaluate(percent);

                Vector3 targetlocal = origin + target;
                Vector3 result = Vector3.LerpUnclamped(origin, targetlocal, curvePercent);
                transform.localPosition = result;

                yield return null;
            }
        }
        IEnumerator AnimateScale(Vector3 origin, Vector3 target, float maxtime, AnimationCurve curve)
        {
            float curtime = 0f;
            while (curtime <= maxtime)
            {
                curtime += Time.deltaTime;
                float percent = Mathf.Clamp01(curtime / maxtime);

                float curvePercent = curve.Evaluate(percent);
                Vector3 result = Vector3.LerpUnclamped(origin, target, curvePercent);
                transform.localScale = result;

                yield return null;
            }
        }
        IEnumerator AnimateRotation(Quaternion origin, Vector3 target, float maxtime, AnimationCurve curve)
        {
            float curtime = 0f;
            while (curtime <= maxtime)
            {
                curtime += Time.deltaTime;
                float percent = Mathf.Clamp01(curtime / maxtime);

                float curvePercent = curve.Evaluate(percent);
                Vector3 result = Vector3.LerpUnclamped(origin.eulerAngles, target, curvePercent);
                transform.localRotation = Quaternion.Euler(result);

                yield return null;
            }
        }
    }
}