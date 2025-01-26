using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    [SerializeField] private AudioPlayback audioPlayback;
    [SerializeField] private CanvasGroup canvasGroup;
    private int _step = 0;
    
    private void Start()
    {
        audioPlayback.PlayVoiceOver("intro_1", OnVoiceOverProgress);
    }

    public void OnVoiceOverProgress(bool finished)
    {
        switch (_step)
        {
            case 0:
                canvasGroup.DOFade(1, 2);

                break;
        }

        if (finished)
        {
            SceneManager.LoadScene(1);
        }

        _step++;
    }
}
