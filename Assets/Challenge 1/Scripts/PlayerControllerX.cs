using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControllerX : MonoBehaviour
{
    public int speed;
    public float rotationSpeed;
    public float verticalInput;
    public Rigidbody rb;
    public Text messageText;
    private bool isGamePaused = false;
    private bool isResetting = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isResetting = true;
            ResetGame();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // Check for input to reset the game
        

        // Only process player input if the game is not paused
        if (!isGamePaused)
        {
            // get the user's vertical input
            verticalInput = Input.GetAxis("Vertical");

            // move the plane forward at a constant rate
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            // tilt the plane up/down based on up/down arrow keys
            if (verticalInput != 0) transform.Rotate(Vector3.right * verticalInput * rotationSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Objective")
        {
            GameWon();
        }
        else if (col.gameObject.tag == "Rock")
        {
            GameLost();
        }
    }

    void GameWon()
    {
        messageText.text = "Game won! Press R to play again.";
        isGamePaused = true;
    }

    void GameLost()
    {
        messageText.text = "Game Over: You hit something! Press R to reset";
        isGamePaused = true;
    }

    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
