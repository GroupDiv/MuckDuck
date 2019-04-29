using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBehavior : MonoBehaviour
{

    //! The enemy that the wave will spawn
    public GameObject hazard; 

    //! The range from the object from which the hazards will spawn
    public Vector2 spawnValues; 

    //! How many enemies to spawn in each wave
    public int hazardCount; 

    //! How long to wait between spawns
    public float spawnWait;

    //! How long to wait at the begining of the wave's life
    public float startWait; 

    //! A reference to the player object, for passing down to the enemies so they know who to follow
    public GameObject playerObject; 

    //! A flag that is true when a wave is completed, to send to the level controller
    public bool waveComplete;

    /*!
    * @pre: none
    * @post: starts enemy spawn by calling SpawnWaves
    !*/
    void Start()
    {
        waveComplete = false;
        StartCoroutine (SpawnWaves());
    }

    /*!
    * @pre: none
    * @post: randomly generates continuously spawning wave of enemies
    !*/
    IEnumerator SpawnWaves()
    {
        while (true) {
            yield return new WaitForSeconds(startWait);
            while (waveComplete == false)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    Vector2 spawnPosition = new Vector2(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y);
                    Quaternion spawnRotation = Quaternion.identity;
                    var obj = Instantiate(hazard, spawnPosition, spawnRotation);
                    obj.GetComponent<EnemyBehavior>().RecievePlayerParameter(playerObject);

                    yield return new WaitForSeconds(spawnWait);
                }

                waveComplete = true;
            }
    }
    }
}