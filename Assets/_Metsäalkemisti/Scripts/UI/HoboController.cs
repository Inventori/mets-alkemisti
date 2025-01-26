using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HoboController : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image hoboItem;
    private float _characterPosY;
    private Vector3 _hoboItemPosition;
    private Order _currentHoboOrder;
    
    public RectTransform RectTransform { get { return characterImage.rectTransform; } }
    public RectTransform ParentRectTransform { get { return rectTransform.parent.GetComponent<RectTransform>(); } }
    
    void Start()
    {
        _characterPosY = characterImage.rectTransform.anchoredPosition.y;
        _hoboItemPosition = hoboItem.rectTransform.anchoredPosition;
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

    public Sequence MoveItem(Vector3 position)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(hoboItem.rectTransform.DOJumpAnchorPos(position, 100f,1,1f));
        return sequence;
    }

    public Sequence MoveItemToHobo()
    {
        var sequence = MoveItem(_hoboItemPosition);
        return sequence;
    }

    
    public void SetItemParent(RectTransform parent)
    {
        hoboItem.rectTransform.SetParent(parent);
    }
    
    public void ResetPosition()
    {
        rectTransform.anchoredPosition = _hoboItemPosition;
    }
    public void UpdateCharacter(Order order)
    {
        _currentHoboOrder = order;
        characterImage.sprite = order.hoboSprite;
        if (_currentHoboOrder.flip)
        {
            if (characterImage.rectTransform.localScale.x > 0)
            {
                var scale = characterImage.rectTransform.localScale;
                scale.x *= -1;
                characterImage.rectTransform.localScale = scale;
            }
        }
        else
        {
            if (characterImage.rectTransform.localScale.x < 0)
            {
                var scale = characterImage.rectTransform.localScale;
                scale.x *= -1;
                characterImage.rectTransform.localScale = scale;
            }
        }
        hoboItem.sprite = order.ingredientsSprite;
    }

    public void UpdateItem(bool success)
    {
        hoboItem.sprite = success ? _currentHoboOrder.succeededSprite : _currentHoboOrder.failedSprite;
    }

    public void ShowItem(bool show)
    {
        hoboItem.gameObject.SetActive(show);
    }
}
