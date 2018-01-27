using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject VRPlayer;
    [HideInInspector] public VRPlayer VRPlayerScript;

    public ColManager ColMgr;

    public enum gState { menu, play, gameover };
    public gState GameState = gState.play;

    private void Awake()
    {
        VRPlayerScript = VRPlayer.GetComponent<VRPlayer>(); 
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

    /// <summary>
    /// Gameover events
    /// </summary>
    public void GameOver()
    {
        GameState = gState.gameover;
    }


}
