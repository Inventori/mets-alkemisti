using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "Scriptable Objects/Order")]
public class Order : ScriptableObject
{
    public float wantedTemperature;
    public float wantedPressure;
    public float wantedStir;

    public Sprite hoboSprite;
}