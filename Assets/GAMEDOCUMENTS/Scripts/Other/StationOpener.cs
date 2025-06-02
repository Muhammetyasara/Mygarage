using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationOpener : MonoBehaviour
{
    public Station station;
    public Image outLineImg;

    public int price;
    public bool isPlayerCollide;

    private void Update()
    {
        if (!isPlayerCollide && outLineImg.fillAmount != 1)
        {
            outLineImg.fillAmount += Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6 && UIManager.Instance.moneyValue >= price)
        {
            isPlayerCollide = true;
            outLineImg.fillAmount -= Time.deltaTime / 3;

            if (outLineImg.fillAmount == 0)
            {
                station.gameObject.SetActive(true);
                station.smokeEffect.Play();
                station.stationValue = 1;
                SaveManager.Instance.SetStation(station.name);
                UIManager.Instance.DecreaseMoney(price);
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            isPlayerCollide = false;
        }
    }
}