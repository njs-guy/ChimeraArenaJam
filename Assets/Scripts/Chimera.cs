using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chimera : MonoBehaviour
{
    public enum Animal
    {
        bird,
        bunny,
        monkey,
        octopus,
        turtle
    }

    [SerializeField] public SpriteRenderer head;
    [SerializeField] public SpriteRenderer torso;
    [SerializeField] public SpriteRenderer legs;

    [SerializeField] public int hp = 30;
    [SerializeField] public int attack = 5;
    [SerializeField] public int defense = 5;
    [SerializeField] public int speed = 5;

    [SerializeField] public Animal headPart;
    [SerializeField] public Animal bodyPart;
    [SerializeField] public Animal legPart;
    [SerializeField] public Sprite[] headSprites;
    [SerializeField] public Sprite[] bodySprites;
    [SerializeField] public Sprite[] legSprites;

    //Assigns each part sprite
    void updateParts()
    {
        //checks what animal each part should be, and changes the sprite
        Sprite currentHead;
        Sprite currentBody;
        Sprite currentLegs;


        switch(headPart)
        {
            case Animal.bird:
                currentHead = headSprites[0];
                Debug.Log("bird head");
                break;
            case Animal.bunny:
                currentHead = headSprites[1];
                Debug.Log("bunny head");
                break;
            case Animal.monkey:
                currentHead = headSprites[2];
                Debug.Log("monkey head");
                break;
            case Animal.octopus:
                currentHead = headSprites[3];
                Debug.Log("octopus head");
                break;
            case Animal.turtle:
                currentHead = headSprites[4];
                Debug.Log("turtle head");
                break;
            default:
                currentHead = headSprites[2];
                Debug.Log("else, return to monke");
                break;
            
        }

        switch(bodyPart)
        {
            case Animal.bird:
                currentBody = bodySprites[0];
                Debug.Log("bird body");
                break;
            case Animal.bunny:
                currentBody = bodySprites[1];
                Debug.Log("bunny body");
                break;
            case Animal.monkey:
                currentBody = bodySprites[2];
                Debug.Log("monkey body");
                break;
            case Animal.octopus:
                currentBody = bodySprites[3];
                Debug.Log("octopus body");
                break;
            case Animal.turtle:
                currentBody = bodySprites[4];
                Debug.Log("turtle body");
                break;
            default:
                currentBody = bodySprites[2];
                Debug.Log("else, return to monke");
                break;

        }

        switch(legPart)
        {
            case Animal.bird:
                currentLegs = legSprites[0];
                Debug.Log("bird legs");
                break;
            case Animal.bunny:
                currentLegs = legSprites[1];
                Debug.Log("bunny legs");
                break;
            case Animal.monkey:
                currentLegs = legSprites[2];
                Debug.Log("monkey legs");
                break;
            case Animal.octopus:
                currentLegs = legSprites[3];
                Debug.Log("octopus legs");
                break;
            case Animal.turtle:
                currentLegs = legSprites[4];
                Debug.Log("turtle legs");
                break;
            default:
                currentLegs = legSprites[2];
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
