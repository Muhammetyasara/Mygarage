using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasolineMachineController : MonoBehaviour
{
    public GasolineShelf gasolineShelf;

    [Header("Gasoline Machine Parts")]
    public Transform leftGasolinePress;
    public Transform rightGasolinePress;
    public GameObject firstGasoline;
    public GameObject secondGasoline;

    public int gasolineCount;

    Vector3 newLeftPos;
    Vector3 newRightPos;

    private void Start()
    {
        newLeftPos = leftGasolinePress.localPosition;
        newRightPos = rightGasolinePress.localPosition;
        InvokeRepeating(nameof(CreateGasoline), 5f, 5f);
    }

    private void Update()
    {
        if (gasolineShelf.gasolineCount < 6)
        {
            PumpThePresses();
        }
        else
        {
            leftGasolinePress.localPosition = Vector3.zero;
            rightGasolinePress.localPosition = Vector3.zero;
            newLeftPos = Vector3.zero;
            newRightPos = Vector3.zero;
        }
    }

    private void PumpThePresses()
    {
        newLeftPos.y = Mathf.PingPong(Time.time, .7f);
        leftGasolinePress.localPosition = newLeftPos;

        newRightPos.y = -Mathf.PingPong(Time.time, .7f);
        rightGasolinePress.localPosition = newRightPos;
    }

    public void CreateGasoline()
    {
        if (gasolineShelf.gasolineCount != 6)
        {
            gasolineShelf.IncreaseGasolineCount(1);
        }
    }
}