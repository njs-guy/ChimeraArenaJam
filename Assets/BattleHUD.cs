using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI battleText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI healthText;
    public Slider hpSlider;

    public void setBattleText(string text)
    {
        battleText.text = text;
    }

    public void setHUD(Chimera chimera)
    {
        nameText.text = chimera.nickname;
        healthText.text = (chimera.currentHp).ToString() + " / " + (chimera.maxHp).ToString();
        hpSlider.maxValue = chimera.maxHp;
        hpSlider.value = chimera.currentHp;
    }

    public void setHp(Chimera chimera)
    {
        healthText.text = (chimera.currentHp).ToString() + " / " + (chimera.maxHp).ToString();
        hpSlider.value = chimera.currentHp;
    }
}
