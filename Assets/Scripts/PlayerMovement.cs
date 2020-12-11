using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 1.0f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;
    private Camera cam;
    private float playerVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
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
    }
}
