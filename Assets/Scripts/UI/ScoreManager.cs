using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private ComboManager comboManager;
    public int currentScore;
    public Text scoreTxt;


    private Animator anim;
    private void Start()
    {
        currentScore = 0;
        comboManager = FindObjectOfType<ComboManager>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        scoreTxt.text = string.Format("{0:D10}", currentScore);
    }
    // 좀비 처치시 실행
    public void IncreaseScore(int _score)
    {
        int _currentCombo = comboManager.CurrentCombo;
        currentScore += _score * _currentCombo;
        comboManager.IncreaseCombo();
        // 콤보 0일때 점수 오르지 않는 현상 방지
        anim.SetTrigger("ScoreUP");
    }


}
