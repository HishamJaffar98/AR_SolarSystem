using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetView : MonoBehaviour
{
    
    [SerializeField] GameObject cameraHolder;
    [SerializeField] Camera mainCamera;
    CameraZoom cameraZoom;
    UIController uiController;
    GameObject[] planets;
    Narrator narrator;
    Dictionary<string, AudioClip> audioClips;

    Vector3 planetRot = new Vector3(11f, -52.221f, 0f);
    bool viewingPlanet;
    float lerpFactor;
    float viewFOV;

    void Start()
    {
        narrator = FindObjectOfType<Narrator>();
        cameraZoom = FindObjectOfType<CameraZoom>();
        uiController = FindObjectOfType<UIController>();
        audioClips = FindObjectOfType<NarrationClips>().AudioClips;
        viewingPlanet = false;
        planets = GameObject.FindGameObjectsWithTag("Planet");
    }

    // Update is called once per frame
    void Update()
    {
        ViewPlanet();
    }

    private void ViewPlanet()
    {
        if (viewingPlanet)
        {
            lerpFactor += 0.05f;
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, viewFOV, lerpFactor);
            cameraHolder.transform.rotation = Quaternion.Lerp(cameraHolder.transform.rotation, Quaternion.Euler(planetRot), lerpFactor);
            
            if (lerpFactor >= 1)
            {
                viewingPlanet = false;
                lerpFactor = 0;
            }
        }
    }

    private void OnMouseDown()
    {
        viewingPlanet = true;
        uiController.BackButtonDisplayed = true;
        CheckPlanetClicked();
        FadeOutPlanet();
        cameraZoom.MinFOV = viewFOV - 4f;
        cameraZoom.MaxFOV = viewFOV + 4f;
    }

    private void CheckPlanetClicked()
    {
        switch (gameObject.name)
        {
            case "Mercury":
                ToggleNarration("Mercury");
                viewFOV = 6f;
                break;
            case "Venus":
                viewFOV = 8f;
                ToggleNarration("Venus");
                break;
            case "Earth":
                viewFOV = 11.3f;
                ToggleNarration("Earth");
                break;
            case "Mars":
                viewFOV = 15f;
                ToggleNarration("Mars");
                break;
            case "Jupiter":
                viewFOV = 28.8329f;
                ToggleNarration("Jupiter");
                break;
            case "Saturn":
                viewFOV = 48.8f;
                ToggleNarration("Saturn");
                break;
            case "Uranus":
                viewFOV = 92f;
                ToggleNarration("Uranus");
                break;
            case "Neptune":
                viewFOV = 125f;
                ToggleNarration("Neptune");
                break;
        }
    }

    private void ToggleNarration(string planetName)
    {
        if (!narrator.IsPlaying)
        {
            narrator.StartNarration(audioClips[planetName]);
        }
        else
        {
            narrator.StopNarration();
        }
    }

    private void FadeOutPlanet()
    {
        foreach(GameObject planet in planets)
        {
            if(planet.name==gameObject.name)
            {
                continue;
            }
            else
            {
                planet.GetComponent<Animator>().SetBool("FadingOut", true);
            }
        }
    }
}
