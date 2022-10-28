using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILayer : MonoBehaviour
{
    //层次，默认就是父Canvas的层级
    [SerializeField]
    internal string layer;
    //层数，UI的层数=Canvas的层数+order
    [SerializeField]
    internal int order = 1;

    private UIFormMono m_formMonoSelf;
    private UIFormMono m_formMonoParent;
    private UILayer m_parentLayer;
    private Canvas canvas;

    private List<UILayer> m_subLayers = new List<UILayer>();
    void Start()
    {
        canvas = GetComponent<Canvas>();
        m_formMonoSelf = GetComponent<UIFormMono>();
        //递归找到界面挂载的UIFormMono，将自身UILayer添加到自动调整层级的列表中
        m_formMonoParent = GetParentComponent<UIFormMono>(transform);
        if (m_formMonoParent != null)
            m_formMonoParent.AddAutoLayer(this);
        //递归找到上一层UILayer，将本身作为子层交给父UILayer管理
        m_parentLayer = GetParentComponent<UILayer>(transform);
        if (m_parentLayer != null)
            m_parentLayer.RegistSubLayer(this);
    }
    private void OnDestroy()
    {
        m_formMonoParent?.RemoveAutoLayer(this);
        m_parentLayer?.UnRegistSubLayer(this);
    }
    internal void Refresh()
    {
        if (!enabled)
            return;
        Canvas parentCanvas = GetParentCanvas(transform);
        if (parentCanvas == null && string.IsNullOrEmpty(layer))
            return;
        int orderValue = order;
        if (parentCanvas != null)
            orderValue += parentCanvas.sortingOrder;
        if (canvas != null)
        {
            if (!string.IsNullOrEmpty(layer))
            {
                canvas.sortingOrder = orderValue;
                canvas.sortingLayerID = SortingLayer.NameToID(layer);
            }
            else
            {
                canvas.sortingOrder = orderValue;
                canvas.sortingLayerID = parentCanvas.sortingLayerID;
            }
        }
        else
        {
            //特效
        }
        //刷新子节点
        for (int i = 0; i < m_subLayers.Count; ++i)
            m_subLayers[i].Refresh();
    }

    private static T GetParentComponent<T>(Transform transform) where T : Component
    {
        if (transform.parent == null)
            return null;
        var t = transform.parent.GetComponent<T>();
        if (t == null)
            return GetParentComponent<T>(transform.parent);
        return t;
    }
    internal static Canvas GetParentCanvas(Transform transform)
    {
        if (transform.parent == null)
            return null;
        var canvas = transform.parent.GetComponent<Canvas>();
        if (canvas == null || !canvas.overrideSorting)
            return GetParentComponent<Canvas>(transform.parent);
        return canvas;
    }
    internal void Show()
    {
        if (canvas != null)
        {
            if (m_formMonoSelf == null)
                canvas.enabled = true;
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    internal void Hide()
    {
        if (canvas != null)
        {
            if (m_formMonoSelf == null)
                canvas.enabled = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    //注册子节点
    protected void RegistSubLayer(UILayer layer)
    {
        if (!m_subLayers.Contains(layer))
            m_subLayers.Add(layer);
    }
    //清除子节点注册
    protected void UnRegistSubLayer(UILayer layer)
    {
        if (m_subLayers.Contains(layer))
            m_subLayers.Remove(layer);
    }
}
