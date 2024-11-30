using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerNumber
{
    Player1,
    Player2
}
public class ScoreManager : MonoBehaviour
{
    private Dictionary<PlayerNumber, int> playerScoreDict = new Dictionary<PlayerNumber, int>();

    private int scoreLimit = 3;

    private void Start()
    {
        Init();
        DontDestroyOnLoad(gameObject);
    }

    private void Init()
    {
        playerScoreDict.Clear();
        playerScoreDict.Add(PlayerNumber.Player1, 0);
        playerScoreDict.Add(PlayerNumber.Player2, 0);
    }

    public void AddVictoryToPlayer(PlayerNumber playerNumber)
    {
        playerScoreDict[playerNumber]++;
        if (playerScoreDict[playerNumber] > scoreLimit)
        {
            GameManager.Instance.EndGame(playerNumber);
        }
    }

    public int GetPlayerScore(PlayerNumber playerNumber)
    {
        return playerScoreDict[playerNumber];
    }
}
