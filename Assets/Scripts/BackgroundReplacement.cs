using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Video;
using SFB; // Add this for StandaloneFileBrowser

public class BackgroundReplacement : MonoBehaviour
{
    public RawImage backgroundRemovedPreview; // The image with the background removed
    public RawImage finalOutput; // The RawImage displaying the video
    public Button selectVideoButton; // Button for selecting the video/GIF

    private string videoPath; // Path to the selected video/GIF file
    private VideoPlayer videoPlayer; // VideoPlayer component for playing the video

    void Start()
    {
        // Add a listener to the select video button
        selectVideoButton.onClick.AddListener(SelectVideo);
    }

    // Method to open a file picker and select a video/GIF file
    public void SelectVideo()
    {
        var extensions = new[] {
            new ExtensionFilter("Video Files", "mp4", "avi", "mov", "mkv", "gif"),
        };
        var paths = StandaloneFileBrowser.OpenFilePanel("Select Video", "", extensions, false);
        if (paths.Length > 0)
        {
            videoPath = paths[0];
            ApplyBackground();
        }
    }

    // Method to start the video loading process
    public void ApplyBackground()
    {
        StartCoroutine(LoadVideo());
    }

    // Coroutine to load and play the selected video
    private IEnumerator LoadVideo()
    {
        // Create a new VideoPlayer component
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.url = videoPath; // Set the URL of the video player to the selected video path
        videoPlayer.renderMode = VideoRenderMode.APIOnly; // Set render mode to APIOnly
        videoPlayer.isLooping = true; // Loop the video if needed

        // Prepare the video player and wait until it's prepared
        videoPlayer.prepareCompleted += PrepareCompleted;
        videoPlayer.Prepare();
        yield return new WaitUntil(() => videoPlayer.isPrepared);
    }

    // Method called when the video player is prepared
    private void PrepareCompleted(VideoPlayer source)
    {
        // Ensure that finalOutput RawImage is correctly set with the video player's texture
        if (finalOutput != null)
        {
            // Set the texture of the final output RawImage to the video player's texture
            finalOutput.texture = videoPlayer.texture;

            // Ensure the backgroundRemovedPreview is on top
            if (backgroundRemovedPreview != null)
            {
                backgroundRemovedPreview.gameObject.SetActive(true);
            }

            // Play the video
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("finalOutput RawImage is missing or not initialized properly.");
        }
    }
}
