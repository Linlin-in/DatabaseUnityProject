using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainPageManager : MonoBehaviour
{
    #region Singleton
    private static MainPageManager _instance;

    public static MainPageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MainPageManager>();
            }
            
            return _instance;
        }
    }
    #endregion

    public TextMeshProUGUI MainPageEnterText;

    public void StartMainPage()
    {
        UIManager.Instance.OpenMainPage();
        MainPageEnterText.text = "Hoşgeldin " + UserLoginManager.Instance.GetCurrentUsername() + ".";
    }
    
}
