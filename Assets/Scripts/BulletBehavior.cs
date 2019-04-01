using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    public float speed;

    // None of the destroy stuff works yet --TB
    public float destroyTimer;

    private Rigidbody2D rb2d;
    private float initialTime;

    /*
    @pre: none
    @post: sets bullet velocity and initial time of bullet descent
    */
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * speed;

        initialTime = Time.time;

    }

    void Update()
    {
        if (initialTime + destroyTimer > Time.time) {
            //Destroy(gameObject);
        }
    }
}
