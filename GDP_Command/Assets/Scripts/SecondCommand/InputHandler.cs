using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject playerObj;

    private Animator anim;

    Command keyQ, keyW, keyE, upArrow;

    List<Command> recordList = new List<Command>();
    WaitForSeconds ws = new WaitForSeconds(1);

    Coroutine replayCoroutine;

    bool bStartReplay;
    bool bIsReplaying;

    private void Start()
    {
        anim = playerObj.GetComponent<Animator>();

        keyQ = new PerformJump();
        keyW = new PerformKick();
        keyE = new PerformPunch();

        upArrow = new MoveForward();

        Camera.main.GetComponent<CameraFollow360>().player = playerObj.transform;
    }

    private void Update()
    {
        if(!bIsReplaying)
           HandleInput();

        StartReplay();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ExecuteCommand(keyQ);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            ExecuteCommand(keyW);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ExecuteCommand(keyE);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ExecuteCommand(upArrow);
        }
        
        if(Input.GetKeyDown(KeyCode.Return))
        {
            bStartReplay = true;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            UndoLastCommand();
        }
    }

    private void ExecuteCommand(Command cmd)
    {
        cmd.Execute(anim, true);
        recordList.Add(cmd);
    }

    void UndoLastCommand()
    {
        if (recordList.Count > 0)
        {
            Command cmd = recordList[recordList.Count - 1];
            recordList.RemoveAt(recordList.Count - 1);
            cmd.Execute(anim, false);
        }
    }

    void StartReplay()
    {
        if(bStartReplay && recordList.Count>0)
        {
            bStartReplay = false;
            if (replayCoroutine != null)
            {
                StopCoroutine(replayCoroutine);
            }

            replayCoroutine = StartCoroutine(ReplayCommands());
        }
    }

    IEnumerator ReplayCommands()
    {
        bIsReplaying = true;

        for(int i=0; i<recordList.Count; i++)
        {
            recordList[i].Execute(anim, true);
            yield return ws;
        }

        bIsReplaying = false;
    }

   /* IEnumerator Rewind()
    {
        while (recordList.Count > 0)
        {
            Command cmd = recordList[recordList.Count - 1];
            recordList.RemoveAt(recordList.Count - 1);
            cmd.Execute(anim,false);
            yield return ws;
        }
    }*/
}
