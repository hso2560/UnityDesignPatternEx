using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Spawner
    {
        private Monster prototype;

        public Spawner(Monster prototype)
        {
            this.prototype = prototype;
        }

        public Monster SpawnMonster()
        {
            return prototype.Clone();
        }

        /*public static Monster CreateMonster(int hp, int speed, string name)
        {
            switch(name)
            {
                case "ghost":
                    return new Ghost(hp, speed);
                case "demon":
                    return new Demon(hp, speed);
                default:
                    if(Random.Range(0,2)<1) return new Ghost(hp, speed);
                    else return new Demon(hp, speed);
            }
        }*/
    }
}