using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider volumeSlider;

    public void StartButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void SettingsButton()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        volumeSlider.value = AudioListener.volume*100;
    }

    public void QuitSettingsButton()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void ChangeVolume()
    {        
        AudioListener.volume = volumeSlider.value / 100;
    }
}
