using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTile : MonoBehaviour
{
    public bool isOccupied;
    public GameObject moneyObj;

    private void Update()
    {
        StatusCheck();
        transform.eulerAngles += Vector3.up;
    }

    public void StatusCheck()
    {
        if (!moneyObj)
        {
            isOccupied = false;
        }
        else
        {
            moneyObj.transform.eulerAngles = transform.eulerAngles;
            isOccupied = true;
        }
    }
}