using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies", order = 51)]
public class EnemyData : ScriptableObject
{
    [Header("View:")]
    public Sprite SpriteImage;
    public RuntimeAnimatorController Animations;

    [Header("Stats:")]
    public int MaxHealth;
    public int Damage;
    public float AttackTime;
    public float CooldownTime;

    [Header("Spawn:")]
    [Range(0, 1)] public float SpawnChance;
}
