using UnityEngine;
using System.Collections;

public class CelestialBody : MonoBehaviour
{

    [SerializeField]
    float mass;
    [SerializeField]
    float radius;
    [SerializeField] Vector2 initialVelocity;
    Vector2 currentVelocity;

    
    // Use this for initialization
    void Start()
    {
        currentVelocity = initialVelocity;
    }

    public void updateVelocity(CelestialBody[] allBodies,float timeStep)
    {
        foreach(var otherBody in allBodies)
        {
            if(otherBody != this)
            {
                float sqrDst = (otherBody.transform.position - transform.position).sqrMagnitude;
                Vector2 forceDir = (otherBody.transform.position - transform.position).normalized;
                Debug.Log("sqrd: " + sqrDst);
                Vector2 force = forceDir * GameController.GRAVITY * mass * otherBody.mass / sqrDst;
                Debug.Log("force: " + force);
                Vector2 acceleration = force / mass;
                currentVelocity += acceleration * timeStep;
            }
        }
    }

    public void updatePosition(float timeStep)
    {
        transform.position += (Vector3)currentVelocity * timeStep;
    }

}
