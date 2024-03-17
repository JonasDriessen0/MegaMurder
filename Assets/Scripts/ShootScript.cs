using UnityEngine;
using UnityEngine.Events;

public class ShootScript : MonoBehaviour
{
    [Header("Gun Properties")]
    //[SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float shootingCooldown = 0.1f;
    public bool canShoot = true;
    private float timeSinceLastShot = 0f;

    [Header("Events")]
    public UnityEvent OnHitEnemy;
    public event System.Action<RaycastHit> GetHit;

    [Header("SFX")]
    //[SerializeField] private AudioSource shootSound;

    [Header("Trail")]
    public bool trailTrue = false;
    public Vector3 endPos;

    [Header("Refrences")]
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Animator animator;
    //private Enemy enemy;

    void Update()
    {
        if (canShoot && Input.GetButton("Fire1"))
        {
            Shoot();
            //shootSound.Play();
        }

        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= shootingCooldown)
        {
            canShoot = true;
        }
    }

    void Shoot()
    {
        if (canShoot)
        {
            trailTrue = true;
            animator.SetTrigger("ShootTrigger");
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                endPos = hit.point;
                //Enemy hitEnemy = hit.collider.GetComponent<Enemy>();
                if (hit.collider.CompareTag("Enemy"))
                {
                    OnHitEnemy.Invoke();
                    GetHit.Invoke(hit);
                    Debug.Log("Enemy hit");
                    //hitEnemy.health -= damage;
                }
                else
                {
                    Debug.Log("Aimlabs is free to download, you know?");
                }
            }

            timeSinceLastShot = 0f;
            canShoot = false;
        }
    }
}
