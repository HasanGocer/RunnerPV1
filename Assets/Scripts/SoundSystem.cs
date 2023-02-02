using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoSingleton<SoundSystem>
{
    [SerializeField] private AudioSource mainSource;
    [SerializeField] private AudioClip mainMusic, sise, coin, glassBreak;

    public void MainMusicPlay()
    {
        mainSource.clip = mainMusic;
        mainSource.Play();
        mainSource.volume = 70;
    }

    public void MainMusicStop()
    {
        mainSource.volume = 0;
    }

    public void CallSise()
    {
        mainSource.PlayOneShot(sise);
    }
    public void CallCoin()
    {
        mainSource.PlayOneShot(coin);
    }
    public void CallGlassBreak()
    {
        mainSource.PlayOneShot(glassBreak);
    }
}
