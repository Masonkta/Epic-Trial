using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class descend : MonoBehaviour
{
    public bool droppingStarted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (droppingStarted)
            transform.Translate(Vector3.down * 5f * Time.deltaTime);
    }
}
