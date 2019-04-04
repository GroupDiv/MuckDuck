using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform Character; // Target Object to follow
    public float speed; // Enemy speed
    private Vector2 directionOfCharacter;
    void Update()
    {
        directionOfCharacter = Character.transform.position - transform.position;
        directionOfCharacter = directionOfCharacter.normalized;    // Get Direction to Move Towards
        transform.Translate(directionOfCharacter * speed, Space.World);
    }
}