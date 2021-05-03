
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public void Menu() { SceneManager.LoadScene(0); }
    public void Mod() { SceneManager.LoadScene(2); }
    public void Easy_Mod() { SceneManager.LoadScene(3); }
    public void Normal_Mod() { SceneManager.LoadScene(4); }
    public void Hard_Mod() { SceneManager.LoadScene(5); }
    public void Gamble_Mod() { SceneManager.LoadScene(6); }


    
}
