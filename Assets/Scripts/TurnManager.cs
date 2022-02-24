using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnManager : MonoBehaviour 
{
    public Player activePlayer;
    public List<Player> players = new List<Player>();
    int playerCount = 0;


    public void SetUp(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject player = new GameObject();
            player.transform.parent = this.transform;
            player.name = "Player " +(i + 1);
            Player p = player.AddComponent<Player>();
            p.SetUp("Player"+ (i + 1));
            players.Add(p);
        }
        playerCount = count;
        activePlayer = players[0];
    }

    public void SetUp(int count, int turnLimit)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject player = new GameObject();
            player.transform.parent = this.transform;
            player.name = "Player "+ ( i+ 1);
            Player p = player.AddComponent<Player>();
            p.SetUp("Player", turnLimit);
            players.Add(p);
        }
        playerCount = count;
        activePlayer = players[0];
    }

    public void NextTurn()
    {
        if (players.IndexOf(activePlayer) < playerCount - 1)
            activePlayer = players[players.IndexOf(activePlayer) + 1];
        else
            activePlayer = players[0];
    }

}
