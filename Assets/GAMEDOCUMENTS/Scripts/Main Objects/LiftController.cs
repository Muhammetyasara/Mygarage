using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LiftController : MonoBehaviour
{
    public Enums enums;

    public GameObject currentCar;

    Station station;

    [Header("Lift Parts")]
    public Transform leftFork;
    public Transform rightFork;
    public GameObject carPartImage;
    public TextMeshProUGUI tireText;
    public ParticleSystem sparkEffectLeft;
    public ParticleSystem sparkEffectRight;
    public ParticleSystem tireIncreaseEffect;
    public MoneyGrid moneyCreator;
    public Image outLineImg;
    Vector3 newLeftForkPos;
    Vector3 newRightForkPos;

    [Header("General Settings")]
    public float countdown = 2f;

    [Header("Lift Status")]
    public bool isLiftingUp;
    public bool isLiftingDown;
    public bool isRepairDone;

    public int tireCount;

    private void Awake()
    {
        station = GetComponent<Station>();
    }
    private void Update()
    {
        tireText.text = tireCount.ToString();

        if (isLiftingUp && countdown >= 0f)
        {
            Invoke(nameof(LiftTheCarUp), .5f);
            countdown -= Time.deltaTime;
        }
        if (isLiftingDown && countdown >= -2.5f)
        {
            Invoke(nameof(LiftTheCarDown), .5f);
            countdown -= Time.deltaTime;
        }
        if (currentCar != null)
        {
            RepairTheCar();
        }
        if (countdown < 0f)
        {
            isLiftingUp = false;
        }

        if (tireCount != 0)
        {
            outLineImg.fillAmount = (float)tireCount / 2;
            GameManager.Instance.isNewbie = false;
            GameManager.Instance.NewbieStart();
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
            isLiftingUp = true;
            carController.isLeaving = false;
            carController.isComing = false;
            carController.isLiftingUp = true;
            carController.pathCreators = null;
        }
        if (other.TryGetComponent(out PlayerCarrying playerCarrying))
        {
            if (tireCount == 2 || currentCar == null || isLiftingDown || isLiftingUp)
            {
                return;
            }
            if (playerCarrying.isTire && playerCarrying.carryingObjectCount == 1)
            {
                tireIncreaseEffect.Play();
                IncreaseTireCount(playerCarrying.carryingObjectCount);
                playerCarrying.DecreaseObjectCount(1);
                playerCarrying.HideObjects(1, playerCarrying.tireObjects);
            }
            else if (playerCarrying.isTire && playerCarrying.carryingObjectCount == 2)
            {
                if (tireCount == 1)
                {
                    tireIncreaseEffect.Play();
                    IncreaseTireCount(1);
                    playerCarrying.DecreaseObjectCount(1);
                    playerCarrying.HideObjects(1, playerCarrying.tireObjects);
                }
                else if (tireCount == 0)
                {
                    tireIncreaseEffect.Play();
                    IncreaseTireCount(playerCarrying.carryingObjectCount);
                    playerCarrying.DecreaseObjectCount(2);
                    playerCarrying.HideObjects(2, playerCarrying.tireObjects);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentCar)
        {
            station.isOccupied = false;
            isLiftingDown = false;
            isLiftingUp = false;
            countdown = 2f;
            tireCount = 0;
            currentCar = null;
        }
    }
    public void IncreaseTireCount(int value)
    {
        tireCount += value;
    }

    public void RepairTheCar()
    {
        if (tireCount == 2 && currentCar != null)
        {
            isRepairDone = true;
            sparkEffectLeft.gameObject.SetActive(true);
            sparkEffectRight.gameObject.SetActive(true);
        }

        if (isRepairDone)
        {
            isLiftingUp = false;
            isLiftingDown = true;
            currentCar.GetComponent<CarController>().isLiftingDown = true;
            if (currentCar.GetComponent<CarController>().countdown <= -1.7f)
            {
                sparkEffectLeft.gameObject.SetActive(false);
                sparkEffectRight.gameObject.SetActive(false);
            }
            if (currentCar.GetComponent<CarController>().countdown <= -2.5f)
            {
                currentCar.GetComponent<CarController>().LeavingTheCurrentStation();
                moneyCreator.MoneyCreateSettings();
                station.TextPopUp();
                tireCount = 0;
                isRepairDone = false;
            }
        }
    }

    public void LiftTheCarUp()
    {
        newLeftForkPos = leftFork.localPosition;
        newRightForkPos = rightFork.localPosition;

        newLeftForkPos.y = Mathf.Clamp(newLeftForkPos.y, 0f, 3f);
        newRightForkPos.y = Mathf.Clamp(newRightForkPos.y, 0f, 3f);

        newLeftForkPos.y = Mathf.Lerp(newLeftForkPos.y, 3f, .01f);
        newRightForkPos.y = Mathf.Lerp(newRightForkPos.y, 3f, .01f);

        leftFork.localPosition = newLeftForkPos;
        rightFork.localPosition = newRightForkPos;
    }

    public void LiftTheCarDown()
    {
        newLeftForkPos = leftFork.localPosition;
        newRightForkPos = rightFork.localPosition;

        newLeftForkPos.y = Mathf.Clamp(newLeftForkPos.y, 0f, 3f);
        newRightForkPos.y = Mathf.Clamp(newRightForkPos.y, 0f, 3f);

        newLeftForkPos.y = Mathf.Lerp(newLeftForkPos.y, 0f, .05f);
        newRightForkPos.y = Mathf.Lerp(newRightForkPos.y, 0f, .05f);

        leftFork.localPosition = newLeftForkPos;
        rightFork.localPosition = newRightForkPos;
    }
}