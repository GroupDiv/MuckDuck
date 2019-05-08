using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! A class of object that defines the fire-rate power up which doubles the player's fire-rate for a set amount of time
public class ShotPowerupBehavior : MonoBehaviour
{
    //! How long (in seconds) the power up should last
    public float powerUpDuration;

    //! The audio played on pickup
    public AudioClip pickUpSound;

    /*!
    @pre: an object collides with the powerup
    @post: sets the flags in PlayerController to indicate that the power-up has been picked up
    !*/
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            playAudio();
            other.gameObject.GetComponent<PlayerController>().shotPowerUp(powerUpDuration);
            Destroy(gameObject);
        }
    }

    //! Plays the power up sound
    void playAudio()
    {
        AudioSource.PlayClipAtPoint(pickUpSound, new Vector3(0, 0, -10f));
    }
}
