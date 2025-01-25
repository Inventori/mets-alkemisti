using UnityEngine;

public class PontikkaSystem
{
    private float _temperature;
    private float _pressure;
    private float _stir;
    private float _completePercentage;
    
    public float Temperature => _temperature;
    public float Pressure => _pressure;
    public float Stir => _stir;
    public float CompletePercentage => _completePercentage;

    public Order currentOrder;
    
    public PontikkaSystem()
    {
      
    }
    public void SetNewGoal(Order newOrder)
    {
        currentOrder = newOrder;
    }

    public void TickPontikka()
    {
        CheckGoalProgress();
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
    
    private void PontikkaGoBoom()
    {
    }


    private float CalculateOrderScore()
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
