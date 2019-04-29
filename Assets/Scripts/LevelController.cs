using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LevelController : MonoBehaviour
{

    public GameObject enemySpawn;

    //! The location to spawn the boss
    public Transform bossSpawn;

    public GameObject queen;

    public int level;

    public GameObject playerObject;

    //! A flag to tell if it is time to spawn a boss
    private bool bossSpawnFlag;

    //! True when a boss is present, false otherwise
    private bool currentlyBoss;

    void Start()
    {
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

    void spawnQueen() {
        bossSpawnFlag = false;
        if (!currentlyBoss){
            currentlyBoss = true;
            var queenTracker = Instantiate(queen, bossSpawn.position, bossSpawn.rotation);
            queenTracker.GetComponent<QueenBauss>().RecievePlayerParameter(playerObject);
        }
    }

    void levelUp() {
        playerObject.GetComponent<PlayerController>().levelUp = false;
        currentlyBoss = false;
        enemySpawn.GetComponent<WaveBehavior>().waveComplete = false;
        level ++;
    }
}
