using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMPoints : MonoBehaviour
{
    public int totalScore = 0;
    public bool player1ReachedObjective;
    public bool player2ReachedObjective;
    public Vehicles vehicle1;
    public Vehicles vehicle2;
    public GameObject objectivePrefab;
    public Vector3 spawnPos;

    int chain;

    void Start()
    {
        SpawnObjective();
    }
    
    public void SpawnObjective()
    {
        // Generate random spawn position
        float randX = Random.Range(50f, 52f);
        float randY = Random.Range(-1f, 0f);
        float randZ = Random.Range(-71f, -80f);

        spawnPos = new Vector3(randX, randY, randZ);
        
        // Instantiate the new objective
        GameObject objectiveInstance = Instantiate(objectivePrefab, spawnPos, Quaternion.identity);
    }

    public void AddPoints(int points)
    {
        totalScore += points;
        Debug.Log("Total score is now: " + totalScore);

    }

    public void CheckObjectiveCompletion()
    {
        if (player1ReachedObjective && player2ReachedObjective)
        {
            // Add bonus points
            AddPoints(500 * chain);
            Debug.Log("Objective completed. Bonus points added.");

            // Reset objective reached flags
            player1ReachedObjective = false;
            player2ReachedObjective = false;
            
        }
    }
    public void setChain(int newchain) { chain = newchain; }
    public int getChain() { return chain; }
}
