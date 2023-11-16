using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSceneController : MonoBehaviour
{
    [SerializeField] private Button m_buttonPlay;

    void Start()
    {
        m_buttonPlay.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GamePlay");
        });

    }

    void Update()
    {
                
    }
}
