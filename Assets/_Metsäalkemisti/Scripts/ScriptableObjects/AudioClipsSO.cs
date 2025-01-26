using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "AudioClips", menuName = "ScriptableObjects/AudioClipsSO", order = 1)]
public class AudioClipsSO : ScriptableObject
{
    public List<AudioClipData> audioClips = new();
    public List<AudioClip> fallbackVOs = new();
    
    
    public AudioClipData GetClip(string id)
    {
        return audioClips.FirstOrDefault(x => x.id == id);
    }
    
    public AudioClip GetFallbackVO()
    {
        return fallbackVOs[Random.Range(0, fallbackVOs.Count)];
    }
}
