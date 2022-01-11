using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern.RebindKeys
{
    public class GameController : MonoBehaviour
    {
        public MoveObject moveObj;

        private Command buttonW;
        private Command buttonA;
        private Command buttonS;
        private Command buttonD;

        private Stack<Command> undoCommandStack = new Stack<Command>();
        private Stack<Command> redoCommandQueue = new Stack<Command>();
        private Queue<Command> replayCommandQueue = new Queue<Command>();

        private bool isReplaying = false;
        private Vector3 startPos;

        private WaitForSeconds ws = new WaitForSeconds(0.25f);

        private void Start()
        {
            startPos = moveObj.transform.position;

            buttonW = new MoveForwardCommand(moveObj);
            buttonA = new MoveLeftCommand(moveObj);
            buttonS = new MoveBackCommand(moveObj);
            buttonD = new MoveRightCommand(moveObj);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                ExecuteNewCommand(buttonW);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                ExecuteNewCommand(buttonA);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                ExecuteNewCommand(buttonS);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                ExecuteNewCommand(buttonD);
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                SwapKeys(ref buttonW, ref buttonS);
            }
            
            if(Input.GetKeyDown(KeyCode.U))
            {
                if (undoCommandStack.Count > 0)
                {
                    Command lastCmd = undoCommandStack.Pop();
                    lastCmd.Undo();
                    redoCommandQueue.Push(lastCmd);
                    replayCommandQueue.Enqueue(lastCmd);
                }
                else
                {
                    Debug.Log("no undo");
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (redoCommandQueue.Count > 0)
                {
                    Command nextCmd = redoCommandQueue.Pop();
                    nextCmd.Excute();
                    undoCommandStack.Push(nextCmd);
                }
                else
                {
                    Debug.Log("no redo");
                }
            }

            if(Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(ReplayCo());
            }
        }

        void ExecuteNewCommand(Command commandButton)
        {
            commandButton.Excute();
            undoCommandStack.Push(commandButton);
            replayCommandQueue.Enqueue(commandButton);
        }

        void SwapKeys(ref Command key1, ref Command key2)
        {
            Command tmp = key1;
            key1 = key2;
            key2 = tmp;
        }

        IEnumerator ReplayCo()
        {
            isReplaying = true;
            moveObj.transform.position = startPos;

            while(replayCommandQueue.Count > 0)
            {
                yield return ws;
                replayCommandQueue.Dequeue().Excute();
            }
            isReplaying = false;
        }
    }
}
