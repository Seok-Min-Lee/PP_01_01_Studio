using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PasswordDigit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void Init(string text)
    {
        this.text.text = text;
    }
}
