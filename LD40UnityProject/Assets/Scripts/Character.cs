using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCharacter", menuName = "Character")]
public class Character : ScriptableObject
{
    public Sprite sprite;
    public string Name;
    public float HealthPoint;
    public float ArmorPoint;
    public float AttackPoint;
    public float Speed;
}