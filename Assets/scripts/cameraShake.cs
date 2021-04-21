using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    public Animator animator;

    public void shakeCamera()
    {
        int randNum = Random.Range(0, 2);
        if(randNum == 0) { animator.SetTrigger("Shake"); }
        else if(randNum == 1) { animator.SetTrigger("shake1"); }
        
    }
}
