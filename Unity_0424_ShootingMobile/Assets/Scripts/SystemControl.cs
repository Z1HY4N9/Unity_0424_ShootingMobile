using Cinemachine;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// namespace 命名空間 : 程式區塊 可以區隔相同名字的程式
namespace Z1HY4N9
{
	/// <summary>
	///  控制系統 : 荒野亂鬥移動功能
	///  虛擬搖桿控制角色移動
	/// </summary>

	public class SystemControl : MonoBehaviourPun
	{
		[SerializeField, Header("移動速度"), Range(0, 300)]
		private float speed = 7f;
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
		private Joystick joystick;
		private Transform traDirectionIcon;
		private CinemachineVirtualCamera cvc;
		private SystemAttack systemAttack;
		private DamageManager damageManager;
		

		private void Awake()
		{
			rig = GetComponent<Rigidbody>();
			ani = GetComponent<Animator>();
			systemAttack = GetComponent<SystemAttack>();
			damageManager = GetComponent<DamageManager>();

			

			// 如果是連線進入的玩家 就生成玩家需要的物件
			if (photonView.IsMine)
			{
				PlayerUIFollow follow = Instantiate(goCanvasPlayerInfo).GetComponent<PlayerUIFollow>();
				follow.traPlayer = transform;
				// 取得角色方向圖示
				traDirectionIcon = Instantiate(goDirection).transform;
				// 取得畫布內的虛擬搖桿 transform.Find(子物件名稱) - 透過名稱搜尋子物件
				GameObject tempCanvas = Instantiate(goCanvas);
				joystick = tempCanvas.transform.Find("Floating Joystick").GetComponent<Joystick>();
				systemAttack.btnFire = tempCanvas.transform.Find("發射").GetComponent<Button>();

				// 取得攝影機 CM管理器
				cvc = GameObject.Find("CM管理器").GetComponent<CinemachineVirtualCamera>();
				// 指定追蹤物件
				cvc.Follow = transform;

				damageManager.imgHp = GameObject.Find("圖片血量").GetComponent<Image>();
				damageManager.textHp = GameObject.Find("文字血量").GetComponent<TextMeshProUGUI>();
			}
			// 否則 不是連線進入的玩家 就關閉控制系統，避免控制到多個物件
			else
			{
				enabled = false;
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

		/// <summary>
		/// 移動功能
		/// </summary>
		private void Move()
		{
			// 剛體.加速度 = 三維向量(X.Y.Z)
			rig.velocity = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * speed;
		}


		/// <summary>
		/// 更新角色方向圖示的座標
		/// </summary>
		private void UpdateDirectionIconPos()
        {
			// 新座標 = 角色的座標 + 三維向量(虛擬搖桿的水平與垂直) * 方向圖示的範圍
			Vector3 pos = transform.position + new Vector3(joystick.Horizontal, 0.5f, joystick.Vertical) * rangeDirectionIcon;
			// 更新方向圖示的座標 = 新座標
			traDirectionIcon.position = pos;
        }

		/// <summary>
		/// 角色面向方向
		/// </summary>
		private void LookDirecrionIcon()
        {
			// 取得面向角度 = 四位元.面向角度(方向圖示 - 角色) - 方向圖示與角色的向量
			Quaternion look = Quaternion.LookRotation(traDirectionIcon.position - transform.position);
			// 角色的角度 = 四位元.插植(角色的角度 , 面向角度.旋轉速度 * 一幀的時間)
			transform.rotation = Quaternion.Lerp(transform.rotation, look, speedTurn * Time.deltaTime);
			// 角色的歐拉角度 = 三維向量(0, 原本的Y歐拉角度, 0)
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		
		}

		/// <summary>
		/// 更新動畫
		/// </summary>
		private void UpdateAnimation()
        {
			// 是否跑步 = 虛擬搖桿.水平 不為零 或 垂直 不為零
			bool run = joystick.Horizontal != 0 || joystick.Vertical != 0;
			ani.SetBool(parameterwalk, run);
        }
	}
}
