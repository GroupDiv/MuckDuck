using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //! Target Object to follow (usually player)
    private GameObject Character; 

    //! Enemy speed settable from Unity editor
    public float speed;

    //! The direction of the character from this instance of the enemy
    private Vector2 directionOfCharacter; 
    
    //! An occasionally-generated random vector to apply to enemy movement
    public Vector2 randomMovement; 

    //! A scalar to multiply randomMovement by, to amplify random movement
    public float wobbleFactor;  

    //! How long until a new randomMovement should be applied to movement
    public float wobbleWait; 

    //! The variable that keeps track of the next time to apply randomMovement
    private float nextWobble; 

    //! The class the contains the animation to apply camera shake behavior to
    private ShakeBehavior shake; 

    /*!
    * @pre: none
    * @post: assigns GameObject Character to playerObject 
    * @param playerObject: the player object we are currently interacting with
    !*/
    public void RecievePlayerParameter(GameObject playerObject)
    {
        Character = playerObject;
        shake = GameObject.FindGameObjectWithTag("Shake").GetComponent<ShakeBehavior>();
        nextWobble = Time.time;

    }

    /*!
    * @pre: none
    * @post: kills the enemy, bullet, and shakes the camera
    * @param other: object the bullet collides with
    !*/
    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy everything that leaves the trigger
        if (other.gameObject.tag == "Bullet"){
            shake.EnemyCameraShake();
            Destroy(other.gameObject);
            Destroy(gameObject);
            Character.GetComponent<PlayerController>().score += 10;
        }
    }

    /*!
    * @pre: none
    * @post: updates enemy position according to player and if nextWobble is up, as well as updating the nextWobble
    !*/
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