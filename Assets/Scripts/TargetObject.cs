using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    [SerializeField] private float maxTargetRange = 20f;
    [SerializeField] private Camera fpsCam;
    public GameObject targetedObject;
    public bool hasTarget = false;
    [SerializeField] private GameObject targetIndicatorPrefab;
    private GameObject currentTargetIndicator;

    void Update()
    {
        RaycastHit hit;
        float thickness = 3.5f;
        Vector3 origin = fpsCam.transform.position + new Vector3(0, 0.6f, -1.6f);
        Vector3 direction = fpsCam.transform.TransformDirection(Vector3.forward);

        if (Physics.SphereCast(origin, thickness, direction, out hit, maxTargetRange))
        {
            targetedObject = hit.collider.gameObject;
            if (hit.collider.CompareTag("DashOrb"))
            {
                hasTarget = true;
                SetTargetIndicator(hit.collider.transform.position);
            }
            //else if (hit.collider.CompareTag("Enemy"))
            //{
            //    hasTarget = true;
            //    SetTargetIndicator(hit.collider.transform.position);
            //}
            else
            {
                hasTarget = false;
                ClearTargetIndicator();
            }
        }
        else
        {
            hasTarget = false;
            ClearTargetIndicator();
        }

        Debug.DrawRay(origin, direction * maxTargetRange, Color.blue);
    }

    void SetTargetIndicator(Vector3 position)
    {
        if (currentTargetIndicator == null)
        {
            currentTargetIndicator = Instantiate(targetIndicatorPrefab, position, Quaternion.identity);
        }
        else
        {
            currentTargetIndicator.transform.position = position;
        }
    }

    void ClearTargetIndicator()
    {
        if (currentTargetIndicator != null)
        {
            Destroy(currentTargetIndicator);
            currentTargetIndicator = null;
        }
    }
}
