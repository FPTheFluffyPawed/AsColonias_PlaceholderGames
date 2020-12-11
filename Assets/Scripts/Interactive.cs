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

    public enum InteractType { Examine, Talk, Pickup, Interact };

    public InteractType type;

    private float timer;
    private Color highlighted, notHighlighted;
    private Material m;
    private Animator _animator;

    private void Start()
    {
        timer = 0f;
        highlighted = new Color(0.2f, 0.2f, 0.2f);
        notHighlighted = new Color(0, 0, 0);

        if(GetComponent<Renderer>() != null)
            m = GetComponent<Renderer>().material;

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
    }
}
