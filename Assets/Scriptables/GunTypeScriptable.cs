using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GunTypeScriptable",
    order = 2)]
public class GunTypeScriptable : ScriptableObject
{
    public int damage;
    public float cooldown;
}
