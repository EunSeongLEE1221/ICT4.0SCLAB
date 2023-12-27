using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    public float scaleSpeed = 0.1f; // ������ ��ȭ �ӵ�
    private OVRInput.Controller controller; // Oculus Rift ��Ʈ�ѷ�

    private void Start()
    {
        controller = OVRInput.Controller.RTouch; // ������ ��ġ ��Ʈ�ѷ��� ���
    }

    private void Update()
    {
        // A ��ư�� ������ ��ü ũ�� ����
        if (OVRInput.GetDown(OVRInput.Button.One, controller))
        {
            ScaleObject(-scaleSpeed);
        }

        // B ��ư�� ������ ��ü ũ�� ����
        if (OVRInput.GetDown(OVRInput.Button.Two, controller))
        {
            ScaleObject(scaleSpeed);
        }
    }

    private void ScaleObject(float deltaScale)
    {
        // ���� �����Ͽ� ��ȭ���� ���� ���ο� �������� ����
        Vector3 newScale = transform.localScale + new Vector3(deltaScale, deltaScale, deltaScale);
        transform.localScale = newScale;
    }
}
