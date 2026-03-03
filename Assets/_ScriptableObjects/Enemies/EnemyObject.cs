using UnityEngine;

[CreateAssetMenu(fileName = "EnemyObject", menuName = "Scriptable Objects/EnemyObject")]
public class EnemyObject : ScriptableObject
{
    public int MaxHealth;
    public int Damage;
    public float Speed;
    public float SpottingRadius;
}
