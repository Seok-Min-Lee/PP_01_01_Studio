using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_Result : MonoBehaviour
{
    [SerializeField] private PasswordDigit[] digits;
    [SerializeField] private Image timebarGuage;

    private float timer = 0f;
    private int timeLimit;
    private void Start()
    {
        timeLimit = ConstantValues.TIME_LIMIT_DEFAULT;
        DisplayPassword(StaticValues.password);
    }
    private void Update()
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
    private void DisplayPassword(int password)
    {
        string numberString = password.ToString("D4");

        for (int i = 0; i < digits.Length; i++)
        {
            digits[i].Init(numberString[i].ToString());
        }
    }
}
