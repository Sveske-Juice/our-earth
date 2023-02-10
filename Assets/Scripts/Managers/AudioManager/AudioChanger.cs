using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioChanger : MonoBehaviour
{
    private static AudioChanger s_Instance;

    [SerializeField]
    private AudioMixer m_AudioMixer;


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

    private void Start()
    {
        UpdateSettings();
    }

    private void OnEnable()
    {
        SettingsManager.OnSettingsChanged += UpdateSettings;
    }
    private void OnDisable()
    {
        SettingsManager.OnSettingsChanged -= UpdateSettings;
    }

    /// <summary> Will update the local settings when settings have been changed ie. setting correct volume etc. </summary>
    private void UpdateSettings()
    {
        SetMusicVolume(SettingsManager.Instance.MusicVolume);
        SetSoundVolume(SettingsManager.Instance.SoundVolume);
    }

    public void SetMusicVolume(float value)
    {
        SetVolume("MusicVolume", value);
    }

    public void SetSoundVolume(float value)
    {
        SetVolume("SoundVolume", value);
    }

    private void SetVolume(string exposedGroup, float value)
    {
        m_AudioMixer.SetFloat(exposedGroup, Mathf.Log10(value) * 20);
    }
}
