using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFacePlayer : MonoBehaviour
{
    private GameObject fpsCam;

    private void Start()
    {
        fpsCam = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {
        transform.LookAt(fpsCam.transform);
    }
}
