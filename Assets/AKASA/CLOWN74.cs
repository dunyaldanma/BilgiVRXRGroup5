using UnityEngine;
using static UnityEngine.ParticleSystem;
using System.Collections;

public class CLOWN74 : MonoBehaviour
{
    LayerMask layerMask;
    AudioSource audioSource;

    [SerializeField] GameObject bullet;
    bool onTarget;
    float miliseconds;

    [SerializeField] LineRenderer rayLine;
    [SerializeField] Transform rayEnd;
    [SerializeField] GameObject bulletHolePrefab;
    [SerializeField] GameObject muzzle;
    [SerializeField] AudioClip Shoot;
    public MagBulletCount mbc;
    int hitAmount = 0;
    public int bulletAmount;

    void Awake()
    {
        layerMask = LayerMask.GetMask("Target");
        muzzle.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        mbc = GetComponentInChildren<MagBulletCount>();
        if (mbc == null) return;
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward * -1), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * -1) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            onTarget = true;

            if (bulletHolePrefab != null && hitAmount > 0)
            {
                GameObject hole = Instantiate(bulletHolePrefab, hit.point, Quaternion.LookRotation(hit.normal));
                hole.transform.position += hole.transform.forward * 0.001f;
                hole.transform.SetParent(hit.collider.transform);
                hitAmount--;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * -1) * 1000, Color.white);
            Debug.Log("Did not Hit");
            onTarget = false;
        }
    }

    void Update()
    {
        rayLine.enabled = true;
        rayLine.SetPosition(0, transform.position);
        rayLine.SetPosition(1, rayEnd.position);

        miliseconds += Time.deltaTime * 1000;

        if (miliseconds > 100)
        {
            miliseconds -= 100;
            if (onTarget)
            {
                hitAmount++;
                bulletAmount--;
                GameObject projectile;
                projectile = Instantiate(bullet, transform.position, transform.rotation);
                projectile.GetComponent<Rigidbody>().linearVelocity = transform.TransformDirection(Vector3.forward * -100.0f);
                mbc.CurrentBullet(bulletAmount);
                StartCoroutine(FlashMuzzle());
                StartCoroutine(SoundFX());
            }
            else
            {

            }
        }
        if(onTarget)
        {
        }
    }


    private IEnumerator FlashMuzzle()
    {
        muzzle.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        muzzle.SetActive(false);
    }

    private IEnumerator SoundFX()
    {
        audioSource.PlayOneShot(Shoot);
        yield return new WaitForSeconds(0.5f);
    }

    public void BulletCheck(int bullet)
    {
        bulletAmount = bullet;
    }
}