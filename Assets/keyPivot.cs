using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyPivot : MonoBehaviour {

    public float startRotX = 0;
    public float endRotX = -30;
    public float currentRot = 0;
    public float speed = 0.1f;

    enum states { idle, goingDown, comingUp };
    states cState = states.idle;

    public PianoKey pKey;

    public void Start()
    {
        pKey = gameObject.GetComponent<PianoKey>();

    }

    public void Update()
    {

        if (pKey.IsTriggered == true)
        {
            cState = states.goingDown;
            pKey.SetIsTriggered(false);
        }

        switch (cState)
        {
            case states.idle:
                transform.Rotate(Vector3.zero);
                break;
            case states.goingDown:
                if(currentRot <= endRotX)
                {
                    cState = states.comingUp;
                    break;
                }
                currentRot -= speed;
                transform.Rotate(new Vector3(-speed, 0, 0));
                break;
            case states.comingUp:
                if(currentRot >= startRotX)
                {
                    cState = states.idle;
                    break;
                }
                currentRot += speed;
                transform.Rotate(new Vector3(speed, 0, 0));
                break;
        }

    }

}
