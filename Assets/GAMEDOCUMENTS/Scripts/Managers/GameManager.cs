using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SaveManager saveManager;

    public GameObject moneyObj;
    public ParticleSystem moneyBlastEffect;

    public List<GameObject> moneyList = new List<GameObject>();

    public HandImage[] handImage;
    public bool isNewbie = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        saveManager.CheckStations();
        saveManager.CheckArea();
        saveManager.CheckHand();
        saveManager.GetStation();
        saveManager.GetArea();
        saveManager.GetHandValue();
    }

    public void CollectMoney(Vector3 position, GameObject gameObject)
    {
        Vector3 worldToScreen = Camera.main.WorldToScreenPoint(position);
        GameObject flyingMoney = Instantiate(UIManager.Instance.moneyIconPrefab, worldToScreen, Quaternion.identity,
                                             UIManager.Instance.moneyIcon.transform);
        Destroy(gameObject);
        ParticleSystem createdEffect = Instantiate(moneyBlastEffect, position, moneyBlastEffect.transform.rotation);
        Destroy(createdEffect.gameObject, 5f);
        flyingMoney.AddComponent<MoneyIcon>().FlyMoney(2f, 1f);
    }

    public void CreateMoney(Transform parent, Vector3 pos)
    {
        GameObject createdMoney = Instantiate(moneyObj, pos, moneyObj.transform.rotation, parent);
        createdMoney.transform.DOLocalJump(Vector3.zero, 2, 1, .5f);
        parent.GetComponent<MoneyTile>().moneyObj = createdMoney;
        createdMoney.AddComponent<MoneyIcon>();
    }

    public void NewbieStart()
    {
        if (isNewbie && handImage[0].handValue != 1)
        {
            handImage[0].gameObject.SetActive(false);
            handImage[1].gameObject.SetActive(true);
            SaveManager.Instance.SetHandValue(1);
            Debug.Log("!!!!!!!!" + isNewbie);
        }
        else
        {
            handImage[0].gameObject.SetActive(false);
            handImage[1].gameObject.SetActive(false);
            Debug.Log("!!!!!!!!" + isNewbie);
        }
    }
}