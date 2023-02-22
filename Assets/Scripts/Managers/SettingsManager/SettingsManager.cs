using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager s_Instance;
    private SettingsData m_Settings;
    private string m_SavePath;

    public static SettingsManager Instance => s_Instance;
    public bool FlatEarthModel => m_Settings.FlatEarthModel;
    public float MusicVolume => m_Settings.MusicVolume;
    public float SoundVolume => m_Settings.SoundVolume;

    /// <summary> Event that gets raised when the settings have been changed and applied. Will also be called in the start of the game. </summary>
    public static event Action OnSettingsChanged;


    [Header("References")]
    [SerializeField]
    private Button m_ApplySettingsButton;

    [SerializeField]
    private Button m_ResetSettingsButton;

    [SerializeField]
    private Toggle m_FlatEarthToggle;

    [SerializeField]
    private Toggle m_FullscreenToggle;

    [SerializeField]
    private Slider m_MusicVolumeSlider;

    [SerializeField]
    private Slider m_SoundVolumeSlider;

    [SerializeField]
    private TMPro.TMP_Dropdown m_MusicSelectDropdown;

    private void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
        {
            Destroy(this);
            return;
        }

        m_SavePath = Application.persistentDataPath + "/settings.sus";

        // Load settings if available
        m_Settings = LoadSettings();
    }

    private void Start()
    {
        // Setup callbacks for UI
        UpdateMusicSelectOptions();
        m_ApplySettingsButton.onClick.AddListener(delegate () { ApplySettings(); SaveSettings(); AudioManager.Instance.Play("Button"); });
        m_ResetSettingsButton.onClick.AddListener(delegate () { ResetSettings(); AudioManager.Instance.Play("Button"); });
        m_FlatEarthToggle.onValueChanged.AddListener((bool state) => m_Settings.FlatEarthModel = state );
        m_FullscreenToggle.onValueChanged.AddListener(delegate (bool state) { m_Settings.Fullscreen = state; });
        m_MusicVolumeSlider.onValueChanged.AddListener(delegate (float value) { m_Settings.MusicVolume = value; });
        m_SoundVolumeSlider.onValueChanged.AddListener(delegate (float value) { m_Settings.SoundVolume = value; });
        m_MusicSelectDropdown.onValueChanged.AddListener(delegate (int option) { m_Settings.MusicSelected = option; });
        
        ApplySettings();
        UpdateSettingValues();
    }

    /// <summary>
    /// Applies the settings set in m_Settings to the current game instance.
    /// </summary>
    private void ApplySettings()
    {
        Screen.fullScreen = m_Settings.Fullscreen;

        // Play default/selected sound on loop
        m_Settings.MusicSelected = Math.Clamp(m_Settings.MusicSelected, 0, AudioManager.Instance.musicSounds.Length - 1); // Music might have been deleted/added since last save, so make sure its a valid soundtrack
        AudioManager.Instance.SetMusicSound(AudioManager.Instance.musicSounds[m_Settings.MusicSelected]);

        OnSettingsChanged?.Invoke();
    }

    private void UpdateMusicSelectOptions()
    {
        Sound[] musicSounds = AudioManager.Instance.musicSounds;

        for (int i = 0; i < musicSounds.Length; i++)        
        {
            m_MusicSelectDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData(musicSounds[i].name));
        }
    }

    /// <summary>
    /// Saves the currently applied settings to disk.
    /// </summary>
    private void SaveSettings()
    {
        // TODO make generic and use with file stream thing
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(m_SavePath, FileMode.Create);

        formatter.Serialize(stream, m_Settings);
        stream.Close();

        print($"Saved settings: {m_Settings.ToString()}");
    }

    /// <summary>
    /// Load the settings from disk. 
    /// If the settings are not available then create default settings.
    /// </summary>
    private SettingsData LoadSettings()
    {
        // If no settings found then just load defaults
        if (!File.Exists(m_SavePath))
        {
            Debug.Log("No settings found on disk. Will load defualt settings");
            return new SettingsData();
        }

        // Load settings found
        // TODO use with file stream again here
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(m_SavePath, FileMode.Open);

        SettingsData data = formatter.Deserialize(stream) as SettingsData;
        stream.Close();
        return data;
    }

    /// <summary>
    /// Updates the UI element in the settings menu, 
    /// to display the currently applied settings.
    /// </summary>
    private void UpdateSettingValues()
    {
        // Set default values      
        m_FlatEarthToggle.isOn = m_Settings.FlatEarthModel;
        m_FullscreenToggle.isOn = m_Settings.Fullscreen;
        m_MusicVolumeSlider.value = m_Settings.MusicVolume;
        m_SoundVolumeSlider.value = m_Settings.SoundVolume;
        m_MusicSelectDropdown.value = m_Settings.MusicSelected;
        m_MusicSelectDropdown.RefreshShownValue();
    }

    private void ResetSettings()
    {
        // Delete saved settings
        File.Delete(m_SavePath);

        // Load defaults
        m_Settings = LoadSettings();
        print($"Loaded settings: {m_Settings.ToString()}");

        UpdateSettingValues();
    }
}

[System.Serializable]
public class SettingsData
{
    public bool FlatEarthModel = false;
    public bool Fullscreen = true;
    public float MusicVolume = 1f;
    public float SoundVolume = 1f;
    public int MusicSelected = 1;

    public override string ToString()
    {
        return $"Settings Data: Flat earth: {FlatEarthModel}, Music volume: {MusicVolume}, Sound Volume: {SoundVolume}, Music selected {MusicSelected}, Fullscreen: {Fullscreen}";
    }
}
