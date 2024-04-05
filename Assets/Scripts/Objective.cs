using UnityEngine;

public class Objective : MonoBehaviour
{
    // Define a delegate type for the event
    public delegate void ObjectiveTriggered();

    // Define the event
    public event ObjectiveTriggered OnTriggered;

    // OnTriggerEnter is called when the GameObject collides with a trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player or other relevant object
        if (other.CompareTag("Player"))
        {
            // Invoke the event when trigger conditions are met
            OnTriggered?.Invoke();
        }
    }
}
