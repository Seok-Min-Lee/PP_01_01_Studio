using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Title : MonoBehaviour
{
    public void OnClickButton(int num)
    {
        AudioManager.Instance.PlaySFX(Sound.Key.Click);

        UnityEngine.SceneManagement.SceneManager.LoadScene("02_Select_" + num);
    }
}
