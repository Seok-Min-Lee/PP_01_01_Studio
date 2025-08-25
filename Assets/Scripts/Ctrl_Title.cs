using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Title : MonoBehaviour
{
    public void OnClickButton(int num)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("02_Select_" + num);
    }
}
