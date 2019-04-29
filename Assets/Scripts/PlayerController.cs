using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour {

    //! speed is a multiplier to movement, because it is a public variable it can be changed without  recompiling
    public float speed;

    /*! fireRate is how fast the gun will fire.  nextFire is updated every time the gun is shot by adding fireRate to the current time.
    the shot won't be ready until this time has passed !*/
    public float fireRate;
    private float nextFire;

    /*! Like speed, friction determines how fast the duck slows down, and it is adjustable on the fly.  This may need
    a better implementation !*/
    public float friction;

    //! These variables store the limits on how far the duck can travel
    public float xMin, xMax, yMin, yMax;

    //! Defines which bullet object the duck will use to shoot.
    public GameObject bullet;

    //! A reference to the object where the bullets spawn (this is always relative to player position)
    public Transform bulletSpawn;

    //! This points to the rigidbody component of the duck
    private Rigidbody2D rb2d;

    //! a Text variable to store the score string
    public Text scoreText;

    //! keeps track of actual score value
    public int score;

    //! a Text variable to store the lives string
    public Text livesText;

    //! keeps track of lives remaining value
    public int lives;

    //! The audio played when the player is hit
    public AudioClip gameClip;

    //! The restart text displayed after a game over
    public Text restartText;

    //! The game over text displayed after a game over
    public Text gameOverText;

    //! The high score text displayed after a game over
    public Text highScoreText;

    //!  The text that displays the current level
    public Text levelText;

    //! True if game over, false otherwise
    public bool gameOver;

    //! True if the player restarts, resets to false once the game is restarted
    public bool restart;

    //! The animation component of the camera that shakes when player is hit or object is destroyed
    private ShakeBehavior shake; 

    //! Keeps track of when (in game-time) the shot power up will end
    private float powerDownTime;

    //! True if shot speed is powered-up
    private bool shotPoweredUp;

    //high score
    public int highScore = 0;

    //! keeps track of current level
    private int level;

    string highScoreKey = "HighScore";

    //testing variables
    public bool testMode = true; // true or false depending on which game mode user selects
    public bool testLives;
    public bool testRestart;
    public bool testPowerUp;
    public bool testPowerDown;
    public bool testUpdatesHighScore;
    public bool testGameOver;

    //! A flag that is true when a boss is defeated, telling the level controller to make everything more difficult
    public bool levelUp;

    /*!
    @pre: none
    @post: initializes player object with initial firing time and movement properties.
    !*/
    void Start()
    {
        levelUp = false;
        //forces the game window to be the resolution we want
        Screen.SetResolution(540, 960, true);
        rb2d = GetComponent<Rigidbody2D> ();
        nextFire = Time.time;
        gameOver = false;
        restart = false;
        gameOverText.text = "";
        restartText.text = "";
        highScoreText.text = "";
        // drag is a property of rigidbody that determines how much the object slows down.
        rb2d.drag = friction;

        score = 0;
        updateScoreString(score);
        lives = 3;
        updateLivesString(lives);
        level = 1;
        updateLevelString(level);

        shake = GameObject.FindGameObjectWithTag("Shake").GetComponent<ShakeBehavior>();

        shotPoweredUp = false;

        highScore = PlayerPrefs.GetInt(highScoreKey, 0);

        testLives = false;
        testRestart = false;
        testPowerUp = false;
        testPowerDown = false;
        testUpdatesHighScore = false;
        testGameOver = false;
    }

    /*!
    @pre: none
    @post: Updates duck position
    !*/
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

    /*!
    @pre: none
    @post: If fire command is triggered and it is time to do so based on fire rate, spawn bullet.
    !*/
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
            if (testMode && !testGameOver)
            {
                if (gameOver)
                {
                    Debug.Log("GameOver() sets gameOver to true: PASSED");
                }
                else
                {
                    Debug.Log("GameOver() sets gameOver to true: FAILED");
                }
                testGameOver = true;
            }
        }

        if (gameOver)
        {
            // Hide the duck's sprite
            this.GetComponent<SpriteRenderer>().enabled = false;
            restartText.text = "Press 'R' to Restart\nPress 'Q' to return to menu";
            restart = true;
        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel (Application.loadedLevel);

                if (testMode && !testRestart)
                {
                    if (restart)
                    {
                        Debug.Log("Restart reloads the game: PASSED");
                    }
                    else
                    {
                        Debug.Log("Restart reloads the game: FAILED");
                    }
                    testRestart = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.LoadLevel(0);  //! Returns player to main menu
            }
        }

        if (shotPoweredUp && Time.time > powerDownTime) {
            shotPowerDown();
        }
    }

    /*!
    * @pre: score has been updated
    * @post: updates score text to be displayed in UI
    * @param newScore: updated player score
    !*/
    void updateScoreString(int newScore) {
        this.scoreText.text = "SCORE: " + newScore.ToString();
    }
    
    public void updateLevelString(int newLevel) {
        this.levelText.text = "LEVEL " + newLevel.ToString();
    }

    /*!
    * @pre: score has been updated
    * @post: updates lives text to be displayed in UI
    * @param newLives: updated number of player lives remaining
    !*/
    void updateLivesString(int newLives)
    {
        this.livesText.text = "LIVES: " + newLives.ToString();
    }

    /*!
    * @pre: player hits another collider gameobject
    * @post: If the collision is with an ememy, decrements player lives
    * @param collision: collider object the player comes into contact with
    !*/
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && gameOver == false)
        {
            int initLives = this.lives;

            shake.EnemyCameraShake();
            Destroy(collision.gameObject);
            playMusic();
            this.lives--;

            if (testMode && !testLives)
            {
                if (initLives - this.lives == 1)
                {
                    Debug.Log("Lives decrement when player comes into contact with enemy: PASSED");
                }
                else
                {
                    Debug.Log("Lives decrement when player comes into contact with enemy: FAILED");
                }
                testLives = true;
            }
        }
    }

    /*!
    * @pre: none
    * @post: plays game music
    !*/
    void playMusic()
    {
        GetComponent<AudioSource>().clip = gameClip;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().volume = 1.0f; // optional
        GetComponent<AudioSource>().loop = false; // for audio looping
    }

    /*!
    * @pre: player has run out of lives
    * @post: updates game over text to "Game Over" which will be displayed in the UI
    !*/
    public void GameOver() {
        gameOverText.text = "Game Over!";
        gameOver = true;
        printHighScore();
    }

    /*!
    * @pre: game is over
    * @post: determines high score and prints result to the screen. If it's a new record, store the new high score
    !*/
    public void printHighScore() {
        if (score > highScore)
        {
            PlayerPrefs.SetInt(highScoreKey, score);
            PlayerPrefs.Save();

            if (testMode && !testUpdatesHighScore)
            {
                if (score == PlayerPrefs.GetInt(highScoreKey, 0))
                {
                    Debug.Log("Updates high score when achieved: PASSED");
                }
                else
                {
                    Debug.Log("Updates high score when achieved: FAILED");
                }
                testUpdatesHighScore = true;
            }
        }
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    /*!
    * @pre: player has picked up rate of fire powerup
    * @post: fire rate is doubled and powerDownTime is set, shotPoweredUp flag is set
    * @param powerUpDuration - length of time (in seconds) for the power up to last
    !*/
    public void shotPowerUp(float powerUpDuration) {
        float initFireRate = fireRate;
        fireRate = fireRate / 2;
        powerDownTime = Time.time + powerUpDuration;
        shotPoweredUp = true;

        if (testMode && !testPowerUp)
        {
            if (fireRate < initFireRate)
            {
                Debug.Log("PowerUps increase fire rate: PASSED");
            }
            else
            {
                Debug.Log("PowerUps increase fire rate: FAILED");
            }
            testPowerUp = true;
        }
    }

    /*!
    * @pre: firerate power up has lived its life and time is up
    * @post: resets fire rate and shotPoweredUp flag
    !*/
    public void shotPowerDown() {
        float initFireRate = fireRate;
        fireRate = fireRate * 2;
        shotPoweredUp = false;

        if (testMode && !testPowerDown)
        {
            if (fireRate > initFireRate)
            {
                Debug.Log("PowerUps decrease fire rate: PASSED");
            }
            else
            {
                Debug.Log("PowerUps decrease fire rate: FAILED");
            }
            testPowerDown = true;
        }
    }
}