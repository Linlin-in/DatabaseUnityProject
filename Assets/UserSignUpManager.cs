using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserSignUpManager : MonoBehaviour
{
    public TMP_InputField Name;
    public TMP_InputField Username;
    public TMP_Dropdown Gender;
    public TMP_InputField Age;
    public TMP_InputField Password;
    public TMP_InputField Location;



    public void GetSignIn()
    {
        WWWForm data = new WWWForm();
        data.AddField("name", Name.text);
        data.AddField("username", Username.text);
        data.AddField("gender", Gender.options[Gender.value].text);
        data.AddField("age", Age.text);
        data.AddField("pswd", Password.text);
        data.AddField("location", Location.text);

        StartCoroutine(DBManager.SignInCoroutine(data));
    }
    
}
