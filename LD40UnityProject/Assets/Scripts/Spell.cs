using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newSpell",menuName = "Spell")]
public class Spell : ScriptableObject
{
    public Sprite sprite;
    public GameObject explosion;
    public string Name;
    public float Damage;
    public float Speed;
    public float MentalCost;
}




