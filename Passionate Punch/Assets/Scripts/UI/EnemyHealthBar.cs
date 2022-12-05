using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    Animator healthBarAnimator;
    private void Start()
    {
        healthBarAnimator = GetComponent<Animator>();
    }
    public void AnimateBar()
    {
        healthBarAnimator.SetBool("Animate", true);
    }
    public void NotAnimateBar()
    {
        healthBarAnimator.SetBool("Animate", false);
    }
}
