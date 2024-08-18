using UnityEngine;


[CreateAssetMenu(fileName = "New weapon", menuName = "Weapons", order = 51)]
public class WeaponData : ScriptableObject
{
    public int damage;
    public float AttackTime;
}
