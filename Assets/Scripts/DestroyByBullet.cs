using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBullet  : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy everything that leaves the trigger
        if (other.gameObject.tag == "Bullet"){
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
