using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum battleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class TurnManager : MonoBehaviour
{

    public GameObject playerObject;
    public GameObject enemyObject;

    Chimera playerChimera;
    Chimera enemyChimera;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public TextMeshProUGUI battleText;

    public GameObject attackBtn;
    public GameObject defendBtn;
    public GameObject skillsBtn;
    
    public battleState state;


    // Start is called before the first frame update
    void Start()
    {
        state = battleState.START;
        StartCoroutine(SetupBattle());
        //SetupBattle();
    }

    public void setBattleText(string text)
    {
        battleText.text = text;
    }

    IEnumerator SetupBattle()
    {
        setBattleText("The battle begins...");

        GameObject playerGO = Instantiate(playerObject);
        playerChimera = playerGO.GetComponent<Chimera>();

        GameObject enemyGO = Instantiate(enemyObject);
        enemyChimera = enemyGO.GetComponent<Chimera>();

        //Set sprites and stats

        playerHUD.setHUD(playerChimera);
        enemyHUD.setHUD(enemyChimera);

        yield return new WaitForSeconds(2f);

        state = battleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        setBattleText("Choose a command.");
        enableCommands();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyChimera.TakeDamage(playerChimera.attack);

        setBattleText("The player attacks!");
        enemyHUD.setHp(enemyChimera);
        
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = battleState.WON;
            //StartCoroutine(NextEnemy());
            NextEnemy();
        }
        else
        {
            state = battleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        setBattleText("The enemy attacks!");

        yield return new WaitForSeconds(1f);

        bool isDead = playerChimera.TakeDamage(enemyChimera.attack);

        playerHUD.setHp(playerChimera);

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = battleState.LOST;
            //lost battle
        } else
        {
            state = battleState.PLAYERTURN;
            PlayerTurn();
        }
        
    }

    void NextEnemy()
    {
        setBattleText("A new challenger approaches!");

        if(state == battleState.WON)
        {
            //regenerate enemy, restore their health
        }
        else if (state == battleState.LOST)
        {
            //lose
        }
    }

    IEnumerator PlayerHeal()
    {
        setBattleText("The player healed!");

        playerChimera.Heal(5);
        playerHUD.setHp(playerChimera);

        yield return new WaitForSeconds(2f);

        state = battleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void onAttackButton()
    {
        disableCommands();

        if(state != battleState.PLAYERTURN)
        {
            return; //do nothing if it is not the player's turn
        }

        StartCoroutine(PlayerAttack());
    }

    public void onSkillButton()
    {
        disableCommands();

        if(state != battleState.PLAYERTURN)
        {
            return; //do nothing if it is not the player's turn
        }

        StartCoroutine(PlayerHeal());
    }

    public void enableCommands()
    {
        attackBtn.SetActive(true);
        defendBtn.SetActive(true);
        skillsBtn.SetActive(true);
    }

    public void disableCommands()
    {
        attackBtn.SetActive(false);
        defendBtn.SetActive(false);
        skillsBtn.SetActive(false);
    }

}
