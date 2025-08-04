using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Flag to enable or disable camera movement
    private bool doMovement = true;

    // Speed of camera movement
    public float panSpeed = 25f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float minY = 10f; // Minimum height of the camera
    public float maxY = 80f; // Maximum height of the camera

    // Update is called once per frame
    void Update()
    {
        // Toggle movement on/off with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        // If movement is disabled, do not process any movement input
        if (!doMovement)
        {
            return;
        }

        // Camera movement controls
        // Use WASD keys or arrow keys to move the camera
        // Move the camera up
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {             
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        // Move the camera down
        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        
        // Move the camera right
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        //Move the camera left
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        // Zoom in and out with the mouse scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;   // Get the current camera position
        pos.y -= scroll * scrollSpeed * 1000f * Time.deltaTime; // Adjust the zoom speed
        pos.y = Mathf.Clamp(pos.y, minY, maxY); // Clamp the camera height
        transform.position = pos;   // Update the camera position

    }
}
