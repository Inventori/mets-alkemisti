using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Initialize : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool _debug;
    [SerializeField] private DebugControls _debugControls;
    [Header("Pan control")]
    [SerializeField] private PontikkaUI pontikkaUI;
    [SerializeField] private HoboOrders orders;
    [SerializeField] private Image orderingHobo;
  
    private PontikkaSystem _pontikkaSystem;
    
    private Sequence _newCharacterSequence;
    
    void Start()
    {
        _pontikkaSystem = new PontikkaSystem();
        _pontikkaSystem.SetNewGoal(orders.Orders[0]);
       
        if (_debug)
        {
          _debugControls.Initialize(_pontikkaSystem);
        }

        InvokeRepeating("TickPontikka", 0, 1);
        pontikkaUI.Initialize(_pontikkaSystem);
        
    }
    
    
    void TickPontikka()
    {
        _pontikkaSystem.TickPontikka();
    }

    private Sequence NewCharacterSequence()
    {
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(2.5f);
        return sequence;
    }
}
