using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private Caliber _caliber;
    [SerializeField] 
    private MeshRenderer _meshRenderer;
    
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

        EnemyPart enemyPart = other.GetComponent<EnemyPart>();
        if (enemyPart)
        {
            enemyPart.HitPart(this);
        }
        
        Contact();
    }

    public void Contact()
    {
        if (_meshRenderer)
        {
            _meshRenderer.enabled = false;
        }
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 0.5f);
    }

    public float GetForce()
    {
        return _caliber.Force;
    }
    
}
