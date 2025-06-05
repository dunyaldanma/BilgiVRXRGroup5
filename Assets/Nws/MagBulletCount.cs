using UnityEngine;
using System.Reflection;

public class MagBulletCount : MonoBehaviour
{
    public int magMaxbullet = 30;
    public int magMinbullet = 0;
    public int magBullet;

    [SerializeField] CLOWN74 cl;
    private MethodInfo bulletCheckMethod;

    void Start()
    {
        cl = this.transform.parent.GetComponentInParent<CLOWN74>();
        if (cl == null) return;

        // BulletCheck metodunu yansýma ile arýyoruz
        bulletCheckMethod = cl.GetType().GetMethod("BulletCheck", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        if (bulletCheckMethod == null)
        {
            Debug.LogWarning("CLOWN74 içinde BulletCheck metodu yok!");
            return;
        }

        cl.enabled = true;
        magBullet = magMaxbullet;
    }

    void Update()
    {
        if (cl == null || bulletCheckMethod == null) return;

        bulletCheckMethod.Invoke(cl, new object[] { magBullet });

        if (magBullet == 0)
        {
            cl.enabled = false;
            this.enabled = false;
        }
    }

    public void CurrentBullet(int bullet)
    {
        magBullet = bullet;
    }
}
