using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // Assign Player here

    private float xRotation = 0f;
    public Transform orientation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate up/down camera only
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate left/right whole player
        playerBody.Rotate(Vector3.up * mouseX);

        orientation.rotation = Quaternion.Euler(0f, playerBody.eulerAngles.y, 0f);
    }
}
