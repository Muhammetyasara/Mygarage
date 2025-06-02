using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI moneyTxt;

    public GameObject moneyIcon;
    public GameObject moneyIconPrefab;

    public int moneyValue;

    private void Awake()
    {
        Instance = this;
        moneyValue = SaveManager.Instance.GetMoney();
    }

    private void Update()
    {
        moneyTxt.text = moneyValue.ToString();
    }

    public void IncreaseMoney(int money)
    {
        moneyValue += money;
        moneyTxt.text = moneyValue.ToString();
        SaveManager.Instance.SetMoney(moneyValue);
    }

    public void DecreaseMoney(int money)
    {
        moneyValue -= money;
        moneyTxt.text = moneyValue.ToString();
        SaveManager.Instance.SetMoney(moneyValue);
    }
}