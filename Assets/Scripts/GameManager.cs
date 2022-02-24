using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Active,Win,Lose,GameOver
}

public abstract class GameManager : MonoBehaviour
{

    static GameManager instance = null;

    public static GameManager Instance { get { return instance; } }


    public TurnManager turnManager;
    public Player winner;
    protected List<GameObject> selectedCards;

    public int cardCount = 12;
    public int playerCount = 1;
    public int turnLimit = 15;
    public int scoreToWin = 5;
    public GameState state;
    public GameObject gameBoardPrefab;
    public GameUI canvas;
    public bool activeTurn = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public virtual void SetUpGame(int playerCount, int cardCount)
    {
        this.selectedCards = new List<GameObject>(2);
        this.state = GameState.Active;
        this.playerCount = playerCount;
        this.cardCount = cardCount;
        this.canvas = GameObject.Find("Canvas").GetComponent<GameUI>();
        GameObject tManager = new GameObject();
        tManager.transform.parent = this.transform;
        tManager.name = "TurnManager";

        if (playerCount == 1)
        {
            tManager.AddComponent<TurnManager>().SetUp(playerCount,turnLimit);
            canvas.GetText("Jugador Actual").gameObject.SetActive(false);
            canvas.GetText("Puntuacion").gameObject.SetActive(false);
        }
        else
        {
            tManager.AddComponent<TurnManager>().SetUp(playerCount);
            canvas.GetText("Turnos Restantes").gameObject.SetActive(false);
            canvas.GetText("Pares Restantes").gameObject.SetActive(false);

        }

        turnManager = tManager.GetComponent<TurnManager>();
    }
    public virtual void SelectCard(GameObject card)
    {
        StartCoroutine("CardSelection", card);
    }
    protected virtual IEnumerator CardSelection(GameObject card)
    {
        yield return new WaitForEndOfFrame();
        CardBehaviour cardB = card.GetComponent<CardBehaviour>();
        if (cardB.isFlipped && !activeTurn)
        {
            if (selectedCards.Count < 2)
            {
                yield return new WaitForEndOfFrame();
                selectedCards.Add(card);
                cardB.Flip();
            }
            yield return new WaitForSeconds(0.3f);
            CheckForPair();
        }
    }
    public virtual void CheckForPair()
    {
        if (selectedCards.Count == 2)
        {
            CardBehaviour card1 = selectedCards[0].GetComponent<CardBehaviour>();
            CardBehaviour card2 = selectedCards[1].GetComponent<CardBehaviour>();
            activeTurn = true;
            if (card1.type == card2.type)
            {
                //PAR IGUAL
                //Eliminar cartas
                //Terminar Turno
                GoodPair();
                StartCoroutine("EndTurn");

            }
            else
            {
                //PAR DIFERENTE
                //Terminar turno
                card1.Flip();
                card2.Flip();
                StartCoroutine("EndTurn");
            }
            selectedCards.Clear();
        }
    }
    protected abstract void GoodPair();
    protected abstract IEnumerator EndTurn();//Debe colocar la variable activeTurn en false al terminar
    protected abstract void UpdateUI(); 
    protected abstract IEnumerator SetBoard();
    public abstract void EndGame();
    protected virtual IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(3);
        GameConfigurator.Instance.MainMenu();
    }
    public virtual void Update()
    {
        EndGame();
    }

}
