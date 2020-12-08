using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogLine", menuName = "ScriptableObjects/Dialog Line", order = 1)]
public class DialogLine : ScriptableObject
{
    public AudioClip dialogSound;
    public string dialogSubtitles;
}
