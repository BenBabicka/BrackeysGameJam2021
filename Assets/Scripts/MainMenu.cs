using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject creditsUI;
    public GameObject settingUI;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider playerSlider;
    public float muiltply;
    public AudioMixer mixer;
    public AudioSource audioSource;
    public AudioClip click;

    private void Start()
    {
        if (FindObjectOfType<MusicManager>())
            audioSource = FindObjectOfType<MusicManager>().audioSource;

    }
    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(masterSliderValueChange);
        musicSlider.onValueChanged.AddListener(musicSliderValueChange);
        playerSlider.onValueChanged.AddListener(playerSliderValueChange);
    }

    private void masterSliderValueChange(float value)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(value) * muiltply);
    }
    private void musicSliderValueChange(float value)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(value) * muiltply);
    }
    private void playerSliderValueChange(float value)
    {
        mixer.SetFloat("PlayerVolume", Mathf.Log10(value) * muiltply);
    }
    public void Play()
    {
        audioSource.PlayOneShot(click);

        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        audioSource.PlayOneShot(click);

        creditsUI.SetActive(!creditsUI.activeSelf);
        mainMenuUI.SetActive(!mainMenuUI.activeSelf);
    }

    public void Settings()
    {
        audioSource.PlayOneShot(click);

        settingUI.SetActive(!settingUI.activeSelf);
        mainMenuUI.SetActive(!mainMenuUI.activeSelf);

    }

    public void Quit()
    {
        Application.Quit();
    }


}
