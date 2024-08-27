using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextscenescript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void nextscenelevel2()
    {
        SceneManager.LoadScene(1);
    }
    public void nextscenelevel3()
    {
        Debug.Log("Next level not avlble");
        SceneManager.LoadScene(2);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
