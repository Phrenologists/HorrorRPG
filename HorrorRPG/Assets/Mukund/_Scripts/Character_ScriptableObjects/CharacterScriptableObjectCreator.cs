using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Stats", menuName = "Character Stats")]
public class CharacterScriptableObjectCreator : ScriptableObject
{
    public float maxHealth = 100f;
    public float maxSanity = 100f;
    public float maxHunger = 100f;
}
