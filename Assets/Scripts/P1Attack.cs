using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Attack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    void Start()
    {

    }

    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            timeBtwAttack = startTimeBtwAttack;
        }
    }



}
