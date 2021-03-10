using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactionDistance = 2.0f;

    public List<Interactive> inventory;
    public float interactionCooldown;

    private Camera _cam;
    private PlayerInterface _ui;
    private Interactive _currentInteractive;
    private AudioSource _audioSource;
    private bool _hasRequirements;

    private void Start()
    {
        inventory = new List<Interactive>();
        _cam = GetComponentInChildren<Camera>();
        _ui = GetComponentInChildren<PlayerInterface>();
        _currentInteractive = null;
        _audioSource = GetComponent<AudioSource>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Screen.SetResolution(1920, 1080, true);
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
            //_currentInteractive.Highlight();

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
                case Interactive.InteractType.Pickup:
                    if (Input.GetMouseButtonDown(0))
                        Pickup();
                    break;
                case Interactive.InteractType.Talk:
                    if (Input.GetMouseButtonDown(0))
                        NewTalk();
                    break;
                case Interactive.InteractType.Examine:
                    if (Input.GetMouseButtonDown(0))
                        Examine();
                    break;
                case Interactive.InteractType.Examine_Once:
                    if (Input.GetMouseButtonDown(0))
                        Examine();
                    break;
                default:
                    if (Input.GetMouseButton(0))
                        Interact();
                    break;
            }
        }
    }

    private void SetCurrentInteractive(Interactive newInteractive)
    {
        _currentInteractive = newInteractive;

        if (HasInteractionRequirements())
        {
            _hasRequirements = true;
        }
        else
        {
            _hasRequirements = false;
        }
    }

    private void ClearCurrentInteractive()
    {
        _currentInteractive = null;
    }

    private void Pickup()
    {
        AddToInventory(_currentInteractive);
        _currentInteractive.transform.parent.gameObject.SetActive(false);
    }

    private void Talk()
    {
        if(_hasRequirements)
        {
            // Call the coroutine and wait for it to end.
            _ui.Talk(_currentInteractive.dialog, _audioSource);

            // Disable/enable anything that has to be disabled/enabled.
            _currentInteractive.EnableDisableGOs();

            // Update the objective text, if there is any to update.
            if (_currentInteractive.newObjectiveText != null)
                _ui.UpdateObjectiveText(_currentInteractive.newObjectiveText);

            // Remove the player's item, if there was a requirement.
            for (int i = 0; i < _currentInteractive.inventoryRequirements.Length; i++)
                RemoveFromInventory(_currentInteractive.inventoryRequirements[i]);

            // Give the player an item, if needed.
            if (_currentInteractive.talkGiveItem != null)
                inventory.Add(_currentInteractive.talkGiveItem);

            interactionCooldown = 1.0f;
            _ui.ClearInteractionText();
        }
    }

    private void NewTalk()
    {
        if (_hasRequirements)
        {
            // Remove the item.
            for (int i = 0; i < _currentInteractive.inventoryRequirements.Length; i++)
                RemoveFromInventory(_currentInteractive.inventoryRequirements[i]);

            // Trigger the Sequence.
            _currentInteractive.Talk();
        }
    }

    private void Examine()
    {
        // Play the animation if it has one, as well if you have the requirements.
        if(_hasRequirements)
            _currentInteractive.PlayAnimation();

        // Check if we can only examine it once.
        if (_currentInteractive.type == Interactive.InteractType.Examine_Once)
            _currentInteractive.gameObject.SetActive(false);

        // Set the clip of the interactive.
        _audioSource.clip = _currentInteractive.audioClip;

        // Play the sound.
        _audioSource.Play();

        // Get the subtitles from the corresponding interactive.
        _ui.DisplaySubtitles(_currentInteractive.subtitles);

        // Set the interaction cooldown to the length of the clip.
        interactionCooldown = _audioSource.clip.length + 0.5f;

        // Clear the text.
        _ui.ClearInteractionText();
    }

    private void Interact()
    {
        if (_hasRequirements)
        {
            for (int i = 0; i < _currentInteractive.inventoryRequirements.Length; i++)
                RemoveFromInventory(_currentInteractive.inventoryRequirements[i]);

            _currentInteractive.Interact();

            interactionCooldown = 0.2f;
        }
        else
        {
            Examine();
        }
    }

    private bool HasInteractionRequirements()
    {
        if (_currentInteractive.inventoryRequirements == null)
            return true;

        for (int i = 0; i < _currentInteractive.inventoryRequirements.Length; ++i)
            if (!HasInInventory(_currentInteractive.inventoryRequirements[i]))
                return false;

        return true;
    }

    private bool CanInteractAgain()
    {
        interactionCooldown -= Time.deltaTime;

        if (interactionCooldown < 0) interactionCooldown = 0;

        if (interactionCooldown <= 0)
            return true;
        else
            return false;
    }

    private void AddToInventory(Interactive item)
    {
        inventory.Add(item);
    }

    private void RemoveFromInventory(Interactive item)
    {
        inventory.Remove(item);
    }

    private bool HasInInventory(Interactive item) => inventory.Contains(item);
}
