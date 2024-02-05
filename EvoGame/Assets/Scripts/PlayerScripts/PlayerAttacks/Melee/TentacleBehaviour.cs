using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleBehaviour : MeleeBehaviour
{
    public Animator animator;


    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        animator.SetTrigger("swipe");
    }
}
