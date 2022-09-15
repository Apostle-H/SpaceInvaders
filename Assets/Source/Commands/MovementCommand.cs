using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCommand
{
    public void Move(Rigidbody2D movableRB, Vector2 direction, float moveSpeed)
    {
        movableRB.velocity = direction * moveSpeed;
    }

    public void Stop(Rigidbody2D movableRB)
    {
        Move(movableRB, Vector2.zero, 0f);
    }
}
