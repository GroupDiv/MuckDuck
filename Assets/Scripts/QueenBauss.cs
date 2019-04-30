using System.Collections;
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

    //! Defines which bullet object the duck will use to shoot.
    public GameObject bullet;

    //! A reference to the object where the bullets spawn (this is always relative to player position)
    public Transform bulletSpawn;

    /*! fireRate is how fast the gun will fire.  nextFire is updated every time the gun is shot by adding fireRate to the current time.
    the shot won't be ready until this time has passed !*/
    public float fireRate;
    private float nextFire;

    private bool dirRight;

    void Start()
    {
        dirRight = true;
        damage = 0;
        nextFire = Time.time;
    }

    /*!
    * @pre: none
    * @post: assigns GameObject Character to playerObject 
    * @param playerObject: the player object we are currently interacting with
    !*/
    public void RecievePlayerParameter(GameObject playerObject, int difficultyModifier, float bulletFrequency)
    {
        Character = playerObject;
        health += difficultyModifier;
        fireRate -= bulletFrequency;
        //shake = GameObject.FindGameObjectWithTag("Shake").GetComponent<ShakeBehavior>();

    }

    /*!
    * @pre: none
    * @post: kills the enemy, bullet, and shakes the camera
    * @param other: object the bullet collides with
    !*/
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet"){
            Destroy(other.gameObject);
            //shake.EnemyCameraShake();
            damage++;
        }
    }

    void Update()
    {
        if (damage == health)
        {
            //Debug.Log("I'm a hit");
            Destroy(gameObject);
            Character.GetComponent<PlayerController>().levelUp = true;
            Character.GetComponent<PlayerController>().score += 100;
        }

        // Checks if the timer is ready for the next fire
        if (nextFire < Time.time)
        {

            // adds time to nextFire to delay the next shot
            nextFire = Time.time + fireRate;

            // spawns a bullet at the end bullet spawn location (bottom of queen)
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
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