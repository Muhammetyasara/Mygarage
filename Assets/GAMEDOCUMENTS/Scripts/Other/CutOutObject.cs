using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;
    
    [SerializeField]
    private Material[] targetMaterials;

    [SerializeField]
    private LayerMask wallMask;

    private void Update()
    {
        Vector2 cutoutPos = new Vector2(.5f, .53f);
        
        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 100123;
        Debug.DrawRay(transform.position, forward, Color.green);
        if (hitObjects.Length == 0)
        {
            for (int i = 0; i < targetMaterials.Length; i++)
            {
                if (targetMaterials[i] != null)
                {
                    targetMaterials[i].SetVector("_CutoutPos", new Vector2(-5, -5));
                    targetMaterials[i].SetFloat("_CutoutSize", 0.15f);
                    targetMaterials[i].SetFloat("_FalloffSize", 0.03f);
                }
            }
        }

        for (int i = 0; i < hitObjects.Length; ++i)
        {
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for (int m = 0; m < materials.Length; ++m)
            {
                targetMaterials[m] = materials[m];
                materials[m].SetVector("_CutoutPos", cutoutPos);
                materials[m].SetFloat("_CutoutSize", 0.15f);
                materials[m].SetFloat("_FalloffSize", 0.03f);
            }
        }
    }
}