using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public Station[] stations;
    public StationOpener[] stationOpeners;
    public AreaOpener[] areaOpeners;

    public HandImage[] handImage;
    int handValue;

    int moneyValue;
    int lift2StationValue, lift3StationValue, lift4StationValue, washer1StationValue, washer2StationValue,
        gas1StationValue, gas2StationValue, gas3StationValue, gas4StationValue;
    int washerAreaValue, gasAreaValue;

    bool isNewbie;

    private void Awake()
    {
        Instance = this;
    }

    #region --MONEYSAVER--
    public void SetMoney(int money)
    {
        PlayerPrefs.SetInt(nameof(moneyValue), money);
    }

    public int GetMoney()
    {
        return PlayerPrefs.GetInt(nameof(moneyValue));
    }
    #endregion

    #region --STATIONSAVER--

    public void SetStation(string name)
    {
        switch (name)
        {
            case "Lift-2":
                PlayerPrefs.SetInt(nameof(lift2StationValue), 1);
                break;
            case "Lift-3":
                PlayerPrefs.SetInt(nameof(lift3StationValue), 1);
                break;
            case "Lift-4":
                PlayerPrefs.SetInt(nameof(lift4StationValue), 1);
                break;
            case "Washer-1":
                PlayerPrefs.SetInt(nameof(washer1StationValue), 1);
                break;
            case "Washer-2":
                PlayerPrefs.SetInt(nameof(washer2StationValue), 1);
                break;
            case "GasPump-1":
                PlayerPrefs.SetInt(nameof(gas1StationValue), 1);
                break;
            case "GasPump-2":
                PlayerPrefs.SetInt(nameof(gas2StationValue), 1);
                break;
            case "GasPump-3":
                PlayerPrefs.SetInt(nameof(gas3StationValue), 1);
                break;
            case "GasPump-4":
                PlayerPrefs.SetInt(nameof(gas4StationValue), 1);
                break;
            default:
                break;
        }
    }

    public void CheckStations()
    {
        stations[0].stationValue = PlayerPrefs.GetInt(nameof(lift2StationValue));
        stations[1].stationValue = PlayerPrefs.GetInt(nameof(lift3StationValue));
        stations[2].stationValue = PlayerPrefs.GetInt(nameof(lift4StationValue));
        stations[3].stationValue = PlayerPrefs.GetInt(nameof(washer1StationValue));
        stations[4].stationValue = PlayerPrefs.GetInt(nameof(washer2StationValue));
        stations[5].stationValue = PlayerPrefs.GetInt(nameof(gas1StationValue));
        stations[6].stationValue = PlayerPrefs.GetInt(nameof(gas2StationValue));
        stations[7].stationValue = PlayerPrefs.GetInt(nameof(gas3StationValue));
        stations[8].stationValue = PlayerPrefs.GetInt(nameof(gas4StationValue));
    }
    public void GetStation()
    {
        for (int i = 0; i < stations.Length; i++)
        {
            if (stations[i].stationValue == 1)
            {
                stations[i].gameObject.SetActive(true);
                stationOpeners[i].gameObject.SetActive(false);
            }
            else
            {
                stations[i].gameObject.SetActive(false);
                stationOpeners[i].gameObject.SetActive(true);
            }
        }
    }

    #endregion

    #region --AREASAVER--
    public void SetArea(string name)
    {
        switch (name)
        {
            case "WasherOpener":
                PlayerPrefs.SetInt(nameof(washerAreaValue), 1);
                break;
            case "GasOpener":
                PlayerPrefs.SetInt(nameof(gasAreaValue), 1);
                break;
            default:
                break;
        }
    }

    public void GetArea()
    {
        for (int i = 0; i < areaOpeners.Length; i++)
        {
            if (areaOpeners[i].areaValue == 1)
            {
                areaOpeners[i].objects[0].SetActive(true);
                areaOpeners[i].objects[1].SetActive(true);
                areaOpeners[i].gameObject.SetActive(false);
            }
            else
            {
                areaOpeners[i].gameObject.SetActive(true);
            }
        }
    }

    public void CheckArea()
    {
        areaOpeners[0].areaValue = PlayerPrefs.GetInt(nameof(washerAreaValue));
        areaOpeners[1].areaValue = PlayerPrefs.GetInt(nameof(gasAreaValue));
    }

    #endregion

    #region

    public void SetHandValue(int value)
    {
        PlayerPrefs.SetInt(nameof(handValue), 1);
    }
    public void GetHandValue()
    {
        if (handImage[0].handValue == 1)
        {
            handImage[0].gameObject.SetActive(false);
            handImage[1].gameObject.SetActive(false);
        }
    }
    public void CheckHand()
    {
        handImage[0].handValue = PlayerPrefs.GetInt(nameof(handValue));
        handImage[1].handValue = PlayerPrefs.GetInt(nameof(handValue));
    }
    #endregion
}