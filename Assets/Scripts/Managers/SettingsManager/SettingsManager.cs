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

    public static event Action OnSettingsChanged;


    [Header("References")]
    [SerializeField]
    private Button m_ApplySettingsButton;

    [SerializeField]
    private Button m_ResetSettingsButton;

    [SerializeField]
    private Toggle m_FlatEarthToggle;

    [SerializeField]
    private Slider m_MusicVolumeSlider;

    [SerializeField]
    private Slider m_SoundVolumeSlider;

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
        // Setup callbacks
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            m_ApplySettingsButton.onClick.AddListener(() => SaveSettings());
            m_ResetSettingsButton.onClick.AddListener(() => ResetSettings());
            m_FlatEarthToggle.onValueChanged.AddListener(delegate (bool state) { m_Settings.FlatEarthModel = state; });
            m_MusicVolumeSlider.onValueChanged.AddListener(delegate (float value) { m_Settings.MusicVolume = value; });
            m_SoundVolumeSlider.onValueChanged.AddListener(delegate (float value) { m_Settings.SoundVolume = value; });
        }

        UpdateSettingValues();
    }

    private void SaveSettings()
    {
        // TODO make generic and use with file stream thing
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(m_SavePath, FileMode.Create);

        formatter.Serialize(stream, m_Settings);
        stream.Close();

        OnSettingsChanged?.Invoke();
        print($"Saved settings: {m_Settings.ToString()}");
    }

    // If settings are available then load. If not then create default settings
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

    private void UpdateSettingValues()
    {
        // Set default values
        if (SceneManager.GetActiveScene().name != "Menu")
            return;
        
        m_FlatEarthToggle.isOn = m_Settings.FlatEarthModel;
        m_MusicVolumeSlider.value = m_Settings.MusicVolume;
        m_SoundVolumeSlider.value = m_Settings.SoundVolume;
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
    public float MusicVolume = 1f;
    public float SoundVolume = 1f;

    public override string ToString()
    {
        return $"Settings Data: Flat earth: {FlatEarthModel}, Music volume: {MusicVolume}, Sound Volume: {SoundVolume}";
    }
}
