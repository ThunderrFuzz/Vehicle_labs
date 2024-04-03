using UnityEngine;

public class CamFollow : MonoBehaviour
{
    Vector3 currentOffset; 
    public Transform target;
    public Vector3 firstPersonOffset;
    public Vector3 thirdPersonOffset;
    public float smoothSpeed = 1f; // smoothing speed value 
    public bool isFirstPerson = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set starting perspective 
        currentOffset = thirdPersonOffset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null) return; // Ensure that there is a target set 

        // Toggle between first-person and third-person perspectives
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFirstPerson = !isFirstPerson; // toggles the first person bool on keypress 
            currentOffset = isFirstPerson ? firstPersonOffset : thirdPersonOffset; // set current offset based on value in isFirstPerson
        }

        // gets the desired postion 
        Vector3 desiredPosition = target.position + currentOffset;

        // Move the camera 
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); 

    }
}
