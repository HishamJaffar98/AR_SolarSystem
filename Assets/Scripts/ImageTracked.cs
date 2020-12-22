using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracked : MonoBehaviour
{
    private ARTrackedImageManager aRTrackedImageManager;
    [SerializeField] GameObject sceneObjects;

    private void Awake()
    {
        aRTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    public void OnEnable()
    {
        aRTrackedImageManager.trackedImagesChanged += OnChanged;
    }

    public void OnDisable()
    {
        aRTrackedImageManager.trackedImagesChanged -= OnChanged;
    }
    public void OnChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            Instantiate(sceneObjects);
        }
    }
}
