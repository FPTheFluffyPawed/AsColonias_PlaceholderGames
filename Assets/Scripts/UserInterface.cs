using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popUpText, interactionText, subtitlesText;

    public void SetInteractionText(string interactionName)
    {
        popUpText.text = interactionName;
    }

    public void ClearInteractionText()
    {
        popUpText.text = null;
    }

    public void Subtitles(string subtitles)
    {

    }
}
