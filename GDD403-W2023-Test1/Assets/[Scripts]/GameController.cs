using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Transform> twoByFourLayout;
    public List<Transform> fourByFourLayout;
    public List<Transform> sixBySixLayout;
    public StandardDeck deck;

    // Start is called before the first frame update
    void Start()
    {
        deck = new StandardDeck(); // example of composition
    }
}
