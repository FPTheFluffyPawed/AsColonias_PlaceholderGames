using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickstartSubtitles : MonoBehaviour
{
    private SubtitleDisplayer _subtitles;

    private void Awake()
    {
        _subtitles = GetComponent<SubtitleDisplayer>();
    }

    private void OnEnable()
    {
        StartCoroutine(_subtitles.Begin());
    }
}
