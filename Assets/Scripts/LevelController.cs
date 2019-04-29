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

    void Start()
    {
        int level = 1;
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
            queenTracker.GetComponent<QueenBauss>().RecievePlayerParameter(playerObject);
        }
    }

    /*!
    * @pre: The boss is defeated
    * @post: Flags are reset and a new level is started
    !*/
    void levelUp() {
        playerObject.GetComponent<PlayerController>().levelUp = false;
        currentlyBoss = false;
        enemySpawn.GetComponent<WaveBehavior>().waveComplete = false;
        level ++;
    }
}
