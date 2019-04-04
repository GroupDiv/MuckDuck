using UnityEngine;

public class Movement : MonoBehaviour
{
    private GameObject Character; // Target Object to follow
    public float speed; // Enemy speed
    private Vector2 directionOfCharacter;

    public void RecievePlayerParameter(GameObject playerObject)
    {
        Character = playerObject;
    }

    void Update()
    {
        

        directionOfCharacter = Character.transform.position - transform.position;
        directionOfCharacter = directionOfCharacter.normalized;    // Get Direction to Move Towards
        transform.Translate(directionOfCharacter * speed, Space.World);
    }
}