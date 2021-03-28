using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private Caliber _caliber;

    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    public void FireTo(Vector3 point)
    {
        transform.LookAt(point);
        _rigidbody.velocity = transform.forward * _caliber.Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }
        Contact();
    }

    public void Contact()
    {
        Destroy(gameObject);
    }

    public float GetForce()
    {
        return _caliber.Force;
    }
    
}
