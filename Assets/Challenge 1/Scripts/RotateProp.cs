using UnityEngine;

public class RotateProp : MonoBehaviour
{
    public Vector3 rotationAxis = new Vector3(0f, 0f, 1f); // Set the rotation axis to Z=1
    public float baseRotationSpeed = 100f; // Base rotation speed in degrees per second
    public PlayerControllerX playerController; // Reference to the player control script
    void Update()
    {
        
        // Calculate rotation speed based on the speed of the plane
        int planeSpeed = playerController.speed;
        float rotationSpeed = baseRotationSpeed * Mathf.Pow(planeSpeed, 1.5f); // Adjust rotation speed based on plane speed

        // Rotate the propeller around the specified axis at the calculated speed
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
        
    }
}
