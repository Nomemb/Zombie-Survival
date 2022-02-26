using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectMap : MonoBehaviour
{   
    public void SelectMap01()
    {
        SceneManager.LoadScene("Map1");
    }

    public void SelectMap02()
    {
        SceneManager.LoadScene("Map2");
    }
}
