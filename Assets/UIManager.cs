using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    #region Singleton
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }
            
            return _instance;
        }
    }
    #endregion
    
    
    public GameObject LoginPage;
    public GameObject SignUpPage;
    public GameObject MainPage;

    private void Start()
    {

        CloseAllPages();
        
        LoginPage.SetActive(true);
    }
    
    private void CloseAllPages()
    {
        MainPage.SetActive(false);
        LoginPage.SetActive(false);
        SignUpPage.SetActive(false);
    }

    public void OpenSignUpPage()
    {
        CloseAllPages();
        SignUpPage.SetActive(true);
    }
    
    public void OpenLogInPage()
    {
        CloseAllPages();
        LoginPage.SetActive(true);
    }

    public void OpenMainPage()
    {
        CloseAllPages();
        MainPage.SetActive(true);
    }
    
}
