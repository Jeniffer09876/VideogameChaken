using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{
    public QuadraticCurve curve;
    public float speed;
    private float sampleTime;
    public bool magic;
    public ParticleSystem boomParticles;
    public ParticleSystem magicParticles;
    ZombieWalk ZombieWalk;

    // Start is called before the first frame update
    void Start()
    {
        ZombieWalk = FindObjectOfType<ZombieWalk>();
        sampleTime = 0f;
    }

private void OnEnable()
    {
        transform.position = curve.A.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (magic)
        {
            shootMagic();
        }
    }

    private void shootMagic() 
    {
        sampleTime += Time.deltaTime * speed;
        transform.position = curve.evaluate(sampleTime);

        if (sampleTime > 1f)
        {
            Debug.Log("boom");
            magicParticles.Stop(true);
            magic = false;
            sampleTime = 0f;
          
        }
    }

  private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "zombieDamage")
        {
            ZombieWalk.ZombieTakeDamage();
        }
        Debug.Log("boom");
        boomParticles.Play(true);
        magicParticles.Stop(true);
        //gameObject.SetActive(false);
    }
}
