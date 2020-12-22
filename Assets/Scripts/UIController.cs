using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    float[] rotationSpeeds = new float[8];
    bool musicPlaying;
    bool axisShowing;
    bool planetRevolving;
    bool backButtonDisplayed;
    bool viewSolarSystem;
    float colorLerpFactor;
    float viewLerpFactor;
    Color transparent;
    Color fullColor;
    Quaternion cameraOriginalRot;

    [SerializeField] GameObject backButton;
    [SerializeField] Camera mainCamera;
    GameObject[] axes;
    PlanetRevolution[] planets;
    AudioSource musicPlayer;
    GameObject cameraHolder;
    CameraZoom cameraZoom;
    Narrator narrator;

    public bool BackButtonDisplayed
    {
        get
        {
            return backButtonDisplayed;
        }
        set
        {
            backButtonDisplayed = value;
        }
    }
    void Start()
    {
        FindGameObjects();
        InitializeVariables();
        SaveOldSpeeds();
    }

    private void FindGameObjects()
    {
        planets = FindObjectsOfType<PlanetRevolution>();
        axes = GameObject.FindGameObjectsWithTag("Axis");
        musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<AudioSource>();
        cameraHolder=GameObject.FindGameObjectWithTag("CameraHolder");
        cameraZoom = FindObjectOfType<CameraZoom>();
        narrator = FindObjectOfType<Narrator>();
    }

    private void InitializeVariables()
    {
        transparent = new Color(1, 1, 1, 0);
        fullColor = new Color(1, 1, 1, 1);
        BackButtonDisplayed = false;
        planetRevolving = true;
        axisShowing = false;
        musicPlaying = true;
        cameraOriginalRot = cameraHolder.transform.rotation;
    }

    void SaveOldSpeeds()
    {
        for (int i = 0; i < planets.Length; i++)
        {
            rotationSpeeds[i] = planets[i].RotationSpeed;
        }
    }
    void Update()
    {
        PlayMusic();
        ShowAxis();
        RevolvePlanet();
        ToggleBackButton();
        ViewSolarSystem();
    }

    void PlayMusic()
    {
        if (musicPlaying && !musicPlayer.isPlaying)
        {
            musicPlayer.Play();
        }
        else if (!musicPlaying)
        {
            musicPlayer.Pause();
        }
    }

    void ShowAxis()
    {
        if (axisShowing)
        {
            foreach (GameObject axis in axes)
            {
                axis.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject axis in axes)
            {
                axis.SetActive(false);
            }
        }
    }

    void RevolvePlanet()
    {
        if (planetRevolving)
        {
            for (int i = 0; i < planets.Length; i++)
            {
                planets[i].RotationSpeed = rotationSpeeds[i];
            }
        }
        else
        {
            foreach (PlanetRevolution planet in planets)
            {
                planet.RotationSpeed = 0f;
            }
        }
    }

    void ToggleBackButton()
    {
        if (BackButtonDisplayed == true && backButton.GetComponent<Image>().color!=fullColor)
        {
            FadeInButton();
        }
        else if (BackButtonDisplayed == false && backButton.GetComponent<Image>().color != transparent)
        {
            FadeOutButton();
        }
    }

    private void FadeInButton()
    {
        if(backButton.activeSelf==false)
        {
            backButton.SetActive(true);
        }    
        colorLerpFactor += 0.005f;
        backButton.GetComponent<Image>().color = Color.Lerp(backButton.GetComponent<Image>().color, fullColor, colorLerpFactor);
        if (colorLerpFactor >= 1)
        {
            colorLerpFactor = 0f;
        }
    }

    private void FadeOutButton()
    {
        colorLerpFactor += 0.005f;
        backButton.GetComponent<Image>().color = Color.Lerp(backButton.GetComponent<Image>().color, transparent, colorLerpFactor);
        if (colorLerpFactor >= 1)
        {
            backButton.SetActive(false);
            colorLerpFactor = 0f;
        }
    }

    void ViewSolarSystem()
    {
        if(viewSolarSystem)
        {
            viewLerpFactor += 0.04f;
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 85f, viewLerpFactor);
            cameraHolder.transform.rotation = Quaternion.Lerp(cameraHolder.transform.rotation, cameraOriginalRot, viewLerpFactor);
            if(viewLerpFactor>=1)
            {
                cameraZoom.MinFOV = 15f;
                cameraZoom.MaxFOV = 85F;
                viewLerpFactor = 0f;
                viewSolarSystem = false;    
            }
        }
    }

    public void ToggleMusic()
    {
        if (musicPlaying)
        {
            musicPlaying = false;
        }
        else if(!musicPlaying)
        {
            musicPlaying = true;
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ToggleAxis()
    {
        if(axisShowing)
        {
            axisShowing = false;
        }
        else
        {
            axisShowing = true;
        }
    }

    public void ToggleRevolution()
    {
        if(planetRevolving)
        {
            planetRevolving = false;
        }
        else
        {
            planetRevolving = true;
        }
    }

    public void ShowSolarSystem()
    {
        if(narrator.IsPlaying)
        {
            narrator.StopNarration();
        }
        viewSolarSystem = true;
        BackButtonDisplayed = false;
        FadeInPlanets();
        GameObject.FindGameObjectWithTag("PlanetName").GetComponent<Animator>().SetBool("FadeOut", true);
        StartCoroutine(DelayAndDisplay());
    }

    void FadeInPlanets()
    {
        foreach(PlanetRevolution planet in planets)
        {
            planet.gameObject.GetComponent<Animator>().SetBool("FadingOut", false);
        }
    }
    IEnumerator DelayAndDisplay()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        //Converts text within planetName to all uppercase
        GameObject.FindGameObjectWithTag("PlanetName").GetComponent<TextMeshProUGUI>().text = "Solar System".ToUpper();
        GameObject.FindGameObjectWithTag("PlanetName").GetComponent<Animator>().SetBool("FadeOut", false);

    }
}
