using UnityEngine;
using Photon.Pun;

namespace Z1HY4N9
{
    public class DestroySelf : MonoBehaviourPun
    {
        [SerializeField, Header("刪除時間"), Range(0, 10)]
        private float timeDestory = 5;
        [SerializeField, Header("是否需要碰撞後刪除")]
        private bool collisionDestory;

        private void Awake()
        {
            Invoke("DestoryDelay", timeDestory);
        }

        /// <summary>
        /// 刪除延遲的方法
        /// </summary>
        private void DestoryDelay()
        {
            // 連線.刪除(遊戲物件) = 刪除伺服器內的物件
            PhotonNetwork.Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            // 如果 需要碰撞後刪除 就 連線.刪除
            if (collisionDestory)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }

    }
}

