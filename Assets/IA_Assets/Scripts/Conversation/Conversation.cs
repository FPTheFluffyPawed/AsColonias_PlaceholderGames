using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement pm;
    private PlayerInteraction pi;

    [Header("Conversation/Cutscene")]
    [Tooltip("The animators of the actors, in case we wanna trigger their animations.")]
    [SerializeField] private Animator[] animators;

    [Tooltip("Dialog SO to get the audio files from.")]
    [SerializeField] private Dialog dialog;

    [Tooltip("Subtitles text file.")]
    [SerializeField] private TextAsset subtitles;

    [Tooltip("Audio sources to be utilised in order to play audio from.")]
    [SerializeField] private AudioSource[] audioSources;

    [Tooltip("Game Objects to enable or disable during the animation.")]
    [SerializeField] private GameObject[] enableDisableGOs;

    private int counter;

    private void Start()
    {
        counter = 0;
        pm = player.GetComponent<PlayerMovement>();
        pi = player.GetComponent<PlayerInteraction>();
    }

    private void EngageSubtitles()
    {

    }

    private void GivePlayerItem()
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

    private void TriggerOtherAnimation(int actor)
    {
        animators[actor].SetTrigger("Sequence");
    }

    private void PlayVoiceLine(int actor)
    {
        audioSources[actor].clip = dialog.lines[counter].dialogSound;
        audioSources[actor].Play();

        // Increment the counter to advance the Dialog array.
        counter++;
    }

    private void EnableDisableGOs()
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
}
