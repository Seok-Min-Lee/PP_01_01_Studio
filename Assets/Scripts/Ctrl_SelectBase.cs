using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_SelectBase : MonoBehaviour
{
    public static Ctrl_SelectBase instance;

    [SerializeField] private Image timebarGuage;
    private float timer = 0f;
    private int timeLimit;
    protected virtual void Start()
    {
        instance = this;
        timeLimit = ConstantValues.TIME_LIMIT_DEFAULT;
    }
    protected virtual void Update()
    {
        if (Input.anyKey)
        {
            timer = 0f;
        }

        timer += Time.deltaTime;
        timebarGuage.fillAmount = 1 - (timer / timeLimit);

        if (timer > timeLimit)
        {
            OnClickHome();
        }
    }
    public void OnClickHome()
    {
        AudioManager.Instance.PlaySFX(Sound.Key.Click);
        UnityEngine.SceneManagement.SceneManager.LoadScene("01_Title");
    }
    public void LoadNext()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("03_Result");
    }
    public void LoadError()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("03_Error");
    }
}
