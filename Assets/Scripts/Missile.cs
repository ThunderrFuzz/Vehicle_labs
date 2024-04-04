using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int blastRadius;

    private void OnCollisionEnter(Collision col)
    {
        // Spawn explosion effects prefab at the point of collision
        Vector3 hitPoint = col.contacts[0].point; // Get the point of collision
        GameObject explosionObject = Instantiate(explosionPrefab, hitPoint, Quaternion.identity);
        // Destroy the explosion effect after it's finished based on the duration of particle effect played
        Destroy(explosionObject, explosionObject.GetComponent<ParticleSystem>().main.duration);

       // creates an array of colliders the missile hit, center is hitpoint, radius blastradius
        Collider[] hitColliders = Physics.OverlapSphere(hitPoint, blastRadius);
        //loops through every hit collider in the array of hitcolliders 
        foreach (var hitCollider in hitColliders)
        {
            if (col.gameObject.layer == 3) // if layer is destroy 
            {
                //destroys all objects within range and is on the destroyable layer (3)
                Destroy(col.gameObject);
                
            }
        }

        // Destroy the missile
        Destroy(gameObject);
        Debug.Log("Missile collided with: " + col.gameObject.name);
    }
}

