using System;
using UnityEngine;

public class MinigameInputManager : MonoBehaviour
{
    #region Singleton
    private static MinigameInputManager instance;
    public static MinigameInputManager Instance => instance;
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
        gameControls.Minigame.Enable();
    }

    // Clean up controls on game object destroyed
    private void OnDestroy()
    {
        gameControls.Dispose();
    }

    #region Input values

    public Vector2 MovePlayer1 => gameControls.Minigame.MovePlayer1.ReadValue<Vector2>();
    public Vector2 MovePlayer2 => gameControls.Minigame.MovePlayer2.ReadValue<Vector2>();
    public bool JumpPlayer1 => gameControls.Minigame.JumpPlayer1.ReadValue<float>() > 0;
    public bool JumpPlayer2 => gameControls.Minigame.JumpPlayer2.ReadValue<float>() > 0;
    public bool SprintPlayer1 => gameControls.Minigame.SprintPlayer1.ReadValue<float>() > 0;
    public bool SprintPlayer2 => gameControls.Minigame.SprintPlayer2.ReadValue<float>() > 0;

    #endregion
}
