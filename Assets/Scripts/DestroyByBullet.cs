﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! The script attached to enemies that will destroy them when a bullet object enters their collider
public class DestroyByBullet  : MonoBehaviour
{
    //! A publicly-settable reference to the player, currently unused
    public GameObject player;

    /*!
    * @pre: none
    * @post: game object gets destroyed if it comes into contact with a bullet
    * @param other: other object the bullet comes into contact with
    !*/
    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy everything that leaves the trigger
        if (other.gameObject.tag == "Bullet"){
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
