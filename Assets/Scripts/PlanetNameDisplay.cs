using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetNameDisplay : MonoBehaviour
{
    private void OnMouseDown()
    {

        //Guard statement to make sure if we click same planet, animation doesnt play again.
        if (GameObject.FindGameObjectWithTag("PlanetName").GetComponent<TextMeshProUGUI>().text == gameObject.name.ToUpper())
        {
            return;
        }

        //Fading out Planet Name
        GameObject.FindGameObjectWithTag("PlanetName").GetComponent<Animator>().SetBool("FadeOut", true);
        StartCoroutine(DelayAndDisplay());
    }

    /// <summary>
    /// Waits for half a second, before changing UI name and fading In.
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayAndDisplay()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        //Converts text within planetName to all uppercase
        GameObject.FindGameObjectWithTag("PlanetName").GetComponent<TextMeshProUGUI>().text = gameObject.name.ToUpper();
        GameObject.FindGameObjectWithTag("PlanetName").GetComponent<Animator>().SetBool("FadeOut", false);

    }
}
