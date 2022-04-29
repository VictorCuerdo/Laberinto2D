using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text player1text;
    public Text player2text;
    [HideInInspector]
    public int player1Points = 0;
    [HideInInspector]
    public int player2Points = 0;
    [HideInInspector]
    public turn turno =  turn.player1;
    [HideInInspector]
    public turn stripes;
    [HideInInspector]
    public bool repetirturno;
    [HideInInspector]
    public turnphase faseactual = turnphase.apuntar;
    public GameObject cue;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public enum turn
    {
        player1, 
        player2
    }
    public enum turnphase
    {
        apuntar,
        moviendo
    }
    public void changeTurn()
    {
        if(turno == turn.player1)
        {
            turno = turn.player2;
        }
        else
        {
            turno = turn.player1;
        }
    }
    public void addPoint(int num)
    {
        if (player1Points == 0 && player2Points == 0)
        {
            if (num>8)
            {
                stripes = turno;
            }
            else
            {
                if (turno == turn.player1)
                {
                    stripes = turn.player2;
                }
                else
                {
                    stripes = turn.player1;
                }
            }
        }
        if (num >= 8)
        {
            if(stripes == turn.player1)
            {
                player1Points++;
                player1text.text = "Player 1: "+ player1Points+" Pts";
            }
            else
            {
                player2Points++;
                player2text.text = "Player 2: " + player2Points + " Pts";
            }
        }
        else
        {
            if (stripes == turn.player1)
            {
                player2Points++;
                player2text.text = "Player 2: " + player2Points + " Pts";
            }
            else
            {
                player1Points++;
                player1text.text = "Player 1: " + player1Points + " Pts";
            }
        }
    }
    public void setStripes(turn player)
    {
        stripes = player;
    }

    public void fault()
    {

    }
}
