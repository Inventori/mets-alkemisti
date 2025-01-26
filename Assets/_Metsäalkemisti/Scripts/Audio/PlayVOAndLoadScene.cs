using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayVOAndLoadScene : MonoBehaviour
{
    [SerializeField] private AudioPlayback audioPlayback;
    [SerializeField] private string clipId;
    [SerializeField] private int sceneToLoad;

    private void Start()
    {
        audioPlayback.PlayVoiceOver(clipId, OnVoiceOverProgress);
    }
    
    public void OnVoiceOverProgress(bool finished)
    {
        if (finished)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
