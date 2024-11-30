using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage the falling trap behaviour
/// </summary>
public class FallingTrapScript : MonoBehaviour
{

    #region Self References
    private GameObject trap;
    private GameObject zone;
    private AudioSource audioSource;
    #endregion

    #region Trap Parameters
    private bool hasFallen;
    [SerializeField] private float fallSpeed = 10;
    private float fallingCooldown;
    private float cooldownValue = 1;
    private float minY = 3;
    private float maxY = 30;
    private float zoneMaxScale = 6;
    #endregion

    #region Initialization Parameters
    private Vector3 trapStartPos;
    private Vector3 zoneStartScale;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        trap = transform.GetChild(0).gameObject;
        zone = transform.GetChild(1).gameObject;
        fallingCooldown = cooldownValue;
        audioSource = GetComponent<AudioSource>();
        trapStartPos = trap.transform.position;
        zoneStartScale = zone.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (fallingCooldown > 0)
            {
                fallingCooldown -= Time.deltaTime;
                zone.transform.localScale += new Vector3(Time.deltaTime * zoneMaxScale,0,Time.deltaTime*zoneMaxScale);
            }
            else if (!hasFallen)
            {
                trap.transform.position -= new Vector3(0, Time.deltaTime * fallSpeed, 0);
                if (trap.transform.position.y <= minY)
                {
                    audioSource.Play();
                    trap.transform.position = new Vector3(trap.transform.position.x, minY, trap.transform.position.z);
                    zone.SetActive(false);
                    fallingCooldown = cooldownValue;
                    hasFallen = true;
                }


            }
            else
            {
                trap.transform.position += new Vector3(0, Time.deltaTime * fallSpeed, 0);
                if (trap.transform.position.y >= maxY)
                {
                    Reset();
                }
            }
        }
    }

    private void Reset()
    {
        zone.SetActive(true);
        trap.transform.position = trapStartPos;
        zone.transform.localScale = zoneStartScale;
        gameObject.SetActive(false);
        fallingCooldown = cooldownValue;
        hasFallen = false;
    }


   
}
