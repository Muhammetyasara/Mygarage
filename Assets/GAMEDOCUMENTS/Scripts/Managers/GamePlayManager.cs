using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public GameObject[] cars;
    public GameObject[] stations;

    public bool isCreated;
    public bool isPathClear = true;

    private void Start()
    {
        InvokeRepeating(nameof(FindAvailableStation), 0f, 10f);
    }

    public void FindAvailableStation()
    {
        if (isPathClear)
        {
            foreach (GameObject item in stations)
            {
                if (!item.GetComponent<Station>().isOccupied && item.activeInHierarchy && !isCreated)
                {
                    switch (item.GetComponent<Station>().enums.type)
                    {
                        case Enums.Type.Washer:
                            StartCoroutine(CarCreator(true, false, false));
                            break;
                        case Enums.Type.Lift:
                            StartCoroutine(CarCreator(false, true, false));
                            break;
                        case Enums.Type.Gas:
                            StartCoroutine(CarCreator(false, false, true));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    IEnumerator CarCreator(bool isForWash, bool isForLift, bool isForGas)
    {
        isCreated = true;
        yield return new WaitForSeconds(0.01f);
        isCreated = false;
        int r = Random.Range(0, 4);
        GameObject createdCar = Instantiate(cars[r]);
        if (createdCar.TryGetComponent(out CarController carController))
        {
            carController.isForWash = isForWash;
            carController.isForLift = isForLift;
            carController.isForGas = isForGas;
            carController.isComing = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            isPathClear = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            isPathClear = true;
        }
    }
}