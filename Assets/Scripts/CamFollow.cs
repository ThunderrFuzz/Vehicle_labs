using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Vehicles vehicles;
    public Transform target;
    public Vector3 firstPersonOffset;
    public Vector3 thirdPersonOffset;
    public float smoothSpeed = 3f; // smoothing speed value 
    public bool isFirstPerson = false;
    public float rotationSpeed = 5f; // rotation speed value

    private Vector3 currentOffset;

    // Start is called before the first frame update
    void Start()
    {
        // Set starting perspective 
        SetCameraOffset();
    }

    // LateUpdate is called once per frame, after all Update calls
    void LateUpdate()
    {
        if (target == null) return; // Ensure that there is a target set 

        // Toggle between first-person and third-person perspectives
        if ((Input.GetKeyDown(KeyCode.F) && !vehicles.isPlayer2) ||
            (Input.GetKeyDown(KeyCode.Comma) && vehicles.isPlayer2))
        {
            isFirstPerson = !isFirstPerson;
            SetCameraOffset();
        }

        // Update desired position and rotation based on player's input
        float hozInput = Input.GetAxis("Horizontal");
        Vector3 desiredPosition = target.position + currentOffset + Vector3.right * hozInput;
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);

        // Smoothly move and rotate the camera towards the desired position and rotation
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed );
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed );
    }

    private void SetCameraOffset()
    {
        currentOffset = isFirstPerson ? firstPersonOffset : thirdPersonOffset;
    }
}



