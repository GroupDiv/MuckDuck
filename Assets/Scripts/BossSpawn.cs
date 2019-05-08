using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! Mostly just an object used to track the location in which the queen is spawned.  The script is unused except for as a physical location in the editor.
public class BossSpawn : MonoBehaviour
{

    //As this is copied and pasted from the senemy spawn script, there is a lot of unused fluff in here

    //! The boss object to be spawned
    public GameObject hazard; 

    //! The range from the object from which the hazards will spawn -- UNUSED -TB
    public Vector2 spawnValues; 

    //! How long to wait between spawns -- UNUSUED -TB
    public float spawnWait;

    //! How long to wait at the begining of the wave's life -- UNUSED -TB
    public float startWait; 

    //! How long to wait in between spawns -- UNUSUED -TB
    public float waveWait; 

    //! A reference to the player object, for passing down to the enemies so they know who to follow -- UNUSED -TB
    public GameObject playerObject; 

    /*!
    * @pre: none
    * @post: calls the SpawnQueen() function, which is never used
    !*/
    void Start()
    {
        StartCoroutine (SpawnQueen());  // even though this is 
    }

    /*!
    * @pre: none
    * @post: spawns the queen, but is never actually used (the queen is spawned using a different script)
    !*/
    IEnumerator SpawnQueen()
    {
        yield return new WaitForSeconds(startWait);   // This will wait forever, because it is unused
        while (true)
        {
            Vector2 spawnPosition = new Vector2(0, 4);
            Quaternion spawnRotation = Quaternion.identity;
            //var obj = Instantiate(hazard, spawnPosition, spawnRotation);
            //obj.GetComponent<QueenBauss>().RecievePlayerParameter(playerObject);

            yield return new WaitForSeconds(spawnWait);
        }
    }
}