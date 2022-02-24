using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MultiGBGameManager : GameManager
{
    public Camera gameCamera;

    void Start()
    {
        gameCamera = FindObjectOfType<Camera>();
    }
    protected override void GoodPair()
    {
        turnManager.activePlayer.board.PairsLeft--;
        turnManager.activePlayer.score++;
    }
    protected override IEnumerator EndTurn()
    {
        yield return new WaitForEndOfFrame();
        if (turnManager.activePlayer.score >= scoreToWin)
        {
            state = GameState.GameOver;
            winner = turnManager.activePlayer;
        }
        yield return new WaitForSeconds(1F);
        Vector3 camPos = gameCamera.transform.position;
        int playerInx = turnManager.players.IndexOf(turnManager.activePlayer);
        gameCamera.transform.position = new Vector3((playerInx*25)+4,12.5f,5);

        yield return new WaitForEndOfFrame();
        turnManager.NextTurn();
        UpdateUI();
        activeTurn = false;
    }
    protected override void UpdateUI()
    {
        canvas.ModifyText("Jugador Actual", "Jugador Actual: " + turnManager.activePlayer.playerName);
        canvas.ModifyText("Puntuacion", "Puntuación: " + turnManager.activePlayer.score);
        canvas.PlayNextTurnAnim();
    }
    public override void EndGame()
    {
        if(state == GameState.GameOver)
        {
            canvas.ActivateEndGame();
            canvas.ModifyText("Resultado", winner.playerName + " gana!");
            StartCoroutine("BackToMenu");
        }
    }
    public override void SetUpGame(int playerCount, int cardCount)
    {
        base.SetUpGame(playerCount, cardCount);
        StartCoroutine("SetBoard");
    }
    protected override IEnumerator SetBoard()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < turnManager.players.Count;i++)
        {
            GameObject board = Instantiate(gameBoardPrefab);
            board.transform.parent = this.gameObject.transform;

            yield return new WaitForEndOfFrame();

            GameBoardBehaviour boardB = board.GetComponent<GameBoardBehaviour>();
            boardB.SetUpBoard(cardCount);
            yield return new WaitForEndOfFrame();

            turnManager.players[i].board = boardB;
        }

        yield return new WaitForSeconds(0.5F);
        for (int i = 0; i < turnManager.players.Count;i++)
        {
            turnManager.players[i].board.transform.Translate(new Vector3(i * 25, 0, 0));
        }

        UpdateUI();
    }
}
