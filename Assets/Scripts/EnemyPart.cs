using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] 
public class EnemyPart : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void HitPart(Bullet bullet)
    {
        _rigidbody.AddForce(bullet.transform.forward * bullet.GetForce());
        gameObject.GetComponentInParent<Enemy>().Kill(_rigidbody);
    }
}
