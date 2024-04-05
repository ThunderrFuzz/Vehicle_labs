using UnityEngine;

public class AICar : MonoBehaviour
{
    public Transform player;    // player location
    public Rigidbody rb;        // ai rb
    public HealthBar healthbar;
    public float maxSpeed = 5f; // ai max speed
    public float acceleration = 2f; // ai acceleration 
    public float health;
    float currentHealth;
    void Start()
    {
        currentHealth = health;
    }
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
    public void takeDamage(int dam)
    {
        currentHealth -= dam;
        healthbar.UpdateHealthBar(currentHealth / health);
    }
    public float GetCurrHealth()
    {
        return currentHealth;
    }
}
