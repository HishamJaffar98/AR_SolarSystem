using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CheckAR : MonoBehaviour
{
    [SerializeField] ARSession _arSession;
    IEnumerator Start()
    {
        if(ARSession.state==ARSessionState.None||ARSession.state==ARSessionState.CheckingAvailability)
        {
            yield return ARSession.CheckAvailability();
        }
        if(ARSession.state==ARSessionState.Unsupported)
        {
            Debug.Log("No AR For You");
        }
        else
        {
            Debug.Log("AR Session is ready");
            _arSession.enabled = true;
        }
    }
}
