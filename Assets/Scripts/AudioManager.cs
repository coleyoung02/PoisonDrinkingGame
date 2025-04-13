using UnityEngine;
using static UnityEditor.Rendering.ShadowCascadeGUI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public static int orphansSaved;

    [SerializeField] private List<AudioSource> sfxSources;
    [SerializeField] private AudioClip newsNoise;
    private int sfxIndex = 0;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
