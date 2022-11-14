using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMAnimations : MonoBehaviour
{
    Animator BMAnimator;
    float timeToTransition, transitionTime_2;
    void Start()
    {
        BMAnimator = GetComponent<Animator>();
        // Maximum time for the animation loop
        timeToTransition = 4.25f;
        transitionTime_2 = 10f;
    }
    void Update()
    {
        AnimTransition();
    }
    void AnimTransition()
    {
        timeToTransition -= Time.deltaTime;
        if (timeToTransition <= 0)
        {
            BMAnimator.ResetTrigger("Look");
            BMAnimator.SetTrigger("Whisper");
            transitionTime_2 -= Time.deltaTime;
        }
        if (transitionTime_2 <= 0)
        {
            // Reset the timer
            timeToTransition = 4.25f;
            transitionTime_2 = 10f;
            BMAnimator.SetTrigger("Look");
            BMAnimator.ResetTrigger("Whisper");
        }
    }
}
