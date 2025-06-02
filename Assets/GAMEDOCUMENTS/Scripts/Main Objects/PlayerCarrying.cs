using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCarrying : MonoBehaviour
{
    public GameObject[] detergentObjects;
    public GameObject[] tireObjects;
    public GameObject[] gasolineObjects;
    public GameObject[] entireObjects;

    public int carryingObjectCount;

    public bool isDetergent;
    public bool isTire;
    public bool isGasoline;

    private void Start()
    {
        InvokeRepeating(nameof(ShakeObjects), .4f, .4f);
    }

    private void Update()
    {
        if (carryingObjectCount != 0)
        {
            GetComponent<PlayerController>().isCarrying = true;
        }
        else
        {
            GetComponent<PlayerController>().isCarrying = false;
            IsCarrying();
        }
    }

    public void ShakeObjects()
    {
        if (GetComponent<PlayerController>().moveVector != Vector3.zero)
        {
            for (int i = 0; i < entireObjects.Length; i++)
            {
                entireObjects[i].transform.DOLocalJump(entireObjects[i].transform.localPosition, .3f, 1, .3f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (carryingObjectCount == 2)
            {
                return;
            }
            else if (carryingObjectCount == 0)
            {
                if (other.TryGetComponent(out DetergentShelf detergent))
                {
                    if (detergent.detergentCount == 0)
                    {
                        return;
                    }
                    print("detergentcount0");

                    isDetergent = true;
                    IncreaseObjectCount(detergent.AvaliableDetergentCheck());
                    RevealObjects(detergent.AvaliableDetergentCheck(), detergentObjects);
                    detergent.DecreaseDetergentCount(detergent.AvaliableDetergentCheck());
                    detergent.avaliableDetergentCount = 0;
                }
                if (other.TryGetComponent(out TireShelf tire))
                {
                    if (tire.tireCount == 0)
                    {
                        return;
                    }
                    print("tirecount0");

                    GameManager.Instance.NewbieStart();
                    isTire = true;
                    IncreaseObjectCount(tire.AvaliableTireCheck());
                    RevealObjects(tire.AvaliableTireCheck(), tireObjects);
                    tire.DecreaseTireCount(tire.AvaliableTireCheck());
                    tire.avaliableTireCount = 0;
                }
                if (other.TryGetComponent(out GasolineShelf gasoline))
                {
                    if (gasoline.gasolineCount == 0)
                    {
                        return;
                    }
                    print("gasolinecount0");

                    isGasoline = true;
                    IncreaseObjectCount(gasoline.AvaliableGasolineCheck());
                    RevealObjects(gasoline.AvaliableGasolineCheck(), gasolineObjects);
                    gasoline.DecreaseGasolineCount(gasoline.AvaliableGasolineCheck());
                    gasoline.avaliableGasolineCount = 0;
                }
            }
            else if (carryingObjectCount == 1)
            {
                if (other.TryGetComponent(out DetergentShelf detergent))
                {
                    if (isDetergent)
                    {
                        if (detergent.detergentCount == 0)
                        {
                            return;
                        }
                        print("detergentcount1");

                        IncreaseObjectCount(1);
                        RevealObjects(detergent.AvaliableDetergentCheck(), detergentObjects);
                        detergent.DecreaseDetergentCount(1);
                        detergent.avaliableDetergentCount = 0;
                    }
                }
                if (other.TryGetComponent(out TireShelf tire))
                {
                    if (isTire)
                    {
                        if (tire.tireCount == 0)
                        {
                            return;
                        }
                        print("tirecount1");

                        IncreaseObjectCount(1);
                        RevealObjects(tire.AvaliableTireCheck(), tireObjects);
                        tire.DecreaseTireCount(1);
                        tire.avaliableTireCount = 0;
                    }
                }
                if (other.TryGetComponent(out GasolineShelf gasoline))
                {
                    if (isGasoline)
                    {
                        if (gasoline.gasolineCount == 0)
                        {
                            return;
                        }
                        print("gasolinecount1");

                        IncreaseObjectCount(1);
                        RevealObjects(gasoline.AvaliableGasolineCheck(), gasolineObjects);
                        gasoline.DecreaseGasolineCount(1);
                        gasoline.avaliableGasolineCount = 0;
                    }
                }
            }
        }

        if (other.gameObject.layer == 10)
        {
            GameManager.Instance.CollectMoney(other.transform.position, other.gameObject);
        }
    }

    public void DecreaseObjectCount(int num)
    {
        carryingObjectCount -= num;
    }
    public void IncreaseObjectCount(int num)
    {
        carryingObjectCount += num;
    }

    public void RevealObjects(int num, GameObject[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (!objects[i].activeInHierarchy)
            {
                objects[i].SetActive(true);
                if (num == 1)
                {
                    break;
                }
            }
        }
    }

    public void HideObjects(int num, GameObject[] objects)
    {
        for (int i = 1; i > -1; i--)
        {
            if (objects[i].activeInHierarchy)
            {
                objects[i].SetActive(false);
                if (num == 1)
                {
                    break;
                }
            }
        }
    }

    public void HideForTrash()
    {
        for (int i = 0; i < entireObjects.Length; i++)
        {
            entireObjects[i].SetActive(false);
            carryingObjectCount = 0;
        }
    }

    public void IsCarrying()
    {
        isDetergent = false;
        isTire = false;
        isGasoline = false;
    }
}