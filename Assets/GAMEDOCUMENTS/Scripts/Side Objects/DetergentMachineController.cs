using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetergentMachineController : MonoBehaviour
{
    public DetergentShelf detergentShelf;

    [Header("Detergent Machine Parts")]
    public Transform leftDetergentPress;
    public Transform rightDetergentPress;
    public GameObject firstDetergent;
    public GameObject secondDetergent;

    public int detergentCount;

    Vector3 newLeftPos;
    Vector3 newRightPos;

    private void Start()
    {
        newLeftPos = leftDetergentPress.localPosition;
        newRightPos = rightDetergentPress.localPosition;

        InvokeRepeating(nameof(CreateDetergent), 5f, 5f);
    }

    private void Update()
    {
        if (detergentShelf.detergentCount < 6)
        {
            PumpThePresses();
        }
        else
        {
            leftDetergentPress.localPosition = Vector3.zero;
            rightDetergentPress.localPosition = Vector3.zero;
            newLeftPos = Vector3.zero;
            newRightPos = Vector3.zero;
        }
    }

    private void PumpThePresses()
    {
        newLeftPos.y = Mathf.PingPong(Time.time, .7f);
        leftDetergentPress.localPosition = newLeftPos;

        newRightPos.y = -Mathf.PingPong(Time.time, .7f);
        rightDetergentPress.localPosition = newRightPos;
    }

    public void CreateDetergent()
    {
        if (detergentShelf.detergentCount != 6)
        {
            detergentShelf.IncreaseDetergentCount(1);
        }
    }

    //public void IncreaseDetergentCount(int value)
    //{
    //    detergentCount += value;
    //}
    //public void DecreaseDetergentCount(int value)
    //{
    //    for (int i = 0; i < value; i++)
    //    {
    //        if (secondDetergent.activeInHierarchy)
    //        {
    //            print(secondDetergent);
    //            secondDetergent.SetActive(false);
    //        }
    //        else if (firstDetergent.activeInHierarchy)
    //        {
    //            print(firstDetergent);
    //            firstDetergent.SetActive(false);
    //        }
    //    }
    //    detergentCount -= value;
    //}
}