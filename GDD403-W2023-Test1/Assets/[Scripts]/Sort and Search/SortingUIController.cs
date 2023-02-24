using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SortingUIController : MonoBehaviour
{
    public SortingAndSearchingScene sceneController;
    public Transform cardParent;
    public GameObject startButton;

    // Start is called before the first frame update
    void Start()
    {
        cardParent = GameObject.Find("[CARDS]").transform;
        sceneController = GetComponent<SortingAndSearchingScene>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnStartButton_Pressed()
    {
        Deal(sceneController.fourByFourLayout, 16);
        startButton.SetActive(false);
    }

    public void OnResetButton_Pressed()
    {
        sceneController.deck.Clean();

        foreach (Transform child in cardParent)
        {
            Destroy(child.gameObject);
        }

        sceneController.deck.Initialize();

        startButton.SetActive(true);
    }

    private void Deal(List<Transform> layout, int cardNumber)
    {
        // Randomizes the Cards
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
            {
                var card = sceneController.deck.Pop();
                card.SetActive(true);
                card.transform.position = layout[i].position;
            }
        }
    }
}
