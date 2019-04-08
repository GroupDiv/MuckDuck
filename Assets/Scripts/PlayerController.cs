﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    // speed is a multiplier to movement, because it is a public variable it can be changed without recompiling
    public float speed;

    // fireRate is how fast the gun will fire.  nextFire is updated every time the gun is shot by adding fireRate to the current time.
    // the shot won't be ready until this time has passed
    public float fireRate;
    private float nextFire;

    // Like speed, friction determines how fast the duck slows down, and it is adjustable on the fly.  This may need
    // a better implementation
    public float friction;

    // These variables store the limits on how far the duck can travel
    public float xMin, xMax, yMin, yMax;

    // Defines which bullet object the duck will use to shoot.
    public GameObject bullet;
    public Transform bulletSpawn;

    // This points to the rigidbody component of the duck
    private Rigidbody2D rb2d;

    // a Text variable to store the score string
    public Text scoreText;
    // keeps track of actual score value
    public int score;

    // a Text variable to store the lives string
    public Text livesText;
    // keeps track of lives remaining value
    public int lives;

    public AudioClip gameClip;

    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;

    /*
    @pre: none
    @post: initializes player object with initial firing time and movement properties.
    */
    void Start()
    {
                //forces the game window to be the resolution we want
        Screen.SetResolution(540, 960, true);
        Debug.Log("DEBUG LOG INITIALIZED");
        rb2d = GetComponent<Rigidbody2D> ();
        nextFire = Time.time;
        gameOver = false;
        restart = false;
        gameOverText.text = "";
        restartText.text = "";
        // drag is a property of rigidbody that determines how much the object slows down.
        rb2d.drag = friction;

        score = 0;
        updateScoreString(score);
        lives = 3;
        updateLivesString(lives);
    }

    /*
    @pre: none
    @post: Updates duck position
    */
    void FixedUpdate()
    {

        // Collect input data and store it to a Vector2 called movement.
        // These values will be between -1 and 1
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

        // AddForce applies forces to the body, in this case a multiple of our movement tuple
        rb2d.AddForce (movement * speed);

        rb2d.position = new Vector2
        (
            Mathf.Clamp(rb2d.position.x, xMin, xMax),
            Mathf.Clamp(rb2d.position.y, yMin, yMax)
        );
    }

    /*
    @pre: none
    @post: If fire command is triggered, spawn bullet.
    */
    void Update()
    {

        // Checks if Fire1 (default spacebar) is pushed and if the timer is ready for the next fire
        if (Input.GetButton("Fire1") && nextFire < Time.time && !gameOver)
        {

            // adds time to nextFire to delay the next shot
            nextFire = Time.time + fireRate;

            // spawns a bullet at the end bullet spawn location (top of the duck)
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        }

        if (!gameOver)
        {
            // Show the duck's sprite  
            this.GetComponent<SpriteRenderer>().enabled = true;
            updateScoreString(this.score);
            updateLivesString(this.lives);
        }

        if (lives == 0)
        {
            GameOver();
        }

        if (gameOver)
        {
            // Hide the duck's sprite
            this.GetComponent<SpriteRenderer>().enabled = false;
            restartText.text = "Press 'R' to Restart";
            restart = true;
        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel (Application.loadedLevel);
            }
        }
    }

    void updateScoreString(int newScore) {
        this.scoreText.text = "SCORE: " + newScore.ToString();
    }

    void updateLivesString(int newLives)
    {
        this.livesText.text = "LIVES: " + newLives.ToString();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected");
        if (collision.gameObject.tag == "Enemy")
        {
            playMusic();
            this.lives--;
        }
    }

    void playMusic()
    {
        GetComponent<AudioSource>().clip = gameClip;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().volume = 1.0f; // optional
        GetComponent<AudioSource>().loop = false; // for audio looping
    }

    public void GameOver() {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}