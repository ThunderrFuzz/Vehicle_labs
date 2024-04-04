using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vehicles : MonoBehaviour
{
    public CamFollow camFollowScript; // reference to camera follow script set in editor 
    public Missile missileScript;
    public GameObject explosionPrefab;
    public GameObject ProjectilePrefab;
    public AudioSource audioSource; //unused currently
    public Rigidbody rb;
    public Camera mainCamera;
    public Vector3 acceleration;
    public Vector3 turnForce;
    public Vector3 expOffset;
    public string inputName;
    public int maxHealth = 100;
    private int currentHealth;

    public float maxSpeed;
    public bool isPlayer2;
    bool isMoving;
    int pointsEarned = 0;
    float currentSpeed; //  store the current speed


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayer2)
        {
            if ((Input.GetMouseButtonDown(0)))
            {
                // Fire missile here
                Fire();
            }
        }
        if (currentHealth <= 0)
        {
            GameLost();
        }
        currentSpeed = rb.velocity.magnitude; // sets current speed to current velocity
        //only moves if speed is less than max 
        if (currentSpeed < maxSpeed)
        {

            if (!isPlayer2)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rb.AddForce(acceleration * Time.deltaTime, ForceMode.Impulse);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(-acceleration * Time.deltaTime, ForceMode.Impulse);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    Quaternion deltaRotation = Quaternion.Euler(-turnForce * Time.deltaTime);
                    rb.MoveRotation(rb.rotation * deltaRotation);
                }

                if (Input.GetKey(KeyCode.D))
                {
                    Quaternion deltaRotation = Quaternion.Euler(turnForce * Time.deltaTime);
                    rb.MoveRotation(rb.rotation * deltaRotation);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    rb.AddForce(acceleration * Time.deltaTime, ForceMode.Impulse);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    rb.AddForce(-acceleration * Time.deltaTime, ForceMode.Impulse);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Quaternion deltaRotation = Quaternion.Euler(-turnForce * Time.deltaTime);
                    rb.MoveRotation(rb.rotation * deltaRotation);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    Quaternion deltaRotation = Quaternion.Euler(turnForce * Time.deltaTime);
                    rb.MoveRotation(rb.rotation * deltaRotation);
                }

            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        // Check if the collided object has a Rigidbody
        Rigidbody otherRb = col.gameObject.GetComponent<Rigidbody>();

        if (otherRb != null)
        {
            // Calculate force direction away from the cars.
            Vector3 forceDirection = col.contacts[0].point - transform.position;
            forceDirection = forceDirection.normalized; // normalizes the vector to apply opposite direcion from our impact angle.
            switch (col.gameObject.tag)
            {
                case "Barrel":
                    otherRb.AddForce(forceDirection * 45f, ForceMode.Impulse);
                    break;
                case "Crate":
                    otherRb.AddForce(forceDirection * 20f, ForceMode.Impulse);
                    break;
                case "Barrier":
                    otherRb.AddForce(forceDirection * 18f, ForceMode.Impulse);
                    break;
                case "Cone":
                    otherRb.AddForce(forceDirection * 20f, ForceMode.Impulse);
                    break;
                case "Spool":
                    otherRb.AddForce(forceDirection * 5f, ForceMode.Impulse);
                    break;
                case "Rock":
                    // Game over logic can be implemented here
                    Debug.Log("Game Over - Hit a Rock!");
                    GameLost();
                    break;
                case "Enemy":
                    // Spawn explosion effects prefab at position of camera in first person, or point of collision in third person
                    Vector3 explosionPosition = camFollowScript.isFirstPerson ? mainCamera.transform.position + expOffset : col.contacts[0].point;
                    // Spawn explosion effects prefab
                    GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
                    // Destroy the explosion effect after it's finished based on the duration 
                    Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
                    // Damage the player
                    TakeDamage(25);
                    // Destroy the collided enemy car
                    Destroy(col.gameObject);
                    break;
                default:
                    Debug.Log("default col switch case");
                    break;

            }
        }

    }

    void GameLost()
    {
        SceneManager.LoadScene("SampleScene"); // Reload the scene
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

    }
    void Fire()
    {
        // Creates an offset so the projectile won't spawn inside the tank and blow the player up right away like it was doing for the first 40 mins
        Vector3 spawnOffset = new Vector3(0f, 3f, 5f); // Adjust the offset value as needed
        
        // Instantiate the projectile with the new starting position
        GameObject projectileInstance = Instantiate(ProjectilePrefab, transform.position + spawnOffset, transform.rotation);
        Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>(); // gets projectiles rigidbody to apply force to 

        if (projectileRb != null)
        {
            
            float missileSpeed = 100f; // speed of projectile setting
            projectileRb.AddForce(transform.forward * missileSpeed, ForceMode.Impulse); // adds the force 
        }
    }
}
