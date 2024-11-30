using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private List<Transform> characterModelPrefabList = new List<Transform>();

    private int indexCharacterModelP1;
    private int indexCharacterModelP2;

    public int IndexCharacterModelP1 => indexCharacterModelP1;
    public int IndexCharacterModelP2 => indexCharacterModelP2;

    [SerializeField] private Transform player1Container;
    [SerializeField] private Transform player2Container;

    [SerializeField] private Button btnNextP1;
    [SerializeField] private Button btnNextP2;
    [SerializeField] private Button btnPrevP1;
    [SerializeField] private Button btnPrevP2;

    private void Start()
    {
        ChangeCharacterModel(0, true);
        ChangeCharacterModel(1, false);

        btnNextP1.onClick.AddListener(() => NextCharacterModel(isPlayer1: true, isNext:true));
        btnNextP2.onClick.AddListener(() => NextCharacterModel(isPlayer1: false, isNext:true));
        btnPrevP1.onClick.AddListener(() => NextCharacterModel(isPlayer1: true, isNext:false));
        btnPrevP2.onClick.AddListener(() => NextCharacterModel(isPlayer1: false, isNext:false));
    }

    private void ChangeCharacterModel(int characterModelIndex, bool isPlayer1)
    {
        Transform playerContainer;

        if (isPlayer1)
            playerContainer = player1Container;
        else
            playerContainer = player2Container;

        try
        {
            Destroy(playerContainer.GetChild(0).gameObject);
        }
        catch
        {
            // Do nothing
        }
        if (isPlayer1)
            indexCharacterModelP1 = characterModelIndex;
        else
            indexCharacterModelP2 = characterModelIndex;
        Instantiate(characterModelPrefabList[characterModelIndex], playerContainer);
    }

    public void NextCharacterModel(bool isPlayer1, bool isNext)
    {
        int indexToChange;
        int otherPlayerIndex;
        if (isPlayer1)
        {
            indexToChange = indexCharacterModelP1;
            otherPlayerIndex = indexCharacterModelP2;
        }
        else
        {
            indexToChange = indexCharacterModelP2;
            otherPlayerIndex = indexCharacterModelP1;
        }

        int indexOfNextCharacterModel = Helper.Mod(indexToChange + (isNext ? 1 : -1), characterModelPrefabList.Count);
        if (indexOfNextCharacterModel == otherPlayerIndex)
        {
            indexOfNextCharacterModel = Helper.Mod(indexOfNextCharacterModel + (isNext ? 1 : -1), characterModelPrefabList.Count);
        }
        ChangeCharacterModel(indexOfNextCharacterModel, isPlayer1);
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame(
            characterModelPrefabList[indexCharacterModelP1],
            characterModelPrefabList[indexCharacterModelP2]
        );
    }
}
