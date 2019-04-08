﻿using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private GameObject Character; // Target Object to follow
    public float speed; // Enemy speed
    private Vector2 directionOfCharacter;
    public Vector2 randomMovement;
    public float wobbleFactor;
    

    public void RecievePlayerParameter(GameObject playerObject)
    {
        Character = playerObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy everything that leaves the trigger
        if (other.gameObject.tag == "Bullet"){
            Destroy(other.gameObject);
            Destroy(gameObject);
            Character.GetComponent<PlayerController>().score ++;
        }
    }

    void FixedUpdate()
    {
        randomMovement = Random.insideUnitCircle;

        directionOfCharacter = Character.transform.position - transform.position;
        directionOfCharacter = directionOfCharacter.normalized + randomMovement * wobbleFactor;    // Get Direction to Move Towards
        transform.Translate(directionOfCharacter * speed, Space.World);
    }
}