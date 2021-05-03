using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing_Box : MonoBehaviour
{
    public GameObject Flag_Sprite;
    public GameObject Mine_Sprite;

    public GameObject Click_Prefab;

    public bool Marked = false;
    public bool Flaged = false;
    public bool Mine = false;

    public int x;
    public int y;

    GameManager GM;
    void Start()    {GM = FindObjectOfType<GameManager>();}

    private void OnMouseDown()
    {
       
         if (GM.Mark_Flag_Mod) { GM.Mark_Flag(x, y); }
         else if (Mine)  {GM.Game_Over();}
         else { GM.Check_Corrosponding_Box(x, y);  }
    }
}
