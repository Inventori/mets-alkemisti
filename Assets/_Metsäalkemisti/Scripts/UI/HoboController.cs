using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HoboController : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private RectTransform rectTransform;
    private float _characterPosY;
    
    void Start()
    {
        _characterPosY = characterImage.rectTransform.anchoredPosition.y;
    }
    
    public void Walking()
    {
        characterImage.rectTransform.DOAnchorPosY(characterImage.rectTransform.anchoredPosition.y + 25f, 0.15f)
            .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutBounce);
    }

    public void StopMovement()
    {
        characterImage.rectTransform.DOKill();
        characterImage.rectTransform.DOAnchorPosY(_characterPosY, 0.1f);
    }

    public void Talking()
    {
        characterImage.rectTransform.DOAnchorPosY(characterImage.rectTransform.anchoredPosition.y + 10f, 0.1f)
            .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InBack);
    }

    public Sequence MoveHoboTo(float x, float duration)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(rectTransform.DOAnchorPosX(x, duration).SetEase(Ease.Linear)
            .OnStart(Walking).OnComplete(StopMovement));
        return sequence;
    }

    public void MoveCharacter(Vector3 position)
    {
        rectTransform.anchoredPosition = new Vector3(position.x, rectTransform.anchoredPosition.y, 0);
    }

    public void UpdateCharacter(Order order)
    {
        characterImage.sprite = order.hoboSprite;
    }
}
