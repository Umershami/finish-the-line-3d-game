using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenuscript : MonoBehaviour
{
    public GameObject mainmenu;
  
    public GameObject settingsbtn;
    public GameObject howtoplaybtn;
    public GameObject creditsbtn;
    public AudioSource mainsound;
    public AudioClip btnsound;
    // Start is called before the first frame update
    void Start()
    {
        playsound();
        mainmenu.SetActive(true);
        settingsbtn.SetActive(false);
        creditsbtn.SetActive(false);
        howtoplaybtn.SetActive(false);
    }
    public void playsound()
    {
        mainsound.Play();
    }
    public void btnnound()
    {
        mainsound.PlayOneShot(btnsound);
    }

   public void howtoplaybtnclicked()
    {
        btnnound();
        mainmenu.SetActive(false);
        settingsbtn.SetActive(false);
        howtoplaybtn.SetActive(true);
        creditsbtn.SetActive(false );
    }
    public void settingbtnclicked()
    {
        btnnound();
        mainmenu.SetActive(false);
        settingsbtn.SetActive(true);
        howtoplaybtn.SetActive(false);
        creditsbtn.SetActive(false);
    }
    public void creditsbtnclicked()
    {
        btnnound();
        mainmenu.SetActive(false);
        settingsbtn.SetActive(false);
        howtoplaybtn.SetActive(false);
        creditsbtn.SetActive(true);
    }
    public void playbtn()
    {
        btnnound();
        SceneManager.LoadScene(1);
        

    }
    public void backbtn()
    {
        btnnound();
        mainmenu.SetActive(true);
        settingsbtn.SetActive(false);
        creditsbtn.SetActive(false);
        howtoplaybtn.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
