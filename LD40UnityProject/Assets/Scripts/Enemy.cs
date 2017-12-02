using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newEnemy",menuName="Enemy")]
public class Enemy : ScriptableObject
{
    public Sprite sprite;
    public string Name;
    public float HealthPoint;
    public float ArmorPoint;
    public float AttackPoint;
    public float Speed;
}
