using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum eState
    {
        IDLE, PATROL, PURSUE, ATTACK, DEAD, RUNAWAY
    };

    public enum eEvent
    {
        ENTER, UPDATE, EXIT
    }

    public eState stateName;

    protected eEvent curEvent;

    protected GameObject myObj;
    protected NavMeshAgent myAgent;
    protected Animator myAnim;
    protected Transform playerTrm;

    protected State nextState;

    float detectDist = 10.0f;
    float detectAngle = 30.0f;
    float shootDist = 7.0f;

    public State(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTrm)
    {
        myObj = obj;
        myAgent = agent;
        myAnim = anim;
        playerTrm = targetTrm;

        curEvent = eEvent.ENTER;
    }

    public virtual void Enter() { curEvent = eEvent.UPDATE; }
    public virtual void MyUpdate() { curEvent = eEvent.UPDATE; }
    public virtual void Exit() { curEvent = eEvent.EXIT; }

    public State Process()
    {
        if (curEvent == eEvent.ENTER) Enter();
        if (curEvent == eEvent.UPDATE) MyUpdate();
        if(curEvent == eEvent.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 dir = playerTrm.position - myObj.transform.position;
        float angle = Vector3.Angle(dir, myObj.transform.forward);

        if(dir.magnitude < detectDist && angle < detectAngle)
        {
            return true;
        }

        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 dir = playerTrm.position - myObj.transform.position;
        if (dir.magnitude < shootDist)
        {
            return true;
        }

        return false;
    }

    public bool IsSurprised()
    {
        //Vector3 dir = myObj.transform.position - playerTrm.position 
        Vector3 dir = playerTrm.position - myObj.transform.position;
        float angle = Vector3.Angle(dir, myObj.transform.forward);

        return (dir.magnitude < 2f && angle > 120f);
    }
}

public class Idle : State
{
    public Idle(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTrm)
        : base(obj,agent,anim,targetTrm)
    {
        stateName = eState.IDLE;
    }

    public override void Enter()
    {
        myAnim.SetTrigger("isIdle");
        base.Enter();
    }

    public override void MyUpdate()
    {
        if(CanSeePlayer())
        {
            nextState = new Pursue(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else if (Random.Range(0, 5000) < 10)
        {
            nextState = new Patrol(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        else if(IsSurprised())
        {
            nextState = new Runaway(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isIdle");
        base.Exit();
    }
}

public class Patrol : State
{
    int curIndex = -1;

    public Patrol(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTrm)
        : base(obj, agent, anim, targetTrm)
    {
        stateName = eState.PATROL;
        myAgent.speed = 2;
        myAgent.isStopped = false;
    }

    public override void Enter()
    {
        //curIndex = 0;

        float lastDist = Mathf.Infinity;
        for(int i=0; i<GameEnvironment.Instance.CheckpointList.Count; i++)
        {
            Transform thisWP = GameEnvironment.Instance.CheckpointList[i].transform;
            float distance = Vector3.Distance(myObj.transform.position, thisWP.position);

            if(distance < lastDist)
            {
                curIndex = i - 1;
                lastDist = distance;
            }
        }
        
        myAnim.SetTrigger("isWalking");
        base.Enter();
    }

    public override void MyUpdate()
    {
        if(myAgent.remainingDistance < 1)
        {
            curIndex = (++curIndex) % GameEnvironment.Instance.CheckpointList.Count;

            myAgent.SetDestination(GameEnvironment.Instance.CheckpointList[curIndex].transform.position);

            
        }
        if (CanSeePlayer())
        {
            nextState = new Pursue(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
        if (IsSurprised())
        {
            nextState = new Runaway(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isWalking");
        base.Exit();
    }
}

public class Pursue : State
{
    public Pursue(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTrm)
        : base(obj, agent, anim, targetTrm)
    {
        stateName = eState.PURSUE;
        myAgent.speed = 5;
        myAgent.isStopped = false;
    }

    public override void Enter()
    {
        myAnim.SetTrigger("isRunning");
        base.Enter();
    }

    public override void MyUpdate()
    {
        myAgent.SetDestination(playerTrm.position);

        if (myAgent.hasPath)
        {
            if(CanAttackPlayer())
            {
                nextState = new Attack(myObj, myAgent, myAnim, playerTrm);
                curEvent = eEvent.EXIT;
            }
            else if (!CanSeePlayer())
            {
                nextState = new Patrol(myObj, myAgent, myAnim, playerTrm);
                curEvent = eEvent.EXIT;
            }
        }
    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isRunning");
        base.Exit();
    }
}

public class Attack : State
{
    float rotationSpeed = 2.0f;

    AudioSource shootEff;

    public Attack(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTrm)
        : base(obj, agent, anim, targetTrm)
    {
        stateName = eState.ATTACK;
        shootEff = obj.GetComponent<AudioSource>();
    }

    public override void Enter()
    {
        myAnim.SetTrigger("isShooting");
        myAgent.isStopped = true;
        shootEff.Play();
        base.Enter();
    }

    public override void MyUpdate()
    {
        //myObj.transform.LookAt(playerTrm);
        Vector3 dir = playerTrm.transform.position - myObj.transform.position;
        float angle = Vector3.Angle(dir, myObj.transform.forward);
        dir.y = 0;
        myObj.transform.rotation = Quaternion.Slerp(myObj.transform.rotation,
            Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);

        if(!CanSeePlayer())
        {
            nextState = new Idle(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }    
    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isShooting");
        shootEff.Stop();
        base.Exit();
    }
}

public class Runaway : State
{
    Transform runawayPoint;

    public Runaway(GameObject obj, NavMeshAgent agent, Animator anim, Transform targetTrm)
        : base(obj, agent, anim, targetTrm)
    {
        stateName = eState.RUNAWAY;
        myAgent.speed = 5;
        myAgent.isStopped = false;

        runawayPoint = GameObject.FindGameObjectWithTag("Runawaypoint").transform;
    }

    public override void Enter()
    {
        myAnim.SetTrigger("isRunning");
        myAgent.isStopped = false;
        base.Enter();
    }

    public override void MyUpdate()
    {
        myAgent.SetDestination(runawayPoint.position);

        if(myAgent.remainingDistance < 1)
        {
            nextState = new Patrol(myObj, myAgent, myAnim, playerTrm);
            curEvent = eEvent.EXIT;
        }
    }

    public override void Exit()
    {
        myAnim.ResetTrigger("isRunning");
        base.Exit();
    }
}