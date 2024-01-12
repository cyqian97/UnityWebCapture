using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseWebCamTexture : MonoBehaviour
{
    static WebCamTexture cam;
    public RawImage rawImage;
    int img_count = 0;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        Debug.Log("device name: "+devices[1].name);
        cam = new WebCamTexture(devices[1].name);
        cam.requestedWidth = 1920;
        cam.requestedHeight = 1080;
        rawImage.texture = cam;
        cam.Play();
    }

    void Update()
    {
        //This is to take the picture, save it and stop capturing the camera image.
        if (Input.GetMouseButtonDown(0))
        {
            TakeSnapshot();
        }
    }


    void TakeSnapshot()
    {
        Texture2D snap = new Texture2D(cam.width, cam.height);
        snap.SetPixels(cam.GetPixels());
        snap.Apply();
        string img_name = img_count.ToString() + "_cam.png";
        System.IO.File.WriteAllBytes(img_name, snap.EncodeToPNG());
        Debug.Log("Image saved: " + img_name);
        img_count++;
    }
}