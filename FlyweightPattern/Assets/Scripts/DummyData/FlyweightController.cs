using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
    public class FlyweightController : MonoBehaviour
    {
        private List<Heavy> heavyList = new List<Heavy>();
        private List<Flyweight> flyweightList = new List<Flyweight>();

        private void Start()
        {
            int numberOfObjs = 100000;

            for(int i=0; i<numberOfObjs; i++)
            {
                Heavy newHeavy = new Heavy();
                heavyList.Add(newHeavy);
            }

            //플라이웨잇 패턴
            DummyData data = new DummyData();
            for (int i = 0; i < numberOfObjs; i++)
            {
                flyweightList.Add(new Flyweight(data));
            }
        }
    }
}
