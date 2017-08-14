using UnityEngine;

[CreateAssetMenu(fileName = "Unknown Enemy", menuName = "Creatures/Enemy")]
public class Enemy : ScriptableObject 
{
    new public string name = "Unknown Enemy";
    public bool ranged = false;
    public int damage = 5;
    public float attackRange = 1;
    public float hp = 150;
}
