using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    public float speed; //! To set speed of bullet from unity editor

    // None of the destroy stuff works yet --TB
    public float destroyTimer; //! This is unused for bullet destruction

    private Rigidbody2D rb2d; //! The rigidbody of the bullet object, used for setting collisions
    private float initialTime; //! Also currently unused for bullet destruction

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
    @post: checks if bullet has reached its target. If so, target is destroyed.
    !*/
    void Update()
    {
        if (initialTime + destroyTimer > Time.time) {
            //Destroy(gameObject);
        }
    }
}
