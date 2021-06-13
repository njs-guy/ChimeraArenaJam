using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCreator : MonoBehaviour
{
    public TextMeshProUGUI attackStat;
    public TextMeshProUGUI defenseStat;
    public TextMeshProUGUI speedStat;

    public TMP_Dropdown headDropdown;
    public TMP_Dropdown torsoDropdown;
    public TMP_Dropdown legsDropdown;

    Chimera previewChimera;
    public GameObject previewObject;
    Chimera plChimera;

    public GameObject battleUI;
    public GameObject playerObject;
    public GameObject enemyObject;
    public GameObject turnManager;
    public GameObject creatorUI;

    public void updateCharacter()
    {
        int[] characterVals = getCharacterValues();
        previewChimera = previewObject.GetComponent<Chimera>();

        //reassigns each animal part in previewChimera from dropdown menus
        previewChimera.headPart = (Chimera.Animal)characterVals[0];
        previewChimera.bodyPart = (Chimera.Animal)characterVals[1];
        previewChimera.legPart = (Chimera.Animal)characterVals[2];

        //updates text and previews
        previewChimera.updateParts();
        attackStat.text = "Attack: " + previewChimera.attack.ToString();
        defenseStat.text = "Defense: " + previewChimera.defense.ToString();
        speedStat.text = "Speed: " + previewChimera.speed.ToString(); 
    }

    public int[] getCharacterValues()
    {
        int headOutput = headDropdown.value;
        int tursoOutput = torsoDropdown.value;
        int legsOutput = legsDropdown.value;

        int[] output = {headOutput, tursoOutput, legsOutput};

        return output;
    }

    // Start is called before the first frame update
    void Start()
    {
        updateCharacter();
    }

    public void finishCharacter()
    {
        //Disable character creator
        battleUI.SetActive(true);
        playerObject.SetActive(true);
        enemyObject.SetActive(true);
        turnManager.SetActive(true);

        //create character
        int[] characterVals = getCharacterValues();
        plChimera = playerObject.GetComponent<Chimera>();

        plChimera.headPart = (Chimera.Animal)characterVals[0];
        plChimera.bodyPart = (Chimera.Animal)characterVals[1];
        plChimera.legPart = (Chimera.Animal)characterVals[2];
        
        //enable battles
        previewObject.SetActive(false);
        creatorUI.SetActive(false);
    }

}
