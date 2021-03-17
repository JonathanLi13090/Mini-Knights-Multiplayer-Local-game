using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_object : MonoBehaviour
{
    public Transform Target;
    public float VerticalOffset = 1;
    public float HorizontalOffset = 2;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Target.position.x + HorizontalOffset, Target.position.y + VerticalOffset);
    }
}