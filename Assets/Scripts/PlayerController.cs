using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform firePoint;
    [SerializeField] 
    private Weapon _weapon;
    [SerializeField] 
    private MovementStyle _moveStyle;

    private float timeAfterLastShoot;
    private bool onFirePosition = false;
    private Camera currentCamera;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        ChangeMovestyle(_moveStyle);
        currentCamera = Camera.main;
        timeAfterLastShoot = _weapon.ShootCooldown;
    }

    void Update()
    {
        CheckClick();
        UpdateAnimation();
    }

    private void CheckClick()
    {
        timeAfterLastShoot += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                if (onFirePosition)
                {
                    ShootAt(hit.point);
                }
                else
                {
                    _navMeshAgent.SetDestination(hit.point);
                }
            }
        } 
    }

    private void ShootAt(Vector3 target)
    {
        if (timeAfterLastShoot < _weapon.ShootCooldown)
        {
            return;
        }
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
        Bullet newBullet = Instantiate(_weapon.BulletPref, firePoint.position, firePoint.rotation);
        newBullet.FireTo(target);
        timeAfterLastShoot = 0;
    }

    private void UpdateAnimation()
    {
        _animator.SetFloat("Forward", _navMeshAgent.desiredVelocity.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        FirePit firePit = other.GetComponent<FirePit>();
        if (firePit && CanHitEnemy())
        {
            onFirePosition = true;
            _animator.SetBool("Firing", true);
        }
    }

    private bool CanHitEnemy()
    {
        List<EnemyPart> enemies = new List<EnemyPart>(FindObjectsOfType<EnemyPart>());

        foreach (var enemy in enemies)
        {
            //Проверяем что часть противника на экране
            Vector3 enemyScreenPoint = currentCamera.WorldToViewportPoint(enemy.transform.position);
            bool onScreen = enemyScreenPoint.z > 0 && enemyScreenPoint.x > 0 && enemyScreenPoint.x < 1 && enemyScreenPoint.y > 0 && enemyScreenPoint.y < 1;
            if (onScreen)
            {
                //Проверяем что часть противника ничего не загораживает
                Ray cameraRay = currentCamera.ViewportPointToRay(enemyScreenPoint);
                RaycastHit hitFormCamera;
                
                //Проверяем что можем попасть из текущей позиции
                Ray ray = new Ray(firePoint.position, (enemy.transform.position - firePoint.position).normalized);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && Physics.Raycast(cameraRay, out hitFormCamera))
                {
                    if (hit.collider.Equals(hitFormCamera.collider) && hit.collider.CompareTag("Enemy"))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        _weapon = newWeapon;
    }

    public void ChangeMovestyle(MovementStyle newMovementStyle)
    {
        _moveStyle = newMovementStyle;
        _navMeshAgent.acceleration = _moveStyle.Acceleration;
        _navMeshAgent.speed = _moveStyle.MoveSpeed;
        _navMeshAgent.angularSpeed = _moveStyle.AngularSpeed;
    }

}
