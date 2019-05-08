using System.Collections;
using UnityEngine;

public class QueenBauss : MonoBehaviour
{
    AudioSource audioData;

    //! Target Object to follow (usually player)
    private GameObject Character;

    //! Boss speed settable from Unity editor
    public float speed;

    //! How far (horizontally) the boss can move from its origin
    public float boundary;

    //! The class the contains the animation to apply camera shake behavior to
    //public ShakeBehavior shake;

    //! Tracks the number of bullet hits the boss can take
    public int health;

    //! Tracks how much damage the boss has received, when damage == health, the boss dies
    private int damage;

    //! Defines which bullet object the boss will use to shoot. (this will be different from player's bullets)
    public GameObject bullet;

    //! A reference to the object where the bullets spawn (this is always relative to boss position, as it is a child prefab of the boss)
    public Transform bulletSpawn;

    /*! fireRate is how fast the gun will fire.  nextFire is updated every time the gun is shot by adding fireRate to the current time.
    the shot won't be ready until this time has passed !*/
    public float fireRate;

    //! The next time the boss can fire
    private float nextFire;

    //! True if the boss is moving right
    private bool dirRight;

    //! Sound that plays when the boss dies
    public AudioClip bossKillSound;

    //! Sound that plays when the boss is hit
    public AudioClip bossHit;

    //! Sound that plays when the boss is spawned
    public AudioClip bossEnter;

    //! Sound that plays when the boss fires
    public AudioClip bossShoot;

    void Start()
    {
        AudioSource.PlayClipAtPoint(bossEnter, new Vector3(0, 0, -10f), 1f);
        audioData = GetComponent<AudioSource>();
        dirRight = true;
        damage = 0;
        nextFire = Time.time + fireRate;
    }

    /*!
    * @pre: called at object initialization
    * @post: inherits difficulty modifiers from Level Controller 
    * @param playerObject: the player object we are currently interacting with
    * @param difficultyModifier: How much health the boss will have
    * @param bulletFrequency: the firerate of the boss
    !*/
    public void RecievePlayerParameter(GameObject playerObject, int difficultyModifier, float bulletFrequency)
    {
        Character = playerObject;
        health += difficultyModifier;
        fireRate *= bulletFrequency;
        //shake = GameObject.FindGameObjectWithTag("Shake").GetComponent<ShakeBehavior>();

    }

    /*!
    * @pre: none
    * @post: damages the boss, bullet, and shakes the camera
    * @param other: the object entering the boss (hopefully a bullet)
    !*/
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet"){
            AudioSource.PlayClipAtPoint(bossHit, new Vector3(0, 0, -10f), 0.7f);
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
            AudioSource.PlayClipAtPoint(bossKillSound, new Vector3(0, 0, -10f), 1);
            Destroy(gameObject);
            Character.GetComponent<PlayerController>().levelUp = true;
            Character.GetComponent<PlayerController>().score += 100;
        }

        //! This functions almost the exact same way as PlayerController
        if (nextFire < Time.time)
        {

            // adds time to nextFire to delay the next shot
            nextFire = Time.time + fireRate;

            // spawns a bullet at the end bullet spawn location (bottom of queen)
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

            AudioSource.PlayClipAtPoint(bossShoot, new Vector3(0, 0, -10f), 1f);
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