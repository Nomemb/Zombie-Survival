using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore;
    private ComboManager comboManager;
    private void Start()
    {
        currentScore = 0;
        comboManager = FindObjectOfType<ComboManager>();
    }

    private void Update()
    {        
        //if(좀비 처치시)
        // IncreaseScore(ZombieType.score);
    }

    // 좀비 처치시 실행
    public void IncreaseScore(int _score, int _currentCombo = 1)
    {

        comboManager.IncreaseCombo();
        // 콤보 0일때 점수 오르지 않는 현상 방지
        _currentCombo = _currentCombo > comboManager.CurrentCombo? _currentCombo : comboManager.CurrentCombo;
        currentScore += _score * _currentCombo;
    }
}
