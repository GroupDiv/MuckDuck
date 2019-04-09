using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBullet  : MonoBehaviour
{

    public GameObject player;

    /*
    * @pre: none
    * @post: game object gets destroyed if it comes into contact with a bullet
    * @param other: other object the bullet comes into contact with
    */
    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy everything that leaves the trigger
        if (other.gameObject.tag == "Bullet"){
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
