using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    [Tooltip("This is what will appear when the Player hovers over it.")]
    public string interactiveText;
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
    public enum InteractType { Examine, Talk, Pickup, Interact, Lock, Examine_Once };

    public InteractType type;

    private float timer;
    private Color highlighted, notHighlighted;
    private Material m;
    


    private void Start()
    {
        timer = 0f;
        highlighted = new Color(0.2f, 0.2f, 0.2f);
        notHighlighted = new Color(0, 0, 0);

        if(GetComponent<Renderer>() != null)
            m = GetComponent<Renderer>().material;

        if(_animator == null)
            _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0 && m != null)
            m.SetColor("_EmissionColor", notHighlighted);
    }

    public void Highlight()
    {
        timer = 0.1f;

        if(m != null)
            m.SetColor("_EmissionColor", highlighted);
    }

    public void Interact()
    {
        if (_animator != null)
            _animator.SetTrigger("Interact");

        EnableDisableGOs();

        if (type == InteractType.Lock)
            GetComponent<Collider>().enabled = false;
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
