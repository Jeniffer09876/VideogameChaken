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
    private AudioSource explotionSfx;
    private Collider colliderProjectile;

    // Start is called before the first frame update
    void Start()
    {
        ZombieWalk = FindObjectOfType<ZombieWalk>();
        sampleTime = 0f;
        explotionSfx = GetComponent<AudioSource>();
        colliderProjectile = GetComponent<Collider>();
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
            colliderProjectile.isTrigger = false;
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
            //StartCoroutine(DestroyParticle());
        }
    }

  private void OnCollisionEnter(Collision collision)
    {
        colliderProjectile.isTrigger = true;
        if (collision.gameObject.tag == "zombieDamage")
        {
            ZombieWalk.ZombieTakeDamage();
        }
        explotionSfx.Play();
        Debug.Log("boom");
        boomParticles.Play(true);
        magicParticles.Stop(true);
        //StartCoroutine(DestroyParticle());
    }
    IEnumerator DestroyParticle()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

}
