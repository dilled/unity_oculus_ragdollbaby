using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BabyToDo : MonoBehaviour
{
    NavMeshAgent agent;
   
    public Transform[] targets;

    public bool settedDestination = false;
    public int wait = 0;
    public int waitTime =50;
    public int toDo = 0;
    public bool idle = false;
    public float idleTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public void StopBaby()
    {
        settedDestination = false;
        if (agent.enabled)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }
        Debug.Log("Stop baby");
    }
    public void SetDestination()
    {
        settedDestination = true;
        Debug.Log("Start baby");
    }
    void Update()
    {
        
        if (wait >= waitTime)
        {
            if (settedDestination)
            {
                if (idleTime >= .5f)
                {
                    GetComponent<BabyController>().BabyScared();
                    //toDo += 1;
                    if (toDo > targets.Length-1)
                    {
                        toDo = 0;
                    }
                    idleTime = 0f;
                }
                agent.SetDestination(targets[0].position);
                if (agent.velocity == Vector3.zero)
                {
                    idleTime += .1f;
                }
                
            }
            wait = 0;
            
        }
        else
        {
            wait += 1;

            if (agent.velocity != Vector3.zero)
            {
                idleTime = 0f;
            }
        }
    }
}
