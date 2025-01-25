using UnityEngine;
using UnityEngine.UI.Extensions;

public class PontikkaUI : MonoBehaviour
{
    [SerializeField] private UI_Knob temperatureValve;
    [SerializeField] private UI_Knob pressureValve;
    [SerializeField] private UI_Knob stirValve;

    private PontikkaSystem _pontikkaSystem;
    public void Initialize(PontikkaSystem pontikkaSystem)
    {
        _pontikkaSystem = pontikkaSystem; 
        temperatureValve.OnValueChanged.AddListener(TemperatureChanged);
        pressureValve.OnValueChanged.AddListener(PressureChanged);
        stirValve.OnValueChanged.AddListener(StirChanged);
    }

    private void TemperatureChanged(float value)
    {
        _pontikkaSystem.AdjustTemperature(value);
    } 
    private void PressureChanged(float value)
    {
        _pontikkaSystem.AdjustPressure(value);
    } 
    private void StirChanged(float value)
    {
        _pontikkaSystem.AdjustStir(value);
    }
}
