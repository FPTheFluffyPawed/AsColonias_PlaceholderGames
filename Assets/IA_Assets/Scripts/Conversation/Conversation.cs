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

    [Tooltip("The next Sequence / Interact to enable after this one is over.")]
    [SerializeField] private GameObject nextSequence;

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

    }

    private void DisablePlayer()
    {
        pm.enabled = false;
        pi.enabled = false;
    }

    private void EnablePlayer()
    {
        pm.enabled = true;
        pi.enabled = true;
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
        nextSequence.SetActive(true);

        // Destroy this one.
        Destroy(gameObject);
    }
}
