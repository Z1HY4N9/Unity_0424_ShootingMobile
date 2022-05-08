using UnityEngine;

namespace Z1HY4N9
{
    /// <summary>
    /// 玩家資訊界面追蹤
    /// 介面追蹤玩家物件座標
    /// </summary>
    public class PlayerUIFollow : MonoBehaviour
    {
        [SerializeField, Header("位移")]
        private Vector3 v3Offset;
        private string namePlayer = "戰士";
        private Transform traPlayer;

        private void Awake()
        {
            // 玩家變形元件 = 遊戲物件.尋找(物件名稱).變形元件
            traPlayer = GameObject.Find(namePlayer).transform;
        }

        private void Update()
        {
            Follow();
        }

        private void Follow()
        {
            transform.position = traPlayer.position + v3Offset;
        }

    }

}
