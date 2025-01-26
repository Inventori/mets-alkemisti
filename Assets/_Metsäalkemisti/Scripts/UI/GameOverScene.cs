using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] private AudioPlayback audioPlayback;
    void Start()
    {
        audioPlayback.PlayVoiceOver("gameover_1", LoadCredits);
    }

    void LoadCredits(bool finished)              
    { 
        if(finished) SceneManager.LoadScene(5);
    }
}
