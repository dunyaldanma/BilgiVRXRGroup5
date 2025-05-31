using UnityEngine;

public class MagBulletCount : MonoBehaviour
{
    public int magMaxbullet = 30;
    public int magMinbullet = 0;
    public int magBullet;

    CLOWN74 cl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cl = GetComponentInParent<CLOWN74>();
        if (cl == null) return;
        cl.enabled = true;
        magBullet = magMaxbullet;
    }

    // Update is called once per frame
    void Update()
    {
        if (cl == null) return;
        cl.BulletCheck(magBullet);
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
