using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireMachineController : MonoBehaviour
{
    public TireShelf tireShelf;

    [Header("Tire Machine Parts")]
    public Transform leftTirePress;
    public Transform rightTirePress;
    public GameObject firstTire;
    public GameObject secondTire;
    public ParticleSystem sleepEffect;

    public int tireCount;

    Vector3 newLeftPos;
    Vector3 newRightPos;

    private void Start()
    {
        newLeftPos = leftTirePress.localPosition;
        newRightPos = rightTirePress.localPosition;
        InvokeRepeating(nameof(CreateTire), 5f, 5f);
    }

    private void Update()
    {
        if (tireShelf.tireCount < 6)
        {
            PumpThePresses();
            sleepEffect.gameObject.SetActive(false);
        }
        else
        {
            leftTirePress.localPosition = Vector3.zero;
            rightTirePress.localPosition = Vector3.zero;
            newLeftPos = Vector3.zero;
            newRightPos = Vector3.zero;
            sleepEffect.gameObject.SetActive(true);
        }
    }

    private void PumpThePresses()
    {
        newLeftPos.y = Mathf.PingPong(Time.time, .7f);
        leftTirePress.localPosition = newLeftPos;

        newRightPos.y = -Mathf.PingPong(Time.time, .7f);
        rightTirePress.localPosition = newRightPos;
    }

    public void CreateTire()
    {
        if (tireShelf.tireCount != 6)
        {
            tireShelf.IncreaseDetergentCount(1);
        }
    }
}