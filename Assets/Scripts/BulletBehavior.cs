using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! Describes the behvaior of the bullets shot by the player
public class BulletBehavior : MonoBehaviour
{
    //! To set speed of bullet from unity editor
    public float speed; 


    //! This is unused for bullet destruction
    public float destroyTimer;

    //! The rigidbody of the bullet object, used for setting collisions
    private Rigidbody2D rb2d; 

    //! Also currently unused for bullet destruction
    private float initialTime;

    /*!
    @pre: none
    @post: sets bullet velocity and initial time of bullet descent
    !*/
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * speed;

        initialTime = Time.time;

    }

    /*!
    @pre: none
    @post: checks if bullet has reached its lifespoan. If so, bullet is destroyed.
    !*/
    void Update()
    {
        if (initialTime + destroyTimer > Time.time) {
            //Destroy(gameObject);
        }
    }
}
