using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Singleton
    private static GameManager instance;
    public static GameManager Instance => instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError($"Only one instance {this.name} authorized");
    }
    #endregion

    private Transform characterModelP1;
    private Transform characterModelP2;
    public Transform CharacterModelP1 => characterModelP1;
    public Transform CharacterModelP2 => characterModelP2;

    private ScoreManager scoreManager;

    private bool isBeginning;

    private PlayerNumber boardPlayerTurn;
    public PlayerNumber BoardPlayerTurn => boardPlayerTurn;

    private int positionOnGraphP1;
    private int positionOnGraphP2;

    [SerializeField] private List<string> minigameSceneNameList = new List<string>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        //ChangeToMinigameScene();
        scoreManager = transform.AddComponent<ScoreManager>();
    }

    public void StartGame(Transform characterModelP1, Transform characterModelP2)
    {
        this.characterModelP1 = characterModelP1;
        this.characterModelP2 = characterModelP2;
        ChangeToBoardScene();
        isBeginning = true;
        boardPlayerTurn = PlayerNumber.Player1;
    }

    public void ChangeToBoardScene()
    {
        SceneManager.LoadScene("BoardScene", LoadSceneMode.Single);
    }

    public void EndMinigame(PlayerNumber winner)
    {
        scoreManager.AddVictoryToPlayer(winner);
    }

    public void EndGame(PlayerNumber winner)
    {
        
    }

    public void LoadMinigameFromNode(Node node)
    {
        SceneManager.LoadScene(minigameSceneNameList[0], LoadSceneMode.Single);
    }

    public bool GetIsBeginning()
    {
        if (isBeginning)
        {
            isBeginning = false;
            return true;
        }
        return false;
    }

    public void ChangePlayerTurn()
    {
        if (boardPlayerTurn == PlayerNumber.Player1)
            boardPlayerTurn = PlayerNumber.Player2;
        else
            boardPlayerTurn = PlayerNumber.Player1;
    }

    public void SetPlayerTurn(PlayerNumber playerTurn)
    {
        boardPlayerTurn = playerTurn;
    }

    public void SetPositionOnGraph(PlayerNumber player, int position)
    {
        if (player == PlayerNumber.Player1)
            positionOnGraphP1 = position;
        else
            positionOnGraphP2 = position;
    }

    public int GetPositionOnGraph(PlayerNumber player)
    {
        if (player == PlayerNumber.Player1)
            return positionOnGraphP1;
        else
            return positionOnGraphP2;
    }

    public int GetPlayerScore(PlayerNumber playerNumber)
    {
        return scoreManager.GetPlayerScore(playerNumber);
    }
}
