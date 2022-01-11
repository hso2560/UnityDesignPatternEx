using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class MoveObject : MonoBehaviour
    {
        private const float MOVE_STEP_DISTANCE = 1f;

        public void MoveForward()
        {
            Move(Vector3.up);
        }

        public void MoveBack()
        {
            Move(Vector3.down);
        }

        public void MoveLeft()
        {
            Move(Vector3.left);
        }

        public void MoveRight()
        {
            Move(Vector3.right);
        }

        private void Move(Vector3 dir)
        {
            transform.Translate(dir * MOVE_STEP_DISTANCE);
        }
    }
}

