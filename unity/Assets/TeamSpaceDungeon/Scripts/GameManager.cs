using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [Header("Objects")]
    public GameObject VRPlayer;
    [HideInInspector] public VRPlayer VRPlayerScript;
    public ColManager ColMgr;

    [Header("GameOver")]
    public string winMessage = "You won!";
    public string lossMessage = "You lost";

    [Header("GameState")]
    public gState GameState = gState.menu;
    public enum gState { menu, play, gameover };


    private Text winLossText;
    private bool loadingNewScene = true;
    [HideInInspector] public bool victory = false;

    private void Awake() { Initialize(); }

    void Initialize()
    {
        DontDestroyOnLoad(this.gameObject);
        AttemptToLoadPlayerGO();
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void Update()
    {
        GameOverScene();
        MenuScene();
    }

    /// <summary>
    /// Returns the distance between a given point and the VR player 
    /// </summary>
    /// <param name="inPoint">Given point</param>
    /// <returns>The distance between a given point and the VR player</returns>
    public float GetDistanceToVRPlayer(Vector3 inPoint)
    {
        return Vector3.Distance(inPoint, VRPlayer.transform.position);
    }

    private void GameOverScene()
    {
        if(GameState == gState.gameover)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GoToMenu();
            }
            if(winLossText == null)
            {
                winLossText = GameObject.Find("WinLose").GetComponent<Text>();
                if(victory)
                {
                    winLossText.text = winMessage;
                } else
                {
                    winLossText.text = lossMessage;
                }
            }
        }
    }

    private void MenuScene()
    {
        if (GameState == gState.menu)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GoToMain();
            }
        }
    }

    public void GoToMenu()
    {
        GameState = gState.menu;
        LoadNewScene("menu");
    }

    public void GoToMain()
    {
        GameState = gState.play;
        LoadNewScene("main");
    }

    public void GoToGameOver()
    {
        GameState = gState.gameover;
        LoadNewScene("gameover");
    }

    private void LoadNewScene(string sceneName)
    {
        loadingNewScene = true;
        SceneManager.LoadScene(sceneName);
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        loadingNewScene = false;
        AttemptToLoadPlayerGO();
    }

    private void AttemptToLoadPlayerGO()
    {
        VRPlayer = GameObject.Find("VRPlayer");
        if (VRPlayer != null) { VRPlayerScript = VRPlayer.GetComponent<VRPlayer>(); }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
}
