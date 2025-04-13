using UnityEngine;
//using static UnityEditor.Rendering.ShadowCascadeGUI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public static int orphansSaved;

    [SerializeField] private List<AudioSource> sfxSources;
    [SerializeField] private AudioClip newsNoise;
    [SerializeField] AudioSource music;
    [SerializeField] AudioResource mainmenumusic;
    [SerializeField] AudioResource gameplaymusic;
    private int sfxIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            orphansSaved = 0;
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }
    void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        if (s.name == "MainMenu")
        {
            if (music.resource != mainmenumusic)
            {
                music.resource = mainmenumusic;
                music.Play();
            }

        }
        else
        {
            if (music.resource != gameplaymusic)
            {
                music.resource = gameplaymusic;
                music.Play();
            }
        }
    }
    public void PlayAudioClip(AudioClip clip)
    {
        sfxIndex++;
        sfxIndex = sfxIndex % sfxSources.Count;
        sfxSources[sfxIndex].clip = clip;
        sfxSources[sfxIndex].Play();
    }
}
