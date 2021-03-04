using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;
    private PlayerMovement pm;
    private PlayerInteraction pi;

    [Header("Dialog Content")]
    [Tooltip("")]
    [SerializeField] private Animator[] animators;
    [SerializeField] private Dialog dialog;
    [SerializeField] private TextAsset subtitles;
    [SerializeField] private AudioSource[] audioSources;

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
}
