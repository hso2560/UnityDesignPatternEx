using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public abstract class Monster
    {
        public abstract Monster Clone();

        public abstract void Talk();
    }
}
