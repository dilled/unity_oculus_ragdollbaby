using UnityEngine;

using System.Collections;

public class ImpactReceiver : MonoBehaviour
{
    float mass = 1.0F; // defines the character mass
    Vector3 impact = Vector3.zero;
    private Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // transform.rotation = collision.transform.rotation;
        // AddImpact(Vector3.up, 125f);
        if (collision.transform.name == "Baby")
        {
            rb.AddRelativeForce(collision.transform.up * 50f);
            rb.AddRelativeForce(collision.transform.forward * 50f);
        }
       // Debug.Log(collision.transform.name);
        //rb.velocity = new Vector3(collision.relativeVelocity.x, collision.relativeVelocity.y);
    }
        void Update()
    {
        // apply the impact force:
        if (impact.magnitude > 0.2F) rb.AddRelativeForce(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }
    // call this function to add an impact force:
    public void AddImpact(Vector3 dir, float force)
    {
        Debug.Log("imp");
        //dir.Normalize();
       // if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir * force / mass;
    }
}