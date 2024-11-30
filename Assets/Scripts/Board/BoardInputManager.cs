using System;
using UnityEngine;

public class BoardInputManager : MonoBehaviour
{
    #region Singleton
    private static BoardInputManager instance;
    public static BoardInputManager Instance => instance;

    private void InitSingleton()
    {
        if (instance == null)
            instance = this;
        else
            throw new Exception($"Only one instance of {this.name} allowed! ");
    }
    #endregion

    private GameControls gameControls;

    // Init the singleton and enable controls
    private void Awake()
    {
        InitSingleton();
        gameControls = new GameControls();
        gameControls.Board.Enable();
    }

    // Clean up controls on game object destroyed
    private void OnDestroy()
    {
        gameControls.Dispose();
    }

    #region Input values
    public bool Roll => gameControls.Board.Roll.ReadValue<float>() > 0;
    #endregion
}
