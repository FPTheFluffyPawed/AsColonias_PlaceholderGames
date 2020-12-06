using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactionDistance = 2.0f;

    private Camera _cam;
    private UserInterface _ui;
    private Interactive _currentInteractive;

    private void Start()
    {
        _cam = GetComponentInChildren<Camera>();
        _ui = GetComponentInChildren<UserInterface>();
        _currentInteractive = null;
    }

    private void FixedUpdate()
    {
        CheckForInteractive();
        CheckForInteraction();
        Hovering();
    }

    private void Hovering()
    {
        if (_currentInteractive != null)
        {
            _currentInteractive.Highlight();
            _ui.SetInteractionText(_currentInteractive.interactiveText);
        }
        else
        {
            _ui.ClearInteractionText();
        }
    }

    /// <summary>
    /// Check for any Interactive GOs in the Player's raycast.
    /// </summary>
    private void CheckForInteractive()
    {
        if (Physics.Raycast(_cam.transform.position,
                            _cam.transform.forward,
                            out RaycastHit hit,
                            interactionDistance))
        {
            Interactive newInteractive = hit.collider.GetComponent<Interactive>();

            if (newInteractive != null && newInteractive != _currentInteractive)
                SetCurrentInteractive(newInteractive);
            else if (newInteractive == null)
                ClearCurrentInteractive();
        }
        else
            ClearCurrentInteractive();
    }

    private void CheckForInteraction()
    {
        if(_currentInteractive != null)
        {
            switch(_currentInteractive.type)
            {
                case Interactive.InteractType.Examine:
                    if (Input.GetMouseButtonDown(0))
                        Examine();
                    break;
            }
        }
    }

    private void SetCurrentInteractive(Interactive newInteractive)
    {
        _currentInteractive = newInteractive;
    }

    private void ClearCurrentInteractive()
    {
        _currentInteractive = null;
    }

    private void Pickup()
    {

    }

    private void Talk()
    {

    }

    private void Examine()
    {
        _ui.
    }

    private void Interact()
    {
        _currentInteractive.Interact();
    }
}
