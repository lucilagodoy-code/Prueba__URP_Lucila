using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimation : MonoBehaviour
{

    Animator animator;
    const float locAnimationSmoothTime = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, 0, z);

        float speedPercent = Mathf.Clamp01(move.magnitude);

        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speedPercent /= 0.5f; 
        }	

        animator.SetFloat("SpeedPercent", speedPercent, locAnimationSmoothTime, Time.deltaTime);
    }
}
