using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;   // 引用 系統查詢語言 (資料結構轉換 API)
using System.Linq;                  // 引用 系統集合一般 (資料結構，List, ArrayList...)
using Photon.Realtime;

namespace Z1HY4N9 
{
	/// <summary>
	/// 遊戲管理器
	/// 判斷如果是連線進入的玩家
	/// 就生成角色物件(戰士)
	/// </summary>

	public class GameManager : MonoBehaviourPunCallbacks
	{
		[SerializeField, Header("角色物件")]
		private GameObject goCharacter;
		[SerializeField, Header("生成座標物件")]
		private Transform[] traSpawnPoint;

		/// <summary>
		/// 儲存生存座標清單
		/// </summary>
		[SerializeField]
		private List<Transform> traSpawnPointList;

		private void Awake()
		{
			// 玩家已經加入房間執行...

			// Photon 連線.當前房間.可視性 = 否 (其他玩家看不到此房間，不能加入)
			PhotonNetwork.CurrentRoom.IsVisible = false;

			#region 隨機生成
			traSpawnPointList = new List<Transform>();   // 新增 清單物件
			traSpawnPointList = traSpawnPoint.ToList();  // 陣列轉為清單資料結構
			
			
			// 如果是連線進入的玩家就在伺服器生成角色物件
			//if (photonView.IsMine)
			//{
				int indexRadom = Random.Range(0, traSpawnPointList.Count);   // 取得隨機清單(0,清單的長度)
				Transform tra = traSpawnPointList[indexRadom];               // 取得隨機座標
				// 透過Photon伺服器.生成(物件, 座標, 角度)
				PhotonNetwork.Instantiate(goCharacter.name, tra.position, tra.rotation);

				traSpawnPointList.RemoveAt(indexRadom);  // 刪除已經取得過的生成座標資料
														 //}
			#endregion

		}

		// 有玩家離開房間會執行一次
		public override void OnPlayerLeftRoom(Player otherPlayer)
		{
			base.OnPlayerLeftRoom(otherPlayer);

			// 如果 當前房間玩家人數 剩下一人 就吃雞
			if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
				Win();
		}

		private void Win() 
		{
			print("勝利");
		}

	}
}

