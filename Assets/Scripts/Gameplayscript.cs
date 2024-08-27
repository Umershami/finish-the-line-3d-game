using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplayscript : MonoBehaviour
{
    public Carscript carscript;
    public Text timerobject;
    public GameObject lostpanel;
    public GameObject countdownpanel;
    public GameObject resumepanel;
    public GameObject winpanel;
    public AudioSource gamesound;
    public AudioClip gameclip;

    public AudioClip bubblesound;
    public AudioClip btnsound;





    public float timerr = 0;
    public float countdown = 3;
    public bool hascountdownfinished;
    public bool resumepanelactivecheck = false;

    void Start()
    {
        

        winpanel.SetActive(false);
        resumepanel.SetActive(false);
        lostpanel.SetActive(false);

        if (timerobject == null)
        {
            Debug.LogError("Timerobject is not assigned.");
        }

        timerr = 20f; // Set the timer to start from 25 seconds
    }
    public void mainmenubtnclicked()
    {
        SceneManager.LoadScene(0);
    }

    public void pausebtnclicked()
    {
        btnclicksound();
        winpanel.SetActive(false);
        resumepanelactivecheck = true;
        resumepanel.SetActive(true);
        PlayerPrefs.SetFloat("pausespeed", carscript.currentSpeed);
        carscript.currentSpeed = 0f; // Stop the car
        Time.timeScale = 0f; // Pause the game
    }

    public void resumebtnclicked()
    {
        btnclicksound();
        winpanel.SetActive(false);
        carscript.currentSpeed = PlayerPrefs.GetFloat("pausespeed", 0f);
        resumepanel.SetActive(false);
        resumepanelactivecheck = false;

        Time.timeScale = 1f; // Resume the game
    }
    public void nextlevelbuttonclicked()
    {
        btnclicksound();
        SceneManager.LoadScene(2);
    }

    public void replaybtnclicked()
    {
        btnclicksound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
    }

    public void winpanelactivated()
    {
        lostpanel.SetActive(false);
        winpanel.SetActive(true);
    }

    void Update()
    {


        countdowntimer();

        // Stop updating the timer if the win panel or lost panel is active
        if (!resumepanelactivecheck && !winpanel.activeSelf && !lostpanel.activeSelf)
        {
            if (timerobject != null && hascountdownfinished)
            {
                if (timerr > 0) // Change from >= 0 to > 0 to count down correctly
                {
                    timerr -= Time.deltaTime; // Use subtraction to count down
                    timerobject.text = Mathf.RoundToInt(timerr).ToString();

                    // Check if the timer has reached or exceeded 0 seconds
                    if (timerr <= 0 && !carscript.hasCrossedFinishLine)
                    {
                        activelostpanel();
                    }
                }
            }
        }
    }



    public void countdowntimer()
    {
        if (!hascountdownfinished)
        {
            countdown -= Time.deltaTime;
            countdownpanel.GetComponent<Text>().text = Mathf.CeilToInt(countdown).ToString();

            if (countdown <= 0)
            {
                hascountdownfinished = true;
                countdownpanel.SetActive(false);  // Hide the countdown panel when finished
            }
        }
    }

    public void activelostpanel()
    {
        if (!carscript.hasCrossedFinishLine && carscript.wincheck == true)
        {
            lostpanel.SetActive(false);
        }
        else
        {
            Debug.Log("Function called");
            Debug.Log("Lost");
            carscript.currentSpeed = 0f;
            lostpanel.SetActive(true);
        }
    }
    public void btnclicksound()
    {

        gamesound.PlayOneShot(btnsound);
    }
}
