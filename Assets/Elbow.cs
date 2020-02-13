using UnityEngine;
using System.Collections;

public class Elbow : MonoBehaviour
{
    Quaternion initialRotation;
    ConfigurableJoint joint;
    int move = 0;
    public GameObject LeftShoulder;
    public float SpringCoefficient = 30;
    public float Damper = 10;
    private float target;
    // Controls

    public KeyCode upKey = KeyCode.UpArrow;
    public KeyCode downKey = KeyCode.DownArrow;
    public KeyCode leftKey = KeyCode.LeftArrow;
    public KeyCode rightKey = KeyCode.RightArrow;
    private JointSpring shoulderSpring;

    // Use this for initialization
    void Start()
    {
        //joint = GetComponent<ConfigurableJoint>();
        //creating variables for the joint
        joint.SetTargetRotationLocal(Quaternion.Euler(0, 0, 0), initialRotation);
        /*
                shoulderSpring = LeftShoulder.GetComponent<HingeJoint>().spring;
                shoulderSpring.targetPosition = 0;
                shoulderSpring.spring = SpringCoefficient;
                shoulderSpring.damper = Damper;
                LeftShoulder.GetComponent<HingeJoint>().spring = shoulderSpring;
                */
    }
    void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();
        initialRotation = joint.transform.localRotation;
    }

    
    void Update()
    {
        joint.SetTargetRotationLocal(Quaternion.Euler(0, 0, move), initialRotation);
        if (Input.GetKey(upKey))
        {
            //Attempt to change the targetposition of the joint, does not work.
            shoulderSpring.spring = SpringCoefficient;
            shoulderSpring.damper = Damper;
            shoulderSpring.targetPosition++;
            LeftShoulder.GetComponent<HingeJoint>().spring = shoulderSpring;

        }
        else if (Input.GetKey(downKey))
        {
            shoulderSpring.targetPosition--;
            shoulderSpring.spring = SpringCoefficient;
            shoulderSpring.damper = Damper;
            LeftShoulder.GetComponent<HingeJoint>().spring = shoulderSpring;
        }
        else if (Input.GetKey(leftKey))
        {
            move += 10;
        }
        else if (Input.GetKey(rightKey))
        {
            move -= 10;

        }


    }
}