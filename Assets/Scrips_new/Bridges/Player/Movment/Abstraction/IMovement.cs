using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    void MoveForward();
    void MoveBackward();
    void MoveLeft();
    void MoveRight();
    void Jump();
}
