using UnityEngine;

public class AICar : MonoBehaviour
{
    public Transform player;    // player location
    public Rigidbody rb;        // ai rb
    public float maxSpeed = 5f; // ai max speed
    public float acceleration = 2f; // ai acceleration 

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player reference not set for AI! " + gameObject.name);
            return;
        }
        // direction to the player
        Vector3 direction = (player.position - transform.position).normalized;
        // desired velocity based on max speed
        Vector3 desiredVelocity = direction * maxSpeed;
        // velocity change needed to reach the desired velocity
        Vector3 velocityChange = (desiredVelocity - rb.velocity);
        // adds acceleration to rb 
        rb.AddForce(velocityChange * acceleration, ForceMode.Acceleration);
    }
}
