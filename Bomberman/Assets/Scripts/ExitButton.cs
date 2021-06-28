using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(exitApp);
    }

    void exitApp()
    {
        Application.Quit();
    }
}
