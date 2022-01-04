using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserLoginManager : MonoBehaviour
{
    
    #region Singleton
    private static UserLoginManager _instance;

    public static UserLoginManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UserLoginManager>();
            }
            
            return _instance;
        }
    }
    #endregion

    public TMP_InputField UserName;
    public TMP_InputField Password;

    private string lastEnteredUserName;
    
    public void LogIn()
    {
        WWWForm data = new WWWForm();
        data.AddField("username", UserName.text);
        data.AddField("pswd", Password.text);

        lastEnteredUserName = UserName.text;
        
        StartCoroutine(DBManager.LogInCoroutine(data));
        
        
    }

    public string GetCurrentUsername()
    {
        return lastEnteredUserName;
    }

}
