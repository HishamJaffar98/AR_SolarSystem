using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Spin the object around the target at 20 degrees/second.
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime,relativeTo:Space.Self);
    }
}
