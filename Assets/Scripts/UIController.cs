﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider hpSlider;
    
    public Text hpText;

    public Text coinText;

    public GameObject deathScreen;

    public Image fadeScreen;

    public float fadeSpeed;

    private bool fadeToBlack, fadeOutBlack;

    public string newGameScene, mainMenuScene;

    public GameObject pauseMenu;

    public GameObject minimap;

    public Image currentGun;

    public Text currentGunText;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeOutBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if(fadeScreen.color.a == 0f) {
                fadeOutBlack = false;
            }
        }

        if(fadeToBlack) {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if(fadeScreen.color.a == 0f) {
                fadeToBlack = false;
            }
        }
    }

    public void startFadeToBlack() {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void newGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGameScene);
    }

    public void ReturnToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void resume() {
        
        LevelManager.instance.pauseUnpause();
    }
}
