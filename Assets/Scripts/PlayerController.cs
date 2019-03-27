﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // speed is a multiplier to movement, because it is a public variable it can be changed without recompiling
    public float speed;

    // Like speed, friction determines how fast the duck slows down, and it is adjustable on the fly.  This may need
    // a better implementation
    public float friction;

    // These variables store the limits on how far the duck can travel
    public float xMin, xMax, yMin, yMax;

    // Defines which bullet object the duck will use to shoot.
    public GameObject bullet;

    // This points to the rigidbody component of the duck
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();

        // drag is a property of rigidbody that determines how much the object slows down.
        rb2d.drag = friction;
    }

    void FixedUpdate()
    {

        // Collect input data and store it to a Vector2 called movement.
        // These values will be between -1 and 1
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

        // AddForce applies forces to the body, in this case a multiple of our movement tuple
        rb2d.AddForce (movement * speed);

        rb2d.position = new Vector2
        (
            Mathf.Clamp(rb2d.position.x, xMin, xMax),
            Mathf.Clamp(rb2d.position.y, yMin, yMax)
        );
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Instantiate(bullet);
        }
    }
}