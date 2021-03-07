using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] enableDisableGO;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip clip;
    [SerializeField] private TextAsset subtitles;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            player.GetComponent<AudioSource>().clip = clip;
            player.GetComponent<AudioSource>().Play();
            player.GetComponentInChildren<PlayerInterface>().DisplaySubtitles(subtitles);
            
            for(int i = 0; i < enableDisableGO.Length; i++)
            {
                if (enableDisableGO[i].activeSelf)
                    enableDisableGO[i].SetActive(false);
                else
                    enableDisableGO[i].SetActive(true);
            }

            Destroy(gameObject);
        }
    }
}
