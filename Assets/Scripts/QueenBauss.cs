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

    //! Defines which projectile the boss shoots
    public GameObject projectile;

    //! A reference to the object where the boss's projectiles spawn (relative to boss position)
    public Transform projectileSpawn;


    //! How many projectiles to spawn in each wave
    public int hazardCount;

    //! How long to wait between spawns
    public float spawnWait;

    //! How long to wait in between spawns
    public float waveWait;


    private bool dirRight;

    void Start()
    {
        dirRight = true;
        damage = 0;
        StartCoroutine(SpawnProjectiles());
    }
    /*!
    * @pre: none
    * @post: assigns GameObject Character to playerObject 
    * @param playerObject: the player object we are currently interacting with
    !*/
    public void RecievePlayerParameter(GameObject playerObject)
    {
        Character = playerObject;
        //shake = GameObject.FindGameObjectWithTag("Shake").GetComponent<ShakeBehavior>();

    }

    /*!
    * @pre: none
    * @post: kills the enemy, bullet, and shakes the camera
    * @param other: object the bullet collides with
    !*/
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something is touching me");
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            //shake.EnemyCameraShake();
            damage++;
        }
    }

    void Update()
    {
        if (damage == health)
        {
            Debug.Log("I'm a hit");
            Destroy(gameObject);
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

    IEnumerator SpawnProjectiles()
    {
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Quaternion spawnRotation = Quaternion.identity;
                var obj = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
                yield return new WaitForSeconds(spawnWait);
                if(health == (damage-1))
                {
                    spawnWait *= (float).25;
                    waveWait *= (float).35;
                }
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}