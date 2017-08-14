using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Creatures/Enemy")]
public class Creature : ScriptableObject 
{
    new public string name = "Unknown Enemy";
    public bool ranged = false;
    public int damage = 5;
    public float attackRange = 1;
    public float hp = 150;
}
