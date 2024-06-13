using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    
    public GameState CurrentGameState { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }





    public void Chest()
    {
        CurrentGameState = GameState.Chest;
        Time.timeScale = 0;

    }

    public void Dialog()
    {
        CurrentGameState = GameState.Dialog;
        Time.timeScale = 0;
    }

    public void Menu()
    {
        CurrentGameState = GameState.Menu;
        Time.timeScale = 0;
    }

    public void Playing()
    {
        CurrentGameState = GameState.Playing;
        Time.timeScale = 1;
    }

    public void Battle() 
    {
        CurrentGameState = GameState.Battle;
        Time.timeScale = 1;
    }

    public void Loading()
    {
        CurrentGameState = GameState.Loading;
        Time.timeScale = 1;
    }
}


public enum GameState
{
    Chest,
    Dialog,
    Menu,
    Playing,
    Battle,
    Loading
}
