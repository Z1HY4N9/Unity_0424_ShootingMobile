using UnityEngine;
using Photon.Pun;

namespace Z1HY4N9
{
    public class DestroySelf : MonoBehaviourPun
    {
        [SerializeField, Header("�R���ɶ�"), Range(0, 10)]
        private float timeDestory = 5;
        [SerializeField, Header("�O�_�ݭn�I����R��")]
        private bool collisionDestory;

        private void Awake()
        {
            Invoke("DestoryDelay", timeDestory);
        }

        /// <summary>
        /// �R�����𪺤�k
        /// </summary>
        private void DestoryDelay()
        {
            // �s�u.�R��(�C������) = �R�����A����������
            PhotonNetwork.Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            // �p�G �ݭn�I����R�� �N �s�u.�R��
            if (collisionDestory)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }

    }
}

