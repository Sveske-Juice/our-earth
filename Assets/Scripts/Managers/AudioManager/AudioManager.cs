using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager s_Instance;
    public Sound[] sounds;

    [SerializeField]
    private AudioSource m_AudioSource;


    [SerializeField]
    private AudioMixerGroup m_SoundMixerGroup;

    [SerializeField]
    private AudioMixerGroup m_MusicMixerGroup;

    private void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
        {
            Destroy(this);
            return;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
    
        s.source = m_AudioSource;

        // Decide mixer group to use
        switch (s.soundType)
        {
            case SoundType.Sound:
                s.source.outputAudioMixerGroup = m_SoundMixerGroup;
                break;

            case SoundType.Music:
                s.source.outputAudioMixerGroup = m_MusicMixerGroup;
                break;
            
            default:
                s.source.outputAudioMixerGroup = m_SoundMixerGroup;
                break;
        }

        Debug.Log($"Playing sound with vol: {s.source.volume}");
        s.source.clip = s.clip;
        s.source.pitch = s.pitch;
        s.source.Play();
    }
}
