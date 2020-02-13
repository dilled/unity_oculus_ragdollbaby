using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BabyController : MonoBehaviour
{
    public bool stopped = false;
    public GameObject music;
    public bool allwaysSleep = false;
    public int wait = 0;
    public int waitTime;
    Animator animator;
    public int sleepMod;
    public float sleepTimer =0f;
    public float sleepWait = 10f;
    public bool sleeping;
    public bool hungry;
    public float awakeTimer;
    float defAwakeTimer;
    public bool eating;
    public bool climbFromBed = false;
    public bool crying = false;
    public bool playing = false;
    public bool diaper = true;
    public float moodLoss = 15f;
    CharacterStats babyStats;
    CharacterSounds sounds;

    public Transform babyPlacingAfterClimb;
    public Transform babyPlacingBed;
    public Transform babyPlacingRiding;

    NavMeshAgent agent;
    public Transform target;
    BabyToDo babyToDo;
    DiaperControl diaperControl;

    public bool tired = false;
    public Quaternion originalRotationValue;
    public bool riding = false;
    GameObject pstate;
    public GameObject babyHand;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Poop")
        {
            if (other.GetComponent<DestroyFallen>().smelly)
            {
                Debug.Log("POOOOOOOOOOOOOOP");
                babyStats.LoseMood(moodLoss);
            }    
        }
        if (other.name == "Bottle")
        {
            other.transform.GetComponent<DistanceGrabbable>().enabled = false;
            other.transform.GetComponent<Rigidbody>().useGravity = false;
            other.transform.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.parent = babyHand.transform;
            other.transform.position = babyHand.transform.position;
            other.transform.rotation = babyHand.transform.rotation;
            BabyEating();
            
        }
        if (other.name == "DiaperItem")
        {
            other.transform.GetComponent<Rigidbody>().useGravity = true;
            other.transform.GetComponent<Rigidbody>().isKinematic = false;
            other.transform.parent = null;
            DiaperNew();
            //Destroy(gameObject);
            other.GetComponent<ReSpawn>().ReSpawing();
        }


    }

    void Start()
    {
        pstate = GameObject.Find("PlayerState");
        if (pstate != null && pstate.GetComponent<Player>().level != 0)
        {
            sleeping = pstate.GetComponent<LevelSettings>().CurrentSleep();
        }
        animator = GetComponent<Animator>();
        babyStats = GetComponent<CharacterStats>();
        sounds = GetComponent<CharacterSounds>();
        agent = GetComponent<NavMeshAgent>();   
        babyToDo = GetComponent<BabyToDo>();
        agent.enabled = false;
        babyToDo.enabled = false;
        diaperControl = GetComponent<DiaperControl>();
        originalRotationValue = transform.rotation;
        defAwakeTimer = awakeTimer;
        if (sleeping)
        {
            BabySleep();
        }
        else
        {
            music.GetComponent<MusicPlayer>().StopAudio();
        }
      //  babyStats.GainMood();
      //  babyStats.GainHungry();
        
    }
    public void DiaperFull()
    {
        if (diaper)
        {
            diaperControl.DropDiaper();
            diaper = false;
        }

    }
    public void DiaperNew()
    {
        if (!diaper)
        {
            babyStats.NewDiaper();
            diaperControl.PutDiaper();
            diaper = true;
        }
    }

    public void BabyTired()
    {
        BabyEaten();
        GetComponent<ItemPickup>().enabled = true;
        music.GetComponent<MusicPlayer>().LullabyTired();
        tired = true;
        animator.SetBool("tired", true);
        animator.SetBool("crying", true);
        animator.SetBool("moving", false);
    }

    public void BabyAwake()
    {
        if (babyStats.inBed)
        {
            GetComponent<ItemPickup>().enabled = true;
        }
        else
        {
            GetComponent<ItemPickup>().enabled = false;
        }
        music.GetComponent<MusicPlayer>().Fade();
        animator.SetBool("sleeping", false);
        animator.SetBool("headLeft", false);
        animator.SetBool("headRight", false);
        animator.SetBool("turnLeft", false);
        animator.SetBool("turnRight", false);
        sleeping = false;
        Debug.Log("awake");
    }

    public void BabySleep()
    {
        if (music != null)
        {
            music.GetComponent<MusicPlayer>().Sleep();
        }
        animator.SetBool("moving", false);
        animator.SetBool("tired", false);
        animator.SetBool("crying", false);
        animator.SetBool("sleeping", true);
        sleeping = true;
        crying = false;
        sounds.StopAudio();
        //tired = false;
        if(!allwaysSleep)
        GetComponent<ItemPickup>().enabled = true;
    }

    public void BabyScared()
    {
        if (sleeping)
        {
            BabyAwake();
        }
        crying = true;
        animator.SetBool("crying", true);
        animator.SetBool("moving", false);
    }

    public void BabyEaten()
    {
        crying = false;
        sleeping = false;
        eating = false;
        animator.SetBool("eating", false);
    }

    public void BabyHungry()
    {
        if (sleeping)
        {
            BabyAwake();
        }
        hungry = true;
        crying = true;
        animator.SetBool("crying", true);
        animator.SetBool("moving", false);
    }

    public void BabyEating()
    {
        crying = false;
        sounds.StopAudio();
        eating = true;
        if (sleeping)
        {
            BabyAwake();
        }
        animator.SetBool("crying", false);
        animator.SetBool("eating", true);
    }

    public void TakeAway()
    {
        eating = false;
        animator.SetBool("eating", false);
        if (babyStats.currentHungry <= babyStats.maxHungry / 3)
        {
            animator.SetBool("crying", true);
            crying = true;
        }
    }
    public void BabyRiding()
    {
        transform.position = babyPlacingRiding.position;
        transform.parent = babyPlacingRiding.transform;
        Debug.Log("ride");
        riding = true;
    }
    public void BabySwitchClimb()
    {
        transform.position = babyPlacingAfterClimb.position;
        animator.SetBool("crying", true);
        crying = true;
        climbFromBed = false;
        sleeping = false;
        animator.SetBool("standUp", false);
        animator.SetBool("climbing", false);
        babyToDo.enabled = true;
        babyStats.inBed = false;
        agent.enabled = true;
    }

    public void BabyPickUp()
    {
        Inventory.instance.items[0].RemoveFromInventory();
        animator.SetBool("sleeping", false);
        animator.SetBool("headLeft", false);
        animator.SetBool("headRight", false);
        animator.SetBool("turnLeft", false);
        animator.SetBool("turnRight", false);
        if (babyStats.inBed)
        {
            transform.position = babyPlacingAfterClimb.position;
            transform.rotation = originalRotationValue;
            agent.enabled = true;
            babyToDo.enabled = true;
            BabyAwake();
        }
        else
        {
            transform.position = babyPlacingBed.position;
            transform.rotation = originalRotationValue;
            agent.enabled = false;
            babyToDo.enabled = false;
        }
    }

    public void StopBaby()
    {
        playing = false;
        babyToDo.StopBaby();
        babyToDo.enabled = false;
        
    }

    public void WaitSetDestination()
    {
        babyToDo.SetDestination();
    }

    public void SetDestination()
    {
        agent.enabled = true;
        Invoke("WaitSetDestination", 3);
        playing = true;
    }

    void Update()
    {
        if (!stopped)
        {
            if (!allwaysSleep)
            {
                if (playing)
                {
                    if (crying || eating || sleeping || tired)
                    {
                        StopBaby();
                    }
                }

                float speedPercent = agent.velocity.magnitude / agent.speed;
                animator.SetFloat("speedPercent", speedPercent, .1f, Time.deltaTime);

                if (wait >= waitTime)
                {
                    babyStats.FillDiaper();
                    if (eating)
                    {
                        animator.SetBool("crying", false);
                        crying = false;
                        babyStats.Eat();
                        babyStats.GainMood();
                        babyStats.NotSleep();
                    }
                    else
                    {
                        babyStats.GainHungry();
                    }
                    if (playing)
                    {
                        babyStats.GainMood();
                    }

                    if (tired)
                    {
                        if (babyStats.inBed && !sleeping)
                        {
                            BabySleep();
                        }
                    }
                    if (awakeTimer <= defAwakeTimer/2)
                        GetComponent<ItemPickup>().enabled = false;
                    if (awakeTimer <= 0)
                    {
                        if (babyStats.inBed)
                        {
                            if (!tired)
                            {
                                climbFromBed = true;
                                Debug.Log("Do something! in bed");
                            }
                        }
                        else
                        {
                            if (!crying)
                            {
                                if (!tired)
                                {
                                    if (!hungry)
                                    {
                                        animator.SetBool("moving", true);
                                        Debug.Log("Do something!");
                                        if (!crying && !eating && !playing)
                                        {
                                            SetDestination();
                                            awakeTimer = defAwakeTimer;
                                        }
                                    }
                                }
                            }

                        }

                    }
                    if (climbFromBed)
                    {
                        if (!tired)
                        {
                            GetComponent<ItemPickup>().enabled = false;
                            sounds.StopAudio();
                            animator.SetBool("crying", false);
                            animator.SetBool("standUp", true);
                            animator.SetBool("climbing", true);

                            climbFromBed = false;
                        }
                    }

                    if (crying)
                    {
                        babyStats.LoseMood(moodLoss);
                    }



                    if (hungry && !eating)
                    {
                        babyStats.LoseMood(moodLoss);
                    }

                    if (sleeping)
                    {
                        sleepTimer += 10f * Time.deltaTime;
                        babyStats.Sleep();
                    }
                    else
                    {
                        babyStats.NotSleep();
                        if (!eating)
                        {
                            awakeTimer -= 10f * Time.deltaTime;
                            if (!crying)
                            {
                                awakeTimer -= 10f * Time.deltaTime;
                            }
                        }
                    }

                    if (sleepTimer >= sleepWait)
                    {
                        SleepMods();
                    }
                    wait = 0;
                }
                else
                {
                    animator.SetBool("blink", false);
                    wait += 1;
                }
            }
            else
            {
                sleepWait = 100f;
                sleepTimer += 10f * Time.deltaTime;
                if (sleepTimer >= sleepWait)
                {
                    SleepMods();
                }
                wait = 0;
            }
        }
    }
    public void SleepMods()
    {
        animator.SetBool("headLeft", false);
        animator.SetBool("headRight", false);
        animator.SetBool("turnLeft", false);
        animator.SetBool("turnRight", false);
        sleepMod = Random.Range(0, 5);
        if (sleepMod == 1)
        {
            animator.SetBool("headLeft", true);
        }
        if (sleepMod == 2)
        {
            animator.SetBool("headRight", true);
        }
        if (sleepMod == 3)
        {
            animator.SetBool("turnLeft", true);
        }
        if (sleepMod == 4)
        {
            animator.SetBool("turnRight", true);
        }
        sleepTimer = 0f;
    }
}
