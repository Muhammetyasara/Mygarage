using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGrid : MonoBehaviour
{
    public MoneyTile moneyTile;

    public float xPos;
    public float yPos;
    public float zPos;

    private void Start()
    {
        for (float i = -4.5f; i < yPos; i++)
        {
            for (int j = 0; j < zPos; j++)
            {
                for (int k = 0; k < xPos; k++)
                {
                    MoneyTile tile = Instantiate(moneyTile, transform.position, Quaternion.identity, transform);
                    tile.transform.localPosition = new Vector3(k, i, j);
                    k++;
                }
                j++;
            }
        }
    }

    Transform availableTile;
    public Transform GetAvailableChild()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out MoneyTile moneyTile))
            {
                if (!moneyTile.isOccupied)
                {
                    availableTile = transform.GetChild(i);
                    break;
                }
            }
        }
        return availableTile;
    }

    public void MoneyCreateSettings()
    {
        GameManager.Instance.CreateMoney(GetAvailableChild(), transform.position);
    }
}
