using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTrail : MonoBehaviour
{
    public GameObject trailPrefab;
    public Transform shootPos;
    public ShootScript shoot;

    private void Update()
    {
        if (shoot.trailTrue)
        {
            Instantiate(trailPrefab, shootPos.position, shootPos.rotation);
            shoot.trailTrue = false;
        }
    }
}
