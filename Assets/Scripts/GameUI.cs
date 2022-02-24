using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    Dictionary<string, Text> textComponents = new Dictionary<string, Text>();
    GameObject endGame;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        endGame = GameObject.Find("EndGame");
        anim = GetComponentInChildren<Animator>();
        foreach(var obj in GetComponentsInChildren<Text>())
        {
            textComponents.Add(obj.name, obj);
        }
        endGame.SetActive(false);
    }

    public Text GetText(string textName)
    {
        return textComponents[textName];
    }

    public void ModifyText(string textName, string text)
    {
        textComponents[textName].text = text;
    }

    public void BackToMenu()
    {
        GameConfigurator.Instance.MainMenu();
    }

    public void PlayNextTurnAnim()
    {
        anim.SetTrigger("NextTurn");
    }

    public void ActivateEndGame()
    {
        endGame.SetActive(true);
    }

}
