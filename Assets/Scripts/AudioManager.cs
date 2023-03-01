using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource levelMusic;

    public AudioSource gameOverMusic;

    public AudioSource winMusic;

    public AudioSource[] sfx;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        playLevelMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playLevelMusic() {
        levelMusic.Play();
        winMusic.Stop();
        gameOverMusic.Stop();
    }

    public void playGameOver() {
        levelMusic.Stop();
        gameOverMusic.Play();
    }

    public void playWinMusic() {
        levelMusic.Stop();
        winMusic.Play();
    }

    public void playSFX(int sfxToPlay) {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }
}
