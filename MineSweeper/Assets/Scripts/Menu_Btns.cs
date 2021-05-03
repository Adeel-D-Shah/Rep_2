using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_Btns : MonoBehaviour
{
    public GameObject Coin_Text;
    private void Start()
    {
        Update_Coins();
    }
    public void play() { SceneManager.LoadScene(1); }
    public void Update_Coins() { Coin_Text.GetComponent<Text>().text = PlayerPrefs.GetInt("COIN").ToString(); }
}
