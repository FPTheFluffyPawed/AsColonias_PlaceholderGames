using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 6.0f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float sprintSpeed = 12.0f;

    private CharacterController controller;
    private Camera cam;
    private float playerVelocity;
    private float oldSpeed;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        oldSpeed = playerSpeed;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

        Vector3 forwardBackward = cam.transform.forward;
        Vector3 sideways = cam.transform.right;

        forwardBackward.y = 0f;
        sideways.y = 0f;
        forwardBackward.Normalize();
        sideways.Normalize();

        controller.Move(forwardBackward * vertical + sideways * horizontal);

        if (controller.isGrounded) playerVelocity = 0;
        else
        {
            playerVelocity -= gravity * Time.deltaTime;
            controller.Move(new Vector3(0, playerVelocity, 0));
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = sprintSpeed;

        }
        else
        {
            playerSpeed = oldSpeed;
        }
    }
}
