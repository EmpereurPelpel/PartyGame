using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage the whole Minigame
/// </summary>
public class APMinigame : MonoBehaviour
{
    #region External references
    [SerializeField] private Transform positionP1;
    [SerializeField] private Transform positionP2;
    [SerializeField] private LayerMask trapLayer;
    [SerializeField] private GameObject TrapManager;
    #endregion

    #region Private variables
    private float timerMax = 180f;
    private float currentTimer;
    private bool isP1Dead,isP2Dead;
    #endregion

    #region Self References
    private AudioSource audioSource;
    #endregion
    
    private void Awake()
    {
        MinigameManager.Instance.OnMinigameLoaded += MinigameManager_OnMinigameLoaded;
        MinigameManager.Instance.OnMinigameStart += MinigameManager_OnMinigameStart;
        audioSource = GetComponent<AudioSource>();
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
            
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            if (!TrapManager.activeSelf)
            {
                TrapManager.SetActive(true);
            }
            
            TrapDetection();
            
            if (MinigameManager.Instance.Player1.position.y < -5)
            {
                isP1Dead = true;
            }
            if (MinigameManager.Instance.Player2.position.y < -5)
            {
                isP2Dead = true;
            }
        }
        if (MinigameManager.Instance.CurrentMinigamePhase == MinigamePhase.End)
        {
            TrapManager.SetActive(false);
            audioSource.Stop();
        }
    }

    /// <summary>
    /// Increase the difficulty over time and check if a Player has won
    /// </summary>
    private void ManageTimer()
    {
        if (currentTimer > 0 && !isP1Dead && !isP2Dead)
        {
            currentTimer -= Time.deltaTime;
            if (TrapManager.GetComponent<TrapGeneratorScript>().cooldownValue > 0.2f)
            {
                TrapManager.GetComponent<TrapGeneratorScript>().cooldownValue -= Time.deltaTime * 0.8f / 150;
                //Debug.Log(TrapManager.GetComponent<TrapGeneratorScript>().cooldownValue);
            }
        }
        else
        {
            PlayerNumber winner = isP2Dead ? PlayerNumber.Player1 : PlayerNumber.Player2;
            MinigameManager.Instance.EndMinigame(winner);
        }
    }

    /// <summary>
    /// Detect if a Player touched a trap
    /// </summary>
    private void TrapDetection()
    {
        float radius = 0.05f;
        isP1Dead = Physics.OverlapSphere(MinigameManager.Instance.Player1.position, radius, trapLayer).Length > 0;
        isP2Dead = Physics.OverlapSphere(MinigameManager.Instance.Player2.position, radius, trapLayer).Length > 0;

    }
}
