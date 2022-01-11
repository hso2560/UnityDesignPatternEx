using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract void Execute(Animator anim, bool forward);
}

public class PerformJump : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        anim.SetTrigger(forward? "isJumping": "isJumpingR");
    }
}

public class PerformPunch : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        anim.SetTrigger(forward ? "isPunching" : "isPunchingR");
        
    }
}

public class PerformKick : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        anim.SetTrigger(forward? "isKicking": "isKickingR");


    }
}

public class MoveForward : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        anim.SetTrigger(forward?"isWalking": "isWalkingR");
        
    }
}

public class DoNothing : Command
{
    public override void Execute(Animator anim, bool forward)
    {
        
    }
}
