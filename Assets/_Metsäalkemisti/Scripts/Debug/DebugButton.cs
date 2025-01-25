using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugButton : MonoBehaviour
{
   [SerializeField] private Button button;
   [SerializeField] private TMP_Text buttonText;

   public void Initialize(string text, Action callback)
   {
      buttonText.text = text;
      button.onClick.RemoveAllListeners();
      button.onClick.AddListener(callback.Invoke);
   }
}
