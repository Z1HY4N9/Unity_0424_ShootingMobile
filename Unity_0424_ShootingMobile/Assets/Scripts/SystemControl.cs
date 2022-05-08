using Photon.Pun;
using UnityEngine;

// namespace 命名空間 : 程式區塊 可以區隔相同名字的程式
namespace Z1HY4N9
{
	/// <summary>
	///  控制系統 : 荒野亂鬥移動功能
	///  虛擬搖桿控制角色移動
	/// </summary>

	public class SystemControl : MonoBehaviour
	{
		[SerializeField, Header("虛擬搖桿")]
		private Joystick joystick;
		[SerializeField, Header("移動速度"), Range(0, 300)]
		private float speed = 7f;

		private Rigidbody rig;

		private void Awake()
		{
			rig = GetComponent<Rigidbody>();	
		}

		private void Update()
		{
			//GetJoystickValue();	
		}

		private void FixedUpdate()
		{
			Move();
		}

		//取得虛擬搖桿值並吐出至Console
		private void GetJoystickValue()
		{
			print("<color=yellow>水平 : " + joystick.Horizontal + "</color>");
		}

		//移動功能
		private void Move()
		{
			// 剛體.加速度 = 三維向量(X.Y.Z)
			rig.velocity = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * speed;
		}	
	}
}
