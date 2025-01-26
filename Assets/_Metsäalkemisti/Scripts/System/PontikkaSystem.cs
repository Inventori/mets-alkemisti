using System;
using UnityEngine;

public class PontikkaSystem
{
    public enum Result
    {
        Perfect,
        Good,
        Okay,
        Poor
    }
    
    private float _temperature;
    private float _pressure;
    private float _stir;
    private float _completePercentage;

    private int _timer;
    private bool _running;
    
    public float Temperature => _temperature;
    public float Pressure => _pressure;
    public float Stir => _stir;
    public float CompletePercentage => _completePercentage;
    public int Timer => _timer;

    private Order currentOrder;
    public Action<bool> OnRoundEnd;
    
    public PontikkaSystem()
    {
      
    }
    public void SetNewGoal(Order newOrder)
    {
        currentOrder = newOrder;
    }

    public void TickPontikka()
    {
        if (!_running) return;
        
        if (_timer > 0)
        {
            CheckGoalProgress();
            _timer--;
        }
        else
        {
            _running = false;
            RoundEnded();
        }
    
    }

    public void AdjustPressure(float value)
    {
        _pressure = value;
    }

    public void AdjustStir(float value)
    {
        _stir = value;
    }

    public void AdjustTemperature(float value)
    {
        _temperature = value;
    }

    public void CheckGoalProgress()
    {
        if(currentOrder ==null)
        {
            return;
        }

        _completePercentage = CalculateOrderScore();
    }

    public void StartNewRound()
    {
        _timer = 20;
        _running = true;
    }

    private void RoundEnded()
    {
        var success = CalculateOrderScore() > .5f;
        OnRoundEnd?.Invoke(success);
    }
    
    private void PontikkaGoBoom()
    {
    }


    public float CalculateOrderScore()
    {
        // Calculate absolute differences
        float temperatureDiff = Mathf.Abs(currentOrder.wantedTemperature - _temperature);
        float pressureDiff = Mathf.Abs(currentOrder.wantedPressure - _pressure);
        float stirDiff = Mathf.Abs(currentOrder.wantedStir - _stir);

        // Normalize the differences (avoid division by zero)
        float normalizedA = currentOrder.wantedTemperature != 0 ? 1 - (temperatureDiff / currentOrder.wantedTemperature): 1;
        float normalizedB = currentOrder.wantedPressure != 0 ? 1 - (pressureDiff / currentOrder.wantedPressure) : 1;
        float normalizedC = currentOrder.wantedStir != 0 ? 1 - (stirDiff / currentOrder.wantedStir) : 1;

        // Combine normalized values into a single score
        float score = (normalizedA + normalizedB + normalizedC) / 3;

        // Clamp the score between 0 and 1
        return Mathf.Clamp(score, 0, 1);
    }
} 
