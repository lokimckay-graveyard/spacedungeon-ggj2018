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
    private SimpleHelvetica[] winLossTextVR = new SimpleHelvetica[4];
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
                GameObject winLossGO = GameObject.Find("WinLose");
                if (winLossGO)
                {
                    winLossText = winLossGO.GetComponent<Text>();
                    if (victory)
                    {
                        winLossText.text = winMessage;
                    }
                    else
                    {
                        winLossText.text = lossMessage;
                    }
                }
            }
            if(winLossTextVR[0] == null)
            {
                GameObject[] GOs = GameObject.FindGameObjectsWithTag("WonLostVRText");
                if (GOs.Length > 0)
                {
                    for (int i = 0; i < GOs.Length; i++)
                    {
                        winLossTextVR[i] = GOs[i].GetComponent<SimpleHelvetica>();
                        if (victory)
                        {
                            winLossTextVR[i].Text = winMessage;
                        }
                        else
                        {
                            winLossTextVR[i].Text = lossMessage;
                        }
                        winLossTextVR[i].GenerateText();
                    }
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
        victory = false;
    }

    public void GoToMain()
    {
        GameState = gState.play;
        LoadNewScene("main");
        victory = false;
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
