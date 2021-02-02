using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatConversation : MonoBehaviour
{
    [SerializeField] private GameObject[] enableDisableGameObjects;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip sampaioClip;
    [SerializeField] private TextAsset eatSubtitles;

    private void UpdateSampaioDialog()
    {
        player.GetComponent<AudioSource>().clip = sampaioClip;
        player.GetComponent<AudioSource>().Play();
    }

    private void StartConversation()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponentInChildren<PlayerInterface>().DisplaySubtitles(eatSubtitles);

    }

    private void TransitionOutOfConversation()
    {
        player.GetComponent<PlayerMovement>().enabled = true;

        for(int i = 0; i < enableDisableGameObjects.Length; i++)
        {
            if (enableDisableGameObjects[i].activeSelf)
                enableDisableGameObjects[i].SetActive(false);
            else
                enableDisableGameObjects[i].SetActive(true);
        }

        Destroy(gameObject);
    }
}
