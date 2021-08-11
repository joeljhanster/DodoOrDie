using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    // Player selected
    private bool _taken = false;

    public bool taken
    {
        get {
            return _taken;
        }
    }

    public void SetTaken(bool taken)
    {
        _taken = taken;
    }

    private PlayerInput _input;

    public PlayerInput Input
    {
        get {
             return _input;
        }
    }

    public void SetInput(PlayerInput input)
    {
        _input = input;
    }
}
