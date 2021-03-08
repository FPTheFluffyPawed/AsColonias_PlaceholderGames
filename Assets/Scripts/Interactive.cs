using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Interactive : MonoBehaviour
{
    public enum InteractType { Examine, Talk, Pickup, Interact, Lock, Examine_Once };

    [Header("Mandatory")]
    [Tooltip("The type of Interactive.")]
    public InteractType type;
    [Tooltip("This is what will appear when the Player hovers over it.")]
    public string interactiveText;

    [Header("Non-Talk Properties")]
    [Tooltip("The audio file to play when the object is examined.")]
    public AudioClip audioClip;
    [Tooltip("The subtitles that are part of this audio file.")]
    public TextAsset subtitles;
    [Tooltip("The dialog to be used for the conversation.")]
    public Dialog dialog;
    [Tooltip("The sprite when in an inventory.")]
    public Sprite sprite;
    [Tooltip("The GameObjects to enable/disable, when interacted.")]
    public GameObject[] gameObjects;
    [Tooltip("The requirements needed to interact with this.")]
    public Interactive[] inventoryRequirements;
    [Tooltip("The new objective to give to the player.")]
    public string newObjectiveText;
    [Tooltip("The item to give to a player, if needed.")]
    public Interactive talkGiveItem;
    [Tooltip("The animator attached, if you want an animation to play.")]
    [SerializeField] private Animator _animator;

    [Header("Talk / Conversations Only")]
    [SerializeField] private PlayableDirector _director;

    private Interactive _interactive;

    private void Start()
    {
        _interactive = GetComponent<Interactive>();

        if(_animator == null)
            _animator = GetComponent<Animator>();

        if (_director == null)
            _director = GetComponent<PlayableDirector>();
    }

    public void Talk()
    {
        // Disable the collider for interactions, since we don't need it anymore.
        // Destroy(gameObject.GetComponent<Collider>());

        // Play the sequence.
        _director.Play();
    }

    public void Interact()
    {
        if (_animator != null)
            _animator.SetTrigger("Interact");

        EnableDisableGOs();

        if (type == InteractType.Lock)
            GetComponent<Collider>().enabled = false;
    }

    public void PlayAnimation()
    {
        if (_animator != null)
            _animator.SetTrigger("Interact");
    }

    public void EnableDisableGOs()
    {
        if (gameObjects.Length != 0)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i].activeSelf)
                    gameObjects[i].SetActive(false);
                else
                    gameObjects[i].SetActive(true);
            }
        }
    }
}
