using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayTrailRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(delayAndActivate());
    }

    IEnumerator delayAndActivate()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        gameObject.GetComponent<TrailRenderer>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
