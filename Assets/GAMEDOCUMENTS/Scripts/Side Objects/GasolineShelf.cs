using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GasolineShelf : MonoBehaviour
{
    public int gasolineCount;
    public int increaseCount;
    public int decreaseCount;
    public int avaliableGasolineCount;
    public float duration;
    public int vibrato;

    public GameObject[] gasolines;

    private void Start()
    {
        InvokeRepeating(nameof(Shake), 3, 5);
    }
    public void Shake()
    {
        transform.DOShakePosition(duration, Vector3.one, vibrato, 0);
    }

    public void IncreaseGasolineCount(int value)
    {
        for (int i = 0; i < gasolines.Length; i++)
        {
            if (!gasolines[i].activeInHierarchy)
            {
                gasolines[i].SetActive(true);
                increaseCount++;
                if (increaseCount == value)
                {
                    break;
                }
            }
        }
        increaseCount = 0;
        gasolineCount += value;
    }

    public void DecreaseGasolineCount(int value)
    {
        for (int i = 0; i < gasolines.Length; i++)
        {
            if (gasolines[i].activeInHierarchy)
            {
                gasolines[i].SetActive(false);
                decreaseCount++;
                if (decreaseCount == value)
                {
                    break;
                }
            }
        }
        decreaseCount = 0;
        gasolineCount -= value;
    }

    public int AvaliableGasolineCheck()
    {
        if (gasolineCount == 1)
        {
            avaliableGasolineCount = 1;
        }
        else if (gasolineCount >= 2)
        {
            avaliableGasolineCount = 2;
        }
        return avaliableGasolineCount;
    }
}