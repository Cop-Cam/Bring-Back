using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TestingBehavior : MonoBehaviour
{
    public UnityEvent RunableMethod;
    // [Flags]
    // public enum casetest 
    // {
    //     a = (1 << 1), 
    //     b = (1 << 2),
    //     c = (1 << 3),
    //     d = (1 << 4),
    //     e = (1 << 5)
    // };
    //public casetest caseTest;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start dipanggil");
        //TestEnumCase();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            DoStuff();
        }
    }

    private void DoStuff()
    {
        RunableMethod.Invoke();
    }

    void Awake() 
    {
        Debug.LogWarning("Init scene dipanggil");
        //StartCoroutine(Loading());
    }

    public void Method1()
    {
        Debug.Log("test: kosong");
    }

    public void Method2(int test)
    {
        Debug.Log("test: " +test*10);
    }

    public void Method3(string test)
    {
        Debug.Log("test: " +test);
    }

    // private IEnumerator Loading()
    // {
    //     yield return new WaitForSeconds(5);
    // }

    // void TestEnumCase()
    // {
    //     switch(caseTest)
    //     {
    //         case casetest.a:
    //             Debug.Log("a");
    //             break;
    //         case casetest.b:
    //             Debug.Log("b");
    //             break;
    //         case casetest.c:
    //             Debug.Log("c");
    //             break;
    //         case casetest.d:
    //             Debug.Log("d");
    //             break;
    //         case casetest.e:
    //             Debug.Log("e");
    //             break;
    //         default:
    //             Debug.Log("kontol");
    //             break;
    //     }
    // }
}
