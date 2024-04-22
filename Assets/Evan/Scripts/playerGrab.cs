using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGrab : MonoBehaviour
{

    public Transform grabPointSphere;
    playerMovement moveScript;
    Transform forwardTransform;
    Transform cameraTransform;

    [Header("Turning")]
    public float distInFront;



    void Start()
    {
        moveScript = GetComponent<playerMovement>();
        forwardTransform = moveScript.forwardTransform;
        cameraTransform = moveScript.cameraTransform;
    }

    void Update()
    {
        Vector3 direction = forwardTransform.position + forwardTransform.transform.forward * distInFront - cameraTransform.position; RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, direction, out hit, direction.magnitude * 2f))
        {
            grabPointSphere.gameObject.SetActive(true);
            grabPointSphere.position = hit.point;
            Debug.DrawLine(cameraTransform.position, hit.point, Color.red);
        }
        else
        {
            grabPointSphere.gameObject.SetActive(false);
        }
    }


}
