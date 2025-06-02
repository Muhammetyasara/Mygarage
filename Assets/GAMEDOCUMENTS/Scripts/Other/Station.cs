using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public Enums enums;
    public bool isOccupied;
    public int stationValue;
    public ParticleSystem smokeEffect;


    public bool isDone;
    public Transform textTransform;
    public GameObject[] textSprites;
    GameObject createdSprite;
    Vector2 spriteScale;
    float dissapearTime = .4f;
    float fadeOutSpeed = 15f;
    float moveYSpeed = 2f;

    void LateUpdate()
    {
        if (isDone)
        {
            createdSprite.transform.localPosition += new Vector3(0f, moveYSpeed * Time.deltaTime, 0f);

            dissapearTime -= Time.deltaTime;

            if (dissapearTime <= 0f)
            {
                spriteScale.x -= fadeOutSpeed * Time.deltaTime;
                spriteScale.y -= fadeOutSpeed * Time.deltaTime;
                createdSprite.transform.localScale = spriteScale;
                if (spriteScale.x <= 0f)
                {
                    createdSprite = null;
                    Destroy(textTransform.GetChild(0).gameObject);
                    isDone = false;
                    dissapearTime = .1f;
                    fadeOutSpeed = 15f;
                    moveYSpeed = 2f;
                    spriteScale = Vector2.zero;
                }
            }
        }
    }
    public void TextPopUp()
    {
        int r = Random.Range(0, 4);
        createdSprite = Instantiate(textSprites[r], textTransform.position, textTransform.rotation, textTransform);
        spriteScale = createdSprite.transform.localScale;
        isDone = true;
    }
}
