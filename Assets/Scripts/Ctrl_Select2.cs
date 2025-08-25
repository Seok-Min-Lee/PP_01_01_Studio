using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Select2 : Ctrl_SelectBase
{
    [SerializeField] private SampleThumbnail[] thumbnails;
    [SerializeField] private Texture2D[] textures;
    private int currentNum = -1;
    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < thumbnails.Length; i++)
        {
            Sprite sprite = Sprite.Create(
                texture: textures[i],
                rect: new Rect(0, 0, textures[i].width, textures[i].height),
                pivot: new Vector2(0.5f, 0.5f)
            );

            thumbnails[i].Init(id: i, sprite: sprite, ctrl: this);
        }
    }
    protected override void Update()
    {
        base.Update();
    }
    public void SelectThumbnail(int num)
    {
        if (currentNum != num)
        {
            if (currentNum > -1)
            {
                thumbnails[currentNum].UnSelect();
            }

            thumbnails[num].Select();
        }

        currentNum = num;
    }
    public void OnClickNext()
    {
        if (currentNum == 99)
        {
            return;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("03_Result");
    }
}
