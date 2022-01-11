using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class SpawnController : MonoBehaviour
    {
        //public int hp = 100;
        //public int speed = 5;

        //private List<Monster> monsters = new List<Monster>();

        private Ghost ghost;
        private Demon demon;
        private Spawner[] monsters;

        private void Start()
        {
            ghost = new Ghost(100, 10);
            demon = new Demon(200, 20);

            monsters = new Spawner[]
            {
                new Spawner(ghost),
                new Spawner(demon)
            };
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.G))
            {
                Spawner ghostSp = new Spawner(ghost);
                Ghost newGhost = ghostSp.SpawnMonster() as Ghost;
                newGhost.Talk();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Spawner demonSp = new Spawner(ghost);
                Demon newDemon = demonSp.SpawnMonster() as Demon;
                newDemon.Talk();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Spawner ranSp = monsters[Random.Range(0, monsters.Length)];
                Monster newMonster = ranSp.SpawnMonster();
                newMonster.Talk();
            }
        }

        /*private void Start()
        {
            InvokeRepeating("Talk", 1, 1);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                monsters.Add(Spawner.CreateMonster(hp, speed, "random"));
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                monsters.Add(Spawner.CreateMonster(hp, speed, "ghost"));
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                monsters.Add(Spawner.CreateMonster(hp, speed, "demon"));
            }
        }

        void Talk()
        {
            monsters.ForEach(x => x.Talk());
        }*/
    }
}