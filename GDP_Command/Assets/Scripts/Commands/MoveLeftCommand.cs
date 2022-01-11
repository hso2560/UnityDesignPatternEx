using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class MoveLeftCommand : Command
    {
        private MoveObject moveObject;

        public MoveLeftCommand(MoveObject moveObject)
        {
            this.moveObject = moveObject;
        }

        public override void Excute()
        {
            moveObject.MoveLeft();
        }

        public override void Undo()
        {
            moveObject.MoveRight();
        }


    }
}
