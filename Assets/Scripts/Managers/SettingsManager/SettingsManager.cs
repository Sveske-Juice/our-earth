using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager s_Instance;
    private SettingsData m_Settings;
    private string m_SavePath;

    public static SettingsManager Instance => s_Instance;
    public bool FlatEarthModel => m_Settings.FlatEarthModel;

    [Header("References")]
    [SerializeField]
    private Toggle m_FlatEarthToggle;

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

        print($"Loaded settings: {m_Settings.ToString()}");
    }

    private void Start()
    {
        // Setup callbacks
        m_FlatEarthToggle.onValueChanged.AddListener(delegate (bool state) { m_Settings.FlatEarthModel = state; SaveSettings(); UpdateSettingValues(); });

        UpdateSettingValues();
    }

    private void SaveSettings()
    {
        // TODO make generic and use with file stream thing
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(m_SavePath, FileMode.Create);

        formatter.Serialize(stream, m_Settings);
        stream.Close();
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
        m_FlatEarthToggle.isOn = m_Settings.FlatEarthModel;
    }
}

[System.Serializable]
public class SettingsData
{
    public bool FlatEarthModel = false;

    public override string ToString()
    {
        return $"Settings Data: Flat earth: {FlatEarthModel}";
    }
}
