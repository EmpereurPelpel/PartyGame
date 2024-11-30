using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TrapGeneratorScript : MonoBehaviour
{

    private int trapCount;

    private float spawnCooldown;
    [Range(0.4f, 1.0f)] public float cooldownValue = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        spawnCooldown = cooldownValue;
        trapCount = gameObject.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (spawnCooldown > 0)
            {
                spawnCooldown -= Time.deltaTime;
            }
            else
            {
                SummonTrap();
                spawnCooldown = cooldownValue;
            }
        }



    }

    /// <summary>
    /// Trigger a random trap
    /// </summary>
    private void SummonTrap()
    {
        bool hasSummoned = true;
        int i = 0;
        while (hasSummoned && i < trapCount) 
        {
            if (!gameObject.transform.GetChild(i).gameObject.activeSelf)
            {
                hasSummoned = false;
            }
            i++;
        }
        while (!hasSummoned)
        {
            int trap = Random.Range(0, trapCount);
            if (!gameObject.transform.GetChild(trap).gameObject.activeSelf )
            {
                gameObject.transform.GetChild(trap).gameObject.SetActive(true);
                hasSummoned = true;
            }
        }

    }
}
