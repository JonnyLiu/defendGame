using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour {
	public int Hp = 1;
	// 导航代理
	private NavMeshAgent agent;
	// 动画
	private Animation ani;
	private PlayerControl playerControl;

	// 是否攻击
	private bool isAttack = false;
	// 攻击计时器
	private float attackTimer = 2;

	// Use this for initialization
	void Start () {
		ani = GetComponent<Animation> ();
		agent = GetComponent<NavMeshAgent> ();
		playerControl = GameObject.FindWithTag ("Player").GetComponent<PlayerControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Hp <= 0) {
			return;
		}
		// 计算和玩家的距离
		float dis = Vector3.Distance(transform.position, playerControl.transform.position);
		// 追击玩家
		if (playerControl.Hp > 0) {
			if (dis > 2f) {
				// 设置开启导航 默认 false 开启
				agent.isStopped = false;
				// 设置目的地
				agent.SetDestination (playerControl.transform.position);
				// 播放移动动画
				ani.Play ("RunFront");
				// 禁止攻击
				isAttack = false;
			} else {
				// 停止导航
				agent.isStopped = true;
				// 允许攻击
				isAttack = true;
			}
		} else {
			/* 当玩家死亡时 */
			isAttack = false;
			ani.Play ("Idle");
			agent.isStopped = true;
		}

		// 攻击
		if (isAttack) {
			attackTimer += Time.deltaTime;
			// 面向玩家
			transform.LookAt(playerControl.transform);
			if (attackTimer > 1f) {
				attackTimer = 0;
				// 攻击
				ani.Play("AttackMelee1");
				playerControl.AddHp(-10);
			}
		}
	}

	public void Die() {
		Hp = 0;
		agent.isStopped = true;
		Destroy (GetComponent<Collider> ());
		Destroy (GetComponent<Rigidbody> ());
		ani.Play ("Death");
		Destroy (gameObject, 2f);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			Hp--;
			if (Hp <= 0) {
				Die ();
			}
		}
	}
}
