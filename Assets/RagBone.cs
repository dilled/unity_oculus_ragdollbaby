using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagBone : MonoBehaviour
{
    public Transform bodyPart;
    
    
    public void LineUp()
    {
        
        Vector3 pos = new Vector3(bodyPart.position.x, bodyPart.position.y, bodyPart.position.z);
        transform.position = pos;
        
        transform.rotation = bodyPart.rotation;
    }
}
