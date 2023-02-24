using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TMP_Dropdown difficultyDropdown;
    public Difficulty difficulty;
    public GameController gameController;
    public Transform cardParent;
    public GameObject startButton;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GetComponent<GameController>();
        difficultyDropdown = FindObjectOfType<TMP_Dropdown>();
        difficulty = Difficulty.EASY;
        cardParent = GameObject.Find("[CARDS]").transform;
    }

    public void OnDifficulty_Changed()
    {
        difficulty = (Difficulty)difficultyDropdown.value;
    }

    public void OnStartButton_Pressed()
    {
        
        switch (difficulty)
        {
            case Difficulty.EASY: 
                Deal(gameController.twoByFourLayout, 8);
                break;
            case Difficulty.NORMAL:
                Deal(gameController.fourByFourLayout, 16);
                break;
            case Difficulty.HARD:
                Deal(gameController.sixBySixLayout, 36);
                break;
        }

        startButton.SetActive(false);
    }

    public void OnResetButton_Pressed()
    {
        gameController.deck.Clean();

        foreach (Transform child in cardParent)
        {
            Destroy(child.gameObject);
        }

        gameController.deck.Initialize();

        startButton.SetActive(true);
    }

    private void Deal(List<Transform> layout, int cardNumber)
    {
        for (var i = 0; i < layout.Count; i++)
        {
            var randomIndex = Random.Range(0, layout.Count);
            if (i != randomIndex)
            {
                (layout[i], layout[randomIndex]) = (layout[randomIndex], layout[i]);
            }
        }

        for (var i = 0; i < cardNumber; i++)
        {
            if (i == 0 || i % 2 == 0)
            {
                var firstCard = gameController.deck.Pop();
                firstCard.SetActive(true);
                firstCard.GetComponent<Card>().Flip();
                var secondCard = Instantiate(firstCard);
                secondCard.transform.SetParent(cardParent);
                firstCard.transform.position = layout[i].position;
                secondCard.transform.position = layout[i + 1].position;
            }
        }
    }
}
