using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LevelController : MonoBehaviour
{

    public GameObject enemySpawn;

    public GameObject bossSpawn;

    public int level;

    //! A flag to tell if it is time to spawn a boss
    private bool bossSpawnFlag;

    void Start()
    {

    }

    void Update()
    {
        if (enemySpawn.GetComponent<WaveBehavior>().waveComplete == true) {
            bossSpawnFlag = true;
        }
    }
}
