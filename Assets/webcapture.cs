using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;
using System.Reflection.Emit;

public class WebCapture : MonoBehaviour
{
    static IWebDriver driver;
    bool is_captured = false;
    public RawImage rawImage;
    int img_count = 0;
    Screenshot TakeScreenshot;

    void TaskOnClick()
    {
        string imagePath = img_count.ToString() + "_web.png";
        TakeScreenshot.SaveAsFile(imagePath);
        Debug.Log("Image saved: " + imagePath);
        img_count++;

    }

    public void seleniumScreenShot()
    {
        driver = new ChromeDriver();

        var weburl = "https://www.oculus.com/casting";
        driver.Navigate().GoToUrl(weburl);
    }

    // Convert a byte array to a Texture2D
    private Texture2D ByteArrayToTexture2D(byte[] byteArray)
    {
        Texture2D texture = new Texture2D(2, 2); // Create a new Texture2D
        bool isLoaded = texture.LoadImage(byteArray); // Load the byte array into the texture

        if (!isLoaded)
        {
            Debug.LogError("Failed to load texture from byte array.");
            return null;
        }

        return texture;
    }

    // Start is called before the first frame update
    void Start()
    {
        seleniumScreenShot();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            TakeScreenshot = ((ITakesScreenshot)driver).GetScreenshot();
        }
        catch (Exception e)
        {
            Debug.Log("capture failed..." + e.Message);
        }
        byte[] bytes = TakeScreenshot.AsByteArray;
        Texture2D texture = ByteArrayToTexture2D(bytes);
        rawImage.texture = texture;

        // Taking screenshots with Selenium
        if (Input.GetMouseButtonDown(0))
        {
            TaskOnClick();
        }
    }

    void OnDestroy()
    {
        driver.Quit();
    }
}
