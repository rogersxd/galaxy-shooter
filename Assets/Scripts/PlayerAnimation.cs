using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animation;

    // Start is called before the first frame update
    void Start()
    {
        _animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("RIGHT");
            _animation.SetBool("TurnRight", true);
            _animation.SetBool("TurnLeft", false);

            return;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _animation.SetBool("TurnLeft", true);
            _animation.SetBool("TurnRight", false);

            return;
        }

        _animation.SetBool("TurnRight", false);
        _animation.SetBool("TurnLeft", false);
    }
}
