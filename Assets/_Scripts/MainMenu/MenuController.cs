using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject settingsObject;

    public AudioMixer mainMixer;
    public AudioMixer musicMixer;
    public AudioMixer SFXMixer;

    public TextMeshProUGUI resolutionText;
    public TextMeshProUGUI fullscreenText;

	private Resolution currentResolution;
    private Resolution[] validResolutions;
	private int currentResolutionIndex;

    private int fullScreenMode;


	public void Awake()
	{
        validResolutions = Screen.resolutions;
        currentResolution = Screen.currentResolution;
        currentResolutionIndex = Array.IndexOf(validResolutions, currentResolution);
        resolutionText.text = currentResolution.ToString();

		fullScreenMode = (int)Screen.fullScreenMode;
        UpdateFullscreenText();
	}

	public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenSettings()
    {
		menuObject.SetActive(false);
		settingsObject.SetActive(true);
	}

    public void CloseSettings()
    {
        menuObject.SetActive(true);
        settingsObject.SetActive(false);
    }

    public void UpdateMasterVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }

	public void UpdateMusicVolume(float volume)
	{
        musicMixer.SetFloat("volume", volume);
	}

    public void UpdateSFXVolume(float volume)
    {
        SFXMixer.SetFloat("volume", volume);
    }

    public void SelectResolution(int direction)
    {
        currentResolutionIndex += direction;
        currentResolutionIndex = (int)Mathf.Repeat(currentResolutionIndex, validResolutions.Length - 1);

        currentResolution = validResolutions[currentResolutionIndex];

        // Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);

        resolutionText.text = currentResolution.ToString();
    }

    public void PushResolution()
    {
        Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
    }

    public void SelectFullscreenType(int direction)
    {
        fullScreenMode += direction;
        fullScreenMode = (int)Mathf.Repeat(fullScreenMode, 4);

        // Screen.fullScreenMode = (FullScreenMode)fullScreenMode;

        UpdateFullscreenText();
	}

	public void PushFullscreen()
	{
        Screen.fullScreenMode = (FullScreenMode)fullScreenMode;
	}

	private void UpdateFullscreenText()
	{
		switch (fullScreenMode)
		{
			case 0:
				fullscreenText.text = "Exclusive Fullscreen";
				break;

			case 1:
				fullscreenText.text = "Fullscreen Window";
				break;

			case 2:
				fullscreenText.text = "Maximized Window";
				break;

			case 3:
				fullscreenText.text = "Windowed";
				break;

			default:
				fullscreenText.text = $"{Screen.fullScreenMode} not valid";
				break;
		}

        // fullscreenText.text = fullScreenMode.ToString();
	}

	public void QuitGame()
    {
        Application.Quit();
    }
}
