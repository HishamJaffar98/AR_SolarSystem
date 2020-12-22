using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetRevolution : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float rotationSpeed;
    float angle;
    Vector3 sunPosition;

    public float RotationSpeed
    {
        get 
        {
            return rotationSpeed;
        }
        set
        {
            rotationSpeed = value;
        }
    }

    void Start()
    {
        sunPosition = GameObject.FindGameObjectWithTag("Sun").transform.position;
    }
    void Update()
    {
        angle += rotationSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius) ;
        transform.position=sunPosition+offset;
    }
   
}
