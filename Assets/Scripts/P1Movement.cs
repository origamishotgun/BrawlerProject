﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Movement : MonoBehaviour
{
    public P1MovementController controller;
    public Animator animator; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     void Update()
    {
        
    }

    public void onLanding()
    {
        animator.SetBool("isJumping", false);
    }
}
