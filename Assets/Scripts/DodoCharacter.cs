using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DodoCharacter", menuName = "ScriptableObjects/DodoCharacter", order = 5)]
public class DodoCharacter : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    // Dodo name
    public string dodoName = "";

    // Dodo default sprite
    public Sprite dodoSprite;

    // Dodo object
    public GameObject dodoObject;

}
