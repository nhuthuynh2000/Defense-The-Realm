using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;

    public void UpdateGoldText(int amount)
    {
        goldText.text = "Gold: " + amount.ToString();
    }
}
