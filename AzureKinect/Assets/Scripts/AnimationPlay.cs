/**
 *
 * @brief Ghost ���� �ִϸ��̼� ���� ������ ������ִ� �ڵ�
 * @author Woosung Shim
 * @date 2021-11-03
 * @version 1.0.0
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlay : MonoBehaviour
{
    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.Play();
    }
}
