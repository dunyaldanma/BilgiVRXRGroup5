using UnityEngine;
using static UnityEngine.ParticleSystem;
using System.Collections;

public class CLOWN74 : MonoBehaviour
{
    public float Damage = 10f;
    public float Range = 100f;
    public float impactF = 30f;
    public float fireR = 15f;
    private float nTTF = 0f;


    public bool controllerActive = false;

    LayerMask layerMask;
    AudioSource audioSource;

    [SerializeField] GameObject bullet;
    bool onTarget;
    float miliseconds;
    [SerializeField] GameObject magg;

    [SerializeField] LineRenderer rayLine;
    [SerializeField] Transform rayEnd;
    //[SerializeField] GameObject bulletHolePrefab;
    [SerializeField] GameObject muzzle;
    [SerializeField] GameObject M1911;
    [SerializeField] AudioClip Shoot;
    public MagBulletCount mbc;
    int hitAmount = 0;
    public int bulletAmount;
    float childcountx;

    void Awake()
    {
        layerMask = LayerMask.GetMask("Target");
        muzzle.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        childcountx = magg.transform.childCount;
    }

    void OnEnable()
    {
        mbc = GetComponentInChildren<MagBulletCount>();
        if (mbc == null) return;
    }
    void FixedUpdate()
    {

        if (controllerActive) { return; }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward * -1), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * -1) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            onTarget = true;
            /*
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactF);
            }
            */
            //if (bulletHolePrefab != null && hitAmount > 0)
            //{
               // GameObject hole = Instantiate(bulletHolePrefab, hit.point, Quaternion.LookRotation(hit.normal));
               //hole.transform.position += hole.transform.forward * 0.001f;
               // hole.transform.SetParent(hit.collider.transform);
               // hitAmount--;
            //}
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * -1) * 1000, Color.white);
            //Debug.Log("Did not Hit");
            onTarget = false;
        }
    }


    void Shooting()
    {
        if (!controllerActive) { return; }
        float triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        if (triggerValue > 0.1f && Time.time >= nTTF && bulletAmount > 0)
        {
            M1911.SetActive(true);
            bulletAmount--;
            mbc.CurrentBullet(bulletAmount);
            nTTF = Time.time + 1f / fireR;
            ShootReal();
            Debug.Log("bruhmoment");
        }
        else {        
            M1911.SetActive(false);
        }
    }
    private void ShootReal()
    {


        RaycastHit Hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward * -1), out Hit, Range))
        {
            EnemyHp enemyHp = Hit.transform.GetComponent<EnemyHp>();
            if (enemyHp != null)
            {
                enemyHp.TakeDamage(Damage);
            }

            if (Hit.rigidbody != null)
            {
                Hit.rigidbody.AddForce(-Hit.normal * impactF);
            }
        }
    }

    void Update()
    {

        if (magg.transform.childCount == 0f)
        {
            bulletAmount = 0;
            this.enabled = false;
        }
        Shooting();
        rayLine.enabled = true;
        rayLine.SetPosition(0, transform.position);
        rayLine.SetPosition(1, rayEnd.position);

        miliseconds += Time.deltaTime * 1000;

        if (miliseconds > 500)
        {
            miliseconds -= 500;
            if (onTarget && bulletAmount > 0)
            {
                Debug.Log("hit");
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
        if (onTarget)
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