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
    
    public static string Username;
    public static string Pswd;
    
    void Start()
    {
        //StartCoroutine(StartGetLikes());
    }

    public static IEnumerator StartGetLikes()
    {
        string uri = "http://localhost/UnityGate/GetLikes.php/";
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
                    print(webRequest.downloadHandler.text);
                    EventsPageManager.Instance.CalculateLikes(webRequest.downloadHandler.text);
                    //EventsPageManager.Instance.OpenEventsPage(webRequest.downloadHandler.text.Split(':'));
                    break;
            }
        }
    }
    
    public static IEnumerator StartGetCategories()
    {
        string uri = "http://localhost/UnityGate/GetEvents.php/";
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
                    //print(webRequest.downloadHandler.text);
                    EventsPageManager.Instance.OpenEventsPage(webRequest.downloadHandler.text.Split(':'));
                    break;
            }
        }
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
                else
                {
                    FeedBackText.Instance.SendFeedBack("Wrong username or password");
                }
            }
        }
    }
    
    public static IEnumerator AddLike(WWWForm data)
    {
        using (UnityWebRequest request = UnityWebRequest.Post("http://localhost/UnityGate/AddLike.php/", data))
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
                print("oldu");
                EventsPageManager.Instance.StartGetLiesMethod();
            }
        }
    }
    
    public static IEnumerator GetLocationWithCategoryData(WWWForm data)
    {
        
        using (UnityWebRequest request = UnityWebRequest.Post("http://localhost/UnityGate/GetLocationsWithCategoryData.php/", data))
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
                print(request.result);
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
                FeedBackText.Instance.SendFeedBack("Bir şeyler yanlış gitti, bilgileri kontrol edip tekrar dene");

            }
            else
            {
                UIManager.Instance.OpenLogInPage();
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


