using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Player", order = 51)]
public class PlayerData : ScriptableObject
{
    public int MaxHealth;
    public int Armor;
    public float CooldownTime;
    public float DamageMultiplier;
}