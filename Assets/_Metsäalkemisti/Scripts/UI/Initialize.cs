using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Initialize : MonoBehaviour
{
    [Header("Debug")] [SerializeField] private bool _debug;
    [SerializeField] private DebugControls _debugControls;

    [Header("Pan control")] [SerializeField]
    private PontikkaUI pontikkaUI;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private HoboOrders orders;
    [SerializeField] private HoboController orderingHobo;
    [SerializeField] private RectTransform spawningPoint;
    [SerializeField] private RectTransform orderingPoint;
    [SerializeField] private RectTransform despawningPoint;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private RectTransform itemInPannu;

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
        pontikkaUI.HideBubbles();
        pontikkaUI.UpdateItemInPannu(success ? orders.Orders[_currentOrder].succeededSprite : orders.Orders[_currentOrder].failedSprite);
        orderingHobo.UpdateItem(success);
        
        if (success)
        {
            _currentOrder++;
        }
        else
        {
            pontikkaUI.GameOver(ReloadScene);
            return;
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
        sequence.Append(orderingHobo.MoveHoboTo(orderingPoint.anchoredPosition.x, 3f).OnComplete(()=>
        {
            orderingHobo.SetItemParent(orderingHobo.ParentRectTransform);
        }));
        sequence.Append(orderingHobo.MoveItem(itemInPannu.anchoredPosition).OnComplete(() =>
        {
            orderingHobo.ShowItem(false);
            pontikkaUI.UpdateItemInPannu(orders.Orders[_currentOrder].ingredientsSprite);
        }));
        sequence.AppendInterval(2.5f).OnComplete(() =>
        {
            _pontikkaSystem.SetNewGoal(orders.Orders[_currentOrder]);
            _pontikkaSystem.StartNewRound();
            pontikkaUI.SetBubbles();
        });

        return sequence;
    }

    private Sequence CharacterLeaveSequence()
    {
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(2.5f);
        sequence.Append(orderingHobo.MoveItemToHobo().OnStart(() =>
        {
            orderingHobo.SetItemParent(orderingHobo.RectTransform);
            orderingHobo.ShowItem(true);
            pontikkaUI.HideItemInPannu();
        }));
        sequence.Append(orderingHobo.MoveHoboTo(despawningPoint.anchoredPosition.x, 6f));
        sequence.AppendInterval(2.5f);
        return sequence;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(4);
    }
}