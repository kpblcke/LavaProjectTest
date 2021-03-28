using UnityEngine;

[CreateAssetMenu(fileName = "Caliber", order = 0)]
public class Caliber : ScriptableObject
{
    [SerializeField]
    [Tooltip("Сила удара, от попадания пули")]
    private float force;
    
    [SerializeField]
    [Tooltip("Скорость полёта")]
    private float speed = 10f;

    public float Force => force;

    public float Speed => speed;
}
