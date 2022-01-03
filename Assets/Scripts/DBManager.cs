using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
     /*
    #region Singleton
    private static DBManager _instance;

    public static DBManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DBManager>();
            }
            
            return _instance;
        }
    }
    #endregion
    */
    
    void Start()
    {
        //StartCoroutine(GetRequest("s"));
    }
    
    public static IEnumerator LogInCoroutine(WWWForm data)
    {
        
        using (UnityWebRequest request = UnityWebRequest.Post("http://localhost/UnityGate/LogIn.php/", data))
        {
            request.timeout = 5;

            yield return request.SendWebRequest();

            //already done
            while (!request.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                if (request.downloadHandler.text.Equals("1"))
                {
                    MainPageManager.Instance.StartMainPage();
                }
            }
        }

    }
    
    public static IEnumerator SignInCoroutine(WWWForm data)
    {
        
        using (UnityWebRequest request = UnityWebRequest.Post("http://localhost/UnityGate/SignUpUser.php/", data))
        {
            request.timeout = 5;

            yield return request.SendWebRequest();

            //already done
            while (!request.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log("Form upload complete! output : " + request.downloadHandler.text);
            }
        }

    }
    

    IEnumerator GetRequest(string uri)
    {
        uri = "http://localhost/UnityGate/GetUserData.php";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: \n" + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    
    
}
