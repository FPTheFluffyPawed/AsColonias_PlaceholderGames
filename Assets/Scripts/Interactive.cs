using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public enum InteractType { Examine, Talk, Pickup };

    public InteractType type;

    private float timer;
    private Color highlighted, notHighlighted;
    private Material m;

    private void Start()
    {
        timer = 0f;
        //highlighted = new Color(0.2f, 0.2f, 0.2f);
        //notHighlighted = new Color(0, 0, 0);
        //m = GetComponent<Renderer>().material;
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
            m.SetColor("_EmissionColor", notHighlighted);
    }

    public void Highlight()
    {
        timer = 0.1f;
        m.SetColor("_EmissionColor", highlighted);
    }

    public void Interact()
    {

    }
}
