using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Button toggleButton;
    public GameObject timer;
    public Text time;
    public GameObject menu;
    public GameObject Count;
    public Text ct;
    int count;
   
    
    
    float tim;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void ToggleText()
    {
        // TextMeshProUGUI ���� ������Ʈ�� Ȱ��ȭ ���¸� ���
        time.gameObject.SetActive(!time.gameObject.activeSelf);
        // �ؽ�Ʈ�� Ȱ��ȭ�Ǹ� ������ "0"���� ����
        if (time.gameObject.activeSelf)
        {
            tim = 0;
            time.text = "Timer : "+(int) tim + " sec";
            
        }
    }
     public void ToggleCount()
    {
        // TextMeshProUGUI ���� ������Ʈ�� Ȱ��ȭ ���¸� ���
        Count.gameObject.SetActive(!time.gameObject.activeSelf);
        // �ؽ�Ʈ�� Ȱ��ȭ�Ǹ� ������ "0"���� ����
        if (ct.gameObject.activeSelf)
        {
            count = 0;
            ct.text = "Count : " + count;
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        tim += Time.deltaTime;
        time.text = "Timer : "+(int) tim + " sec";
    }
    public void mactive()
    {
        if (menu.activeSelf == true)
        {
            menu.gameObject.SetActive(false);
        }
        else
        {
            menu.gameObject.SetActive(true);
        }
    }
}
