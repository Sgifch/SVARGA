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

    //public string nameScene;
    //public float volume = 0; //Громкость
    //public int quality = 0; //Качество
    //public bool isFullscreen = false; //Полноэкранный режим
    public AudioMixer audioMixer; //Регулятор громкости
    //public TMP_Dropdown resolutionDropdown; //Список с разрешениями для игры
    private Resolution[] resolutions; //Список доступных разрешений
    private int currResolutionIndex = 0; //Текущее разрешение

    // UI элементы для настроек
    [Header("UI элементы для настроек")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Dropdown resolutionDropdownUI;
    [SerializeField] private TMP_Dropdown qualityDropdownUI;
    [SerializeField] private Toggle fullscreenToggleUI;

    [Header("Переход между сценами")]
    public string sceneName;
    public GameObject changeCanvas;
    public GameObject blackout;
    public Animator anim;

    void Start()
    {
        pause.SetActive(false);
        settings.SetActive(false);
        exit.SetActive(false);
        resolutionDropdownUI.ClearOptions();
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

        resolutionDropdownUI.AddOptions(options); //Добавление элементов в выпадающий список
        resolutionDropdownUI.value = currResolutionIndex; //Выделение пункта с текущим разрешением
        resolutionDropdownUI.RefreshShownValue(); //Обновление отображаемого значения
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

    //public void SettingsOff()
    //{
    //}

    //public void ChangeVolume(float val) //Изменение звука
    //{
    //    volume = val; 
    //}

    //public void ChangeResolution(int index) //Изменение разрешения
    //{
    //    currResolutionIndex = index;
    //}

    //public void ChangeFullscreenMode(bool val) //Включение или отключение полноэкранного режима
    //{
    //    isFullscreen = val;
    //}

    //public void ChangeQuality(int index) //Изменение качества
    //{
    //    quality = index;
    //}

    public void MainMenu(string name)
    {
        Time.timeScale = 1f;
        //nameScene = name;
        //SceneManager.LoadScene(name);

        changeCanvas.SetActive(true);
        blackout.GetComponent<LobbyLoadScene>().sceneName = sceneName;
        anim.SetTrigger("Blackout");
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
        // 1. Громкость
        if (volumeSlider != null)
        {
            float volume = volumeSlider.value;
            audioMixer.SetFloat("MyExposedParam", volume);
        }

        // 2. Качество
        if (qualityDropdownUI != null)
        {
            QualitySettings.SetQualityLevel(qualityDropdownUI.value);
        }

        // 3. Полноэкранный режим
        if (fullscreenToggleUI != null)
        {
            Screen.fullScreen = fullscreenToggleUI.isOn;
        }

        // 4. Разрешение
        if (resolutionDropdownUI != null && resolutions != null)
        {
            int index = resolutionDropdownUI.value;
            if (index >= 0 && index < resolutions.Length)
            {
                Screen.SetResolution(resolutions[index].width,
                                    resolutions[index].height,
                                    Screen.fullScreen);
            }
        }

        settings.SetActive(false);
        Debug.Log("Настройки применены!");
    }

    //public void SaveSettings()
    //{
    //    audioMixer.SetFloat("MyExposedParam", volume); 
    //    QualitySettings.SetQualityLevel(quality); 
    //    Screen.fullScreen = isFullscreen; 
    //    Screen.SetResolution(Screen.resolutions[currResolutionIndex].width, Screen.resolutions[currResolutionIndex].height, isFullscreen);
    //}

    public void SaveLobby()
    {
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().SaveAll();
    }
}
