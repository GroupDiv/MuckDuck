using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
