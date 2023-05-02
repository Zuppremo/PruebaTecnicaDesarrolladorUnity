using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject uiCanvas = default;
    [SerializeField] private GameObject winScreen = default;
    [SerializeField] private GameObject loseScreen = default;
    [SerializeField] private GameObject startScreen = default;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    public void ToggleUI(bool value)
    {
        uiCanvas.SetActive(value);
    }

    public void ToggleWinScreen(bool value)
    {
        winScreen.SetActive(value);
    }

    public void ToggleLoseScreen(bool value)
    {
        loseScreen.SetActive(value);
    }
    public void ToggleStartScreen(bool value)
    {
        startScreen.SetActive(value);
    }

    private void Update()
    {
        SetUIScreenInState();
    }

    private void SetUIScreenInState()
    {
        switch (gameManager.CurrentGameState)
        {
            case GameState.Start:
                ToggleUI(true);
                ToggleStartScreen(true);
                break;
            case GameState.Playing:
                ToggleStartScreen(false);
                ToggleUI(false);
                break;
            case GameState.Won:
                ToggleUI(true);
                ToggleWinScreen(true);
                break;
            case GameState.Lost:
                ToggleUI(true);
                ToggleLoseScreen(true);
                break;
            default:
                break;
        }
    }
}
