using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    private Vector2 movement;
    private bool jump;
    private bool rewindState;

    //normalized vector
    public Vector2 Movement
    {
        get
        {
            return this.movement;
        }
    }

    public bool Jump
    {
        get
        {
            return this.jump;
        }
    }

    public bool RewindState
    {
        get
        {
            return this.rewindState;
        }
    }


    public PlayerInput()
    {
        movement = new Vector2(0, 0);
        jump = false;
    }

    public void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        jump = Input.GetButton("Jump");
        rewindState = Input.GetButtonDown("Rewind");
    }
}
