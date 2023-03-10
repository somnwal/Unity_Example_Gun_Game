using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToLoad = 4f;

    public string nextLevel;

    public bool isPaused = false;

    public int currentCoins;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        Time.timeScale = 1f;

        UIController.instance.coinText.text = currentCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            pauseUnpause();
        }
    }

    public IEnumerator levelEnd() {
        AudioManager.instance.playWinMusic();

        PlayerController.instance.canMove = false;

        UIController.instance.startFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        SceneManager.LoadScene(nextLevel);


    }

    public void pauseUnpause() {
        if(!isPaused) {
            UIController.instance.pauseMenu.SetActive(true);
            isPaused = true;

            Time.timeScale = 0f;
        } else {
            UIController.instance.pauseMenu.SetActive(false);
            isPaused = false;

            Time.timeScale = 1f;
        }
    }

    public void getCoins(int amount) {
        currentCoins += amount;

        UIController.instance.coinText.text = currentCoins.ToString();
    }

    public void spendCoins(int amount) {
        currentCoins -= amount;

        if(currentCoins < 0) {
            currentCoins = 0;
        }

        UIController.instance.coinText.text = currentCoins.ToString();
    }
}
