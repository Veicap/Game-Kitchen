using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    private const string MUSIC_OPTION_SAVE = "music";
    public static MusicManager Instance { get; private set; }
    
    private float volume = .5f;
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();  
        volume = PlayerPrefs.GetFloat(MUSIC_OPTION_SAVE, 1f);
        audioSource.volume = volume;
        
    }
    public void ChangeMusic()
    {
        volume += .1f;
        if (volume > 1f) volume = 0f;
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_OPTION_SAVE, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return volume;
    }
}
