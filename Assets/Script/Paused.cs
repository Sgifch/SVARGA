using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using UnityEngine.Audio; 

public class Paused : MonoBehaviour
{
    [SerializeField]
    GameObject pause;
    [SerializeField]
    GameObject settings;
    [SerializeField]
    GameObject exit;
    [SerializeField]
    GameObject healthbar;
    [SerializeField]
    GameObject weaponfast;
    [SerializeField]
    GameObject inventoryfast;

    public float volume = 0; //���������
    public int quality = 0; //��������
    public bool isFullscreen = false; //������������� �����
    public AudioMixer audioMixer; //��������� ���������
    public TMP_Dropdown resolutionDropdown; //������ � ������������ ��� ����
    private Resolution[] resolutions; //������ ��������� ����������
    private int currResolutionIndex = 0; //������� ����������

    void Start()
    {
        pause.SetActive(false);
        settings.SetActive(false);
        exit.SetActive(false);
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].Equals(Screen.currentResolution))
            {
                currResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options); //���������� ��������� � ���������� ������
        resolutionDropdown.value = currResolutionIndex; //��������� ������ � ������� �����������
        resolutionDropdown.RefreshShownValue(); //���������� ������������� ��������
    }


    void Update()
    {

    }

    public void PauseOff()
    {
        GameObject.FindWithTag("UIControll").GetComponent<UIControll>().PauseClose();
        Time.timeScale = 1;

    }

    public void Settings()
    {
        settings.SetActive(true);
    }

    public void SettingsOff()
    {
        settings.SetActive(false);
    }

    public void ChangeVolume(float val) //��������� �����
    {
        volume = val;
    }

    public void ChangeResolution(int index) //��������� ����������
    {
        currResolutionIndex = index;
    }

    public void ChangeFullscreenMode(bool val) //��������� ��� ���������� �������������� ������
    {
        isFullscreen = val;
    }

    public void ChangeQuality(int index) //��������� ��������
    {
        quality = index;
    }

    public void Exit()
    {
        exit.SetActive(true);
    }

    public void NotQuitGame()
    {
        exit.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveSettings()
    {
        audioMixer.SetFloat("MasterVolume", volume); 
        QualitySettings.SetQualityLevel(quality); 
        Screen.fullScreen = isFullscreen; 
        Screen.SetResolution(Screen.resolutions[currResolutionIndex].width, Screen.resolutions[currResolutionIndex].height, isFullscreen); 
    }

    public void SaveLobby()
    {
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().SaveAll();
    }
}
