using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawn : MonoBehaviour
{

    //! The enemy that the wave will spawn
    public GameObject hazard; 

    //! The range from the object from which the hazards will spawn
    public Vector2 spawnValues; 

    //! How long to wait between spawns
    public float spawnWait;

    //! How long to wait at the begining of the wave's life
    public float startWait; 

    //! How long to wait in between spawns
    public float waveWait; 

    //! A reference to the player object, for passing down to the enemies so they know who to follow
    public GameObject playerObject; 

    /*!
    * @pre: none
    * @post: starts enemy spawn by calling SpawnWaves
    !*/
    void Start()
    {
        StartCoroutine (SpawnQueen());
    }

    /*!
    * @pre: none
    * @post: randomly generates continuously spawning wave of enemies
    !*/
    IEnumerator SpawnQueen()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            Vector2 spawnPosition = new Vector2(0, 4);
            Quaternion spawnRotation = Quaternion.identity;
            var obj = Instantiate(hazard, spawnPosition, spawnRotation);
            obj.GetComponent<QueenBauss>().RecievePlayerParameter(playerObject);

            yield return new WaitForSeconds(spawnWait);
        }
    }
}