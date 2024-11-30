using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using TMPro;

public class BoardManager : MonoBehaviour
{
    #region Singleton
    private static BoardManager instance;
    public static BoardManager Instance => instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError($"Only one instance of {this.name} authorized !");
    }
    #endregion

    [SerializeField] private Transform playerPrefab;
    [SerializeField] private Graph graph;

    private int positionP1;
    private int positionP2;

    private BoardPlayerController player1;
    private BoardPlayerController player2;

    [SerializeField] private TextMeshProUGUI scorePlayer1Text;
    [SerializeField] private TextMeshProUGUI scorePlayer2Text;

    
    private void Start()
    {
        LoadBoardScene();
    }

    private void LoadBoardScene()
    {
        if (GameManager.Instance.GetIsBeginning())
        {
            var array = Enumerable.Range(0, graph.transform.childCount - 1).OrderBy(x => Guid.NewGuid()).ToArray();
            positionP1 = array[0];
            positionP2 = array[1];
            GameManager.Instance.SetPlayerTurn(PlayerNumber.Player1);
        }
        else
        {
            positionP1 = GameManager.Instance.GetPositionOnGraph(PlayerNumber.Player1);
            positionP2 = GameManager.Instance.GetPositionOnGraph(PlayerNumber.Player2);
            GameManager.Instance.ChangePlayerTurn();
        }
        player1 = Instantiate(playerPrefab, graph.transform.GetChild(positionP1).position, Quaternion.identity).GetComponent<BoardPlayerController>();
        player1.SetPlayerNumber(PlayerNumber.Player1);
        player1.SetCurrentNode(graph.transform.GetChild(positionP1).GetComponent<Node>());
        Instantiate(GameManager.Instance.CharacterModelP1, player1.transform);

        player2 = Instantiate(playerPrefab, graph.transform.GetChild(positionP2).position, Quaternion.identity).GetComponent<BoardPlayerController>();
        player2.SetPlayerNumber(PlayerNumber.Player2);
        player2.SetCurrentNode(graph.transform.GetChild(positionP2).GetComponent<Node>());
        Instantiate(GameManager.Instance.CharacterModelP2, player2.transform);

        if (GameManager.Instance.BoardPlayerTurn == PlayerNumber.Player1)
            player1.StartTurn();
        else
            player2.StartTurn();
        UpdateScoreText();
    }

    public void SavePlayerPositionOnBoard()
    {
        GameManager.Instance.SetPositionOnGraph(PlayerNumber.Player1, player1.CurrentNode.transform.GetSiblingIndex());
        GameManager.Instance.SetPositionOnGraph(PlayerNumber.Player2, player2.CurrentNode.transform.GetSiblingIndex());
    }

    private void UpdateScoreText()
    {
        scorePlayer1Text.text = $"Player 1: {GameManager.Instance.GetPlayerScore(PlayerNumber.Player1)}";
        scorePlayer2Text.text = $"Player 2: {GameManager.Instance.GetPlayerScore(PlayerNumber.Player2)}";
    }
}
