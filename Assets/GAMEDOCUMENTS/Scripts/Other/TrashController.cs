using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashController : MonoBehaviour
{
    public Image outLineImg;
    public bool isPlayerCollide;

    private void Update()
    {
        if (!isPlayerCollide && outLineImg.fillAmount != 1f)
        {
            outLineImg.fillAmount += Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6 && other.GetComponent<PlayerCarrying>().carryingObjectCount > 0)
        {
            isPlayerCollide = true;
            outLineImg.fillAmount -= Time.deltaTime / 0f;

            if (outLineImg.fillAmount == 0)
            {
                other.GetComponent<PlayerCarrying>().HideForTrash();
                outLineImg.fillAmount = 1f;
                return;
            }
        }
    }
}