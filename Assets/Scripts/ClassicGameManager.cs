using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ClassicGameManager : GameManager
{ 

    protected override void GoodPair()
    {
        turnManager.activePlayer.board.PairsLeft--;

        if(playerCount > 1)
            turnManager.activePlayer.score++;
    }

    protected override IEnumerator EndTurn()
    {
        yield return new WaitForEndOfFrame();
        if (playerCount == 1)
        {
            yield return new WaitForEndOfFrame();
            turnManager.activePlayer.turns--;

            yield return new WaitForEndOfFrame();
            if (turnManager.activePlayer.board.PairsLeft == 0)
                state = GameState.Win;
            else if (turnManager.activePlayer.turns <= 0)
                state = GameState.Lose;
        }
        else
        {
            if (turnManager.activePlayer.board.PairsLeft <= 0)
            {
                state = GameState.GameOver;
                int max = 0;
                foreach(var player in turnManager.players)
                {
                    if (player.score > max)
                        winner = player;
                }
            }
        }

        yield return new WaitForSeconds(1F);
        turnManager.NextTurn();
        UpdateUI();
        activeTurn = false;

    }

    protected override void UpdateUI()
    {
        if(playerCount == 1)
        {
            canvas.ModifyText("Turnos Restantes", "Turnos: " + turnManager.activePlayer.turns);
            canvas.ModifyText("Pares Restantes", "Pares Restantes: " + turnManager.activePlayer.board.PairsLeft);
        }
        else
        {
            canvas.ModifyText("Jugador Actual", "Jugador Actual: " + turnManager.activePlayer.playerName);
            canvas.ModifyText("Puntuacion", "Puntuación: " + turnManager.activePlayer.score);
        }
        canvas.PlayNextTurnAnim();
    }

    public override void EndGame()
    {
        if(state != GameState.Active)
            canvas.ActivateEndGame();
        switch(state)
        {
            case GameState.Win:
                canvas.ModifyText("Resultado", "¡Tu ganas!");
                StartCoroutine("BackToMenu");
                break;
            case GameState.Lose:
                canvas.ModifyText("Resultado", "¡Tu pierdes!");
                StartCoroutine("BackToMenu");
                break;
            case GameState.GameOver:
                canvas.ModifyText("Resultado", winner.playerName  +" gana!");
                StartCoroutine("BackToMenu");
                break;
            default:
                break;
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
        GameObject board = Instantiate(gameBoardPrefab);
        board.transform.parent = this.gameObject.transform;

        yield return new WaitForEndOfFrame();
        GameBoardBehaviour boardB = board.GetComponent<GameBoardBehaviour>();
        boardB.SetUpBoard(cardCount);

        yield return new WaitForEndOfFrame();
        foreach (Player player in turnManager.players)
        {
            player.board = boardB;
        }

        yield return new WaitForEndOfFrame();
        UpdateUI();
    }

}
