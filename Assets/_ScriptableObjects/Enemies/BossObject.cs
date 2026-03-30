using UnityEngine;

[CreateAssetMenu(fileName = "BossObject", menuName = "Scriptable Objects/BossObject")]
public class BossObject : ScriptableObject
{
    public int MaxHealth;
    public int Damage;

    public float AttackInterval;
    public float AttackDistance;

    public float StartSpeed;
    public float MidSpeed;
    public float FinalSpeed;
}
