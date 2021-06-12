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
    [SerializeField] public Sprite headSprite;
    [SerializeField] public Sprite bodySprite;
    [SerializeField] public Sprite legsSprite;

    void updatePart()
    {
        head.sprite = headSprite;
        torso.sprite = bodySprite;
        legs.sprite = legsSprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        updatePart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
