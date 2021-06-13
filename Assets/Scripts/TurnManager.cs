using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum battleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, CREATOR}

public class TurnManager : MonoBehaviour
{

    public GameObject playerObject;
    public GameObject enemyObject;

    Chimera playerChimera;
    Chimera enemyChimera;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public TextMeshProUGUI battleText;
    public TextMeshProUGUI scoreText;

    public GameObject attackBtn;
    public GameObject defendBtn;
    public GameObject skillsBtn;
    public GameObject retryBtn;
    
    public int score = 0;
    public battleState state;


    // Start is called before the first frame update
    void Start()
    {
        state = battleState.START;
        StartCoroutine(SetupBattle());
    }

    public void setBattleText(string text)
    {
        battleText.text = text;
    }

    //creates an enemy to fight
    void generateEnemy()
    {
        int rand1 = Random.Range(0,5);
        int rand2 = Random.Range(0,5);
        int rand3 = Random.Range(0,5);

        int[] chVals = {rand1, rand2, rand3} ; //Rolls numbers from 0 to 4

        enemyChimera.currentHp = enemyChimera.maxHp; //restores HP

        enemyChimera.headPart = (Chimera.Animal)chVals[0]; //sets headPart
        enemyChimera.bodyPart = (Chimera.Animal)chVals[1]; //sets bodyPart
        enemyChimera.legPart = (Chimera.Animal)chVals[2]; //sets legPart

        enemyChimera.updateParts(); //updates the sprite and stats
    }

    IEnumerator SetupBattle()
    {
        setBattleText("The battle begins...");

        playerChimera = playerObject.GetComponent<Chimera>();
        enemyChimera = enemyObject.GetComponent<Chimera>();

        generateEnemy();
        
        playerHUD.setHUD(playerChimera);
        enemyHUD.setHUD(enemyChimera);

        yield return new WaitForSeconds(2f);

        setTurnOrder();
    }

    void setTurnOrder()
    {
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
        playerChimera.isDefending = false; //no longer defending
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyChimera.TakeDamage(playerChimera.attack);

        setBattleText("The player attacks!");
        enemyHUD.setHp(enemyChimera);
        
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            enemyObject.SetActive(false);
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
        enemyChimera.isDefending = false; //no longer defending

        int rand = Random.Range(0,10); //Rolls a number from 0 to 9

        if (rand == 0) //defend
        {
            setBattleText("The enemy defends!");

            yield return new WaitForSeconds(2f);
            enemyChimera.Defend();

            startPlayerTurn();
        } 
        else //attack
        {
            setBattleText("The enemy attacks!");

            yield return new WaitForSeconds(1f);

            bool isDead = playerChimera.TakeDamage(enemyChimera.attack);

            playerHUD.setHp(playerChimera);

            yield return new WaitForSeconds(1f);

            if(isDead)
            {
                playerObject.SetActive(false);
                state = battleState.LOST;
                GameOver();
            } else
            {
                startPlayerTurn();
            }
        }
        
        Debug.Log(rand);
    }

    IEnumerator NextEnemy()
    {
        score += 1;
        setBattleText("A new challenger approaches!");
        yield return new WaitForSeconds(2f);

        scoreText.text = "Foes defeated: " + score.ToString();
        enemyObject.SetActive(true);
        generateEnemy();
        enemyHUD.setHUD(enemyChimera);
        setTurnOrder();  
    }

    void GameOver()
    {
        setBattleText("You have ascended to Valhalla. Game over...");
        retryBtn.SetActive(true);
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

    public void onRetryButton()
    {
        SceneManager.LoadScene(0); //Loads into Title Screen
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
