using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    private int currentCombo = 1;
    [SerializeField]
    private float comboResetTime;
    private float comboDurationTime;
    [SerializeField]
    private Image comboDurationImage;

    public int CurrentCombo{
        get
        {
            return currentCombo;
        }
    }

    private void Update()
    {
        if (currentCombo > 1)
        {
            comboDurationTime += Time.deltaTime;
            comboDurationImage.fillAmount = 1 - comboDurationTime / comboResetTime;
            if (comboDurationTime > comboResetTime)
            {
                DecreaseCombo();
            }
        }
    }
    // 콤보 지속시간 안에 새로 몬스터를 처치하지 못하면 콤보 감소
    public void IncreaseCombo()
    {
        currentCombo += 1;
        comboDurationTime = 0;        
    }

    public void DecreaseCombo()
    {
        currentCombo -= 1;
        comboDurationTime = 0;

    }
}
