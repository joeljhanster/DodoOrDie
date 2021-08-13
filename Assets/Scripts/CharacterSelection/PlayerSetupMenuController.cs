using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerSetupMenuController : MonoBehaviour
{
    private PlayerInput input;

    // Testing
    private SelectionControls controls;

    // private float moveLeft;
    // private float moveRight;
    // private float selectReady;
    // private float moveBack;

    public PlayerManager playerManager;

    public Image dodoImage;
    public TextMeshProUGUI dodoName;

    public List<DodoCharacter> dodoCharacters;

    private int currIndex = 0;

    [SerializeField]
    private GameObject readyPanel;
    [SerializeField]
    private GameObject menuPanel;

    private float ignoreInputTime = 0.0f;
    private float ignoreInputInterval = 0.3f;

    private bool selected = false;

    public void SetPlayerInput(PlayerInput pi)
    {
        input = pi;
        input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.Selection.Prev.name && Time.time > ignoreInputTime && !selected)
        {
            float moveLeft = obj.ReadValue<float>();
            Debug.Log(moveLeft);

            if (moveLeft >= 0.9f) {
                // Prev
                if (currIndex == 0) {
                    currIndex = dodoCharacters.Count - 1;
                } else {
                    currIndex -= 1;
                }

                dodoImage.sprite = dodoCharacters[currIndex].dodoSprite;
                dodoName.text = dodoCharacters[currIndex].dodoName;
                ignoreInputTime = Time.time + ignoreInputInterval;
            }
        }
        else if (obj.action.name == controls.Selection.Next.name && Time.time > ignoreInputTime && !selected)
        {
            float moveRight = obj.ReadValue<float>();
            Debug.Log(moveRight);

            if (moveRight >= 0.9f) {
                // Next
                if (currIndex == dodoCharacters.Count - 1) {
                    currIndex = 0;
                } else {
                    currIndex += 1;
                }

                dodoImage.sprite = dodoCharacters[currIndex].dodoSprite;
                dodoName.text = dodoCharacters[currIndex].dodoName;
                ignoreInputTime = Time.time + ignoreInputInterval;
            }
        }
        else if (obj.action.name == controls.Selection.Ready.name && !dodoCharacters[currIndex].taken)
        {
            // Ready
            readyPanel.SetActive(true);
            selected = true;
            dodoCharacters[currIndex].SetTaken(true);
            dodoCharacters[currIndex].SetInput(input);
        }
        else if (obj.action.name == controls.Selection.Back.name && selected)
        {
            // Back
            readyPanel.SetActive(false);
            selected = false;
            dodoCharacters[currIndex].SetTaken(false);
            dodoCharacters[currIndex].SetInput(null);
        }
    }

    void Awake()
    {
        controls = new SelectionControls();
    }
    void Start()
    {
        dodoImage.sprite = dodoCharacters[currIndex].dodoSprite;
        dodoName.text = dodoCharacters[currIndex].dodoName;
    }

    // Update is called once per frame
    void Update()
    {
        if (dodoCharacters[currIndex].taken && !selected) {
            dodoImage.color = Color.black;
        } else {
            dodoImage.color = Color.white;
        }
    }
}
