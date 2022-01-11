using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Linq;

public static class Utils 
{
    public static string AllStr = "12367890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPLKJHGFDSAAZXCVBNM";

    public static string GetRandomName(int length)
    {
        return new string(Enumerable.Repeat(AllStr, length).Select(s => s[UnityEngine.Random.Range(0, s.Length)]).ToArray());
        /*int count = UnityEngine.Random.Range(6, 11);
        StringBuilder name = new StringBuilder("");

        for(int i=0; i<count; i++)
        {
            name.Append(AllStr[UnityEngine.Random.Range(0, AllStr.Length)]);
        }

        return name.ToString();*/
    }

    public static T GetRandomEnum<T>()
    {
        Array arr = Enum.GetValues(typeof(T));
        return (T)arr.GetValue(UnityEngine.Random.Range(0, arr.Length));
    }
}
