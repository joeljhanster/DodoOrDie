using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    public int gameDuration = 90;

    // Used in PlayerController.cs
    public int startingLives = 10;

    public float rockEffect = 10.0f;

    public int immunityDuration = 3;

    // Thin Dodo
    public float maxSpeed = 5.0f;
    public float speed = 70.0f;

    // Naughty Dodo

    
    // Gastric Dodo


    // Chubby Dodo


    // Used in CliffScroller.cs
    public float cliffStartSpeed = 1.0f;
    public float mistStartSpeed = 1.0f;
    public float cliffMaxSpeed = 5.0f;
    public float mistMaxSpeed = 5.0f;

    // Used in CatchingEagleController.cs
    public float patrolTime = 0.5f;

    // Game start duration
    public float startDuration = 3.0f;

    // Used in FlyingEagleController.cs
    public float flyInterval = 7.0f;
    public float flyDuration = 1.0f;

    // Used in Break.cs
    public int numDebris = 5;

    // Used in ChangeSceneEV.cs

    public string menuScene = "Menu";
    public string selectScene = "DodoSelection";
    public string cliffScene = "1_Cliff";
    public string forestScene = "2_Forest";
    public string riverScene = "3_River";
    public string bridgeScene = "4_Bridge";
    public string beachScene = "5_Beach";
}
