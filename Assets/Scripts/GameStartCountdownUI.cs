using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";
    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    private int previousCountdonwNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        int countdonwNumber = Mathf.CeilToInt(MinigameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countdonwNumber.ToString();
        if (previousCountdonwNumber != countdonwNumber)
        {
            previousCountdonwNumber = countdonwNumber;
            animator.SetTrigger(NUMBER_POPUP);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
