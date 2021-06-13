using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI healthText;
    public Slider hpSlider;
    public TextMeshProUGUI scoreText;

    public void setHUD(Chimera chimera)
    {
        nameText.text = chimera.nickname;
        healthText.text = (chimera.currentHp).ToString() + " / " + (chimera.maxHp).ToString();
        hpSlider.maxValue = chimera.maxHp;
        hpSlider.value = chimera.currentHp;
    }

    public void setHp(Chimera chimera)
    {
        if (chimera.currentHp <= 0) //If Chimera is dead, set text to 0
        {
            healthText.text = "0 / " + (chimera.maxHp).ToString();
        }
        else //Chimera is not dead, proceed as normal
        {
            healthText.text = (chimera.currentHp).ToString() + " / " + (chimera.maxHp).ToString();
        }
        
        hpSlider.value = chimera.currentHp;
    }
}
