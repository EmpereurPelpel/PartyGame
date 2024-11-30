using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage the sweeping trap's behaviour
/// </summary>
public class SweepingTrapScript : MonoBehaviour
{

    

    #region Self references
    private GameObject trap;
    private GameObject indicator;
    private AudioSource audioSource;
    #endregion

    #region Trap Parameters
    [SerializeField] private float sweepSpeed = 10;
    private float sweepLimit = 25;
    private float sweepingCooldown;
    private float cooldownValue = 1;
    #endregion

    #region Initialization Parameters
    private Vector3 startPos;
    [SerializeField] private bool isRight;
    private int sign;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        trap = transform.GetChild(0).gameObject;
        indicator = transform.GetChild(1).gameObject;
        sweepingCooldown = cooldownValue;
        startPos = trap.transform.position;
        sign = isRight ? 1 : -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (sweepingCooldown > 0)
            {
                sweepingCooldown -= Time.deltaTime;
            }
            else
            {
                trap.transform.position -= new Vector3(sign *Time.deltaTime * sweepSpeed, 0, 0);
                indicator.SetActive(false);
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                if (Mathf.Abs(trap.transform.position.x) >= sweepLimit)
                {
                    sweepingCooldown = cooldownValue;
                    Reset();
                    audioSource.Stop();

                }


            }
        }
    }

    private void Reset()
    {
        indicator.SetActive(true);
        trap.transform.position = startPos;
        gameObject.SetActive(false);
        sweepingCooldown = cooldownValue;
    }
}
