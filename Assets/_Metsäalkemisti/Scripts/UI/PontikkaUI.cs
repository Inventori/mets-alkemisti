using UnityEngine;

public class PontikkaUI : MonoBehaviour
{
    [SerializeField] private ControlValve temperatureValve;
    [SerializeField] private ControlValve pressureValve;
    [SerializeField] private ControlValve stirValve;

    public void Initialize(PontikkaSystem pontikkaSystem)
    {
        temperatureValve.Initialize(pontikkaSystem);
        pressureValve.Initialize(pontikkaSystem);
        stirValve.Initialize(pontikkaSystem);
    }
}
