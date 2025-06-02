using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandImage : MonoBehaviour
{
    public float speed;
    public int handValue;
    Vector3 newPos;
    private void Start()
    {
        newPos = transform.position;
    }
    private void Update()
    {
        newPos.y = Mathf.PingPong(Time.time * speed, 3);
        transform.position = newPos;
    }
}