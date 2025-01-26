using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class AudioClipData
{
    public string id;
    public AudioClip audioClip;
    [TextArea]
    public string subtitle;
    public string nextClipId;
}
