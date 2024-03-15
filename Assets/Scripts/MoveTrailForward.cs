using UnityEngine;

public class MoveTrailForward : MonoBehaviour
{
    public TrailRenderer trail;
    private Vector3 startPos;
    private ShootScript shoot;

    private void Start()
    {
        trail.startWidth = 0.09f;
        startPos = transform.position;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            shoot = player.GetComponent<ShootScript>();
        }
    }

    private void Update()
    {
        if (trail.startWidth >= 0)
        {
            trail.startWidth -= 0.6f * Time.deltaTime;
        }
        if (trail.startWidth <= 0)
        {
            Destroy(gameObject);
        }

        transform.position = shoot.endPos;
    }
}
