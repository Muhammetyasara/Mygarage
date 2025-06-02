using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TireShelf : MonoBehaviour
{
    public int tireCount;
    public int increaseCount;
    public int decreaseCount;
    public int avaliableTireCount;

    public float duration;
    public int vibrato;

    public GameObject[] tires;

    private void Start()
    {
        InvokeRepeating(nameof(Shake), 3, 5);
    }

    public void Shake()
    {
        transform.DOShakePosition(duration, Vector3.one, vibrato, 0);
    }

    public void IncreaseDetergentCount(int value)
    {
        for (int i = 0; i < tires.Length; i++)
        {
            if (!tires[i].activeInHierarchy)
            {
                tires[i].SetActive(true);
                increaseCount++;
                if (increaseCount == value)
                {
                    break;
                }
            }
        }
        increaseCount = 0;
        tireCount += value;
    }
    public void DecreaseTireCount(int value)
    {
        for (int i = 0; i < tires.Length; i++)
        {
            if (tires[i].activeInHierarchy)
            {
                tires[i].SetActive(false);
                decreaseCount++;
                if (decreaseCount == value)
                {
                    break;
                }
            }
        }
        decreaseCount = 0;
        tireCount -= value;
    }

    public int AvaliableTireCheck()
    {
        if (tireCount == 1)
        {
            avaliableTireCount = 1;
        }
        else if (tireCount >= 2)
        {
            avaliableTireCount = 2;
        }
        return avaliableTireCount;
    }
}