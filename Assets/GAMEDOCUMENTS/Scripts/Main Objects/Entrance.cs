using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public Enums enums;
    public PathCreators pathCreators;

    [Header("Main Objects")]
    public GameObject[] washers; 
    public GameObject[] lifts;
    public GameObject[] gasPumps;

    [Header("Indexes")]
    public int availableLiftIndex;
    public int availableWasherIndex;
    public int availableGasPumpIndex;

    public int AvailableWasherFinder()
    {
        for (int i = 0; i < washers.Length; i++)
        {
            if (washers[i] != null && washers[i].activeInHierarchy)
            {
                if (!washers[i].GetComponent<Station>().isOccupied)
                {
                    availableWasherIndex = i + 1;
                    washers[i].GetComponent<Station>().isOccupied = true;
                    break;
                }
            }
        }
        return availableWasherIndex;
    }

    public int AvailableLiftFinder()
    {
        for (int i = 0; i < lifts.Length; i++)
        {
            if (lifts[i] != null && lifts[i].activeInHierarchy)
            {
                if (!lifts[i].GetComponent<Station>().isOccupied)
                {
                    availableLiftIndex = i + 1;
                    lifts[i].GetComponent<Station>().isOccupied = true;
                    break;
                }
            }
        }
        return availableLiftIndex;
    }
    
    public int AvailableGasPumpFinder()
    {
        for (int i = 0; i < gasPumps.Length; i++)
        {
            if (gasPumps[i] != null && gasPumps[i].activeInHierarchy)
            {
                if (!gasPumps[i].GetComponent<Station>().isOccupied)
                {
                    availableGasPumpIndex = i + 1;
                    gasPumps[i].GetComponent<Station>().isOccupied = true;
                    break;
                }
            }
        }
        return availableGasPumpIndex;
    }
}