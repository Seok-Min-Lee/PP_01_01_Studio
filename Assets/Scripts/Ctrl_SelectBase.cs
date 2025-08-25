using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_SelectBase : MonoBehaviour
{
    [SerializeField] private Image timebarGuage;
    private float timer = 0f;
    private int timeLimit;
    protected virtual void Start()
    {
        timeLimit = StaticValues.TIME_LIMIT_DEFAULT;
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("01_Title");
    }
}
