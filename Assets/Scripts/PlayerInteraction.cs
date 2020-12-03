using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactionDistance = 2.0f;

    private Camera cam;
    private UserInterface ui;
    private Ray ray;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        ui = GetComponentInChildren<UserInterface>();
    }

    private void FixedUpdate()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, interactionDistance))
        {
            Debug.DrawRay(cam.transform.position, transform.TransformDirection(cam.transform.forward * interactionDistance), Color.yellow);
            Debug.Log("Hit");

        }
    }
}
