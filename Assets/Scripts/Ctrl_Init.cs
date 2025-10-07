using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Init : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Client is Available? " + (Client.Instance != null));

        StaticValues.sampleTextures = StaticValues.LoadSampleTextures($"{Application.streamingAssetsPath}/samples");

        AudioManager.Instance.Load(() =>
        {
            AudioManager.Instance.Init(volumeBGM: 1f, volumeSFX: 1f);
        });

        Debug.Log(TouchManager.Instance.Canvas == null);

        UnityEngine.SceneManagement.SceneManager.LoadScene("01_Title");
    }

}
