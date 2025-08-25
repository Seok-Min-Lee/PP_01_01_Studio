using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Init : MonoBehaviour
{
    private void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("01_Title");
    }
}
