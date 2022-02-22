using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore;
    private ComboManager comboManager;
    private void Start()
    {
        currentScore = 0;
        comboManager = FindObjectOfType<ComboManager>();
    }

    // 좀비 처치시 실행
    public void IncreaseScore(int _score)
    {

        comboManager.IncreaseCombo();
        // 콤보 0일때 점수 오르지 않는 현상 방지
        int _currentCombo = comboManager.CurrentCombo;
        currentScore += _score * _currentCombo;
    }
}
