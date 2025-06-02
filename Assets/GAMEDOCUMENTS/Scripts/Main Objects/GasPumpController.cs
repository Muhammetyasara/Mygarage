using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GasPumpController : MonoBehaviour
{
    public Enums enums;

    Station station;

    public GameObject currentCar;

    [Header("General Settings")]
    public float countdown;

    [Header("Gas Pump Parts")]
    public GameObject carPartImage;
    public TextMeshProUGUI gasolineTxt;
    public MoneyGrid moneyCreator;
    public ParticleSystem gasIncreaseEffect;
    public Image outLineImg;

    [Header("Gas Pump Status")]
    public bool isFilling;
    public bool isFillingDone;

    public int gasolineCount;
    private void Awake()
    {
        station = GetComponent<Station>();
    }

    private void Update()
    {
        gasolineTxt.text = gasolineCount.ToString();

        RefillTheCar();

        if (gasolineCount != 0)
        {
            outLineImg.fillAmount = (float)gasolineCount / 2;
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
            currentCar = other.gameObject;
            station.isOccupied = true;
            carController.isLeaving = false;
            carController.isComing = false;
            carController.pathCreators = null;
            carPartImage.SetActive(true);
        }
        if (other.TryGetComponent(out PlayerCarrying playerCarrying))
        {
            if (gasolineCount == 2 || currentCar == null)
            {
                return;
            }
            if (playerCarrying.isGasoline && playerCarrying.carryingObjectCount == 1)
            {
                IncreaseGasolineCount(playerCarrying.carryingObjectCount);
                playerCarrying.DecreaseObjectCount(1);
                playerCarrying.HideObjects(1, playerCarrying.gasolineObjects);
                gasIncreaseEffect.Play();
            }
            else if (playerCarrying.isGasoline && playerCarrying.carryingObjectCount == 2)
            {
                if (gasolineCount == 1)
                {
                    IncreaseGasolineCount(1);
                    playerCarrying.DecreaseObjectCount(1);
                    playerCarrying.HideObjects(1, playerCarrying.gasolineObjects);
                    gasIncreaseEffect.Play();
                }
                else if (gasolineCount == 0)
                {
                    IncreaseGasolineCount(playerCarrying.carryingObjectCount);
                    playerCarrying.DecreaseObjectCount(2);
                    playerCarrying.HideObjects(2, playerCarrying.gasolineObjects);
                    gasIncreaseEffect.Play();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentCar)
        {
            station.isOccupied = false;
            currentCar = null;
        }
    }

    public void IncreaseGasolineCount(int value)
    {
        gasolineCount += value;
    }

    public void RefillTheCar()
    {
        if (gasolineCount == 2 && currentCar != null)
        {
            isFillingDone = true;
            gasolineCount = 0;
        }

        if (isFillingDone)
        {
            currentCar.GetComponent<CarController>().LeavingTheCurrentStation();
            moneyCreator.MoneyCreateSettings();
            station.TextPopUp();
            isFillingDone = false;
        }
    }
}