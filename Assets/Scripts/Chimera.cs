using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chimera : MonoBehaviour
{
    public enum Animal { bird, bunny, monkey, octopus, turtle }

    [SerializeField] public SpriteRenderer head;
    [SerializeField] public SpriteRenderer torso;
    [SerializeField] public SpriteRenderer legs;

    [SerializeField] public string nickname = "Chimera";
    [SerializeField] public int maxHp = 30;
    [SerializeField] public int currentHp = 30;
    [SerializeField] public int attack = 5;
    [SerializeField] public int defense = 5;
    [SerializeField] public int speed = 5;
    [SerializeField] public bool isDefending = false;

    [SerializeField] public Animal headPart;
    [SerializeField] public Animal bodyPart;
    [SerializeField] public Animal legPart;
    [SerializeField] public Sprite[] headSprites;
    [SerializeField] public Sprite[] bodySprites;
    [SerializeField] public Sprite[] legSprites;

    private enum statNames {ATTACK, DEFENSE, SPEED}; //Used in changeStats()

    //Changes stats
    void changeStats(statNames raise, statNames lower)
    {
        int statChange = 3; //what the stat will be raised or lowered by.

        switch(raise)
        {
            case statNames.ATTACK:
                attack += statChange;
                break;
            case statNames.DEFENSE:
                defense += statChange;
                break;
            case statNames.SPEED:
                speed += statChange;
                break;
        }

        switch(lower)
        {
            case statNames.ATTACK:
                attack -= statChange;
                break;
            case statNames.DEFENSE:
                defense -= statChange;
                break;
            case statNames.SPEED:
                speed -= statChange;
                break;
        }

        //if a stat is less than or equal to 0, set to 1
        if(attack <= 0)
        {
            attack = 1;
        }

        if(defense <= 0)
        {
            defense = 1;
        }

        if(speed <= 0)
        {
            speed = 1;
        }
    }

    //Assigns each part sprite, and determines stats
    public void updateParts()
    {
        //checks what animal each part should be, and changes the sprite
        Sprite currentHead;
        Sprite currentBody;
        Sprite currentLegs;

        int baseStat = 5; //The base value of each stat

        attack = baseStat;
        defense = baseStat;
        speed = baseStat;

        switch(headPart)
        {
            case Animal.bird:
                currentHead = headSprites[0];
                changeStats(statNames.SPEED, statNames.DEFENSE);
                Debug.Log("bird head");
                break;
            case Animal.bunny:
                currentHead = headSprites[1];
                changeStats(statNames.SPEED, statNames.ATTACK);
                Debug.Log("bunny head");
                break;
            case Animal.monkey:
                currentHead = headSprites[2];
                changeStats(statNames.ATTACK, statNames.DEFENSE);
                Debug.Log("monkey head");
                break;
            case Animal.octopus:
                currentHead = headSprites[3];
                changeStats(statNames.ATTACK, statNames.SPEED);
                Debug.Log("octopus head");
                break;
            case Animal.turtle:
                currentHead = headSprites[4];
                changeStats(statNames.DEFENSE, statNames.SPEED);
                Debug.Log("turtle head");
                break;
            default:
                currentHead = headSprites[2];
                changeStats(statNames.ATTACK, statNames.DEFENSE);
                Debug.Log("else, return to monke");
                break;
            
        }

        switch(bodyPart)
        {
            case Animal.bird:
                currentBody = bodySprites[0];
                changeStats(statNames.SPEED, statNames.DEFENSE);
                Debug.Log("bird body");
                break;
            case Animal.bunny:
                currentBody = bodySprites[1];
                changeStats(statNames.SPEED, statNames.ATTACK);
                Debug.Log("bunny body");
                break;
            case Animal.monkey:
                currentBody = bodySprites[2];
                changeStats(statNames.ATTACK, statNames.DEFENSE);
                Debug.Log("monkey body");
                break;
            case Animal.octopus:
                currentBody = bodySprites[3];
                changeStats(statNames.ATTACK, statNames.SPEED);
                Debug.Log("octopus body");
                break;
            case Animal.turtle:
                currentBody = bodySprites[4];
                changeStats(statNames.DEFENSE, statNames.SPEED);
                Debug.Log("turtle body");
                break;
            default:
                currentBody = bodySprites[2];
                changeStats(statNames.ATTACK, statNames.DEFENSE);
                Debug.Log("else, return to monke");
                break;

        }

        switch(legPart)
        {
            case Animal.bird:
                currentLegs = legSprites[0];
                changeStats(statNames.SPEED, statNames.DEFENSE);
                Debug.Log("bird legs");
                break;
            case Animal.bunny:
                currentLegs = legSprites[1];
                changeStats(statNames.SPEED, statNames.ATTACK);
                Debug.Log("bunny legs");
                break;
            case Animal.monkey:
                currentLegs = legSprites[2];
                changeStats(statNames.ATTACK, statNames.DEFENSE);
                Debug.Log("monkey legs");
                break;
            case Animal.octopus:
                currentLegs = legSprites[3];
                changeStats(statNames.ATTACK, statNames.SPEED);
                Debug.Log("octopus legs");
                break;
            case Animal.turtle:
                currentLegs = legSprites[4];
                changeStats(statNames.DEFENSE, statNames.SPEED);
                Debug.Log("turtle legs");
                break;
            default:
                currentLegs = legSprites[2];
                changeStats(statNames.ATTACK, statNames.DEFENSE);
                Debug.Log("else, return to monke");
                break;

        }

        head.sprite = currentHead;
        torso.sprite = currentBody;
        legs.sprite = currentLegs;
    }

    // Start is called before the first frame update
    void Start()
    {
        updateParts();
    }

    public bool TakeDamage(int dmg)
    {
        int damage = dmg - defense; //Takes the dmg and reduces it by the defense stat
        

        if (isDefending) //if they are defending, take half damage
        {
            damage = damage / 2;
        }

        if (damage <= 0) //If damage is less than or equal to 0, change damage to 1
        {
            damage = 1;
        }

        currentHp -= damage; //Decrease the currentHp by damage

        isDefending = false;

        if (currentHp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        //If chimera is dead, return true. If alive, return false.
    }

    public void Heal(int amount)
    {
        currentHp += amount; //increase currentHp by amount

        if (currentHp > maxHp)
        {
            currentHp = maxHp; //Sets currentHp to MaxHP if it is higher.
        }
    }

    public void Defend()
    {
        isDefending = true;
    }
}
