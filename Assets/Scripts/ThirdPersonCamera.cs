using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool camCenter;
    public float camPosX = 0f;
    public float camPosY = 0.6f;
    public bool lockCursor;
    public float mouseSensitivity = 3;
    public Transform target;
    public GameObject targetObj;
    public float dstFromTarget = 2;
    public float currentDstFromTarget;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float currentPitch;
    //Vector3 nextPos;
    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    
    public float yaw;
    public float yaw2;
    public float pitch;
    public bool moving;
    
    public float defPitch = 3.2f;
    PlayerController pCont;
    
    
    
    GameObject playerState;

    void Start()
    {
        playerState = GameObject.Find("PlayerState");
        pCont = targetObj.GetComponent<PlayerController>();
       // mouseSensitivity = playerState.GetComponent<Player>().sensitivity;
        currentDstFromTarget = dstFromTarget;
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        //currentRotation = target.eulerAngles;
    }
    private void FixedUpdate()
    {
        /*
        if (underAttack && attackZoom)
        {
            if (dstFromTarget < aZoom)
            {
                currentDstFromTarget += .1f;
                dstFromTarget += .1f;
            }
            if (pitch < 40f)
            {
                pitch += .7f;
            }
        }
        else
        {
            if (dstFromTarget > 3.3f)
            {
                currentDstFromTarget -= .1f;
                dstFromTarget -= .1f;
            }
            if (pitch > 3f)
            {
                pitch -= .7f;
            }
            //currentDstFromTarget = 3.26f;
            //dstFromTarget = 3.26f;
        }*/
    }
    public void OnValueChanged(float newValue)
    {
        mouseSensitivity = newValue;
        //Debug.Log(newValue);
    }
    
    private void Update()
    {
        if (camCenter)
        {
           
            
                // Debug.Log(pitch);
                if (pitch < defPitch)
                {
                    if (pitch + 3f < defPitch)
                    {
                        pitch += 3f;
                    }
                    else
                    {
                        pitch = defPitch;
                    }
                }
                if (pitch > defPitch)
                {
                    if (pitch - 3f > defPitch)
                    {
                        pitch -= 3f;
                    }
                    else
                    {
                        pitch = defPitch;
                    }
                }
            
        }
        

        currentDstFromTarget = Mathf.Clamp(currentDstFromTarget, 0.2f, dstFromTarget);
       
        
        }

    void LateUpdate()
    {
        // yaw2 = 0f;
        Vector2 input2 = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        yaw2 += input2.x * mouseSensitivity;
        yaw += input2.x * mouseSensitivity;

        //yaw2 += joystick2.Horizontal * mouseSensitivity;
        //pitch -= joystick2.Vertical * mouseSensitivity;
        //yaw += CrossPlatformInputManager.GetAxis("Mouse X") * mouseSensitivity/2;
        //pitch -= CrossPlatformInputManager.GetAxis("Mouse Y") * mouseSensitivity;
        //yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        //pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
        currentPitch = pitch;
        
            /*
            if (moving && !backing )
            {

                //currentRotation = target.eulerAngles;

                    yaw2 = yaw;
                    currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw2), ref rotationSmoothVelocity, rotationSmoothTime);

                //currentRotation = Vector3.SmoothDamp(currentRotation, target.eulerAngles, ref rotationSmoothVelocity, rotationSmoothTime);


                //Debug.Log(target.eulerAngles + " " + transform.eulerAngles);
                //currentRotation = Vector3.SmoothDamp(currentRotation, target.eulerAngles, ref rotationSmoothVelocity, rotationSmoothTime);
            }
            else if (!moving)
            {
                if (joystick.Horizontal == 0)
                {
                    currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw2), ref rotationSmoothVelocity, rotationSmoothTime);

                }
                else 
                {
                   // currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
                }

                //currentRotation = target.eulerAngles;
                //currentRotation = Vector3.SmoothDamp(currentRotation, target.eulerAngles, ref rotationSmoothVelocity, rotationSmoothTime);
            }*/
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.parent.eulerAngles = currentRotation;
        //transform.eulerAngles = new Vector3(pitch, target.rotation.x, 0);

        transform.position = target.position - (transform.forward + new Vector3(camPosX, camPosY, camPosX)) * currentDstFromTarget;
        //nextPos = target.position - transform.forward * (currentDstFromTarget + 0.1f);
    }
    void OnDrawGizmosSelected()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward) * dstFromTarget;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction);
    }
}