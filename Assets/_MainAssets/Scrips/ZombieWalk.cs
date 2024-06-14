using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.UI;

public class ZombieWalk : MonoBehaviour
{
    NavMeshAgent _Agent;
    public Animator _animator;

    public GameObject _player;
    public float speed = 1.5f;
    public ZombieAttack _ZombieAttack;
    public ZombieFollow _ZombieFollow;
    public Vector3 damageVector;
    public bool isDead;
    public int hitDamaged;
    public ParticleSystem dieParticles;
    
    
    
    void Start()
    {
        _Agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        isDead = false;
        hitDamaged = 0;

    }

    // Update is called once per frame
    void Update()
    {

        damageVector = Vector3.forward;
        if (!isDead)
        {

        if (_ZombieFollow.idle == false)
        {
            transform.LookAt(_player.transform);

            if (_ZombieAttack.isAttacking == false)
            {
                
                    transform.Translate(Vector3.forward * Time.deltaTime * speed);
                    _animator.SetBool("isWalk", true);
                
            }

        }
        else
        {
            _animator.SetBool("isWalk", false);
        }
        
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Magic")
        {
            ZombieTakeDamage();
        }
    }

    public void ZombieTakeDamage()
    {
        if (!isDead)
        {
            hitDamaged++;
                //wdprint("zombie " + hitDamaged);
            if (hitDamaged >= 2)
            {
                isDead = true;
                ZombieDead();
                Debug.Log("Zombie muerto");
            }
            else
            {
                _animator.SetTrigger("takeDamage");
            }
        }
    }

    void ZombieDead()
    {
        _animator.SetTrigger("die");
        StartCoroutine(Disapier());

    }

    IEnumerator Disapier()
    {
        dieParticles.Play(true);
        yield return new WaitForSeconds(4.14f);
        dieParticles.Stop(true);
        gameObject.SetActive(false);
    }
}
