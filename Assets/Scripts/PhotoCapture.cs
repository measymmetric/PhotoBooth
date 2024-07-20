using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    public RawImage webcamDisplay;
    public RawImage photoPreview;
    public Button captureButton;
    private WebCamTexture webcamTexture;

    void Start()
    {
        AssignWebcamTexture();
        captureButton.onClick.AddListener(CapturePhoto);
    }

    void AssignWebcamTexture()
    {
        // Retrieve the webcam texture from the WebcamController script
        WebcamController webcamController = FindObjectOfType<WebcamController>();
        if (webcamController != null)
        {
            webcamTexture = webcamController.GetWebCamTexture();
        }
        else
        {
            Debug.LogError("WebcamController not found in the scene.");
            return;
        }

        if (webcamTexture == null)
        {
            Debug.LogError("WebcamDisplay does not have a WebCamTexture assigned.");
            return;
        }
        Debug.Log("WebCamTexture successfully assigned in PhotoCapture script.");
    }

    void CapturePhoto()
    {
        // Create a new Texture2D with the same dimensions as the webcam feed
        Texture2D photo = new Texture2D(webcamTexture.width, webcamTexture.height);
        // Read the pixels from the webcam feed
        photo.SetPixels(webcamTexture.GetPixels());
        photo.Apply();

        // Assign the captured photo to the photo preview RawImage
        photoPreview.texture = photo;
    }
}
