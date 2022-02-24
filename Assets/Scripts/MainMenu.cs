using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject configUI;
    Button classic;
    Button multi;
    Slider cardC;
    Slider playerC;
    void Start()
    {
        classic = GameObject.Find("Classic").GetComponent<Button>();
        multi = GameObject.Find("MultiBoard").GetComponent<Button>();
        cardC = GameObject.Find("Pares").GetComponent<Slider>();
        playerC = GameObject.Find("Jugadores").GetComponent<Slider>();
        DeactivateConfig();
    }

    public void ActivateClassic()
    {
        GameConfigurator.Instance.mode = GameMode.Classic;
        classic.image.color = Color.blue;
        multi.image.color = Color.white;
    }

    public void ActivateMulti()
    {
        GameConfigurator.Instance.mode = GameMode.MultiGB;
        multi.image.color = Color.blue;
        classic.image.color = Color.white;
    }

    public void SetCardCount()
    {
        GameConfigurator.Instance.cardCount = (int )cardC.value * 2;
        cardC.gameObject.GetComponentInChildren<Text>().text = "" +(int)cardC.value;
    }

    public void SetPlayerCount()
    {
        GameConfigurator.Instance.playerCount = (int)playerC.value;
        playerC.gameObject.GetComponentInChildren<Text>().text = "" + (int)playerC.value;
    }

    public void StartGame()
    {
        GameConfigurator.Instance.StartGame();
    }

    public void ActivateConfig()
    {
        configUI.SetActive(true);
    }

    public void DeactivateConfig()
    {
        configUI.SetActive(false);
    }

    public void Exit()
    {
        GameConfigurator.Instance.QuitGame();
    }
}
