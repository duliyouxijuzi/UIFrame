using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainConfig : MonoBehaviour
{
    private static MainConfig m_Instance;
    public static MainConfig mInstance
    {
        get { return m_Instance; }
    }
    public RectTransform UIRoot;

    void Awake()
    {
        m_Instance = this;
    }
}
