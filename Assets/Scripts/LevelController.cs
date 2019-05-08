using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//! The script that maintains difficulty and spawning, and updates accordingly each level
public class LevelController : MonoBehaviour
{
    //! The object that manages spawning smaller bees
    public GameObject enemySpawn;

    //! The location to spawn the boss
    public Transform bossSpawn;

    //! The game object that will be spawned in as a boss
    public GameObject queen;

    //! The variable that keeps track of the level
    public int level;

    //! The player object.  This is used for updating score and setting levelUp flags
    public GameObject playerObject;

    //! A flag to tell if it is time to spawn a boss
    private bool bossSpawnFlag;

    //! True when a boss is present, false otherwise
    private bool currentlyBoss;

    //! The number of additional enemies to spawn each successive level
    public int waveDifficultyModifier;

    //! The amount of health to add to the boss each level
    public int bossDifficultyModifier;

    //! The object to track the background (to speed up each level)
    public GameObject scrollingBackground;

    //! The object that will spawn when a power up is needed
    public GameObject powerUpObject;

    //! Tracks where to spawn the power ups
    public Transform powerUpSpawn;

    //! The score interval in which to spawn a power up
    public int powerUpThreshold;

    //! Tracks the last score at which a power up was spawned
    private int lastPowerUp;

    //! Tracks if it is time to spawn a power up
    private bool powerUpFlag;

    //! Tracks the current modification to boss's fire rate
    private float bossFireRateModifier;


    /*!
    @pre: a game is started
    @post: initializes level at easiest values
    !*/
    void Start()
    {
        bossFireRateModifier = 1;
        lastPowerUp = 0;
        level = 1;
        bossSpawnFlag = false;
        currentlyBoss = false;
    }

    void Update()
    {
        if (enemySpawn.GetComponent<WaveBehavior>().waveComplete == true && bossSpawnFlag == false) {
            bossSpawnFlag = true;
        }
        if (bossSpawnFlag == true) {
            spawnQueen();
        }
        if (playerObject.GetComponent<PlayerController>().levelUp == true){
            levelUp();
        }
        if (playerObject.GetComponent<PlayerController>().score % powerUpThreshold == 0 && playerObject.GetComponent<PlayerController>().score != lastPowerUp) {
            lastPowerUp = playerObject.GetComponent<PlayerController>().score;
            spawnPowerUp();
        }
    }

    /*!
    * @pre: The wave of small enemies is completely spawned and there is currently no boss
    * @post: A boss is spawned and all flats are properly spet 
    !*/
    void spawnQueen() {
        bossSpawnFlag = false;
        if (!currentlyBoss){
            currentlyBoss = true;
            var queenTracker = Instantiate(queen, bossSpawn.position, bossSpawn.rotation);
            queenTracker.GetComponent<QueenBauss>().RecievePlayerParameter(playerObject, (bossDifficultyModifier * level), bossFireRateModifier);
        }
    }

    /*!
    @pre: the powerup score threshold is met 
    @post: a power-up is spawned to the left of the viewable play area
    !*/
    void spawnPowerUp() {
        Instantiate(powerUpObject, powerUpSpawn.position, powerUpSpawn.rotation);
    }

    /*!
    * @pre: The boss is defeated
    * @post: Flags are reset and a new level is started.  Difficulty is updated.
    !*/
    void levelUp() {

        //! Increases the level as tracked by LevelController
        level ++;

        //! Resets the player's levelUp flag
        playerObject.GetComponent<PlayerController>().levelUp = false;

        //! Sets the player's level to match LevelController's
        playerObject.GetComponent<PlayerController>().level = level;

        //! Resets the flag that tracks if there is currently a boss on screen -- this is a redundancy
        currentlyBoss = false;

        //! Resets the flag that turns off the enemy spawner 
        enemySpawn.GetComponent<WaveBehavior>().waveComplete = false;

        //! Increases the amount of enemies that will be spawned next level
        enemySpawn.GetComponent<WaveBehavior>().hazardCount += waveDifficultyModifier * level;

        //! Decreases the amount of time between enemy spawns
        enemySpawn.GetComponent<WaveBehavior>().spawnWait *= 0.8f;

        //! Increases the speed of the scrolling background
        scrollingBackground.GetComponent<ScrollBehavior>().scrollOffsetSpeed /= 2;

        //! Increments the change to the boss's fire rate
        bossFireRateModifier *= 0.75f;
    }
}
