using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarObject
{
   public Image icon { get; set; }
    public GameObject owner { get; set; }
}

public class Radar : MonoBehaviour
{
    public Transform playerTrm;

    float mapScale = 2.0f;

    public static List<RadarObject> radObjList = new List<RadarObject>();

   

    private void Update()
    {
        DrawRadarDots();
    }

    void DrawRadarDots()
    {
        foreach(RadarObject radObj in radObjList)
        {
            Vector3 radarPos = (radObj.owner.transform.position - playerTrm.position);

            float distToObj = Vector3.Distance(playerTrm.position, radObj.owner.transform.position) * mapScale;
            float deltaY = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270 - playerTrm.eulerAngles.y;

            radarPos.x = distToObj * Mathf.Cos(deltaY * Mathf.Deg2Rad) * -1;
            radarPos.z = distToObj * Mathf.Sin(deltaY * Mathf.Deg2Rad);

            radObj.icon.transform.SetParent(this.transform);
            RectTransform rt = this.GetComponent<RectTransform>();

            radObj.icon.transform.position = new Vector3(radarPos.x + rt.pivot.x, radarPos.z + rt.pivot.y, 0) + this.transform.position;
        }
    }

    public void ItemDropped(GameObject obj)
    {
        print("ItemDropped");
        RegisterRadarObj(obj, obj.GetComponent<Item>().icon);
    }

    void RegisterRadarObj(GameObject obj, Image img)
    {
        Image image = Instantiate(img);
        radObjList.Add(new RadarObject() { owner = obj, icon = image });
    }

    public void ItemPickup(GameObject obj)
    {
        print("ItemPickup");
        UnRegisterRadarObj(obj);
    }

    void UnRegisterRadarObj(GameObject obj)
    {
        

        RadarObject ro = radObjList.Find(x => x.owner == obj);

        

        radObjList.Remove(ro);
        Destroy(ro.owner);
        Destroy(ro.icon);
        
    }

    
}
