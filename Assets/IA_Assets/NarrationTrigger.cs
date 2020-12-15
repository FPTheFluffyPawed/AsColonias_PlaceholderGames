using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private TextAsset subtitles;

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            GameObject player = other.gameObject;

            player.GetComponent<AudioSource>().clip = clip;
            player.GetComponent<AudioSource>().Play();
            player.transform.GetChild(0).GetComponent<SubtitleDisplayer>().Subtitle = subtitles;
            player.GetComponent<PlayerInteraction>().interactionCooldown = clip.length + 0.5f;
            StartCoroutine(player.transform.GetChild(0).GetComponent<SubtitleDisplayer>().Begin());

            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
