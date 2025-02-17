using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class PontikkaUI : MonoBehaviour
{
    [SerializeField] private UI_Knob temperatureValve;
    [SerializeField] private UI_Knob pressureValve;
    [SerializeField] private UI_Knob stirValve;
    [SerializeField] private ParticleSystem bubbles;
    [SerializeField] private Image itemInPannu;
    
    [SerializeField] private CanvasGroup gameOver;

    private PontikkaSystem _pontikkaSystem;
    public void Initialize(PontikkaSystem pontikkaSystem)
    {
        _pontikkaSystem = pontikkaSystem; 
        temperatureValve.OnValueChanged.AddListener(TemperatureChanged);
        pressureValve.OnValueChanged.AddListener(PressureChanged);
        stirValve.OnValueChanged.AddListener(StirChanged);
        HideItemInPannu();
        HideBubbles();
        gameOver.gameObject.SetActive(false);
    }

    private void TemperatureChanged(float value)
    {
        _pontikkaSystem.AdjustTemperature(value);
        SetBubbles();
    } 
    private void PressureChanged(float value)
    {
        _pontikkaSystem.AdjustPressure(value);
        SetBubbles();

    } 
    private void StirChanged(float value)
    {
        _pontikkaSystem.AdjustStir(value);
        SetBubbles();
    }
    
    public void HideBubbles()
    {
        var emission = bubbles.emission;    
        emission.rateOverTime = 0;
    }

    
    public void GameOver(Action callback)
    {
        gameOver.gameObject.SetActive(true);
        gameOver.DOFade(1, 4f).OnComplete(callback.Invoke);
    }
    
    public void SetBubbles()
    {
        if (!_pontikkaSystem.Running)
        {
            return;
        }
        
        var emission = bubbles.emission;
       var goal = _pontikkaSystem.CalculateOrderScore();

       var rateMulti = 1f;
       if (goal > .8)
       {
           rateMulti = 2;
       }
       else if (goal > .5)
       {
           rateMulti = 1.25f;
       }
       else if (goal > .25)
       {
           rateMulti = 0.75f;
       }
       else
       {
           rateMulti = .5f;
       }
       
       emission.rateOverTime = goal * 100 * rateMulti;
    }

    public void UpdateItemInPannu(Sprite newSprite)
    {
        itemInPannu.gameObject.SetActive(true);
        itemInPannu.sprite = newSprite;
    }

    public void HideItemInPannu()
    {
        itemInPannu.gameObject.SetActive(false);
    }
    
}
