using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFormMono : UIPrefabMono
{
    private RectTransform m_rectTransform;
    private Canvas m_canvas;
    private List<UILayer> m_autoLayers;

    private string m_layerName = string.Empty;
    public Canvas mCanvas => m_canvas;

    void Awake()
    {
        //测试时将层级设为Home
        m_layerName = "Home";
        transform.localScale = Vector3.zero;
        m_rectTransform = GetComponent<RectTransform>();
        m_canvas = GetComponent<Canvas>();
        m_autoLayers = new List<UILayer>();

        //重置坐标相关
        m_rectTransform.anchorMin = Vector2.zero;
        m_rectTransform.anchorMax = Vector2.one;
        m_rectTransform.anchoredPosition3D = Vector3.zero;
        m_rectTransform.offsetMin = Vector2.zero;
        m_rectTransform.offsetMax = Vector2.zero;
        m_rectTransform.pivot = Vector3.one * 0.5f;
        m_rectTransform.localScale = Vector3.one;

        SetLayer(m_layerName);
    }

    private void OnEnable()
    {
        SetLayer(m_layerName);
    }
    //设置显示层级
    public void SetLayer(string layerName)
    {
        m_layerName = layerName;
        if (m_canvas != null)
            m_canvas.sortingLayerName = layerName;
        foreach (var p in m_autoLayers)
            p.Refresh();
    }

    //设置显示层层数
    public void SetSortOrder(int sortOrder)
    {
        if (m_canvas == null)
            return;
        if (!m_canvas.overrideSorting)
            m_canvas.overrideSorting = true;
        m_canvas.sortingOrder = sortOrder;
        foreach (var p in m_autoLayers)
            p.Refresh();
    }
    //添加到自动调整层级的列表
    public void AddAutoLayer(UILayer autoLayer)
    {
        if (!m_autoLayers.Contains(autoLayer))
            m_autoLayers.Add(autoLayer);

        autoLayer.Refresh();
        if (!IsShow())
            autoLayer.Hide();
        else
            autoLayer.Show();
    }
    //从自动调整层级的列表中移除
    public void RemoveAutoLayer(UILayer autoLayer)
    {
        if (m_autoLayers.Contains(autoLayer))
            m_autoLayers.Remove(autoLayer);
    }

    public bool IsShow()
    {
        return gameObject.activeSelf && m_canvas.enabled;
    }
}
