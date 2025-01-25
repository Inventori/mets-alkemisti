using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugControls : MonoBehaviour
{
    [SerializeField] private DebugButton _buttonPrefab;
    [SerializeField] private TMP_Text _debugText;
    [SerializeField] private RectTransform _infoContainer;
    [SerializeField] private RectTransform _buttonContainer;

    private PontikkaSystem _pontikkaSystem;
    private List<TMP_Text> _texts = new List<TMP_Text>();
    private bool _initialised = false;
    
    public void Initialize(PontikkaSystem pontikkaSystem)
    {
        if (pontikkaSystem == null)
        {
            Debug.LogError("Pontikka is null");
        }

        _pontikkaSystem = pontikkaSystem;
        
        var heat = _pontikkaSystem.Stir;
        
        print(_pontikkaSystem.Stir);
        
        CreateButton("Increase heat", _pontikkaSystem.IncreaseHeat);
        CreateButton("Decrease heat", _pontikkaSystem.DecreaseHeat);
        CreateButton("Drop pressure", _pontikkaSystem.DropPressure);
        
        _texts.Add(CreateText());
        _texts.Add(CreateText());
        _texts.Add(CreateText());
         _initialised = true;
    }

    private void Update()
    {
         if (!_initialised) return;
         
         var heat = _pontikkaSystem.Stir;
         var temperature = _pontikkaSystem.Temperature;
         var pressure = _pontikkaSystem.Pressure;
         _texts[0].text = $"Current Temperature {temperature}";
         _texts[1].text = $"Current heat {heat}";
         _texts[2].text =  $"Current pressure {pressure}";
    }


    private void CreateButton(string text, Action callback)
    {
        var button = Instantiate(_buttonPrefab, _buttonContainer);
        button.Initialize(text, callback);
    }
    
    private TMP_Text CreateText()
    {
        var textField = Instantiate(_debugText, _infoContainer);
        return textField;
    }
}