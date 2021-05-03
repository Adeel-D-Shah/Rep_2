
using UnityEngine;
public class Timer : MonoBehaviour
{
    public float Time_Remaing = 10;
    private void Update()
    {
        Time_Remaing = (Time_Remaing > 0) ? Time_Remaing-Time.deltaTime : Time_Remaing;
    }
}
