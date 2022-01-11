using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class MoveRightCommand : Command
    {
        private MoveObject moveObject;

        public MoveRightCommand(MoveObject moveObject)
        {
            this.moveObject = moveObject;
        }

        public override void Excute()
        {
            moveObject.MoveRight();
        }

        public override void Undo()
        {
            moveObject.MoveLeft();
        }
    }
}
