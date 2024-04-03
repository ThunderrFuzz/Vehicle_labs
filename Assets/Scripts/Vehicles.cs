using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vehicles : MonoBehaviour
{
    public CamFollow camFollowScript; // reference to camera follow script set in editor 
    public GameObject explosionPrefab;
    public AudioSource audioSource;
    public Rigidbody rb;
    public Camera mainCamera;
    public Vector3 acceleration;
    public Vector3 turnForce;
    public Vector3 expOffset;

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
        if(currentHealth <= 0)
        {
            GameLost();
        }
        currentSpeed = rb.velocity.magnitude; // sets current speed to current velocity
        //only moves if speed is less than max 
        if( currentSpeed < maxSpeed) {

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
                    rb.AddTorque(-turnForce * Time.deltaTime, ForceMode.Impulse);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    rb.AddTorque(turnForce * Time.deltaTime, ForceMode.Impulse);
                }
            } else
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
                    rb.AddTorque(-turnForce * Time.deltaTime, ForceMode.Impulse);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    rb.AddTorque(turnForce * Time.deltaTime, ForceMode.Impulse);
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
                    otherRb.AddForce(forceDirection * 45f , ForceMode.Impulse);
                    break;
                case "Crate":
                    otherRb.AddForce(forceDirection * 20f , ForceMode.Impulse);
                    break;
                case "Barrier":
                    otherRb.AddForce(forceDirection * 18f , ForceMode.Impulse);
                    break;
                case "Cone":
                    otherRb.AddForce(forceDirection * 50f, ForceMode.Impulse);
                    break;
                case "Spool":
                    otherRb.AddForce(forceDirection * 15f , ForceMode.Impulse);
                    break;
                case "Rock":
                    // Game over logic can be implemented here
                    Debug.Log("Game Over - Hit a Rock!");
                    GameLost();
                    break;
                case "Enemy":                 
                    // Spawn explosion effects prefab at position of camera in first person, or point of collision in thrid person
                    Vector3 explosionPosition = camFollowScript.isFirstPerson ? mainCamera.transform.position + expOffset : col.contacts[0].point;
                    // spawn explosion effects prefab
                    GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
                    // Destroy the explosion effect after its finished based on the duration 
                    Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
                    //explosionParticles.Play();
                    TakeDamage(25);
                    Debug.Log(currentHealth);
                    break;
                default:
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
}

