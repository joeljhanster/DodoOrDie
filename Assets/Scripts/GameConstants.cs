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

    public int score = 100;

    // Mini game 1: Cliff
    public float maxSpeed = 5.0f;
    public float speed = 70.0f;

    // Mini game 2: Forest
    public float maxSpeedForest = 20.0f;
    public float speedForest = 30.0f;
    public float upForceForest = 20.0f;
    public float dieUpForceForest = 30.0f;

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
