using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBehavior : MonoBehaviour
{
    public GameObject hazard;
    public Vector2 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GameObject playerObject;

    /*
    * @pre: none
    * @post: starts enemy spawn by calling SpawnWaves
    */
    void Start()
    {
        StartCoroutine (SpawnWaves());
    }

    /*
    * @pre: none
    * @post: randomly generates continuously spawning wave of enemies
    */
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