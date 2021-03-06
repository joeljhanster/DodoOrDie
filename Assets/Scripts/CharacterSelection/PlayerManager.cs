using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // private int playerIndex = 0;

    public List<GameObject> selectionPanels;

    public List<DodoCharacter> dodoCharacters;
    
    public GameConstants gameConstants;

    public UnityEvent onSceneChange;

    // public InputActionAsset playerControlsActions;
    
    private List<PlayerConfiguration> playerConfigurations;
    
    [SerializeField]
    private int MaxPlayers = 4;

    private bool changeScene = false;

    private PlayerInputManager playerInputManager;

    public static PlayerManager Instance { get; private set;}

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("SINGLETON - Trying to create another instanceo of singleton!!");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigurations = new List<PlayerConfiguration>();
        }

        foreach(DodoCharacter dodo in dodoCharacters)
        {
            dodo.SetTaken(false);
            dodo.SetInput(null);
        }
    }

    void Start()
    {
        // Screen.SetResolution(Screen.width, Screen.width / 16 * 9);
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    public void ReadyPlayer(int index)
    {
        playerConfigurations[index].isReady = true;
        if (playerConfigurations.Count == MaxPlayers && playerConfigurations.All(p => p.isReady == true))
        {
            // Load next scene
            SceneManager.LoadScene("1_Cliff");
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player joined" + pi.playerIndex);

        // Enable selection panel


        pi.transform.SetParent(transform);

        if (!playerConfigurations.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            playerConfigurations.Add(new PlayerConfiguration(pi));
        }

        // GameObject optionsPanel = selectionPanels[pi.playerIndex].transform.Find("Options").gameObject;
        // if (optionsPanel != null)
        // {
        //     optionsPanel.SetActive(true);
        //     pi.uiInputModule = optionsPanel.GetComponentInChildren<InputSystemUIInputModule>();
        //     PlayerSetupMenuController menuController = optionsPanel.GetComponent<PlayerSetupMenuController>();
        //     menuController.SetPlayerInput(pi);
        // }
    }

    void Update()
    {
        if (!changeScene) {
            for (int i=0; i<playerInputManager.playerCount; i++) {
                GameObject optionsPanel = selectionPanels[i].transform.Find("Options").gameObject;
                if (optionsPanel && !optionsPanel.activeSelf)
                {
                    optionsPanel.SetActive(true);
                    PlayerInput pi = playerConfigurations[i].Input;
                    pi.uiInputModule = optionsPanel.GetComponentInChildren<InputSystemUIInputModule>();
                    PlayerSetupMenuController menuController = optionsPanel.GetComponent<PlayerSetupMenuController>();
                    menuController.SetPlayerInput(pi);
                }
            }

            int joinCount = playerInputManager.playerCount;
            int readyCount = 0;

            foreach(DodoCharacter dodo in dodoCharacters)
            {
                if (dodo.taken) {
                    readyCount += 1;
                    dodo.SetLives(gameConstants.startingLives);
                } else {
                    dodo.SetLives(0);
                }
                dodo.SetScore(0);
                // if (dodo.taken) {
                //     readyCount += 1;
                //     Debug.Log("Set active true");
                //     dodo.dodoScorePanel.SetActive(true);
                // } else {
                //     Debug.Log("Set active false");
                //     dodo.dodoScorePanel.SetActive(false);
                // }
            }

            if (readyCount == joinCount && joinCount >= 2) {
                // Load next scene
                onSceneChange.Invoke();
                changeScene = true;
                // Switch controls
                // for (int i=0; i<dodoCharacters.Count; i++)
                // {
                //     dodoCharacters[i].Input.enabled = false;
                //     dodoCharacters[i].Input.actions = playerControlsActions;
                //     dodoCharacters[i].Input.enabled = true;
                //     dodoCharacters[i].Input.SwitchCurrentActionMap("Gameplay");
                // }
            }
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool isReady { get; set; }
    public GameObject SelectionPanel { get; set; }
}