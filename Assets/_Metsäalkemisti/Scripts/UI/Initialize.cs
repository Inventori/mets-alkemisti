using UnityEngine;

public class Initialize : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool _debug;
    [SerializeField] private DebugControls _debugControls;
    [Header("Pan control")]
    [SerializeField] private float specificHeatCapacity = 1;
    [SerializeField] private float gasMass = 1;
    [SerializeField] private float proportionalityConstant = 1;
    [SerializeField] private PontikkaUI pontikkaUI;
    //private DebugControls _debugController;
    private PontikkaSystem _pontikkaSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pontikkaSystem = new PontikkaSystem();
        
        if (_debug)
        {
          //  _debugController = Instantiate(_debugControls, transform);
          _debugControls.Initialize(_pontikkaSystem);
        }

        InvokeRepeating("TickPontikka", 0, 1);
    }

    void TickPontikka()
    {
        _pontikkaSystem.TickPontikka();
    }
    
    // Update is called once per frame
    void Update()
    {                                
        
    }
}
