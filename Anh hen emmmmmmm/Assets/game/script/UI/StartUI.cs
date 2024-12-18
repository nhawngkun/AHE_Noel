using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.OpenUI<HomeCanvas>();
        Debug.Log("anh hen em");
        Debug.Log("ko hen gi hetS");
    }

    // Update is called once per frame

}
