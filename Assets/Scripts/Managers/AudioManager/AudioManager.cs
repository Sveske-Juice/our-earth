using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // PLease dont see code belowww :DD (super guud code)

    private static AudioManager s_Instance;
    public Sound[] sounds;

    public Sound[] musicSounds;

    [SerializeField]
    private AudioSource m_AudioSource;

    [SerializeField]
    private AudioSource m_MusicSource;


    [SerializeField]
    private AudioMixerGroup m_SoundMixerGroup;

    [SerializeField]
    private AudioMixerGroup m_MusicMixerGroup;

    public static AudioManager Instance => s_Instance;

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

        s.source.clip = s.clip;
        s.source.pitch = s.pitch;
        s.source.Play();
    }

    public void SetMusicSound(Sound music)
    {
        if (music.soundType != SoundType.Music)
            return;

        music.source = m_MusicSource;
        music.source.clip = music.clip;
        music.source.outputAudioMixerGroup = m_MusicMixerGroup;
        music.source.loop = true;
    }
}
