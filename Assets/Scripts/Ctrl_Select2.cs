using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Select2 : Ctrl_SelectBase
{
    [SerializeField] private Transform sampleParent;
    [SerializeField] private SampleThumbnail samplePrefab;
    private List<SampleThumbnail> thumbnails = new List<SampleThumbnail>();

    private List<Texture2D> sampleTextures = new List<Texture2D>();
    private int currentNum = -1;
    protected override void Start()
    {
        base.Start();

        sampleTextures = StaticValues.sampleTextures != null ? 
            StaticValues.sampleTextures : 
            StaticValues.sampleTextures = StaticValues.LoadSampleTextures($"{Application.streamingAssetsPath}/samples"); ;

        for (int i = 0; i < sampleTextures.Count; i++)
        {
            SampleThumbnail thumbnail = GameObject.Instantiate<SampleThumbnail>(samplePrefab, sampleParent);

            Sprite sprite = Sprite.Create(
                texture: sampleTextures[i],
                rect: new Rect(0, 0, sampleTextures[i].width, sampleTextures[i].height),
                pivot: new Vector2(0.5f, 0.5f)
            );

            thumbnail.Init(id: i, sprite: sprite, ctrl: this);
            thumbnails.Add(thumbnail);
        }

        Debug.Log("Client is Available? " + (Client.Instance != null));
    }
    //protected override void Update()
    //{
    //    base.Update();
    //}
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
        if (currentNum == -1)
        {
            return;
        }

        StaticValues.textureBytes = sampleTextures[currentNum].EncodeToJPG();
        Client.Instance.RequestGetPassword();
    }
}
