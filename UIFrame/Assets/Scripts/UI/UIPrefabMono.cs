///////////////////////////////////////////////////////////////////
using UnityEngine;
using Object = UnityEngine.Object;
public class UIPrefabMono : MonoBehaviour
{
    public Object[] components = new Object[0];

    //获取组件
    public Object GetComponent(int index)
    {
        if (components != null && components.Length > index)
        {
            if (components[index] == null)
                return null;
            return components[index];
        }
        else
            return null;
    }

    internal T GetComponent<T>(int index) where T : Object
    {
        if (components != null && components.Length > index)
        {
            if (components[index] == null)
                return null;
            return components[index] as T;
        }
        else
            return null;
    }
}