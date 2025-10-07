using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleThumbnail : MonoBehaviour
{

    [SerializeField] private Image image;
    [SerializeField] private Image cover;

    [SerializeField] private Ctrl_Select2 ctrl;

    [SerializeField] private int id;
    [SerializeField] private bool isSelected;

    public void Init(int id, Sprite sprite, Ctrl_Select2 ctrl)
    {
        this.id = id;
        this.ctrl = ctrl;
        image.sprite = sprite;
        cover.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        AudioManager.Instance.PlaySFX(Sound.Key.Click);
        ctrl.SelectThumbnail(id);
    }
    public void Select()
    {
        cover.gameObject.SetActive(true);
    }
    public void UnSelect()
    {
        cover.gameObject.SetActive(false);
    }
}
