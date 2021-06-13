using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum battleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, CREATOR}

public class TurnManager : MonoBehaviour
{

    public GameObject playerObject;
    public GameObject enemyObject;
    //public GameObject creatorObject;

    Chimera playerChimera;
    Chimera enemyChimera;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public TextMeshProUGUI battleText;

    public GameObject attackBtn;
    public GameObject defendBtn;
    public GameObject skillsBtn;
    
    public int score = 0;
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

    //The first battle needs to set up the player
    IEnumerator FirstBattle()
    {
        yield return new WaitForSeconds(2f);
    }

    //Subsequent battles need to create a new enemy to fight
    IEnumerator SubBattle()
    {
        yield return new WaitForSeconds(2f);
    }

    IEnumerator SetupBattle()
    {
        setBattleText("The battle begins...");

        //GameObject playerGO = Instantiate(playerObject);
        playerChimera = playerObject.GetComponent<Chimera>();

        //GameObject enemyGO = Instantiate(enemyObject);
        enemyChimera = enemyObject.GetComponent<Chimera>();

        //int[] characterVals = CharacterCreator.getCharacterValues();
        //CharacterCreator.getChara
        
        playerHUD.setHUD(playerChimera);
        enemyHUD.setHUD(enemyChimera);

        yield return new WaitForSeconds(2f);

        if(playerChimera.speed > enemyChimera.speed) //If the player is faster, they move first.
        {
            startPlayerTurn();

        } else if(playerChimera.speed < enemyChimera.speed) //if the enemy is faster, they move first.
        {
            startEnemyTurn();
        }
        else if(playerChimera.speed == enemyChimera.speed) //if there is a speed tie
        {
            int rand = Random.Range(0,10); //Rolls a number from 0 to 9

            if (rand > 4) //player moves first
            {
                startPlayerTurn();
            } 
            else //enemy moves first
            {
                startEnemyTurn();
            }
        }

        
    }

    //sets state to ENEMYTURN and starts PlayerTurn()
    void startPlayerTurn()
    {
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
            setBattleText("The enemy has acended to Valhalla...");
            
            yield return new WaitForSeconds(2f);
            StartCoroutine(NextEnemy());
        }
        else
        {
            startEnemyTurn();
        }
    }

    IEnumerator PlayerDefend()
    {
        bool isDead = enemyChimera.TakeDamage(playerChimera.attack);

        setBattleText("The player defends!");
        
        yield return new WaitForSeconds(2f);

        playerChimera.Defend();

        startEnemyTurn();
    }

    IEnumerator PlayerHeal()
    {
        setBattleText("The player healed!");

        playerChimera.Heal(5);
        playerHUD.setHp(playerChimera);

        yield return new WaitForSeconds(2f);

        startEnemyTurn();
    }

    //sets state to ENEMYTURN and starts EnemyTurn()
    void startEnemyTurn()
    {
        state = battleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        int rand = Random.Range(0,10); //Rolls a number from 0 to 9

        if (rand > 3) //attack
        {
            setBattleText("The enemy attacks!");

            yield return new WaitForSeconds(1f);

            bool isDead = playerChimera.TakeDamage(enemyChimera.attack);

            playerHUD.setHp(playerChimera);

            yield return new WaitForSeconds(1f);

            if(isDead)
            {
                state = battleState.LOST;
                GameOver();
            } else
            {
                startPlayerTurn();
            }
        } 
        else //defend
        {
            setBattleText("The enemy defends!");

            yield return new WaitForSeconds(2f);
            enemyChimera.Defend();

            startPlayerTurn();
        }
        
        //Debug.Log(rand);
    }

    IEnumerator NextEnemy()
    {
        setBattleText("A new challenger approaches!");
        yield return new WaitForSeconds(2f);

        SetupBattle();
    }

    void GameOver()
    {
        setBattleText("You have ascended to Valhalla. Game over...");
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

    public void onDefendButton()
    {
        disableCommands();

        if(state != battleState.PLAYERTURN)
        {
            return; //do nothing if it is not the player's turn
        }

        StartCoroutine(PlayerDefend());
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
