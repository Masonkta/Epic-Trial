using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class descend : MonoBehaviour
{
    public Tutorial tutorialScript;

    public bool droppingStarted = false;
    public float timeOfDrop;

    // Start is called before the first frame update
    void Start()
    {
        tutorialScript = GameObject.FindGameObjectWithTag("TutorialHandler").GetComponent<Tutorial>();
    }

    // Update is called once per frame
    void Update()
    {
        if (droppingStarted)
        {
            transform.Translate(Vector3.down * 1f * Time.deltaTime);

            if (Time.time > timeOfDrop + 3f)
            {
                tutorialScript.wallsAreDone();
                droppingStarted = false;
            }
        }

        
    }
}
