using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AmbushTransitionScene : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip gunShotHitPlayer;
    [SerializeField] private Animator animator;

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            player.GetComponent<AudioSource>().clip = gunShotHitPlayer;
            player.GetComponent<AudioSource>().Play();
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerInteraction>().enabled = false;
            player.GetComponentInChildren<PlayerInterface>().enabled = false;
            animator.SetTrigger("Trigger");
            StartCoroutine(TransitionScene());
        }
    }

    private IEnumerator TransitionScene()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(3);
    }
}
