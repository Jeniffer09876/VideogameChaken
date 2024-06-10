using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieWalk : MonoBehaviour
{
    NavMeshAgent _Agent;
    Animator _animator;

    public GameObject _player;
    public float speed = 1.5f;

    
    
    void Start()
    {
        _Agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");  
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(_player.transform);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);


        //animation
        if (_Agent.isStopped)
        {
            _animator.SetBool("isWalk",false);
        }
        else
        {
            _animator.SetBool("isWalk", true);
        }


    }

    
}
