using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! Holds the shake animation and applies it to the enemy objects
public class ShakeBehavior : MonoBehaviour
{

    //! A reference to the animator that contains the shake itself
    public Animator shakeAnimator;  

    /*!
    pre: an enemy has died and the camera needs to be shaken
    post: the camera is shaken by a call to a shakeAnimator trigger
    !*/
    public void EnemyCameraShake(){
        shakeAnimator.SetTrigger("shake");
    }
}
