using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! A script attached to the boundary object that will destroy whatever leaves it (usually just player and boss projectiles)
public class DestroyByBoundary : MonoBehaviour
{
    /*!
    * @pre: none
    * @post: destroys everything that leaves the screen (saves memory)
    !*/
    void OnTriggerExit2D(Collider2D other)
    {
        //! Destroy everything that leaves the trigger
        Destroy(other.gameObject);
    }
}
