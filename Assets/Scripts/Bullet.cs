using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private Caliber _caliber;
    [SerializeField] 
    private MeshRenderer _meshRenderer;
    [SerializeField] 
    private TrailRenderer _trailRenderer;
    
    private Rigidbody _rigidbody;
    private bool hit = false; 
    
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
        Contact(other);
    }

    private void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Ray bulletRay = new Ray(transform.position, fwd);
        RaycastHit bulletHit;
        if (Physics.Raycast(bulletRay, out bulletHit, _rigidbody.velocity.magnitude * Time.fixedDeltaTime)) { 
            Contact(bulletHit.collider);
        }
    }

    public void Contact(Collider other)
    {
        if (hit || other.CompareTag("Player"))
        {
            return;
        }

        hit = true;

        EnemyPart enemyPart = other.GetComponent<EnemyPart>();
        if (enemyPart)
        {
            enemyPart.HitPart(this);
        }
        
        if (_meshRenderer)
        {
            _meshRenderer.enabled = false;
        }

        if (_trailRenderer)
        {
            _trailRenderer.enabled = false;
        }
        
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 0.5f);
    }

    public float GetForce()
    {
        return _caliber.Force;
    }
    
}
