using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class BackgroundRemoval : MonoBehaviour
{
    public RawImage photoPreview;
    public RawImage backgroundRemovedPreview;

    private string apiKey = "auU6igvfNm6yvfKvLT77GPFB"; // Replace with your remove.bg API key

    public void RemoveBackground()
    {
        StartCoroutine(RemoveBackgroundCoroutine());
    }

    private IEnumerator RemoveBackgroundCoroutine()
    {
        Texture2D photo = (Texture2D)photoPreview.texture;
        byte[] photoBytes = photo.EncodeToPNG();

        string url = "https://api.remove.bg/v1.0/removebg";
        WWWForm form = new WWWForm();
        form.AddField("size", "auto");
        form.AddBinaryData("image_file", photoBytes, "photo.png", "image/png");

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            www.SetRequestHeader("X-Api-Key", apiKey);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                byte[] resultBytes = www.downloadHandler.data;
                Texture2D resultTexture = new Texture2D(2, 2);
                resultTexture.LoadImage(resultBytes);
                backgroundRemovedPreview.texture = resultTexture;
            }
        }
    }
}