using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    float newZoom;
    float lerpFactor;
    float lastFieldOfView;
    const float zoomScaleFactor = 500f;
    float minFOV;
    float maxFOV;
    enum zoomState {zoomingIn, zoomingOut,noZoom};
    zoomState currentZoomState;

    [SerializeField] Camera mainCamera;

    public float MinFOV
    {
        get
        {
            return minFOV;
        }
        set
        {
            minFOV = value;
        }
    }

    public float MaxFOV
    {
        get
        {
            return maxFOV;
        }
        set
        {
            maxFOV = value;
        }
    }
    private void Start()
    {
        MinFOV = 18f;
        MaxFOV = 85f;
        currentZoomState = zoomState.noZoom;
    }
    void Update()
    {
        if(Input.touchCount==2 && Input.touches[0].phase==TouchPhase.Moved && Input.touches[1].phase==TouchPhase.Moved)
        {
            lerpFactor = 0f;
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);
            Vector2 initialPosition1 = firstTouch.position;
            Vector2 initialPosition2 = secondTouch.position;
            Vector2 finalPosition1 = firstTouch.position + firstTouch.deltaPosition;
            Vector2 finalPosition2 = secondTouch.position + secondTouch.deltaPosition;
            float initialDistance = Vector2.Distance(initialPosition2, initialPosition1);
            float finalDistance = Vector2.Distance(finalPosition2, finalPosition1);
            float zoomFactor = finalDistance/zoomScaleFactor;
            if (finalDistance>initialDistance)
            {
                newZoom = mainCamera.fieldOfView+zoomFactor;
                currentZoomState = zoomState.zoomingOut;
            }
            else
            {
                newZoom = mainCamera.fieldOfView - zoomFactor;
                currentZoomState = zoomState.zoomingIn;
            }
            mainCamera.fieldOfView = Mathf.Clamp(newZoom, MinFOV, MaxFOV); 
            lastFieldOfView = mainCamera.fieldOfView;
        }
        else
        {
            if(lerpFactor>=1)
            {
                return;
            }
            lerpFactor += 0.05f;
            if(currentZoomState==zoomState.zoomingIn && mainCamera.fieldOfView != MinFOV)
            {
                mainCamera.fieldOfView = Mathf.Lerp(lastFieldOfView, lastFieldOfView - 3, lerpFactor);
            }
            else if(currentZoomState==zoomState.zoomingOut&& mainCamera.fieldOfView!=MaxFOV)
            {
                mainCamera.fieldOfView = Mathf.Lerp(lastFieldOfView, lastFieldOfView + 3, lerpFactor);
            }
        }
    }
}
