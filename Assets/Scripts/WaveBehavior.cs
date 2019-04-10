using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBehavior : MonoBehaviour
{
    public GameObject hazard; //! The enemy that the wave will spawn
    public Vector2 spawnValues; //! The range from the object from which the hazards will spawn
    public int hazardCount; //! How many enemies to spawn in each wave

    public float spawnWait; //! How long to wait between spawns
    public float startWait; //! How long to wait at the begining of the wave's life
    public float waveWait; //! How long to wait in between spawns

    public GameObject playerObject; //! A reference to the player object, for passing down to the enemies so they know who to follow

    /*!
    * @pre: none
    * @post: starts enemy spawn by calling SpawnWaves
    !*/
    void Start()
    {
        StartCoroutine (SpawnWaves());
    }

    /*!
    * @pre: none
    * @post: randomly generates continuously spawning wave of enemies
    !*/
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector2 spawnPosition = new Vector2(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(0, spawnValues.y));
                Quaternion spawnRotation = Quaternion.identity;
                var obj = Instantiate(hazard, spawnPosition, spawnRotation);
                obj.GetComponent<EnemyBehavior>().RecievePlayerParameter(playerObject);

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}