using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticValues
{
    public static int password;
    public static byte[] textureBytes;

    public static List<Texture2D> sampleTextures;


    public static List<Texture2D> LoadSampleTextures(string dir)
    {
        List<Texture2D> textures = new List<Texture2D>();

        string[] pathes = System.IO.Directory.GetFiles(dir, "*.jpg");

        for (int i = 0; i < pathes.Length; i++)
        {
            Texture2D tex = new Texture2D(0, 0);
            tex.LoadImage(System.IO.File.ReadAllBytes(pathes[i]));

            textures.Add(tex);
        }

        return textures;
    }
}
