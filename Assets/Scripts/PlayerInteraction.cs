using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactionDistance = 2.0f;

    private Camera _cam;
    private UserInterface _ui;
    private Interactive _currentInteractive;
    private AudioSource _audioSource;
    private float interactionCooldown;

    private void Start()
    {
        _cam = GetComponentInChildren<Camera>();
        _ui = GetComponentInChildren<UserInterface>();
        _currentInteractive = null;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(CanInteractAgain())
        {
            CheckForInteractive();
            CheckForInteraction();
            Hovering();
        }
    }

    private void Hovering()
    {
        if (_currentInteractive != null)
        {
            _currentInteractive.Highlight();

            if(CanInteractAgain())
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
        // Set the clip of the interactive.
        _audioSource.clip = _currentInteractive.audioClip;

        // Play the sound.
        _audioSource.Play();

        // Get the subtitles from the corresponding interactive.
        _ui.DisplaySubtitles(_currentInteractive.subtitles);

        // Set the interaction cooldown to the length of the clip.
        interactionCooldown = _audioSource.clip.length;

        // Clear the text.
        _ui.ClearInteractionText();
    }

    private void Interact()
    {
        _currentInteractive.Interact();
    }

    private bool CanInteractAgain()
    {
        Debug.Log(interactionCooldown);

        interactionCooldown -= Time.deltaTime;

        if (interactionCooldown < 0) interactionCooldown = 0;

        if (interactionCooldown <= 0)
            return true;
        else
            return false;
    }
}
