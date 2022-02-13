using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private int currentCombo = 0;
    [SerializeField]
    private float comboResetTime;
    private float comboDurationTime;
    public int CurrentCombo{
        get
        {
            return currentCombo;
        }
    }

    // 콤보 지속시간 안에 새로 몬스터를 처치하지 못하면 콤보 초기화
    public void IncreaseCombo(int _combo = 1)
    {
        currentCombo += _combo;
        comboDurationTime = 0;
        if(currentCombo > 1)
        {
            comboResetTime += Time.deltaTime;
            if(comboDurationTime > comboResetTime)
            {
                ResetCombo();
            }
        }
    }

    public void ResetCombo()
    {
        currentCombo = 0;
    }
}
