///**
// *
// * @brief Unity���� AR/VR ��Ȱ �ڵ���� �Ѱ��ϴ� �ڵ� 
// * @author Woosung Shim
// * @date 2021-11-03
// * @version 1.0.0
// * 
// */


//using System.Collections.Generic;
//using UnityEngine;
//using Avatar = LightBuzz.Kinect4Azure.Avateering.Avatar;

//namespace LightBuzz.Kinect4Azure
//{
//    /**
//    *
//    * @brief Main Script
//    * @details Body FrameSource�� LightBuzz ���̺귯������ ������ Unity ����Ʈ���� ���̴� UI�� �Ѱ�
//    * 
//    */

//    public class Demo_Kinect4Azure_Avateering : MonoBehaviour
//    {
//        public JointType type;

//        // @see Unity ���� Inspector�� ���� ����
//        [SerializeField] private Configuration _configuration;
//        [SerializeField] private UniformImage _image;
//        [SerializeField] private Avatar[] _avatars;

//        [SerializeField] private Vector3[] _joints_position;
//        [SerializeField] private Vector3[] _joints_rotation;

//        private KinectSensor sensor;

//        [System.Serializable]
//        private class JointData
//        {
//            public string name;
//            public string position;
//        }

//        [System.Serializable]
//        private class JointDataListWrapper
//        {
//            public JointData[] jointsDatas;
//        }

//        [System.Serializable]
//        private struct JointDataWriteDescriptor
//        {
//            public string filePathAndName;
//            public bool isActive;
//        }

//        [Header("- JointDataWrite Setting")]
//        [SerializeField] private JointDataWriteDescriptor jointDataWriteDescriptor;
//        private List<JointData> jointDatas = new List<JointData>();

//        private void Start()
//        {
//            sensor = KinectSensor.Create(_configuration);

//            if (sensor == null)
//            {
//                Debug.LogError("Sensor not connected!");
//                return;
//            }

//            _joints_position = new Vector3[32];
//            _joints_rotation = new Vector3[32];

//            sensor.Open();
//        }

//        private void Update()
//        {
//            bool cantLoadCaptureFrame;
//            cantLoadCaptureFrame = sensor == null || !sensor.IsOpen;
//            if (cantLoadCaptureFrame) return;

//            //Debug.Log($"{nameof(sensor)} �� �غ���� �ʾҽ��ϴ�.");

//            var frame = sensor.Update();

//            if (frame != null)
//            {
//                if (frame.ColorFrameSource != null)
//                {
//                    _image.Load(frame.ColorFrameSource);
//                }

//                if (frame.BodyFrameSource != null)
//                {
//                    UpdateAvatars(frame.BodyFrameSource.Bodies);
//                    if (frame.BodyFrameSource.Bodies.Count <= 0) return;

//                    Vector3D position;
//                    foreach (var joint in frame.BodyFrameSource.Bodies[0].Joints)
//                    {
//                        position = frame.BodyFrameSource.Bodies[0].Joints[type].Position;
//                        //Debug.Log($"{joint.Key}, {position}");
//                        jointDatas.Add(new JointData() { name = joint.Key.ToString(), position = position.ToString() });
//                    }

//                    position = frame.BodyFrameSource.Bodies[0].Joints[type].Position;
//                    Sample.value = position.MagnitudeSquared;
//                }
//            }
//        }

//        private void OnApplicationQuit()
//        {
//            if (jointDatas.Count == 0) { Debug.LogError("Recorded count is 0"); return; }
//            if (!jointDataWriteDescriptor.isActive) { Debug.LogWarning("is not active"); return; }
//            if (string.IsNullOrWhiteSpace(jointDataWriteDescriptor.filePathAndName)) { Debug.LogError("Data write descrptor has not file path and name"); return; }

//            var path = jointDataWriteDescriptor.filePathAndName;

//            if (!System.IO.File.Exists(path))
//            {
//                var file = System.IO.File.Create(path);
//                file.Close();
//                file.Dispose();
//            }

//            var json = JsonUtility.ToJson(new JointDataListWrapper() { jointsDatas = jointDatas.ToArray() }, true);
//            if (string.IsNullOrWhiteSpace(json)) { Debug.LogError("json is null"); return; }
//            System.IO.File.WriteAllText(path, json);
//        }


//        private void OnDestroy()
//        {
//            sensor?.Close();
//        }

//        public void DoTPose()
//        {
//            foreach (Avatar item in _avatars)
//            {
//                item.DoTPose();
//            }
//        }

//        private void UpdateAvatars(IList<Body> bodies)
//        {
//            if (bodies == null || bodies.Count == 0) return;
//            if (_avatars == null || _avatars.Length == 0) return;

//            UpdateAvaOrigin(bodies);
//        }

//        private void UpdateAvaOrigin(IList<Body> bodies)
//        {
//            // @see ������ �� ����� Ű��Ʈ ���� ����
//            Body body = bodies.Closest();

//            //body.Joints[JointType.KneeRight] = new Joint();
//            //body.Joints[JointType.KneeLeft] = new Joint();

//            foreach (Avatar avatar in _avatars)
//            {
//                avatar.Update(body);
//                avatar.AvatarRoot.transform.position = Vector3.zero;
//            }
//        }

//        // Deprecated
//        private void UpdateAvaNew(IList<Body> bodies)
//        {

//            Body body = bodies.Closest();

//            // @see ���� ������ Ű��Ʈ ���� ���� ��ġ
//            Dictionary<JointType, Joint> js = body.Joints;

//            int i = 0;

//            Debug.Log("js count : " + js.Count);

//            Joint[] _joints = new Joint[32];

//            foreach (JointType jt in js.Keys)
//            {
//                if (js.TryGetValue(jt, out _joints[i]))
//                {
//                    Vector3 _p = _joints[i].Position;
//                    Vector4 _r_tmp = _joints[i].Orientation;
//                    Vector3 _r = new Quaternion(_r_tmp.x, _r_tmp.y, _r_tmp.z, _r_tmp.w).EulerAngles;
//                    //Debug.Log(string.Format("(%f,%f,%f)", _v.x, _v.y, _v.z));
//                    _joints_position[i] = _p;
//                    _joints_rotation[i++] = _r;
//                    //_joints_value[i++] = _joints[i].Position;
//                }
//            }

//            /* @see
//             * �ƹ�Ÿ�� ����� ��ġ�� ������Ʈ ��
//             * �� ������ ��ġ ��ǥ�� ���� rotation ���
//             * v1 : �̵��� ���� ��ǥ
//             * v2 : �̵��Ǵ� ������ ����� ���� ���� ��ǥ
//             */
//            //_avatars[0].Update
//            //_avatars[0].GetBone(HumanBodyBones.Head).UpdatePosition(_joints[(int)JointType.Head].Position);
//            Vector3 v1 = _joints_position[(int)JointType.KneeLeft];
//            Vector3 v2 = _joints_position[(int)JointType.HipLeft];
//            Quaternion v2_r = Quaternion.Euler(_joints_rotation[(int)JointType.HipLeft]);
//            Debug.Log("(" + v1.x + "," + v1.y + "," + v1.z + ")");
//            if (v1 == null || v1.magnitude == 0) return;

//            foreach (Avatar avatar in _avatars)
//            {
//                Quaternion q1, q2;
//                q1 = Quaternion.LookRotation(v1 - v2);
//                q2 = v2_r;
//                avatar.GetBone(HumanBodyBones.LeftLowerLeg).UpdateRotation(q1 * q2, _configuration.Smoothing);
//            }
//        }
//    }
//}
