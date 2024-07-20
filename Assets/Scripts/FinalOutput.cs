using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FinalOutput : MonoBehaviour
{
    public RawImage backgroundRemovedPreview; // RawImage for photo with background removed
    public RawImage finalOutput; // RawImage for displaying final output
    public RawImage videoBackground; // RawImage for background video/GIF

    // Method to generate the final combined image
    public void GenerateFinalOutput()
    {
        // Retrieve textures from RawImage components
        Texture2D photoTexture = (Texture2D)backgroundRemovedPreview.texture;
        Texture2D backgroundTexture = (Texture2D)videoBackground.texture;

        // Create a RenderTexture to combine textures
        RenderTexture renderTexture = new RenderTexture(photoTexture.width, photoTexture.height, 24);
        RenderTexture.active = renderTexture;

        // Create a new Texture2D for the final output
        Texture2D finalTexture = new Texture2D(photoTexture.width, photoTexture.height);

        // Draw the background texture to the RenderTexture
        Graphics.Blit(backgroundTexture, renderTexture);

        // Draw the photo texture on top of the background
        Graphics.Blit(photoTexture, renderTexture);

        // Read the pixels from RenderTexture into finalTexture
        finalTexture.ReadPixels(new Rect(0, 0, photoTexture.width, photoTexture.height), 0, 0);
        finalTexture.Apply();

        // Display the final texture
        finalOutput.texture = finalTexture;

        // Clean up
        RenderTexture.active = null;
        renderTexture.Release();
    }

    // Method to save the final image as a PNG file
    public void SaveFinalImage()
    {
        // Check if finalOutput.texture is of type Texture2D
        if (finalOutput.texture is Texture2D finalTexture)
        {
            // Encode texture to PNG
            byte[] bytes = finalTexture.EncodeToPNG();

            // Define the file path
            string path = Application.persistentDataPath + "/finalImage.png";

            // Save the PNG file
            File.WriteAllBytes(path, bytes);

            // Log the path for debugging
            Debug.Log("Image saved to: " + path);
        }
        else
        {
            Debug.LogError("finalOutput.texture is not a Texture2D.");
        }
    }
}