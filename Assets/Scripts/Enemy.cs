using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator _animator;
    private List<Rigidbody> ragdollParts;
    private void Start()
    {
        ragdollParts = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        _animator = GetComponent<Animator>();
    }

    public void Kill(Rigidbody onPart)
    {
        _animator.enabled = false;
        _animator.avatar = null;
        
        foreach (var part in ragdollParts)
        {
            if (part != onPart)
            {
                part.velocity = Vector3.zero;
            }
        }
    }
}
