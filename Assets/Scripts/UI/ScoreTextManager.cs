using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextManager : MonoBehaviour
{
    private ScoreManager scoreManager;
    public Text scoreTxt; 
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreTxt.text = string.Format("{0:D10}", scoreManager.currentScore);
    }
}
