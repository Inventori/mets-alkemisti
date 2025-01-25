using UnityEngine;
using UnityEngine.UI;

public class HoboController : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    private RectTransform _rectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }


    void Walking()
    {
        
    }

    void UpdateCharacter()
    {
    }
}
