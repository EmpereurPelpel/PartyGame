using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPMinigame : MonoBehaviour
{
    [SerializeField] private Transform positionP1;
    [SerializeField] private Transform positionP2;

    private float timerMax = 5f;
    private float currentTimer;
    private void Awake() 
    {
        MinigameManager.Instance.OnMinigameLoaded += MinigameManager_OnMinigameLoaded;
        MinigameManager.Instance.OnMinigameStart += MinigameManager_OnMinigameStart;
    }

    private void MinigameManager_OnMinigameLoaded(object sender, System.EventArgs e)
    {
        MinigameManager.Instance.Player1.position = positionP1.position;
        MinigameManager.Instance.Player2.position = positionP2.position;
    }

    private void MinigameManager_OnMinigameStart(object sender, System.EventArgs e)
    {
        currentTimer = timerMax;
    }

    private void Update()
    {
        if (MinigameManager.Instance.CurrentMinigamePhase == MinigamePhase.Playing)
        {
            ManageTimer();
        }
    }

    private void ManageTimer()
    {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }
        else
        {
            PlayerNumber winner = Random.Range(1, 6) % 2 == 0 ? PlayerNumber.Player1 : PlayerNumber.Player2;
            MinigameManager.Instance.EndMinigame(winner);
        }
    }
}
