using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BabyAngryController : MonoBehaviour
{
    public Transform hips;
    public Transform head;
    public Transform toesL;
    public Transform toesR;

    public float ragdollBlendAmount;

    //How long do we blend when transitioning from ragdolled to animated
    public float ragdollToMecanimBlendTime = 0.5f;
    public float mecanimToGetUpTransitionTime = 0.05f;

    //A helper variable to store the time when we transitioned from ragdolled to blendToAnim state
    public float ragdollingEndTime = -100f;

    //Declare a class that will hold useful information for each body part
    public class BodyPart
    {
        public Transform transform;
        public Vector3 storedPosition;
        public Quaternion storedRotation;
    }
    //Additional vectores for storing the pose the ragdoll ended up in.
    public Vector3 ragdolledHipPosition, ragdolledHeadPosition, ragdolledFeetPosition;

    //Declare a list of body parts, initialized in Start()
    public List<BodyPart> bodyParts = new List<BodyPart>();
    public Component[] components;


    public GameObject music;
    public GameObject hand;
    RagdollHelper raghel;
    public bool dropped = false;

    public int wait = 0;
    public int waitTime; 

    Animator animator;
    public Transform target;
    NavMeshAgent agent;
    public bool switched = false;
    public float followWait = 0f;
    public bool following = false;
    public bool attack = false;
    public float distance;
    public GameObject cam;
    public bool grabbed = false;
    public GameObject armature;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Floor")
        {
             Debug.Log("hitted   " + other.gameObject.name);
        }
            if (grabbed)
        {
            if (other.transform.tag == "Floor")
            {
                Debug.Log("grabbed  hitted   " + other.gameObject.name);
                //animator.SetTrigger("grabbed");

                //Invoke("WakeUp", 5);

            }
        }
    }
    public void ResetParent(Transform pos)
    {
        //transform.position = pos.position;
    }

    void SetKinematic(bool newValue)
    {/*
        //Get an array of components that are of type Rigidbody
        Rigidbody[] bodies = gameObject.GetComponentsInChildren<Rigidbody>();

        //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
        foreach (Rigidbody rb in bodies)
        {
            //if(rb.gameObject.tag != "Hand")
            rb.isKinematic = newValue;
        }
        Rigidbody[] bodiesp = gameObject.GetComponentsInParent<Rigidbody>();
        foreach (Rigidbody rb in bodiesp)
        {
            rb.isKinematic = newValue;
        }
        */

    }
    public void WakeUp(Transform pos)
    {
         //Transition from ragdolled to animated through the blendToAnim state
        SetKinematic(true); //disable gravity etc.
        ragdollingEndTime = Time.time; //store the state change time
        ResetParent(pos);
        animator.enabled = true; //enable animation
        //state = RagdollState.blendToAnim;

        //Store the ragdolled position for blending
        foreach (BodyPart b in bodyParts)
        {
            b.storedRotation = b.transform.rotation;
            b.storedPosition = b.transform.position;
        }
        // Debug.Log(animator.GetBoneTransform(HumanBodyBones.LeftToes).position);
        //Remember some key positions
        ragdolledFeetPosition = 0.5f * toesL.position + toesR.position; //(animator.GetBoneTransform(HumanBodyBones.LeftToes).position + animator.GetBoneTransform(HumanBodyBones.RightToes).position);
        ragdolledHeadPosition = head.position;//animator.GetBoneTransform(HumanBodyBones.Head).position;
        ragdolledHipPosition = hips.position; // animator.GetBoneTransform(HumanBodyBones.Hips).position;

        //Initiate the get up animation
        animator.SetFloat("speedPercent", 0f);
        animator.SetTrigger("grabbed");
        //transform.position = hand.transform.position;
        dropped = true;
        attack = false;
        grabbed = false;
        following = false;
        switched = true;
        Debug.Log("Wake         ");
    }
    public void Switched()
    {
        animator.SetBool("sitting", true);
        switched = true;
       
    }
    void Start()
    {
        Physics.IgnoreCollision(target.gameObject.GetComponent<Collider>(), GetComponent<Collider>());

        raghel = GetComponent<RagdollHelper>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
        music.GetComponent<MusicPlayer>().Fade();

        components = GetComponentsInChildren(typeof(Transform));

        //For each of the transforms, create a BodyPart instance and store the transform 
        foreach (Component c in components)
        {
            BodyPart bodyPart = new BodyPart();
            bodyPart.transform = c as Transform;
            bodyParts.Add(bodyPart);
        }
        


    }

    // Update is called once per frame
    public void Grabbed()
    {
        attack = false;
        grabbed = true;
        cam.GetComponent<TakeDamage>().NotTakeHit();
        // armature.transform.parent = null;
    }
    public void NotGrabbed()
    {
        grabbed = false;
        // armature.transform.parent = null;
    }
    void Update()
    {
        if (!grabbed)
        {
            if (followWait >= 2f)
            {
                FaceTarget(5);
            }
            if (attack)
            {

                FaceTarget(0);
                //cam.GetComponent<TakeDamage>().TakeHit();
                transform.GetComponent<Rigidbody>().useGravity = false;
                transform.GetComponent<Rigidbody>().isKinematic = false;
                agent.enabled = false;
                //GetComponent<Collider>().enabled = false;
                //transform.parent = hand.transform;
                transform.position = hand.transform.position;
                //attack = false;
            }
            if (wait >= waitTime)
            {
                if (switched)
                {
                    followWait += .1f;

                    if (followWait >= 2f)
                    {
                      
                        following = true;
                        switched = false;
                    }
                }
                if (following)
                {
                    distance = Vector3.Distance(target.position, transform.position);
                    SetDestination(target);
                    float speedPercent = agent.velocity.magnitude / agent.speed;
                    animator.SetFloat("speedPercent", speedPercent, .1f, Time.deltaTime);
                    if (distance <= agent.stoppingDistance + .4f)
                    {
                        GameObject gm = GameObject.Find("GameManager");
                        gm.GetComponent<Timer>().stopped = true;
                        attack = true;
                        following = false;
                        followWait = 0;
                        animator.SetBool("attacking", true);
                    }
                }
                wait = 0;
            }
            else
            {
                wait += 1;
            }
        }
    }
    void FaceTarget(int rotTime)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotTime);
    }

    public void StopAngryBaby()
    {
        if (agent != null)
        {
            agent.isStopped = true;
        }
    }

    public void SetDestination(Transform target)
    {
        //Debug.Log(target.position);
        if (agent != null)
        {
            agent.SetDestination(target.position);
        }
    }
    void LateUpdate()
    {
        //Clear the get up animation controls so that we don't end up repeating the animations indefinitely
        //anim.SetBool("GetUpFromBelly",false);
        //anim.SetBool("GetUpFromBack",false);

        //Blending from ragdoll back to animated
        if (dropped)
        {
            animator.SetBool("sitting", false);
            animator.SetBool("attacking", false);
            Debug.Log("LATE");
            if (Time.time <= ragdollingEndTime + mecanimToGetUpTransitionTime)
            {
                //If we are waiting for Mecanim to start playing the get up animations, update the root of the mecanim
                //character to the best match with the ragdoll
                Vector3 animatedToRagdolled = ragdolledHipPosition - animator.GetBoneTransform(HumanBodyBones.Hips).position;
                Vector3 newRootPosition = transform.position + animatedToRagdolled;

                //Now cast a ray from the computed position downwards and find the highest hit that does not belong to the character 
                RaycastHit[] hits = Physics.RaycastAll(new Ray(newRootPosition, Vector3.down));
                newRootPosition.y = 0;
                foreach (RaycastHit hit in hits)
                {
                    if (!hit.transform.IsChildOf(transform))
                    {
                        newRootPosition.y = Mathf.Max(newRootPosition.y, hit.point.y);
                    }
                }
                transform.position = newRootPosition;

                //Get body orientation in ground plane for both the ragdolled pose and the animated get up pose
                Vector3 ragdolledDirection = ragdolledHeadPosition - ragdolledFeetPosition;
                ragdolledDirection.y = 0;

                Vector3 meanFeetPosition = 0.5f * (animator.GetBoneTransform(HumanBodyBones.LeftFoot).position + animator.GetBoneTransform(HumanBodyBones.RightFoot).position);
                Vector3 animatedDirection = animator.GetBoneTransform(HumanBodyBones.Head).position - meanFeetPosition;
                animatedDirection.y = 0;

                //Try to match the rotations. Note that we can only rotate around Y axis, as the animated characted must stay upright,
                //hence setting the y components of the vectors to zero. 
                transform.rotation *= Quaternion.FromToRotation(animatedDirection.normalized, ragdolledDirection.normalized);
            }
            //compute the ragdoll blend amount in the range 0...1
            ragdollBlendAmount = 1.0f - (Time.time - ragdollingEndTime - mecanimToGetUpTransitionTime) / ragdollToMecanimBlendTime;
            ragdollBlendAmount = Mathf.Clamp01(ragdollBlendAmount);

            //In LateUpdate(), Mecanim has already updated the body pose according to the animations. 
            //To enable smooth transitioning from a ragdoll to animation, we lerp the position of the hips 
            //and slerp all the rotations towards the ones stored when ending the ragdolling
            foreach (BodyPart b in bodyParts)
            {
                if (b.transform != transform)
                { //this if is to prevent us from modifying the root of the character, only the actual body parts
                  //position is only interpolated for the hips
                    if (b.transform == animator.GetBoneTransform(HumanBodyBones.Hips))
                        b.transform.position = Vector3.Lerp(b.transform.position, b.storedPosition, ragdollBlendAmount);
                    //rotation is interpolated for all body parts
                    b.transform.rotation = Quaternion.Slerp(b.transform.rotation, b.storedRotation, ragdollBlendAmount);
                }
            }

            //if the ragdoll blend amount has decreased to zero, move to animated state
            if (ragdollBlendAmount == 0)
            {
                dropped = false;
                SetKinematic(true);
                transform.GetComponent<Rigidbody>().useGravity = true;
                transform.GetComponent<Rigidbody>().isKinematic = true;
                agent.enabled = true;
                //animator.enabled = true;
                GetComponent<Collider>().enabled = true;
                //state = RagdollState.animated;
                ragdollBlendAmount = 1;
                return;
            }
        }
    }
    }
