using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] private GameObject m_wallResetPoint;
    [SerializeField] private GameObject m_wallSpawnPoint;

    void Start()
    {
        
    }

    void Update()
    {
        if(transform.position.y < m_wallResetPoint.transform.position.y)
        {
            transform.position = m_wallSpawnPoint.transform.position;
        }
    }


}
