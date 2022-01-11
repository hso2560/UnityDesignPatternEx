
using UnityEngine;
using UnityEngine.UI;

public class MsgCon : MonoBehaviour
{
    Text msgTxt;

    private void Awake()
    {
        msgTxt = GetComponent<Text>();
        msgTxt.enabled = false;
    }

    public void SetMessage(GameObject obj)
    {
        msgTxt.text = obj.name + " æ∆¿Ã≈€ »πµÊ";
        msgTxt.enabled = true;

        CancelInvoke("TurnOff");
        Invoke("TurnOff", 2);
    }

    void TurnOff()
    {
        msgTxt.enabled = false;
    }
}
