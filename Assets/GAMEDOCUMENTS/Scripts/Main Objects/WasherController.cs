using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WasherController : MonoBehaviour
{
    public Enums enums;

    Station station;

    public GameObject currentCar;

    [Header("Washer Parts")]
    public Transform leftCylinder;
    public Transform rightCylinder;
    public GameObject carPartImage;
    public TextMeshProUGUI detergentTxt;
    public MoneyGrid moneyCreator;
    public ParticleSystem detergentIncreaseEffect;
    public Image outLineImg;

    [Header("General Settings")]
    public float countdown;

    [Header("Washer Status")]
    public bool isWashing;
    public bool isWashingDone;

    public int detergentCount;
    public float cylinderRotSpeed;
    public Vector3 firstWashPos;

    private void Awake()
    {
        station = GetComponent<Station>();
    }

    private void Update()
    {
        detergentTxt.text = detergentCount.ToString();
        WashTheCar();

        if (detergentCount != 0)
        {
            outLineImg.fillAmount = (float)detergentCount / 2;
        }
        else
        {
            outLineImg.fillAmount = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CarController carController))
        {
            isWashing = false;
            isWashingDone = false;
            currentCar = other.gameObject;
            firstWashPos = currentCar.transform.position;
            station.isOccupied = true;
            carController.isLeaving = false;
            carController.isComing = false;
            carController.pathCreators = null;
        }
        if (other.TryGetComponent(out PlayerCarrying playerCarrying))
        {
            if (detergentCount == 2 || currentCar == null)
            {
                return;
            }
            if (playerCarrying.isDetergent && playerCarrying.carryingObjectCount == 1)
            {
                IncreaseDetergentCount(playerCarrying.carryingObjectCount);
                playerCarrying.DecreaseObjectCount(1);
                playerCarrying.HideObjects(1, playerCarrying.detergentObjects);
                detergentIncreaseEffect.Play();
            }
            else if (playerCarrying.isDetergent && playerCarrying.carryingObjectCount == 2)
            {
                if (detergentCount == 1)
                {
                    IncreaseDetergentCount(1);
                    playerCarrying.DecreaseObjectCount(1);
                    playerCarrying.HideObjects(1, playerCarrying.detergentObjects);
                    detergentIncreaseEffect.Play();
                }
                else if (detergentCount == 0)
                {
                    IncreaseDetergentCount(playerCarrying.carryingObjectCount);
                    playerCarrying.DecreaseObjectCount(2);
                    playerCarrying.HideObjects(2, playerCarrying.detergentObjects);
                    detergentIncreaseEffect.Play();
                }
            }
            if (detergentCount == 2 && currentCar != null)
            {
                WashingDone();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CarController>())
        {
            station.isOccupied = false;
            currentCar = null;
            detergentCount = 0;
            StopCoroutine(Delay());
        }
    }

    public void IncreaseDetergentCount(int value)
    {
        detergentCount += value;
    }

    public void WashTheCar()
    {
        if (isWashing && !isWashingDone)
        {
            RotateTheWasherCylinders(leftCylinder, rightCylinder);
            currentCar.GetComponent<CarController>().WashTheCar();
        }
    }

    public void RotateTheWasherCylinders(Transform transform, Transform transform1)
    {
        transform.eulerAngles += Vector3.down * cylinderRotSpeed;
        transform1.eulerAngles += Vector3.down * cylinderRotSpeed;
    }
    public void WashingDone()
    {
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        isWashing = true;
        yield return new WaitForSeconds(8f);
        currentCar.GetComponent<CarController>().sparkle.gameObject.SetActive(true);
        currentCar.GetComponent<CarController>().LeavingTheCurrentStation();
        moneyCreator.MoneyCreateSettings();
        station.TextPopUp();
        isWashingDone = true;
        isWashing = false;
    }
}