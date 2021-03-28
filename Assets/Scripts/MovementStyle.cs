using UnityEngine;

[CreateAssetMenu(fileName = "MoveStyle")]
public class MovementStyle : ScriptableObject
{
    [Tooltip("Максимальная скорость передвижения")]
    [SerializeField] 
    private float moveSpeed;

    [Tooltip("Скорость поворота")]
    [SerializeField] 
    private float angularSpeed;

    [Tooltip("Ускорение")]
    [SerializeField] 
    private float acceleration;

    public float MoveSpeed => moveSpeed;

    public float AngularSpeed => angularSpeed;

    public float Acceleration => acceleration;
}
