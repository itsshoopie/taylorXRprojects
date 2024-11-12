using DefaultCompany.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{

    [Header("UI Options")]
    [SerializeField]
    private float offsetPositionFromPlayer = 1.0f; //How far to place from XR Camera

    [SerializeField]
    private GameObject menuContainer; //Canvas with a couple of buttons

    private const string GAME_SCENE_NAME = "Game"; //Allows to restart the game

    [Header("Events")]
    public Action onGameResumedAction; //Notifies other classes like GameManager to update class

    private Menu menu;

    public void Awake()
    {
        //Bind menu buttons
        menu = menuContainer.GetComponentInChildren<Menu>(true); //Adding "true" in case any part of the menu is hidden

        menu.ResumeButton.onClick.AddListener(() =>
        {
            HandleMenuOptions(GameState.Playing);
            onGameResumedAction?.Invoke();
        });
        menu.RestartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(GAME_SCENE_NAME);
            onGameResumedAction?.Invoke();
        });

    }

    private void OnEnable() //Add it on enable
    {
        
    }
    private void OnDisable()
    {
        
    }

    private void HandleMenuOptions(GameState gameState) //This has to match enum values
    {
        if (gameState == GameState.Paused)
        {
            menuContainer.SetActive(true);
            //PlaceMenuInFront();

        }

        else if (gameState == GameState.ChallengeSolved)
        {
            menuContainer.SetActive(true);
            menu.ResumeButton.gameObject.SetActive(false);
            menu.SolvedText.gameObject.SetActive(true);
            //PlaceMenuInFront():
        }

        else
        {
            menuContainer.SetActive(false);//We dont want the menu to apper while playing
        }
    }

    private void PlaceMenuInFront()
    {
        //Place the UI in front of player
        var playerHead = Camera.main.transform;
        menuContainer.transform.position = playerHead.position + (playerHead.forward * offsetPositionFromPlayer);
        menuContainer.transform.rotation = playerHead.rotation;
    }

}