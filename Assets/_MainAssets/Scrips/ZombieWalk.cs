using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.UI;

public class ZombieWalk : MonoBehaviour
{
    NavMeshAgent _Agent;
    Animator _animator;

    public GameObject _player;
    public float speed = 1.5f;
    public ZombieAttack _ZombieAttack;
    public ZombieFollow _ZombieFollow;
    public Vector3 damageVector;
    
    void Start()
    {
        _Agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("hell yeah");
        }
    }

    // Update is called once per frame
    void Update()
    {

        damageVector = Vector3.forward;

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
