using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerVR : MonoBehaviour
{
    public Interactable focus;


    private Vector3 moveDirection = Vector3.zero;

    [Range(0, 3)]
    public float rotateSensitivity = 1;
    
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    
    public bool running;
    public float walkSpeed = 3;

    public float runSpeed = 8;

    public float gravity = -12;
    

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    public float currentSpeed;
    float velocityY;
    public Vector2 input;
    //Vector3 inputDir;
    Transform cameraT;
    CharacterController controller;
    ThirdPersonCamera tpsCam;

    private void OnTriggerEnter(Collider other)
    {

        // Debug.Log(other.transform.name);

        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {

            SetFocus(interactable);

        }


    }
    private void OnTriggerExit(Collider other)
    {

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.name == "Ball")
        {
            hit.gameObject.GetComponent<ImpactReceiver>().AddImpact(transform.forward, 5f);
            Debug.Log(hit.transform.name);
        }
    }
    void SetFocus(Interactable newFocus)
    {
        // If our focus has changed
        if (newFocus != focus)
        {
            // Defocus the old one
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;   // Set our new focus
            //motor.FollowTarget(newFocus);   // Follow the new focus
        }

        newFocus.OnFocused(transform);
    }
    void Start()
    {
        ResetView();
        //Debug.Log("plactrlstart");
        //Application.targetFrameRate = 60;
        
        controller = GetComponent<CharacterController>();
        tpsCam = GetComponentInChildren<ThirdPersonCamera>();     
    }
    public void ResetView()
    {
        UnityEngine.XR.InputTracking.Recenter();
    }
    void Update()
    {


        //input += new Vector2(joystick2.Horizontal * rotateSensitivity, joystick2.Vertical);
        // Debug.Log(joystick.Vertical);
        //Vector2 input = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
        //inputDir = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical);

        input = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);// .PrimaryThumbstick);
       // inputDir = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            //Jump();
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            //Sleeping();
        }
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            //Flosh();
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        {
            ResetView();
        }
        Vector2 inputDir = input;
        Move(inputDir, input);




    }

    void Move(Vector2 inputDir, Vector2 input)
    {
        // Debug.Log(input);
        /*
        if (inputDir != Vector2.zero)
        {
            
            tpsCam.enabled = false;
        }
        else
        {
            tpsCam.enabled = true;
        }
        */
        running = true;

        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.sqrMagnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        if (currentSpeed >= 5)
        {
            //footStep.
        }

        //float targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
        //transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        //transform.eulerAngles = new Vector3(0, input.x, 0);

        /*
        if (inputDir != Vector3.zero)
        {
            if (inputDir.y >= 0)
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
            }
            else
            {
                float targetRotation = -inputDir.x * Mathf.Rad2Deg + cameraT.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
                currentSpeed *= -1;
            }
        }*/
        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * inputDir.y * currentSpeed + Vector3.up * velocityY;
        Vector3 velocityRight = transform.right * inputDir.x * currentSpeed + Vector3.up * velocityY;

       // Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        //controller.Move(velocity * Time.deltaTime);


        controller.Move(velocityRight * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            velocityY = 0;
        }

    }



    float GetModifiedSmoothTime(float smoothTime)
    {

        return smoothTime;/// airControlPercent;
    }
}
