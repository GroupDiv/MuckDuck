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

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            playAudio();
            other.gameObject.GetComponent<PlayerController>().shotPowerUp(powerUpDuration);
            Destroy(gameObject);
        }
    }

    void playAudio()
    {
        GetComponent<AudioSource>().clip = pickUpSound;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().volume = 1.0f; // optional
        GetComponent<AudioSource>().loop = false; // for audio looping
    }
}
