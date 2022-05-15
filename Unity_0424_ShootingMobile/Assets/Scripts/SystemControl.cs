using Photon.Pun;
using UnityEngine;


// namespace 命名空間 : 程式區塊 可以區隔相同名字的程式
namespace Z1HY4N9
{
	/// <summary>
	///  控制系統 : 荒野亂鬥移動功能
	///  虛擬搖桿控制角色移動
	/// </summary>

	public class SystemControl : MonoBehaviourPun
	{
		[SerializeField, Header("虛擬搖桿")]
		private Joystick joystick;
		[SerializeField, Header("移動速度"), Range(0, 300)]
		private float speed = 7f;
		[SerializeField, Header("角色方向圖示")]
		private Transform traDirectionIcon;
		[SerializeField, Header("角色方向圖示範圍"), Range(0, 5)]
		private float rangeDirectionIcon = 2.5f;
		[SerializeField, Header("角色旋轉速度"), Range(0, 100)]
		private float speedTurn = 1.5f;
		[SerializeField, Header("動畫參數跑步")]
		private string parameterwalk = "開關跑步";
		[SerializeField, Header("畫布")]
		private GameObject goCanvas;
		[SerializeField, Header("畫布玩家資訊")]
		private GameObject goCanvasPlayerInfo;
		[SerializeField, Header("角色方向圖示")]
		private GameObject goDirection;


		private Rigidbody rig;
		private Animator ani;

		private void Awake()
		{
			rig = GetComponent<Rigidbody>();
			ani = GetComponent<Animator>();

			// 如果是連線進入的玩家 就生成玩家需要的物件
			if (photonView.IsMine) 
			{
				Instantiate(goCanvas);
				Instantiate(goCanvasPlayerInfo);
				Instantiate(goDirection);
			}


		}

		private void Update()
		{
			//GetJoystickValue();	
			UpdateDirectionIconPos();
			LookDirecrionIcon();
			UpdateAnimation();
		}

		private void FixedUpdate()
		{
			Move();
		}

		// 取得虛擬搖桿值並吐出至Console
		private void GetJoystickValue()
		{
			print("<color=yellow>水平 : " + joystick.Horizontal + "</color>");
		}

		// 移動功能
		private void Move()
		{
			// 剛體.加速度 = 三維向量(X.Y.Z)
			rig.velocity = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * speed;
		}	


		// 更新角色方向圖示的座標
		private void UpdateDirectionIconPos()
        {
			// 新座標 = 角色的座標 + 三維向量(虛擬搖桿的水平與垂直) * 方向圖示的範圍
			Vector3 pos = transform.position + new Vector3(joystick.Horizontal, 0.5f, joystick.Vertical) * rangeDirectionIcon;
			// 更新方向圖示的座標 = 新座標
			traDirectionIcon.position = pos;
        }

		// 角色面向方向
		private void LookDirecrionIcon()
        {
			// 取得面向角度 = 四位元.面向角度(方向圖示 - 角色) - 方向圖示與角色的向量
			Quaternion look = Quaternion.LookRotation(traDirectionIcon.position - transform.position);
			// 角色的角度 = 四位元.插植(角色的角度 , 面向角度.旋轉速度 * 一幀的時間)
			transform.rotation = Quaternion.Lerp(transform.rotation, look, speedTurn * Time.deltaTime);
			// 角色的歐拉角度 = 三維向量(0, 原本的Y歐拉角度, 0)
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		
		}

		// 更新動畫
		private void UpdateAnimation()
        {
			// 是否跑步 = 虛擬搖桿.水平 不為零 或 垂直 不為零
			bool run = joystick.Horizontal != 0 || joystick.Vertical != 0;
			ani.SetBool(parameterwalk, run);
        }
	}
}
