using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaOpener : MonoBehaviour
{
    public Image outLineImg;
    public int price;
    public bool isPlayerCollide;

    public GameObject[] objects;

    public GameObject emot;
    public ParticleSystem thumbEffect;
    public ParticleSystem confettiEffect;

    public int areaValue;

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
                SaveManager.Instance.SetArea(name);
                UIManager.Instance.DecreaseMoney(price);
                objects[0].SetActive(true);
                objects[1].SetActive(true);
                EffectsShowUp();
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

    public void EffectsShowUp()
    {
        confettiEffect.Play();
        thumbEffect.Play();
        emot.SetActive(true);
    }
}