using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Ghost : Monster
    {
        private int hp;
        private int speed;

        private static int ghostCounter = 0;

        public Ghost(int hp, int speed)
        {
            this.hp = hp;
            this.speed = speed;
            ghostCounter++;
        }

        public override Monster Clone()
        {
            return new Ghost(hp,speed);
        }

        public override void Talk()
        {
            Debug.Log(string.Format("난 {0}번째 유령, 체력: {1}, 스피드: {2}",ghostCounter,hp,speed));
        }
    }
}

