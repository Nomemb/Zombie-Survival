using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboTextManager : MonoBehaviour
{
    private ComboManager comboManager;
    public Text comboTxt;
    // Start is called before the first frame update
    void Start()
    {
        comboManager = FindObjectOfType<ComboManager>();
    }

    // Update is called once per frame
    void Update()
    {
        comboTxt.text = string.Format("X" + "{0}", comboManager.CurrentCombo);
    }
}
