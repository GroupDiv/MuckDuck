using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{

    public Animator shakeAnimator;  //! A reference to the animator that contains the shake itself

    /*!
    pre: an enemy has died and the camera needs to be shaken
    post: the camera is shaken by a call to a shakeAnimator trigger
    !*/
    public void EnemyCameraShake(){
        shakeAnimator.SetTrigger("shake");
        Debug.Log("Shaking...");
    }
}
