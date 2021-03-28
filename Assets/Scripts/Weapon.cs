using UnityEngine;

[CreateAssetMenu(fileName = "Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField]
    [Tooltip("Время между выстрелами")]
    private float shootCooldown = 0.1f;

    [SerializeField]
    [Tooltip("Пули, которыми стреляет оружие")]
    private Bullet bulletPref;
    
    public float ShootCooldown => shootCooldown;

    public Bullet BulletPref => bulletPref;
}
