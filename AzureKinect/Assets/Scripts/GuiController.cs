/**
 *
 * @brief Unity Sceneâ���� �������� UI�� ������ �ڵ�
 * @author Woosung Shim
 * @date 2021-11-03
 * @version 1.0.0
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    public Simulatermodified controllerTarget;
    public Slider progressbar;
    private bool isPlay = false;

    public Animation defaultd;
    public Animation[] anims;

    [SerializeField]
    private List<string> actionNames;
    private Dropdown dropdown;

    [SerializeField] private string textAreaStr = "";
    [SerializeField] private float animationPlayTime = 1.0f;
    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        actionNames = new List<string>();

        SetDropdownOptions();
        anim = GetComponent<Animation>();
    }

    private void SetDropdownOptions()
    {
        dropdown.ClearOptions();

        string[] tmp = {
            "Rupperlegup",
            "Rlowerlegup",
            "Rfootup",
            "Lupperlegup",
            "Llowerlegup",
            "Lfootup"
        };

        foreach (string t in tmp)
        {
            actionNames.Add(t);
        }

        dropdown.AddOptions(actionNames);
    }

    private void OnGUI()
    {
        // @see ���� ������ ���� �� ����
        //bool tmp = GUI.Button(new Rect(0, 0, 160, 30), "Action Start");
        string str = dropdown.options[dropdown.value].text;
        //dropdown.onValueChanged()<= 
        //textAreaStr = GUI.TextArea(new Rect(170, 0, 160, 30), textAreaStr);

        //if (tmp)
        //{
        //    if (!float.TryParse(textAreaStr, out animationPlayTime)) return;
        //    if (!isPlay) StartCoroutine(ProgressbarUpdate());
        //    anims[dropdown.value].Play();
        //    Debug.Log(dropdown.value);  
        //}

        /* @see���� ����
         *controllerTarget.SetAnimSpeed(animationPlayTime);
         *controllerTarget.AnimBool(str, tmp);
        */
        // @TODO : ���ǵ� �����ϴ� �ڵ�

    }

    private IEnumerator ProgressbarUpdate()
    {
        float timer = 0.0f;
        isPlay = true;
        progressbar.maxValue = animationPlayTime;
        while (timer <= animationPlayTime)
        {
            timer += Time.deltaTime;
            progressbar.value = timer;
            //Debug.Log(progressbar.value / animationPlayTime);
            yield return null;
        }
        isPlay = false;
        progressbar.value = 0.0f;
    }

    public void OVC()
    {
        defaultd.Play();
    }
}

    


