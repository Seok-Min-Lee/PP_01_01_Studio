using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Init : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Client is Available? " + (Client.Instance != null));

        StaticValues.sampleTextures = StaticValues.LoadSampleTextures($"{Application.streamingAssetsPath}/samples");

        UnityEngine.SceneManagement.SceneManager.LoadScene("01_Title");
    }

}
