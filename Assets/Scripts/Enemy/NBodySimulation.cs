using UnityEngine;
using System.Collections;

public class NBodySimulation : MonoBehaviour
{
    CelestialBody[] bodies;

    // Use this for initialization
    void Start()
    {
        //Time.timeScale = 2f;
        bodies = FindObjectsOfType<CelestialBody>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var body in bodies)
        {
            body.updateVelocity(bodies,Time.deltaTime);
        }

        foreach (var body in bodies)
        {
            body.updatePosition(Time.deltaTime);
        }

    }
}
