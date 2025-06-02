using System.Collections.Generic;
using UnityEngine;

public class Levitate : MonoBehaviour
{
    public float radius = 10f;
    public float levitateForce = 5f;
    private List<Rigidbody> objsInRange = new List<Rigidbody>();

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            LevitateNearbyObjects();
        }
        else
        {
            objsInRange.Clear();
        }
    }

    void LevitateNearbyObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearObj in colliders)
        {
            Rigidbody rb = nearObj.attachedRigidbody;
            if (rb != null && !rb.CompareTag("Player"))
            {
                rb.AddForce(Vector3.up * levitateForce, ForceMode.Acceleration);
                objsInRange.Add(rb);
            }
        }
    }
}
