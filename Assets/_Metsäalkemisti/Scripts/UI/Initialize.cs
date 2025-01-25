using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Initialize : MonoBehaviour
{
    [Header("Debug")] [SerializeField] private bool _debug;
    [SerializeField] private DebugControls _debugControls;

    [Header("Pan control")] [SerializeField]
    private PontikkaUI pontikkaUI;

    [SerializeField] private HoboOrders orders;
    [SerializeField] private HoboController orderingHobo;
    [SerializeField] private RectTransform spawningPoint;
    [SerializeField] private RectTransform orderingPoint;
    [SerializeField] private RectTransform despawningPoint;
    [SerializeField] private TMP_Text _timerText;

    private PontikkaSystem _pontikkaSystem;

    private Sequence _newCharacterSequence;
    private int _currentOrder;

    void Start()
    {
        _pontikkaSystem = new PontikkaSystem();
        _pontikkaSystem.OnRoundEnd = EndCurrentRound;

        if (_debug)
        {
            _debugControls.Initialize(_pontikkaSystem);
        }

        InvokeRepeating("TickPontikka", 0, 1);
        pontikkaUI.Initialize(_pontikkaSystem);

        _currentOrder = 0;
        StartNewRound();
    }

    void TickPontikka()
    {
        _pontikkaSystem.TickPontikka();
        _timerText.text = _pontikkaSystem.Timer.ToString();
    }

    private void StartNewRound()
    {
        orderingHobo.UpdateCharacter(orders.Orders[_currentOrder]);
        orderingHobo.MoveCharacter(spawningPoint.anchoredPosition);
        var newSeq = NewCharacterSequence();
        newSeq.Play();
    }

    private void EndCurrentRound(bool success)
    {
        if (success)
        {
            _currentOrder++;
        }
        else
        {
        }

        var sequence = DOTween.Sequence();
        sequence.Append(CharacterLeaveSequence());
        sequence.AppendInterval(1f).OnComplete(StartNewRound);
        sequence.Play();
    }

    private Sequence NewCharacterSequence()
    {
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(2.5f);
        sequence.Append(orderingHobo.MoveHoboTo(orderingPoint.anchoredPosition.x, 3f));
        sequence.AppendInterval(2.5f).OnComplete(() =>
        {
            _pontikkaSystem.SetNewGoal(orders.Orders[_currentOrder]);
            _pontikkaSystem.StartNewRound();
        });

        return sequence;
    }

    private Sequence CharacterLeaveSequence()
    {
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(2.5f);
        sequence.Append(orderingHobo.MoveHoboTo(despawningPoint.anchoredPosition.x, 6f));
        sequence.AppendInterval(2.5f);
        return sequence;
    }
}