using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int score = -1;
    public string playerName = "";
    public int turns = 0;
    public GameBoardBehaviour board;

    public void SetUp(string name)
    {
        score = 0;
        this.playerName = name;
        turns = -1;
    }

    public void SetUp(string name, int turns)
    {
        score = 0;
        this.playerName = name;
        this.turns = turns;
    }
}
