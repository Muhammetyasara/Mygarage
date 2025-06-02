using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    private Animator animator;

    public bool isCarrying;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        ChangeBlendTree();
    }
    public void BlendTree(float value)
    {
        animator.SetFloat("Blend", value);
    }

    public void ChangeBlendTree()
    {
        animator.SetBool("isCarrying", isCarrying);
    }
}
