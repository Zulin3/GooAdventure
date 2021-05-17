using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text LoseText;
    [SerializeField] private Text WinText;
    [SerializeField] private RectTransform PausePanel;
    [SerializeField] private AudioClip normalMusic;
    [SerializeField] private AudioClip bossMusic;

    private AudioSource audio;
    private bool paused = false;

    public void Awake()
    {

        audio = GetComponent<AudioSource>();
    }

    public void PlayNormalMusic()
    {
        audio.clip = normalMusic;
        audio.Play();
    }

    public void PlayBossMusic()
    {
        audio.clip = bossMusic;
        audio.Play();
    }

    public void Lose()
    {
        LoseText.text = "Game Over! :(";
        StartCoroutine(StopTime());
    }

    public void Win()
    {
        WinText.text = "You win! :)";
        StartCoroutine(StopTime());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!paused)
            {
                paused = true;
                PausePanel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                paused = false;
                PausePanel.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    
    private IEnumerator StopTime()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
    }
}
