using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.OpenUI<HomeCanvas>();
        Debug.Log("anh hen em");
        Debug.Log("thang kun");


    }

    // Update is called once per frame

}
