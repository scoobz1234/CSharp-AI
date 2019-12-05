using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    public GameObject verticalReticle;
    public GameObject horizontalReticle;

    void Update() 
    {
        int layerMask = 1 << 8;

       //layerMask = ~layerMask;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else 
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
