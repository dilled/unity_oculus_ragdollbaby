using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ride : MonoBehaviour
{
    Animator animator;
    public bool ride = false;
    UnityEngine.AI.NavMeshAgent agent;
    public Transform target;


    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

    }

 
    void Update()
    {
        if (ride)
        {
            agent.SetDestination(target.position);
            if (agent.remainingDistance <= agent.stoppingDistance+3f){
                FaceTarget();
                ride = false;
                animator.SetTrigger("ride");
            }
            //FaceTarget();
            //ride = false;
            //animator.SetTrigger("ride");
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        animator.SetTrigger("ride");
    }
}
