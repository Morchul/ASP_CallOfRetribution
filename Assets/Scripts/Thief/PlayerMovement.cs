using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float speed = 10f;
    public float sprintSpeed = 15f;
    public float crouchSpeed = 5f;
    public float mouseSensitivity = 1f;
    public float crouchHeight = 0.5f;


    void Update()
    {
        // Hide the mouse cursor
        Cursor.visible = false;

        // Get the WASD input
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        // Move the player
        rigidbody.MovePosition(transform.position + new Vector3(x, 0, z));

        // Move the camera
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(0, mouseX, 0);
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - crouchHeight, Camera.main.transform.position.z);
        }
        else
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + crouchHeight, Camera.main.transform.position.z);
        }
        Camera.main.transform.Rotate(-mouseY, 0, 0);

        // Change the speed based on user input
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = crouchSpeed;
        }
        else
        {
            speed = 10f;
        }
    }
}
