using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class webcapture : MonoBehaviour
{
    static IWebDriver driver;
    bool is_captured = false;
    public Button yourButton;


    void TaskOnClick()
    {
        try
        {
            Debug.Log("capture started...");
            Screenshot TakeScreenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string imagePath = "./selenium-screenshot-2.png";
            TakeScreenshot.SaveAsFile(imagePath);
            Debug.Log("capture finished...");
        }
        catch (Exception e)
        {
            Debug.Log("capture failed...");
        }
    }

    public void seleniumScreenShot()
    {

        Debug.Log("in function...");
        driver = new ChromeDriver();

        Debug.Log("driver created...");
        var weburl = "https://www.oculus.com/casting";
        driver.Navigate().GoToUrl(weburl);
        try
        {
            Debug.Log("capture started...");
            Screenshot TakeScreenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string imagePath = "./selenium-screenshot.png";
            TakeScreenshot.SaveAsFile(imagePath);
            Debug.Log("capture finished...");
        }
        catch (Exception e)
        {
            Debug.Log("capture failed...");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
            Button btn = yourButton.GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);
            Debug.Log("starting...");
    }

    // Update is called once per frame
    void Update()
    {

        // Taking screenshots with Selenium
        if (!is_captured)
        {
            is_captured = true;

            Debug.Log("before function...");
            seleniumScreenShot();
            Debug.Log("is captured: "+is_captured);
        }
        //end

    }

    void OnDestroy()
    {
        driver.Quit();
    }
}
