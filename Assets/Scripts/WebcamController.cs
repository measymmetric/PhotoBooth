using UnityEngine;
using UnityEngine.UI;

public class WebcamController : MonoBehaviour
{
    public RawImage webcamDisplay;

    private WebCamTexture webcamTexture;

    void Start()
    {
        InitializeWebcam();
    }

    void InitializeWebcam()
    {
        // Get the device's default webcam
        webcamTexture = new WebCamTexture();
        // Check if the webcamTexture is successfully created
        if (webcamTexture == null)
        {
            Debug.LogError("Failed to create WebCamTexture.");
            return;
        }
        // Assign the webcam texture to the RawImage
        webcamDisplay.texture = webcamTexture;
        Debug.Log("WebCamTexture assigned to RawImage.");
        // Start the webcam feed
        webcamTexture.Play();
    }

    public WebCamTexture GetWebCamTexture()
    {
        return webcamTexture;
    }
}
