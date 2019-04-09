using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private GameObject Character; // Target Object to follow
    public float speed; // Enemy speed
    private Vector2 directionOfCharacter;
    public Vector2 randomMovement;
    public float wobbleFactor;
    public float wobbleWait;
    private float nextWobble;

    private ShakeBehavior shake;

    /*
    * @pre: none
    * @post: assigns GameObject Character to playerObject 
    * @param playerObject: the player object we are currently interacting with
    */
    public void RecievePlayerParameter(GameObject playerObject)
    {
        Character = playerObject;
        shake = GameObject.FindGameObjectWithTag("Shake").GetComponent<ShakeBehavior>();
        nextWobble = Time.time;

    }

    /*
    * @pre: none
    * @post: destroys everything a bullet comes into contact with
    * @param other: object the bullet collides with
    */
    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy everything that leaves the trigger
        if (other.gameObject.tag == "Bullet"){
            shake.EnemyCameraShake();
            Destroy(other.gameObject);
            Destroy(gameObject);
            Character.GetComponent<PlayerController>().score ++;
        }
    }

    /*
    * @pre: none
    * @post: updates player position according to player controls
    */
    void FixedUpdate()
    {
        if (nextWobble < Time.time){
            randomMovement = Random.insideUnitCircle;
            nextWobble = Time.time + wobbleWait;
        }

        directionOfCharacter = Character.transform.position - transform.position;
        directionOfCharacter = directionOfCharacter.normalized + randomMovement * wobbleFactor;    // Get Direction to Move Towards
        transform.Translate(directionOfCharacter * speed, Space.World);
    }
}