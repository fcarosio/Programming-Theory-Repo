using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUtils : MonoBehaviour
{
    public static string ToTime(int seconds)
    {
        int h = seconds / 3600;
        int m = seconds / 60;
        int s = seconds % 60;

        return string.Format("{0,2:D2}:{1,2:D2};{2,2:D2}", h, m, s);
    }
}
