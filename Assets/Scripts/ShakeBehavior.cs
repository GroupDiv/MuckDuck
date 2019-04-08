using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{

    public Animator shakeAnimator;

    public void EnemyCameraShake(){
        shakeAnimator.SetTrigger("shake");
        Debug.Log("Shaking...");
    }
}
