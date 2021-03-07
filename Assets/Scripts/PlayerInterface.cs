using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popUpText, dialogText, dialogContinueText;
    [SerializeField] private GameObject _inventoryPanel, _loadPanel;
    [SerializeField] private Image[] inventorySlots;
    [SerializeField] private TextMeshProUGUI objectiveText;

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

    private void Update()
    {
        OpenInventory();
        
        if(_loadPanel != null)
            if (_loadPanel.activeSelf)
                DisablePlayer();
    }

    private void UpdateSlots()
    {
        CleanInventorySlots();

        for(int i = 0; i < _pi.inventory.Count; i++)
        {
            inventorySlots[i].sprite = _pi.inventory[i].sprite;
            inventorySlots[i].gameObject.SetActive(true);
        }
    }

    private void CleanInventorySlots()
    {
        foreach(Image i in inventorySlots)
        {
            i.sprite = null;
            i.gameObject.SetActive(false);
        }
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
            yield return new WaitForSeconds(audioSource.clip.length + 0.5f);

            // Show that they can continue.
            //dialogContinueText.gameObject.SetActive(true);

            // Continue after key press.
            //yield return WaitForKeyPress(KeyCode.Mouse0);

            // Hide the prompt again.
            //dialogContinueText.gameObject.SetActive(false);
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

    private void OpenInventory()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            _inventoryPanel.SetActive(true);
            UpdateSlots();
        }
        else
            _inventoryPanel.SetActive(false);
    }

    private bool HasUIOpen()
    {
        if (_inventoryPanel.activeSelf)
            return true;
        else
            return false;
    }

    private void DisablePlayer()
    {
        _pm.enabled = false;
        _pr.enabled = false;
        _pi.enabled = false;
    }

    private void EnablePlayer()
    {
        _pm.enabled = true;
        _pr.enabled = true;
        _pi.enabled = true;
    }

    public void UpdateObjectiveText(string newObjectiveText)
    {
        objectiveText.text = newObjectiveText;
    }

    public void ClearObjectiveText()
    {
        objectiveText.text = null;
    }
}
