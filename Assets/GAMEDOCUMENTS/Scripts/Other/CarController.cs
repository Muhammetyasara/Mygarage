using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarController : MonoBehaviour
{
    public Enums enums;
    public PathCreators pathCreators;

    Entrance currentEntrance;

    public GameObject currentStationObject;

    [Header("Car Parts")]
    public Transform tireFrontLeft;
    public Transform tireFrontRight;
    public Transform backTires;
    public Transform hood;
    public ParticleSystem sparkle;
    public ParticleSystem gas;

    [Header("Car Movement Variables")]
    public float carMovementSpeed = 25f;
    public float pathDistanceTravelled;
    public float tireRotSpeed;
    Vector3 tireNewEulerAngles;

    [Header("General Car Settings")]
    public bool isForLift;
    public bool isForWash;
    public bool isForGas;
    public bool isComing;
    public bool isLeaving;
    public bool isPathClear = true;
    public float countdown = 2f;
    public int checkPathIndex;

    [Header("Car Washer Settings")]
    public bool isWashing;
    Vector3 newWashPos;

    [Header("Car Lift Settings")]
    public bool isLiftingUp;
    public bool isLiftingDown;
    Vector3 newHoodEuler;
    Vector3 newCarPos;
    Vector3 backToThisPos;

    [Header("Car Gas Pump Settings")]
    public bool isFillingGas;

    private void Awake()
    {
        pathCreators = FindAnyObjectByType<PathCreators>();
    }

    private void Start()
    {
        tireNewEulerAngles = backTires.localEulerAngles;
    }

    void Update()
    {
        if (isPathClear)
        {
            if (isComing)
            {
                CarOnTheMainPath();
            }
            if (isLeaving)
            {
                CarExit();
            }
        }
        if (isLiftingUp)
        {
            transform.position = newCarPos;
            if (countdown >= 0f && isLiftingUp)
            {
                Invoke(nameof(CarArrivesLiftUp), .5f);
            }
        }
        if (isLiftingDown)
        {
            transform.position = backToThisPos;
            if (countdown >= -2.5f && isLiftingDown)
            {
                Invoke(nameof(CarLeavingLiftDown), .5f);
            }
        }

        GasParticle();
    }

    public void GasParticle()
    {
        if (currentStationObject != null)
        {
            gas.gameObject.SetActive(false);
        }
        else
        {
            gas.gameObject.SetActive(true);
        }
    }
    void CarOnTheMainPath()
    {
        CarMovingIn(checkPathIndex);
    }

    void CarExit()
    {
        CarMovingOut(checkPathIndex);
    }

    public void CarArrivesLiftUp()
    {
        newHoodEuler = hood.localEulerAngles;

        newHoodEuler.x = Mathf.LerpAngle(newHoodEuler.x, -35f, .01f);
        newCarPos.y = Mathf.Lerp(newCarPos.y, 2.5f, .01f);

        hood.localEulerAngles = newHoodEuler;
        transform.position = newCarPos;
        backToThisPos = newCarPos;

        countdown -= Time.deltaTime;

    }

    public void CarLeavingLiftDown()
    {
        newHoodEuler = hood.localEulerAngles;
        newHoodEuler.x = Mathf.LerpAngle(newHoodEuler.x, 0f, .05f);
        hood.localEulerAngles = newHoodEuler;

        backToThisPos.y = Mathf.Lerp(backToThisPos.y, 0f, .05f);
        transform.position = backToThisPos;
        countdown -= Time.deltaTime;
    }

    public bool carPosCheck;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Entrance entrance))
        {
            currentEntrance = entrance;
            switch (entrance.enums.name)
            {
                case Enums.Name.LiftAreaEntrance:
                    LiftAreaSettings();
                    break;
                case Enums.Name.WasherAreaEntrance:
                    WasherAreaSettings();
                    break;
                case Enums.Name.GasStationAreaEntrance:
                    GasStationAreaSettings();
                    break;
                default:
                    break;
            }
        }

        if (other.TryGetComponent(out WasherController washerController))
        {
            if (!carPosCheck)
            {
                newWashPos = transform.position;
                carPosCheck = true;
            }

            if (!isLeaving)
            {
                currentStationObject = other.gameObject;
            }
        }

        if (other.TryGetComponent(out LiftController liftController))
        {
            if (!carPosCheck)
            {
                newCarPos = transform.position;
                carPosCheck = true;
            }
            if (!isLeaving)
            {
                backToThisPos = transform.position;
                currentStationObject = other.gameObject;
            }
        }

        if (other.TryGetComponent(out GasPumpController gasPumpController))
        {
            if (!isLeaving)
            {
                currentStationObject = other.gameObject;
            }
        }

        if (other.gameObject.layer == 6)
        {
            isPathClear = false;
        }

        if (other.gameObject.layer == 7)
        {
            if (pathDistanceTravelled < other.GetComponent<CarController>().pathDistanceTravelled)
            {
                isPathClear = false;
            }
        }

        if (other.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            isPathClear = true;
        }
        if (other.gameObject.layer == 7)
        {
            isPathClear = true;
        }
    }

    bool returning;
    public void WashTheCar()
    {
        if (newWashPos.z > 4f && !returning)
        {
            if (newWashPos.z < 5f)
            {
                returning = true;
            }
            newWashPos.z = Mathf.Lerp(newWashPos.z, 3f, .02f);
        }
        else
        {
            newWashPos.z = Mathf.Lerp(newWashPos.z, 13f, .02f);
        }

        transform.position = newWashPos;
    }

    public void WasherAreaSettings()
    {
        if (!isLeaving)
        {
            pathDistanceTravelled = 0f;
            checkPathIndex = currentEntrance.AvailableWasherFinder();
        }
    }

    public void LiftAreaSettings()
    {
        if (!isLeaving)
        {
            pathDistanceTravelled = 0f;
            checkPathIndex = currentEntrance.AvailableLiftFinder();
        }
    }

    public void GasStationAreaSettings()
    {
        if (!isLeaving)
        {
            pathDistanceTravelled = 0f;
            checkPathIndex = currentEntrance.AvailableGasPumpFinder();
        }
    }

    public void LeavingTheCurrentStation()
    {
        StartCoroutine(Delay(checkPathIndex));
    }
    IEnumerator Delay(int value)
    {
        yield return new WaitForSeconds(1f);
        pathCreators = FindAnyObjectByType<PathCreators>();
        checkPathIndex = value;
        pathDistanceTravelled = 0f;
        isComing = false;
        isLiftingUp = false;
        isLiftingDown = false;
        isLeaving = true;
        currentStationObject = null;
    }

    public void CarMovingIn(int index)
    {
        pathDistanceTravelled += carMovementSpeed * Time.deltaTime;
        if (isForWash)
        {
            transform.position = pathCreators.washerPaths[index].path.GetPointAtDistance(pathDistanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = pathCreators.washerPaths[index].path.GetRotationAtDistance(pathDistanceTravelled, EndOfPathInstruction.Stop);
        }
        if (isForLift)
        {
            transform.position = pathCreators.liftPaths[index].path.GetPointAtDistance(pathDistanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = pathCreators.liftPaths[index].path.GetRotationAtDistance(pathDistanceTravelled, EndOfPathInstruction.Stop);
        }
        if (isForGas)
        {
            transform.position = pathCreators.gasStationPaths[index].path.GetPointAtDistance(pathDistanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = pathCreators.gasStationPaths[index].path.GetRotationAtDistance(pathDistanceTravelled, EndOfPathInstruction.Stop);
        }
        tireNewEulerAngles += Vector3.right * Time.deltaTime * tireRotSpeed;
        backTires.localEulerAngles = tireNewEulerAngles;
        tireFrontLeft.localEulerAngles = tireNewEulerAngles;
        tireFrontRight.localEulerAngles = tireNewEulerAngles;
    }

    public void CarMovingOut(int index)
    {
        pathDistanceTravelled += carMovementSpeed * Time.deltaTime;
        if (isForWash)
        {
            transform.position = pathCreators.washerExitPaths[index].path.GetPointAtDistance(pathDistanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = pathCreators.washerExitPaths[index].path.GetRotationAtDistance(pathDistanceTravelled, EndOfPathInstruction.Stop);
        }
        if (isForLift)
        {
            transform.position = pathCreators.liftExitPaths[index].path.GetPointAtDistance(pathDistanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = pathCreators.liftExitPaths[index].path.GetRotationAtDistance(pathDistanceTravelled, EndOfPathInstruction.Stop);
        }
        if (isForGas)
        {
            transform.position = pathCreators.gasStationExitPaths[index].path.GetPointAtDistance(pathDistanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = pathCreators.gasStationExitPaths[index].path.GetRotationAtDistance(pathDistanceTravelled, EndOfPathInstruction.Stop);
        }
        tireNewEulerAngles += Vector3.right * Time.deltaTime * tireRotSpeed;
        backTires.localEulerAngles = tireNewEulerAngles;
        tireFrontLeft.localEulerAngles = tireNewEulerAngles;
        tireFrontRight.localEulerAngles = tireNewEulerAngles;
    }
}