using UnityEngine;

public class QueenBauss : MonoBehaviour
{
    //! Target Object to follow (usually player)
    private GameObject Character; 

    //! Enemy speed settable from Unity editor
    public float speed;

    //! The class the contains the animation to apply camera shake behavior to
    private ShakeBehavior shake;

    //! Tracks the number of time the bauss has been hit
    public int hits;

    private bool dirRight = true;

    /*!
    * @pre: none
    * @post: assigns GameObject Character to playerObject 
    * @param playerObject: the player object we are currently interacting with
    !*/
    public void RecievePlayerParameter(GameObject playerObject)
    {
        Character = playerObject;
        shake = GameObject.FindGameObjectWithTag("Shake").GetComponent<ShakeBehavior>();

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
            hits++;
        }
        if(hits == 15)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Character.GetComponent<PlayerController>().score += 10;
        }
    }

    /*!
    * @pre: none
    * @post: updates boss to move back and forth
    !*/
    void FixedUpdate()
    {
        if (dirRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= 4.0f)
        {
            dirRight = false;
        }

        if (transform.position.x <= -4)
        {
            dirRight = true;
        }
    }
}