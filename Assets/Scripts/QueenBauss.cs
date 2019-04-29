using UnityEngine;

public class QueenBauss : MonoBehaviour
{
    //! Target Object to follow (usually player)
    private GameObject Character; 

    //! Enemy speed settable from Unity editor
    public float speed;

    //! How far (horizontally) the boss can move from its origin
    public float boundary;

    //! The class the contains the animation to apply camera shake behavior to
    //public ShakeBehavior shake;

    //! Tracks the number of bullet hits the boss can take
    public int health;

    //! Tracks how much damage the boss has received, when damage == health, the boss dies
    private int damage;

    private bool dirRight;

    void Start() {
        dirRight = true;
        damage = 0;
    }

    /*!
    * @pre: none
    * @post: assigns GameObject Character to playerObject 
    * @param playerObject: the player object we are currently interacting with
    !*/
    public void RecievePlayerParameter(GameObject playerObject, int difficultyModifier)
    {
        Character = playerObject;
        health += difficultyModifier;
        //shake = GameObject.FindGameObjectWithTag("Shake").GetComponent<ShakeBehavior>();

    }

    /*!
    * @pre: none
    * @post: kills the enemy, bullet, and shakes the camera
    * @param other: object the bullet collides with
    !*/
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Something is touching me");
        if (other.gameObject.tag == "Bullet"){
            Destroy(other.gameObject);
            //shake.EnemyCameraShake();
            damage++;
        }
    }

    void Update() {
        if (damage == health)
        {
            //Debug.Log("I'm a hit");
            Destroy(gameObject);
            Character.GetComponent<PlayerController>().levelUp = true;
            Character.GetComponent<PlayerController>().score += 100;
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

        if (transform.position.x >= boundary)
        {
            dirRight = false;
        }

        if (transform.position.x <= -boundary)
        {
            dirRight = true;
        }
    }
}