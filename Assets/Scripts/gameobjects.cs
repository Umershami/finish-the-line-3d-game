using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameobjects : MonoBehaviour
{
    public Gameplayscript gameplayscript;
    public Carscript carscript;
    public GameObject pausebtn;
    public GameObject resumepanel;
    public bool resumepanelactive = false;
    // Start is called before the first frame update
    void Start()
    {
    pausebtn.SetActive(true);
        resumepanel.SetActive(false);
        
    }
    public void pausebtnclicked()
    {
        resumepanel.SetActive(true);
        pausebtn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
