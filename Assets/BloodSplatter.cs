using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    [SerializeField] private GameObject bloodSplatter;
    [SerializeField] private ShootScript shootScript;

    private void Start()
    {
        shootScript.GetHit += DoBloodSplatter;
    }

    public void DoBloodSplatter(RaycastHit hit)
    {
        GameObject instantiatedObject = Instantiate(bloodSplatter, hit.point, Quaternion.identity);

        // Align object with surface normal
        instantiatedObject.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
    }
}
