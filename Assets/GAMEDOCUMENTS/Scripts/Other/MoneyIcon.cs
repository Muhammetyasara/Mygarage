using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyIcon : MonoBehaviour
{
    private bool letFlyMoney;
    private float timerFlyMoney;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 startScale;
    private Vector3 endScale;
    private float speed;

    private void Update()
    {
        FlyingMoney();
    }

    public void FlyMoney(float flySpeed, float scale)
    {
        letFlyMoney = true;
        timerFlyMoney = 0f;
        speed = flySpeed;
        startPos = transform.position;
        startScale = transform.localScale;
        endScale = startScale * scale;
        endPos = UIManager.Instance.moneyIcon.transform.position;
    }

    private void FlyingMoney()
    {
        if (letFlyMoney)
        {
            timerFlyMoney += Time.deltaTime * speed;

            transform.position = Vector3.Lerp(startPos, endPos, timerFlyMoney);
            transform.localScale = Vector3.Lerp(startScale, endScale, timerFlyMoney);

            if (timerFlyMoney >= 1f)
            {
                UIManager.Instance.IncreaseMoney(10);
                letFlyMoney = false;
                gameObject.SetActive(false);
            }
        }
    }
}