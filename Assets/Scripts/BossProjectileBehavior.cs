﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileBehavior : MonoBehaviour
{
    //! To set speed of projectile from unity editor
    public float speed;

    //! The rigidbody of the boss's projectiles, used for setting collisions
    private Rigidbody2D rb2d;

    /*!
    @pre: none
    @post: sets bullet velocity and initial time of bullet descent
    !*/
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = -(transform.up * speed);

    }

    /*!
    @pre: none
    @post: none
    !*/
    void Update()
    {

    }
}