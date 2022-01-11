using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Demon : Monster
    {
        private int hp;
        private int speed;

        private static int demonCounter = 0;

        public Demon(int hp, int speed)
        {
            this.hp = hp;
            this.speed = speed;
            demonCounter++;
        }

        public override Monster Clone()
        {
            return new Demon(hp, speed);
        }

        public override void Talk()
        {
            Debug.Log(string.Format("�� {0}��° ����, ü��: {1}, ���ǵ�: {2}", demonCounter, hp, speed));
        }
    }
}