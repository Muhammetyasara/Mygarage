using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DetergentShelf : MonoBehaviour
{
    public int detergentCount;
    public int increaseCount;
    public int decreaseCount;
    public int avaliableDetergentCount;
    public float duration;
    public int vibrato;

    public GameObject[] detergents;

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
        for (int i = 0; i < detergents.Length; i++)
        {
            if (!detergents[i].activeInHierarchy)
            {
                detergents[i].SetActive(true);
                increaseCount++;
                if (increaseCount == value)
                {
                    break;
                }
            }
        }
        increaseCount = 0;
        detergentCount += value;
    }

    public void DecreaseDetergentCount(int value)
    {
        for (int i = 0; i < detergents.Length; i++)
        {
            if (detergents[i].activeInHierarchy)
            {
                detergents[i].SetActive(false);
                decreaseCount++;
                if (decreaseCount == value)
                {
                    break;
                }
            }
        }
        decreaseCount = 0;
        detergentCount -= value;
    }

    public int AvaliableDetergentCheck()
    {
        if (detergentCount == 1)
        {
            avaliableDetergentCount = 1;
        }
        else if (detergentCount >= 2)
        {
            avaliableDetergentCount = 2;
        }
        return avaliableDetergentCount;
    }
}