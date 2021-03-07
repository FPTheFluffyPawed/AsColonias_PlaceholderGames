using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckRideSubtitlesStart : MonoBehaviour
{
    [SerializeField] private TextAsset subtitles;
    [SerializeField] private GameObject player;

    private void OnEnable()
    {
        StartSubtitles();
    }

    private void StartSubtitles()
    {
        player.GetComponentInChildren<PlayerInterface>().DisplaySubtitles(subtitles);
    }
}
