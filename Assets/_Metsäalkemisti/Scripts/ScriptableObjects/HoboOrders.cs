using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HoboOrders", menuName = "Scriptable Objects/HoboOrder")]
public class HoboOrders : ScriptableObject
{
   [SerializeField] private List<Order> orders;
   public List<Order> Orders { get { return orders; } }
}