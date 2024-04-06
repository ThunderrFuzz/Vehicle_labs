using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    
    public int maxObjectives = 2;
    [SerializeField]
    int objectiveCount;
    int chain;
    GameObject currObj;

    void Start()
    {
        SpawnObjective();
    }
    
    public void SpawnObjective()
    {

        if (objectiveCount < maxObjectives)
        {
            // Generate random spawn position
            spawnPos = randomSpawnPoint();
            // Instantiate the new objective
            GameObject objectiveInstance = Instantiate(objectivePrefab, spawnPos, Quaternion.identity);
            currObj = objectiveInstance;
            // Increment the objective count
            objectiveCount++;
        }
    }
    public void despawnObjective()
    {
        
        objectiveCount--;
        Destroy(currObj);
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
    Vector3 randomSpawnPoint()
    {
        int sidesel = Random.Range(1, 4);
        float randX = 0f;
        float randZ = 0f;
        float randY = Random.Range(2f, 2.5f); // only need one y pos for all of them

        switch (sidesel)
        {
            //box co-ords to generate random positon within the roads of the map 
            case 1: // Side 1
                randX = Random.Range(-13f, -10f);
                randZ = Random.Range(-120f, 250f);
                break;
            case 2: // Side 2
                randX = Random.Range(-7f, 360f);
                randZ = Random.Range(245f, 250f);
                break;
            case 3: // Side 3
                randX = Random.Range(355f, 365f);
                randZ = Random.Range(-120f, 250f);
                break;
            case 4: // Side 4
                randX = Random.Range(-10f, 350f);
                randZ = Random.Range(-120f, -116f);
                break;
            default:
                break;
        }
        return new Vector3(randX, randY, randZ); 
        
    }

    

    //getters and setters
    public void setObjectiveCount(int obj) { objectiveCount -= obj; }
    public int getObjectiveCount() { return objectiveCount; }
    public void setChain(int newchain) { chain = newchain; }
    public int getChain() { return chain; }
}
