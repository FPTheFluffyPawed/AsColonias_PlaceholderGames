using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Obtained online through UnityCoder.
/// https://unitycoder.com/blog/2015/12/03/ui-text-typewriter-effect-script/
/// </summary>
public class TypewriterEffect : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private string _narration;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _narration = _text.text;
        _text.text = null;

        StartCoroutine(PlayText());
    }

    IEnumerator PlayText()
    {
        foreach(char c in _narration)
        {
            _text.text += c;
            yield return new WaitForSeconds(0.050f);
        }
    }
}
