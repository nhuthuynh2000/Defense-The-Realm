using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;


    public int CurrentBalance { get => currentBalance; }

    UIManager uiManager;
    private void Start()
    {
        currentBalance = startingBalance;
        uiManager = FindAnyObjectByType<UIManager>();
        uiManager.UpdateGoldText(currentBalance);
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        uiManager.UpdateGoldText(currentBalance);

    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        uiManager.UpdateGoldText(currentBalance);

        if (currentBalance < 0)
        {
            ///lose
        }
    }
}
