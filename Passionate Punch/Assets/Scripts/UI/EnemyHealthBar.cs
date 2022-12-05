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
        Debug.Log("This is the function");
        healthBarAnimator.SetBool("Animate", true);
    }
    public void NotAnimateBar()
    {
        Debug.Log("This is the OTHER function");
        healthBarAnimator.SetBool("Animate", false);
    }
}
