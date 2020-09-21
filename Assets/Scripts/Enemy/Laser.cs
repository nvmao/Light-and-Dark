using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{

    LineRenderer lineRenderer;

    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    int left = -1;

   

    //[SerializeField] Transform start;
    //[SerializeField] Transform end;

    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;

    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position,left * transform.right, 1000, layerMask);

        if (hit.collider != null)
        {
            Debug.DrawLine(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1), Color.yellow);
            lineRenderer.SetPosition(1, hit.point );

        }
        else
        {
            lineRenderer.SetPosition(1, transform.right);
        }
    }

  

}
