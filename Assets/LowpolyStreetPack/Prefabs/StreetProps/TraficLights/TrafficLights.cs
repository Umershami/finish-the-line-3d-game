using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLights : MonoBehaviour
{
    public GameObject greenLight; // Assign the green light GameObject in the Inspector
    public GameObject redLight;
    public GameObject yellowlight;
    // Assign the red light GameObject in the Inspector
    public float blinkDuration = 1f; // Duration for each blink

    private void Start()
    {
        StartCoroutine(BlinkLights());
    }

    private IEnumerator BlinkLights()
    {
        while (true) // Repeat indefinitely, or change to a condition if needed
        {
            // Turn on the green light and turn off the red light
            greenLight.SetActive(true);
            redLight.SetActive(false);
            yellowlight.SetActive(false);
            yield return new WaitForSeconds(blinkDuration); // Wait for the blink duration

            // Turn off the green light and turn on the red light
            greenLight.SetActive(false);
            redLight.SetActive(true);
            yellowlight.SetActive(false);
            yield return new WaitForSeconds(blinkDuration); // Wait for the blink duration

            greenLight.SetActive(false);
            redLight.SetActive(false);
            yellowlight.SetActive(true);
            yield return new WaitForSeconds(blinkDuration);
            // Turn on the green light and turn off the red light again
            // Wait for the blink duration
            greenLight.SetActive(true);
            redLight.SetActive(false);
            yellowlight.SetActive(false);
            yield return new WaitForSeconds(blinkDuration); // Wait for the blink duration

            // Turn off the green light and turn on the red light
            greenLight.SetActive(false);
            redLight.SetActive(true);
            yellowlight.SetActive(false);
            yield return new WaitForSeconds(blinkDuration); // Wait for the blink duration

            greenLight.SetActive(false);
            redLight.SetActive(false);
            yellowlight.SetActive(true);
            yield return new WaitForSeconds(blinkDuration);
            // Turn on the green light and turn off the red light again
            // Wait for the blink duration
        }
    }
}
