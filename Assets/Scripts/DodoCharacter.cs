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

    // Dodo score panel
    public GameObject dodoScorePanel;

    // Dodo lives
    private int _lives = 0;
    public int lives
    {
        get {
            return _lives;
        }
    }

    public void SetLives(int lives)
    {
        _lives = lives;
    }

    public void AddLives(int amount)
    {
        _lives += amount;
    }

    // Player score
    private int _score = 0;
    public int score
    {
        get {
            return _score;
        }
    }

    public void SetScore(int score)
    {
        _score = score;
    }

    public void AddScore(int amount)
    {
        _score += amount;
    }

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

    // Player input
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
