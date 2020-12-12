using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource soldier;
    [SerializeField] private AudioClip clip;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "GAZ-66")
        {
            soldier.clip = clip;
            soldier.Play();
        }
    }
}
