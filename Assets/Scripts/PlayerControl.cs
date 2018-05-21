using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	// 玩家生命值
	public int Hp = 100;
	// 移动速度
	public float Speed = 5;
	// 触发器
	public Collider Trigger;
	// 动画组件
	private Animation ani;
	// 攻击间隔
	private float attackTimer = 0.5f;

	// 技能预设体
	public GameObject SkillPre;
	private float skillTimer = 8f;

	// 技能预设体大招
	public GameObject Dazhao;
	private float dazhao = 8f;

    // 技能预设体小招
	public GameObject Xiaozhao;
	private float xiaozhao = 4f;




	// Use this for initialization
	void Start () {
		ani = GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Hp <= 0) {
			return;
		}
		// 纵轴 y 方向 1 0 -1
		float vertical = Input.GetAxis ("Vertical");
		// 横轴 x 方向 1 0 -1
		float horizontal = Input.GetAxis ("Horizontal");
		// 方向
		Vector3 dir = new Vector3(horizontal, 0, vertical);
		// 横轴和纵轴至少按了一个
		if (dir != Vector3.zero) {
			// 转向
			transform.rotation = Quaternion.LookRotation (dir);
			// 移动
			transform.Translate (Vector3.forward * Speed * Time.deltaTime);
			// 播放跑步动画
			ani.Play ("RunFront");
		} else if (ani.IsPlaying("AttackMelee1") == false) {
			ani.Play ("Idle");
		}

		attackTimer += Time.deltaTime;

		// 按 U 键攻击
		if (Input.GetKeyDown(KeyCode.U) && attackTimer > 0.5f) {
			attackTimer = 0;
			ani.Play("AttackMelee1");
			Trigger.enabled = true;
			// 延时函数 0.1s 以后攻击结束 不能跨脚本
			Invoke("AttackEnd", 0.1f);
		}

		skillTimer += Time.deltaTime;

		// 按 I 键攻击
		if (Input.GetKeyDown(KeyCode.I) && skillTimer > 8f) {
			skillTimer = 0;
			ani.Play("AttackMelee1");
			Instantiate (SkillPre, transform.position + transform.forward * 5f, transform.rotation);
		}

		dazhao += Time.deltaTime;
		//按o键放技能
		if (Input.GetKeyDown(KeyCode.O) && attackTimer > 8f)
		{
			dazhao = 0;
			ani.Play("AttackMelee1");
			Instantiate(Dazhao,transform.position + transform.forward * 5f,transform.rotation);
		}


		xiaozhao += Time.deltaTime;
		//按P键放技能
		if (Input.GetKeyDown(KeyCode.P) && attackTimer > 4f)
		{
			xiaozhao = 0;
			ani.Play("AttackMelee1");
			Instantiate(Xiaozhao,transform.position + transform.forward * 5f,transform.rotation);
		}
	}

	// 攻击结束
	void AttackEnd() {
		Trigger.enabled = false;
	}

	// 增加血量
	public void AddHp(int hp) {
		Hp += hp;
		if (Hp < 0) {
			Hp = 0;  
		}
		if (Hp <= 0) {
			ani.Play ("Death");
		}
	}
}
