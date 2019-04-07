using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyByContact : MonoBehaviour
{
    public WaveBehavior game;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary" || other.tag == "EnemyDog")
		{
			return;
		}

        if (other.gameObject.tag == "Player"){
            Destroy(other.gameObject);
            Destroy(gameObject);
            game.GameOver();
        }
    }
}
