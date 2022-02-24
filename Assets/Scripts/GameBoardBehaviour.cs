using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardBehaviour : MonoBehaviour
{
    public GameObject instanceCard;
    public List<CardBehaviour> cards;
    private int pairsLeft;
    public int PairsLeft { get { return pairsLeft; } set { pairsLeft = value; } }
    int cardCount;

    // Start is called before the first frame update
    void Start()
    {
        cards = new List<CardBehaviour>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            SetUpBoard(12);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            InstantiateCards(12);
        }
        else if(Input.GetKeyDown(KeyCode.F3))
        {
            FlipCards();
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            ShuffleCards();
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            SetCards();
        }
    }

    public void ShuffleCards()
    {
        List<CardBehaviour> randomizedList = new List<CardBehaviour>();
        while (cards.Count > 0)
        {
            int index = Random.Range(0, cards.Count - 1); 
            randomizedList.Add(cards[index]); 
            cards.RemoveAt(index);
        }
        cards = randomizedList;
    }

    public void FlipCards()
    {
        foreach(var card in cards)
        {
            card.GetComponent<CardBehaviour>().Flip();
        }
    }

    public void SetCards()
    {
        for(int i = 0; i < cards.Count;i++)
        {
            int x = (i % 4) * 3;
            int z = (i / 4) * 2;
            cards[i].transform.position = new Vector3(x, 0, z);
        }
    }

    public void CenterCards()
    {
        //TODO animar las cartas al centro
    }

    public void AssignType()
    {
        int i = 0;
        foreach(var card in cards)
        {
            CardBehaviour cardB = card.GetComponent<CardBehaviour>();
            cardB.type = i/2;
            cardB.ReloadImage();
            i++;
        }
    }

    public void InstantiateCards(int count)
    {
        for(int i = 0; i < count; i++)
        {
            var obj = Instantiate(instanceCard).GetComponent<CardBehaviour>();
            obj.transform.SetParent(this.transform);
            obj.transform.position = new Vector3(0, 0, 0);
            cards.Add(obj);
        }
        this.pairsLeft = count / 2;
        this.cardCount = count;
    }

    public void SetUpBoard(int cardCount)
    {
        InstantiateCards(cardCount);
        StartCoroutine("PrepareBoard");
    }

    IEnumerator PrepareBoard()
    {
        yield return new WaitForEndOfFrame();
        FlipCards();
        yield return new WaitForEndOfFrame();
        AssignType();
        yield return new WaitForEndOfFrame();
        ShuffleCards();
        yield return new WaitForEndOfFrame();
        SetCards();
    }
}
