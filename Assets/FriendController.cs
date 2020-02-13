using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FriendController : MonoBehaviour
{
    public bool startEating = false;
    public bool allEaten = true;
    public bool eatables = false;
    public bool startPos = true;
    NavMeshAgent agent;
    public Transform poops;
    public Transform poopsToEat;
    public Transform startPlacing;
    Animator animator;
    public GameObject unicornButton;
    public Quaternion originalRotationValue;
    public List<Transform> poopies;
    public int destPoint = 0;
    
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            unicornButton.SetActive(false);       
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (eatables && startPos)
            {
                unicornButton.SetActive(true);
            }
        }
        if (other.transform.tag == "Poop")
        {
            animator.SetTrigger("eat");
            Destroy(other.gameObject);
        }
        if (other.transform.tag == "Baby")
        {
            animator.SetBool("ride", true);
        }
    }

    public void AllNotEaten()
    {
        eatables = true;
    }

    public void StartEating()
    {
        startEating = true;
        eatables = false;
    }

    void Start()
    {
        originalRotationValue = transform.rotation;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Ride()
    {
        //agent.isStopped = true;

    }

    void GotoNextPoint()
    {
        if (poopies.Count == 0)
        {
            allEaten = true;
            return;
        }
        if (poopies[destPoint] != null)
        {
            agent.destination = poopies[destPoint].position;
        }
        else
        {
            GetEatList();
        }
        if (poopies.Count != 0)
        {
            destPoint = (destPoint + 1) % poopies.Count;
        }
    }

    public void AllEaten()
    {
        agent.SetDestination(startPlacing.position);
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.deltaTime * 5);
            startPos = true;
        }
    }

    public void GetEatList()
    {
        poopies.Clear();
        foreach (Transform poop in poopsToEat)
        {
            poopies.Add(poop);
        }
    }

    public void GetDestination()
    {
        poopies.Clear();
        foreach (Transform poop in poops)
        {
            poopies.Add(poop);
            poop.transform.parent = poopsToEat;
        }     
        allEaten = false;
        startEating = false;
    }

    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, .1f, Time.deltaTime);

        if (!allEaten)
        {
            if (!agent.pathPending)
            {
                    GotoNextPoint();
            }
        }
        else
        {
            if (!agent.pathPending)
            {
                AllEaten();
            }
        }

        if (startEating)
        {
            GetDestination();
            startPos = false;
        }
        
    }
     
}
