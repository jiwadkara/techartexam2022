using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingEffector : MonoBehaviour
{
    private float currentScale;
    private const float targetScale = 1.1f;
    private float startScale;
    private const int FramesCount = 100;
    [SerializeField] private float breathTime = 2.0f;

    private bool scalingUp = true;
    private IEnumerator Breath(float deltasize, float deltatime)
    {
        while (true)
        {
            while (scalingUp)
            {
                currentScale += deltasize;
                if (currentScale > targetScale)
                {
                    scalingUp = false;
                    currentScale = targetScale;
                }
                transform.localScale = Vector3.one * currentScale;
                yield return new WaitForSeconds(deltatime);
            }

            while (!scalingUp)
            {
                currentScale -= deltasize;
                if (currentScale < startScale)
                {
                    scalingUp = true;
                    currentScale = startScale;
                }
                transform.localScale = Vector3.one * currentScale;
                yield return new WaitForSeconds(deltatime);
            }
        }
    }
    private void Start()
    {
        startScale = currentScale = transform.localScale.x;
        float dt = breathTime / FramesCount;
        float ds = (targetScale - startScale) / FramesCount;

        StartCoroutine(Breath(dt, ds));
    }
}
