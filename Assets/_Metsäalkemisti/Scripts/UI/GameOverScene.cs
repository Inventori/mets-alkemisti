using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] private AudioPlayback audioPlayback;
    [SerializeField] private Image fade;
    void Start()
    {
        fade.DOFade(1, 1f).From();
        audioPlayback.PlayVoiceOver("gameover_1", LoadCredits);
    }

    void LoadCredits(bool finished)              
    { 
        if(finished) SceneManager.LoadScene(4);
    }
}
