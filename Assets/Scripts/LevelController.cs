using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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

    void Start()
    {
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
            queenTracker.GetComponent<QueenBauss>().RecievePlayerParameter(playerObject, (bossDifficultyModifier * level), (.25f));
        }
    }

    void spawnPowerUp() {
        Instantiate(powerUpObject, powerUpSpawn.position, powerUpSpawn.rotation);
    }

    /*!
    * @pre: The boss is defeated
    * @post: Flags are reset and a new level is started.  Difficulty is updated.
    !*/
    void levelUp() {
        level ++;
        playerObject.GetComponent<PlayerController>().levelUp = false;
        playerObject.GetComponent<PlayerController>().level = level;
        currentlyBoss = false;
        enemySpawn.GetComponent<WaveBehavior>().waveComplete = false;
        enemySpawn.GetComponent<WaveBehavior>().hazardCount += waveDifficultyModifier * level;
        enemySpawn.GetComponent<WaveBehavior>().spawnWait *= 0.75f;
        scrollingBackground.GetComponent<ScrollBehavior>().scrollOffsetSpeed /= 2;
    }
}
