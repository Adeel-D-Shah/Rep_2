using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool Mark_Flag_Mod = false ;
    public Color Marked_Color;
    public Button Flag_Btn;
    public bool Norma_timer = true;
    public GameObject Total_Flags_TXT;
    public GameObject Planted_Flags_TXT;
    public GameObject Timer_M_TXT;
    public GameObject Timer_S_TXT;
    public GameObject Reward_Coin_TXT;
    public GameObject Win_M_TXT;
    public GameObject Win_S_TXT;
    public int M = 0;
    public float S = 0;
    public int Coins_Reward = 10;

    //--------------------------------------
    public GameObject P_1;
    public GameObject P_2;
    public GameObject Box_Prefab;
    public GameObject Box_Click_Prefab;
    public GameObject Mine_Prefab;
    public GameObject Flag_Prefab;
    public GameObject No_1;
    public GameObject No_2;
    public GameObject No_3;
    public GameObject No_4;
    public GameObject No_5;
    public GameObject No_6;
    public GameObject No_7;
    //--------------------------------------
    public GameObject[,] Boxes;
    //public List<GameObject> Mines;
    public List<Vector2> Box_vectors;
    public List<Vector2> Mine_Vectors;
    public List<Vector2> Flag_Vectors;
    public int Min_Mines;
    public int Max_Mines;
    public int Total_Mines;
    public int Total_Empty_Space;
    public int Total_Flags;
    //--------------------------------------
    public float Spacing;
    public int x_length;
    public int y_length;
    public int x = 0;
    public int y = 0;
    //--------------------------------------
    private void Start()
    {
        Total_Mines = Random.Range(Min_Mines,Max_Mines);
        Update_Reward_Coin_Text();
        Spawn_Grid();
        Spawn_Mines();
        Set_Mine_True();
        Total_Empty_Space = Box_vectors.Count;
        Total_Flags = Total_Mines;
        Update_Flags();
    }

    private void FixedUpdate()
    {
        if (Norma_timer) { Timer(); }
        else { Count_Down(); }
    }
    public void Spawn_Grid() {
        Boxes = new GameObject[x_length, y_length];
        float tem_x = x;
        float tem_y = y;
        for (int i = 0; i < x_length; i++)
        {
            for (int j = 0; j < y_length; j++)
            {
                Box_vectors.Add(new Vector2(i, j));
                Boxes[i, j] = Instantiate(Box_Prefab, new Vector3(tem_x, tem_y, 0), Quaternion.identity);
                Boxes[i, j].gameObject.name = i + "" + j;
                Boxes[i, j].GetComponent<Testing_Box>().x = i;
                Boxes[i, j].GetComponent<Testing_Box>().y = j;
                tem_x += Spacing;
            }
            tem_x = x;
            tem_y += Spacing;
        }
    }
    public void Spawn_Mines()
    {
        for (int i = 0; i < Total_Mines; i++)
        {
            int x = Random.Range(0, Box_vectors.Count);
            Vector2 Tem_mine = new Vector2();
            Tem_mine = Box_vectors[x];
            Box_vectors.RemoveAt(x);
            Mine_Vectors.Add(Tem_mine);
            GameObject mn = Instantiate(Mine_Prefab, transform.position, Quaternion.identity);
            mn.transform.position = Boxes[(int)Tem_mine.x, (int)Tem_mine.y].transform.position;
            Boxes[(int)Tem_mine.x, (int)Tem_mine.y].GetComponent<Testing_Box>().Mine_Sprite = mn;
            
        }
    }
    public void Set_Mine_True() {

        for (int i = 0; i < Mine_Vectors.Count; i++)
        {
            Boxes[(int)Mine_Vectors[i].x,(int)Mine_Vectors[i].y].GetComponent<Testing_Box>().Mine = true;
        }
    }
    public void Check_Corrosponding_Box(int x, int y)
    {
        
        if (Boxes[x,y].GetComponent<Testing_Box>().Marked || Boxes[x, y].GetComponent<Testing_Box>().Flaged) { return; }
        spawn_at(Box_Click_Prefab, Boxes[x, y].transform.position);
        Boxes[x, y].GetComponent<SpriteRenderer>().color = Marked_Color;
        Boxes[x, y].GetComponent<BoxCollider2D>().enabled = false;
        if (Boxes[x, y].GetComponent<Testing_Box>().Marked == false)
        {
            Total_Empty_Space--;
            if (Total_Empty_Space == 0) { Game_won(); }
            Boxes[x, y].GetComponent<Testing_Box>().Marked = true;
            int z = Count_Corrosponding_Mines(x, y);
            if (z == 0)   { Mark_Corrosponding_Box(x, y);}
            else          { Spawn_Number(z, Boxes[x, y].transform.position);  }
        }
    }
    public int Count_Corrosponding_Mines(int x, int y) {
        int z = 0;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (!Array_Out_Of_Index(i, j))
                {
                    if (i == x && j == y) { continue; }

                    if (Search_V2_List(new Vector2(i, j), Mine_Vectors)) { z++; }

                }
            }
        }
        return z;
    }
    public void Mark_Corrosponding_Box(int x, int y) {

        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (!Array_Out_Of_Index(i, j) && !Boxes[i,j].GetComponent<Testing_Box>().Flaged)
                {
                     Check_Corrosponding_Box(i, j);
                     

                }
            }
        }

    }
    public bool Array_Out_Of_Index(int x,int y) {
        if (x < 0) { return true; }
        if (x >= x_length) { return true; }
        if (y < 0) { return true; }
        if (y >= y_length) { return true; }
        return false;
    }
    public bool Search_V2_List(Vector2 xy, List<Vector2> z) {

        for (int i = 0; i < z.Count; i++)
        {
            if (z[i]==xy) { return true; }
        }
        return false;
    }
    public void Spawn_Number(int x,Vector2 pos) {
    switch (x) {
            case 1: Instantiate(No_1, pos, Quaternion.identity); break;
            case 2: Instantiate(No_2, pos, Quaternion.identity); break;
            case 3: Instantiate(No_3, pos, Quaternion.identity); break;
            case 4: Instantiate(No_4, pos, Quaternion.identity); break;
            case 5: Instantiate(No_5, pos, Quaternion.identity); break;
            case 6: Instantiate(No_6, pos, Quaternion.identity); break;
            case 7: Instantiate(No_7, pos, Quaternion.identity); break;
        
        }
    }
    public void Mark_Flag(int x, int y) {
        if (Boxes[x, y].GetComponent<Testing_Box>().Flaged) { Un_Flag_Box(x, y);  } else { Flag_Box(x,y);  }
        Update_Flags();
    }
    public void Flag_Box(int x , int y) {
        if (Total_Flags <= 0) { return; }
        Total_Flags--;
        Testing_Box TB_Src = Boxes[x, y].GetComponent<Testing_Box>();
        TB_Src.Flaged = true;
        Vector3 pos = Boxes[x, y].transform.position;
        TB_Src.Flag_Sprite = Instantiate(Flag_Prefab, pos, Quaternion.identity);
        if (Mine_Vectors.Contains(new Vector2 (x,y))) { Flag_Vectors.Add(new Vector2(x, y)); }
        return;
    }
    public void Un_Flag_Box(int x, int y)
    {
        Total_Flags++;
        Testing_Box TB_Src = Boxes[x, y].GetComponent<Testing_Box>();
        TB_Src.Flaged = false;
        Flag_Vectors.Remove(new Vector2(x, y));
        Destroy(TB_Src.Flag_Sprite);
        return;
    }
    public void Switch_to_Flag_Mod() { 
        if (Mark_Flag_Mod) { 
            Mark_Flag_Mod = false;
            Flag_Btn.GetComponent<Image>().color = Color.grey;
        } 
        else { 
            Mark_Flag_Mod = true;
            Flag_Btn.GetComponent<Image>().color = Color.green;
        } }
    public void Load_Menu() { SceneManager.LoadScene(0); }
    public void Reset_Level() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    public void Un_Hide_Mines() {
        for (int i = 0; i < Mine_Vectors.Count; i++)
        {
            int x = (int)Mine_Vectors[i].x;
            int y = (int)Mine_Vectors[i].y;

            if (!Boxes[x,y].GetComponent<Testing_Box>().Flaged) {

                Boxes[x, y].GetComponent<Testing_Box>().Mine_Sprite.GetComponent<SpriteRenderer>().sortingOrder = 2;


            }


        }

    }
    public void Mark_Wrong_Flags() {

        for (int i = 0; i < Box_vectors.Count; i++)
        {
            int x = (int)Box_vectors[i].x;
            int y = (int)Box_vectors[i].y;
            if (Boxes[x, y].GetComponent<Testing_Box>().Flaged && !Boxes[x, y].GetComponent<Testing_Box>().Mine) {

                Boxes[x, y].GetComponent<SpriteRenderer>().color = Color.red;
            }
        }


    }
    public void Game_Over() {
        Norma_timer = false;
        Un_Hide_Mines();
        Mark_Wrong_Flags();
        Stop_Box_Interaction();
        P_2.SetActive(true);
    }
    public void Stop_Box_Interaction() {
        foreach (var item in Boxes) {   item.GetComponent<BoxCollider2D>().enabled = false; }
    }
    public void Game_won() {
        Add_Coins(Coins_Reward);
        Norma_timer = false;
        Stop_Box_Interaction();
        Win_M_TXT.GetComponent<Text>().text = ((int)M).ToString("00");
        Win_S_TXT.GetComponent<Text>().text = ((int)S).ToString("00");
        P_1.SetActive(true);
    }
    public void Auto_Flag_Mine() {

        if (Total_Flags > 0 ) {
            int index = 0;
            for (int i = 0; i < Mine_Vectors.Count; i++)
            {
                if ( !Flag_Vectors.Contains(Mine_Vectors[i])) { index = i;break; }
            }
            Flag_Box((int)Mine_Vectors[index].x,(int) Mine_Vectors[index].y);
        
        }
        Update_Flags();
    }
    public void Timer()
    {
        S += Time.deltaTime;

        if (S >= 60) { S = 0; M++; if (M > 60) { M = 0; } }
        Timer_S_TXT.GetComponent<Text>().text = S.ToString("00");
        Timer_M_TXT.GetComponent<Text>().text = M.ToString("00");
    }
    public void Update_Flags() {

        Total_Flags_TXT.GetComponent<Text>().text = Total_Mines.ToString();
        Planted_Flags_TXT.GetComponent<Text>().text = (Total_Mines-Total_Flags).ToString();

    
    }
    public void Add_Coins(int x) {
        int z = PlayerPrefs.GetInt("COIN",0);
        z += x;
        PlayerPrefs.SetInt("COIN",z);
    
    }
    public void Update_Reward_Coin_Text() {
        Reward_Coin_TXT.GetComponent<Text>().text = Coins_Reward.ToString();
    }
    public void Count_Down()
    {
        S -= Time.deltaTime;

        if (S <=0) { S = 60; M--; if (M < 0) { Game_Over(); } }
        Timer_S_TXT.GetComponent<Text>().text = S.ToString("00");
        Timer_M_TXT.GetComponent<Text>().text = M.ToString("00");
    }
    public void spawn_at(GameObject gm, Vector3 pos) { Instantiate(gm,pos,Quaternion.identity); }
}
