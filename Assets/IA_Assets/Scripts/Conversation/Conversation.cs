using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement pm;
    private PlayerInteraction pi;
    private PlayerInterface pui;

    [Header("Conversation / Cutscene")]
    [Tooltip("Subtitles text file for this Sequence.")]
    [SerializeField] private TextAsset subtitles;

    [Tooltip("Game Objects to enable or disable during the animation.")]
    [SerializeField] private GameObject[] enableDisableGOs;

    [Tooltip("Items to give to the player when the event is called.")]
    [SerializeField] private Interactive[] itemsToGive;

    [Tooltip("The next Sequence / Interact to enable after this one is over.")]
    [SerializeField] private GameObject nextSequence;

    [Tooltip("New objective to give to the player, if needed.")]
    [SerializeField] private string newObjectiveText;

    private void Start()
    {
        pm = player.GetComponent<PlayerMovement>();
        pi = player.GetComponent<PlayerInteraction>();
        pui = player.GetComponentInChildren<PlayerInterface>();
    }

    public void EngageSubtitles()
    {
        pui.DisplaySubtitles(subtitles);
    }

    public void GivePlayerItem()
    {
        if(itemsToGive.Length != 0)
        {
            for(int i = 0; i < itemsToGive.Length; i++)
            {
                pi.inventory.Add(itemsToGive[i]);
            }
        }
    }

    public void EnableDisablePlayerInteraction()
    {
        if (pi.enabled == true)
            pi.enabled = false;
        else
            pi.enabled = true;

        pui.ClearInteractionText();
    }

    public void EnableDisablePlayerMovement()
    {
        if (pm.enabled == true)
            pm.enabled = false;
        else
            pm.enabled = true;
    }

    public void EnableDisableGOs()
    {
        if (enableDisableGOs.Length != 0)
        {
            for (int i = 0; i < enableDisableGOs.Length; i++)
            {
                if (enableDisableGOs[i].activeSelf)
                    enableDisableGOs[i].SetActive(false);
                else
                    enableDisableGOs[i].SetActive(true);
            }
        }
    }

    public void SequenceEnd()
    {
        // Enable the next Sequence / Interact.
        if(nextSequence != null)
            nextSequence.SetActive(true);

        // Update the Objective text.
        if (newObjectiveText != null)
            pui.UpdateObjectiveText(newObjectiveText);

        // Destroy this one.
        Destroy(gameObject);
    }
}
