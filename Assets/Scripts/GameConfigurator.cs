using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMode
{
    Classic, MultiGB
}
public class GameConfigurator : Singleton<GameConfigurator>
{
    public GameMode mode = GameMode.Classic;
    [Range(1,4)]
    public int playerCount = 1;
    [Range(2,24)]
    public int cardCount = 20;
    public GameObject gameboardPrefab;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartGame()
    {
        StartCoroutine("SetUpGameManager");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(this.gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator SetUpGameManager()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync("Game");
        while (!load.isDone)
            yield return null ;
        yield return new WaitForSeconds(1F);
        GameObject manager = new GameObject();
        manager.name = "GameManager";
        switch(mode)
        {
            case GameMode.Classic:
                {
                    GameManager gManager = manager.AddComponent<ClassicGameManager>();

                    yield return new WaitForEndOfFrame();
                    gManager.gameBoardPrefab = gameboardPrefab;
                    gManager.SetUpGame(playerCount, cardCount);
                    break;
                }
            case GameMode.MultiGB:
                {
                    GameManager gManager = manager.AddComponent<MultiGBGameManager>();
                    if (playerCount == 1)
                        playerCount++;
                    yield return new WaitForEndOfFrame();
                    gManager.gameBoardPrefab = gameboardPrefab;
                    gManager.SetUpGame(playerCount, cardCount);
                    break;
                }
            default:
                break;
        }
    }
}
