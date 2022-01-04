using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class FeedBackText : MonoBehaviour
{
    
    
    #region Singleton
    private static FeedBackText _instance;

    public static FeedBackText Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<FeedBackText>();
            }
            
            return _instance;
        }
    }
    #endregion
    
    
    public TextMeshProUGUI FeedbackText;


    private bool isFeedback;

    private void Awake()
    {
        FeedbackText.transform.DOScale(0, 0);
    }

    private void Start()
    {
        
    }

    public void SendFeedBack(string feedback)
    {
        if(isFeedback)
            return;
        
        isFeedback = true;
        
        FeedbackText.text = feedback;
        FeedbackText.transform.DOScale(1, .3f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(2.4f, () =>
            {
                FeedbackText.transform.DOScale(0, .3f).OnComplete(() =>
                {
                    isFeedback = false;
                });
            });
        });
    }


}
