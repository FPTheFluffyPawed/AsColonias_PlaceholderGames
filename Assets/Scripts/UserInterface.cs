using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popUpText, dialogText, dialogContinueText;

    private SubtitleDisplayer _subtitleDisplayer;
    private PlayerMovement _pm;
    private PlayerRotation _pr;
    private PlayerInteraction _pi;

    private void Start()
    {
        _subtitleDisplayer = GetComponent<SubtitleDisplayer>();
        _pm = GetComponentInParent<PlayerMovement>();
        _pr = GetComponentInParent<PlayerRotation>();
        _pi = GetComponentInParent<PlayerInteraction>();
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

    public void Talk(Dialog dialog, AudioSource audioSource)
    {
        StartCoroutine(DialogScreen(dialog, audioSource));
    }

    private IEnumerator DialogScreen(Dialog dialog, AudioSource audioSource)
    {
        DisablePlayer();

        foreach(DialogLine line in dialog.lines)
        {
            // Get the values from the line.
            audioSource.clip = line.dialogSound;
            dialogText.text = line.dialogSubtitles;

            // Play the audio and make the subtitles appear.
            audioSource.Play();

            // Wait a bit.
            yield return new WaitForSeconds(audioSource.clip.length + 1.0f);

            // Show that they can continue.
            dialogContinueText.gameObject.SetActive(true);

            // Continue after key press.
            yield return WaitForKeyPress(KeyCode.Mouse1);

            // Hide the prompt again.
            dialogContinueText.gameObject.SetActive(false);
        }

        dialogText.text = null;

        EnablePlayer();
    }

    private IEnumerator WaitForKeyPress(KeyCode key)
    {
        bool done = false;
        while(!done)
        {
            if(Input.GetKeyDown(key))
            {
                done = true;
            }
            yield return null;
        }
    }

    private void DisablePlayer()
    {
        Debug.Log("Disabled player");
        _pm.enabled = false;
        _pr.enabled = false;
        _pi.enabled = false;
    }

    private void EnablePlayer()
    {
        Debug.Log("Enabled player");
        _pm.enabled = true;
        _pr.enabled = true;
        _pi.enabled = true;
    }
}
