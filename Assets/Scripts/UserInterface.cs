using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popUpText, interactionText, subtitlesText;
    private SubtitleDisplayer _subtitleDisplayer;

    private void Start()
    {
        _subtitleDisplayer = GetComponent<SubtitleDisplayer>();
    }

    public void SetInteractionText(string interactionName)
    {
        popUpText.text = interactionName;
    }

    public void ClearInteractionText()
    {
        popUpText.text = null;
    }

    public void DisplaySubtitles(TextAsset subtitles)
    {
        // Add the subtitle text to the displayer.
        _subtitleDisplayer.Subtitle = subtitles;

        // Begin the subtitles.
        StartCoroutine(_subtitleDisplayer.Begin());
    }
}
