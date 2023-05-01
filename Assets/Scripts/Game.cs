using System;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private VCamTrigger pinkTrigger;
    [SerializeField] private VCamTrigger brownTrigger;
    [SerializeField] private VCamTrigger blueTrigger;
    [SerializeField] private VCamTrigger greenTrigger;

    [SerializeField] private Arrow arrow;

    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private GameObject letter;
    [SerializeField] private GameObject plant;

    [SerializeField] private GameObject startGraphics;
    [SerializeField] private GameObject startCam;
    [SerializeField] private GameObject startMusic;
    [SerializeField] private GameObject gameMusic;
    [SerializeField] private GameObject astro;

    private Character _characterToVisit;

    public enum Character
    {
        Pink = 0,
        Brown = 1,
        Blue = 2,
        Green = 3,
    }

    private enum GameState
    {
        TalkToRosette = 0,
        TalkToBrowdy,
        GiveRosetteLetter,
        GiveVerdezLetter,
        GiveAzulinPlant,
        Last,
    }

    private GameState _state;

    private bool started = false;
    
    private void Start()
    {
        _state = GameState.TalkToRosette;

        UpdateGame();
    }

    private void Update()
    {
        if (started) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            started = true;
            startGraphics.SetActive(false);
            startCam.SetActive(false);
            startMusic.SetActive(false);
            gameMusic.SetActive(true);
            astro.SetActive(true);
        }
    }

    public void TriggerEntered(int characterInt)
    {
        var character = (Character)characterInt;
        Debug.Log("character = " + character);
        UpdateDialogue(character);
        
        if (character == _characterToVisit && _state != GameState.Last)
        {
            var nextState = (int)_state + 1;
            _state = (GameState)nextState;
            UpdateGame();
        }
    }

    private void UpdateGame()
    {
        var target = _state switch
        {
            GameState.TalkToRosette => pinkTrigger,
            GameState.TalkToBrowdy => brownTrigger,
            GameState.GiveRosetteLetter => pinkTrigger,
            GameState.GiveVerdezLetter => greenTrigger,
            GameState.GiveAzulinPlant => blueTrigger,
            GameState.Last => pinkTrigger,
            _ => throw new ArgumentOutOfRangeException(),
        };

        arrow.target = target.transform;
        _characterToVisit = target.Character;

        var letterActive = _state switch
        {
            GameState.GiveRosetteLetter => true,
            GameState.GiveVerdezLetter => true,
            _ => false,
        };
        letter.SetActive(letterActive);
        
        var plantActive = _state switch
        {
            GameState.GiveAzulinPlant => true,
            _ => false,
        };
        plant.SetActive(plantActive);
    }

    private void UpdateDialogue(Character character)
    {
        var dialogue = _state switch
        {
            GameState.TalkToRosette => character switch
            {
                Character.Pink => "Oh hello friend. So good to see you on this fine day. Have you spoken with Browdy? I think he has something for you.",
                _ => "I think Rosette wanted to talk with you.",
            },
            GameState.TalkToBrowdy => character switch
            {
                Character.Pink => "I think you should speak with Browdy. Your arrow should point the way! :)",
                Character.Brown => "Took you long enough! I have a... sensitive letter I need you to give to Rosette. Don't you dare open it! I'm just a bit shy to deliver it myself is all.",
                _ => "I think Browdy wanted to talk with you.",
            },
            GameState.GiveRosetteLetter => character switch
            {
                Character.Pink => "Oh dear. Another of Browdy's propositions. I'm afraid I'm just not interested. But... while I have you here, do you think you could deliver me a letter to Verdez on the hill? DON'T OPEN IT PLEASE.",
                Character.Brown => "Get going! This is very urgent business!",
                _ => "Is that another letter for Rosette? You should see her.",
            },
            GameState.GiveVerdezLetter => character switch
            {
                Character.Pink => "Oh I do hope he takes it well... The arrow will lead you there! Across the bridge, up the stairs, and across the other bridge!",
                Character.Brown => "I hope she took it well.",
                Character.Blue => "Oh, Verdez? Yeah, he's close. Just up the bridge.",
                Character.Green => "A letter from Rosette? I'm flattered. But you see. I have someone else in mind. I'm just smitten by Azulin. Can you give her this plant?",
                _ => "",
            },
            GameState.GiveAzulinPlant => character switch
            {
                Character.Pink => "How did Verdez like my letter??",
                Character.Brown => "I should get into my best space suit.",
                Character.Blue => "A plant? From Verdez? That's quaint. Lots of those around. I think though, that I may just write a letter to Rosette. She's quite the catch. [Game Over!]",
                Character.Green => "Please give Azulin my fondest regards.",
                _ => "",
            },
            GameState.Last => character switch
            {
                _ => "Thank you for playing!",
            },
            _ => throw new ArgumentOutOfRangeException(),
        };

        dialogueText.text = dialogue;
    }
}
