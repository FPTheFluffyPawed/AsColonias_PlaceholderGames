using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource soldier;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SubtitleDisplayer playerSubtitles;
    [SerializeField] private TextAsset newSubtitles;
    [SerializeField] private GameObject[] enableDisableGameObjects;
    [SerializeField] private string newObjective;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            other.gameObject.GetComponentInChildren<PlayerInterface>().UpdateObjectiveText(newObjective);
            soldier.Play();
            playerSubtitles.Subtitle = newSubtitles;
            StartCoroutine(WaitForOtherCoroutine());
        }
    }

    private IEnumerator WaitForOtherCoroutine()
    {
        playerMovement.enabled = false;
        yield return StartCoroutine(playerSubtitles.Begin());
        playerMovement.enabled = true;

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
