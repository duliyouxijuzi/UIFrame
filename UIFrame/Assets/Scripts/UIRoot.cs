
using UnityEngine;

public class UIRoot : MonoBehaviour
{
    //主界面层
    [SerializeField]
    public Transform layerMainMenu = null;
    //功能层
    [SerializeField]
    public Transform layerHome = null;
    //功能弹窗层
    [SerializeField]
    public Transform layerPop = null;

    public static UIRoot mInstance = null;
    void Awake()
    {
        mInstance = this;
    }
}
