using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeftUI : MonoBehaviour
{
    public Image start;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == true)
        {
            start.rectTransform.anchoredPosition = new Vector2(240, 0);
        }
        else
        {
            start.rectTransform.anchoredPosition = new Vector2(-75, 0);
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene("InGame");
    }
}
