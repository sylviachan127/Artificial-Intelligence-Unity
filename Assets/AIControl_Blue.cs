using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIControl_Blue : MonoBehaviour
{

	public UnityEngine.AI.NavMeshAgent navMeshAgent;
	public TyMovement character;
	public Animator anim;
	public enemyDetection_Blue enmDect;
	public AIControl aiRedCurrent;
	public bool currentTargetEnemyInPrison;
	public AIControl_Blue teammateCurrent;
	public bool currentTeammateInPrison;
	//public bool enemyInRange;

	public enum State
	{
		GETBALL, PRISON, SHOOT, HIDE, START, SIDELANE, SIDELANEEND
	}

	public State state;
	public bool inPrison;
	public bool goPrison;
	public bool inDanger;
	public bool getHit;
	public bool hasBall;
	public bool outPrison;
	public bool shootBall;
	// Own team ball
	public bool getHitBlue;

	public GameObject[] shootPoint;
	public GameObject[] ballPoint;
	public GameObject[] hidePoint;
	public GameObject[] sideLanePoint;
	public GameObject[] sideLaneEndPoint;
	public GameObject[] prisonPoint;

	public GameObject[] enemyPoint;
	public GameObject[] teammatePoint;

	private int shootPointIndex = 0;
	private int ballPointIndex = 0;
	public float walkSpeed = 3f;
	public int thrustForce = 4;

	public Vector3 ballHeightOffset;

	public GameObject target;
	Vector3 previousPosition;

	private bool winNotSet;
	public bool testMove;
	public float curSpeed;
	public float testDistance;
	public float m_TurnAmount;
	public float m_ForwardAmount;
	public float turnSpeed = 3f;

	public bool sideLaneReach;
	public bool sideLaneEndReach;
	public bool shootReach;
	public bool targetEnemyInPrison;
	public bool targetTeammateInPrison;

	public GameObject closest_enemyPoint;
	public GameObject closest_teammatePoint;

	public Text stateText;
	public Text winText;

	public GameObject blueBall;
	public GameObject bb;
	public float ball_spawnTime = 6f;

	Vector3 m_GroundNormal;
	bool m_IsGrounded;
	float m_MovingTurnSpeed = 360;
	float m_StationaryTurnSpeed = 180;
	public float baseSpeed = 6;
	public float m_AnimSpeedMultiplier = 1.5f;

	public AudioSource ballGetSource;
	public AudioSource ballThrowSource;
	public AudioSource gameEndSource;
	public AudioSource getHitSource;
	public AudioSource prisonBallCatchSource;

	// Use this for initialization
	void Start()
	{
		//navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		//anim = GetComponent<Animator>();
		character = GetComponent<TyMovement>();

		navMeshAgent.updatePosition = false;
		navMeshAgent.updateRotation = false;

		state = AIControl_Blue.State.GETBALL;
		inPrison = false;
		hasBall = false;
		inDanger = false;
		getHit = false;
		testMove = false;
		goPrison = false;
		outPrison = false;
		shootBall = false;

		sideLaneReach = false;
		sideLaneEndReach = false;
		shootReach = false;

		StartCoroutine("FSM");
		SetStateText();
		InvokeRepeating("Spawn", ball_spawnTime, ball_spawnTime);
		ballHeightOffset = new Vector3(0, 3f, 0);
		inDanger = enmDect.enemyDetect;
		targetEnemyInPrison = false;

		getHitBlue = false;
		winNotSet = true;

	}

	void Update()
	{
		inDanger = enmDect.enemyDetect;
		if (closest_enemyPoint != null)
		{
			AIControl aiRed = closest_enemyPoint.GetComponent<AIControl>();
			targetEnemyInPrison = aiRed.inPrison;
		}
	}

	IEnumerator FSM()
	{
		while (!inPrison)
		{
			switch (state)
			{
				case State.GETBALL:
					GetBall();
					break;
				case State.SIDELANE:
					GoSideLane();
					break;
				case State.SIDELANEEND:
					GoSideLaneEnd();
					break;
				case State.PRISON:
					GoPrison();
					break;
				case State.SHOOT:
					Shoot();
					break;
				case State.HIDE:
					Hide();
					break;
				case State.START:
					StartState();
					break;

			}
			yield return null;
		}
	}

	void GetBall()
	{
		getHitBlue = false;
		outPrison = false;
		shootBall = false;
		SetStateText();
		if (inDanger)
		{
			state = AIControl_Blue.State.HIDE;
		}
		if (getHit)
		{
			getHit = false;
			goPrison = true;
			state = AIControl_Blue.State.SIDELANE;
		}
		GameObject ballPlace = findCloestLocation(ballPoint);
		MoveToPosition(ballPlace);
		if (hasBall)
		{
			Vector3 playerPos = navMeshAgent.transform.position;
			Vector3 ballPos = playerPos + ballHeightOffset;
			bb = Instantiate(blueBall, ballPos, navMeshAgent.transform.rotation);
			ballGetSource.Play();
			state = AIControl_Blue.State.SHOOT;
		}
	}

	void MoveToPosition(GameObject location)
	{
		//GameObject ballPlace = findCloestLocation(ballPoint);
		navMeshAgent.SetDestination(location.transform.position);
		bool reachYet = reachDestination();
		if (!reachYet)
		{
			Vector3 move = navMeshAgent.desiredVelocity;
			if (move.magnitude > 1f) move.Normalize();
			move = transform.InverseTransformDirection(move);
			Vector3 curMove = transform.position - previousPosition;
			curSpeed = curMove.magnitude;
			previousPosition = transform.position;
			m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;
			ApplyExtraTurnRotation();
			anim.SetFloat("Speed", navMeshAgent.velocity.magnitude);
			anim.SetFloat("Direction", m_TurnAmount, 0.1f, Time.deltaTime);
		}
	}

	// give True Upon First Reach, then return False;
	bool reachDestination()
	{
		bool reachYet = false;

		if (!navMeshAgent.pathPending)
		{
			if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
			{
				if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
				{
					reachYet = true;
				}
				//reachYet = true;
			}
		}
		return reachYet;
	}
	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "blueSideLane")
		{
			sideLaneReach = true;
		}
		if (coll.tag == "blueSideLaneEnd")
		{
			sideLaneEndReach = true;
		}
		if (coll.tag == "bluePrison")
		{
			inPrison = true;
			goPrison = false;
		}
		if (coll.tag == "blueBall")
		{
			if (!inDanger)
			{
				hasBall = true;
				getHitBlue = true;
			}
		}
		if (coll.tag == "redBall")
		{
			getHit = true;
			getHitSource.Play();
		}
		if (coll.tag == "shootReach")
		{
			shootReach = true;
		}
	}

	void OnTriggerExit(Collider coll)
	{
		if (coll.tag == "blueSideLane")
		{
			sideLaneReach = false;
		}
		if (coll.tag == "blueSideLaneEnd")
		{
			sideLaneEndReach = false;
		}
		if (coll.tag == "bluePrison")
		{
			inPrison = false;
			outPrison = true;
			goPrison = false;
		}
		if (coll.tag == "redBall")
		{
			getHit = false;
		}
	}

	void Shoot()
	{
		getHitBlue = false;
		SetStateText();
		if (inDanger)
		{
			state = AIControl_Blue.State.HIDE;
		}
		if (getHit)
		{
			if (hasBall)
			{
				if (bb != null)
				{
					Destroy(bb);
				}
			}
			getHit = false;
			goPrison = true;
			state = AIControl_Blue.State.SIDELANE;
		}
		GameObject shootPlace = findCloestLocation(shootPoint);
		MoveToPosition(shootPlace);
		if (hasBall)
		{
			if (!shootBall)
			{
				Component halo = GetComponent("Halo");
				halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
			}
			if (reachDestination())
			{
				if (bb != null)
				{
					Vector3 newballPosition = this.transform.forward + this.transform.forward;
					//print("pos: " + newballPosition.ToString());
					newballPosition += this.transform.position;
					newballPosition.y = 1.54f;
					bb.transform.position = newballPosition;
					Rigidbody blueBall_bb = bb.GetComponent<Rigidbody>();
					////redBall_rb.AddForce(transform.right * 10000);//cannon's x axis

					closest_teammatePoint = findTeammate(teammatePoint);

					// target - current position, then normailize it.
					closest_enemyPoint = findTargetEnemy(enemyPoint);
					//MoveToPosition(sidePoint);
					if (closest_teammatePoint != null)
					{
						//AIControl aiTeammate = closest_teammatePoint.GetComponent<AIControl>();
						Vector3 enemyDirect = closest_teammatePoint.transform.position;
						enemyDirect.y = 1.6f;
						Vector3 shootDirect = enemyDirect - (bb.transform.position);
						Vector3 testVector = new Vector3(1, 0, 1);
						shootDirect = shootDirect.normalized;
						blueBall_bb.AddForce(shootDirect * thrustForce, ForceMode.Impulse);
						hasBall = false;
						shootReach = false;
						shootBall = true;
						ballThrowSource.Play();
						state = AIControl_Blue.State.GETBALL;
					}
					else if (closest_enemyPoint != null)
					{
						AIControl aiRed = closest_enemyPoint.GetComponent<AIControl>();
						targetEnemyInPrison = aiRed.inPrison;
						Vector3 enemyDirect = closest_enemyPoint.transform.position;
						enemyDirect.y = 1.6f;
						Vector3 shootDirect = enemyDirect - (bb.transform.position);
						// adding randominess
						float x_random = UnityEngine.Random.Range(-5.0f, 5.0f);
						shootDirect.x += x_random;
						Vector3 testVector = new Vector3(1, 0, 1);
						shootDirect = shootDirect.normalized;
						blueBall_bb.AddForce(shootDirect * thrustForce, ForceMode.Impulse);
						hasBall = false;
						shootReach = false;
						shootBall = true;
						ballThrowSource.Play();
						state = AIControl_Blue.State.GETBALL;
					}
				}
			}
		}
		if (hasBall && !shootBall)
		{
			//print("sylvia is here 1");
			if (bb != null)
			{
				Vector3 playerPos = navMeshAgent.transform.position;
				Vector3 ballPos = playerPos + ballHeightOffset;
				bb.transform.position = ballPos;
				//print ("sylvia is here 2");
				//rb = Instantiate(redBall, ballPos, navMeshAgent.transform.rotation);
			}
		}
	}

	void GoSideLane()
	{
		getHitBlue = false;
		SetStateText();
		if (goPrison)
		{
			GameObject sidePoint = findCloestLocation(sideLanePoint);
			MoveToPosition(sidePoint);
			if (sideLaneReach)
			{
				//print(state.ToString() + " reach");
				state = AIControl_Blue.State.SIDELANEEND;
			}
		}
		if (outPrison)
		{
			//outPrison = false;
			GameObject sidePoint = findCloestLocation(sideLanePoint);
			MoveToPosition(sidePoint);
			if (sideLaneReach)
			{
				//print(state.ToString() + " reach");
				state = AIControl_Blue.State.GETBALL;
			}
		}
		if (getHit)
		{
			getHit = false;
		}
	}

	void GoSideLaneEnd()
	{
		getHitBlue = false;
		SetStateText();
		if (goPrison)
		{
			GameObject sideEndPoint = findCloestLocation(sideLaneEndPoint);
			MoveToPosition(sideEndPoint);
			if (sideLaneEndReach)
			{
				//print(state.ToString() + " reach");
				state = AIControl_Blue.State.PRISON;
			}
		}
		if (outPrison)
		{
			GameObject sideEndPoint = findCloestLocation(sideLaneEndPoint);
			MoveToPosition(sideEndPoint);
			if (sideLaneEndReach)
			{
				//print(state.ToString() + " reach");
				state = AIControl_Blue.State.SIDELANE;
			}
		}
		if (getHit)
		{
			getHit = false;
		}
	}

	void GoPrison()
	{
		SetStateText();
		if (getHit)
		{
			getHit = false;
		}
		//print("i am goingPrison");
		if (goPrison)
		{
			GameObject prison = findCloestLocation(prisonPoint);
			MoveToPosition(prison);
			//inPrison = true;
		}
		if (getHitBlue && reachDestination())
		{
			goPrison = false;
			getHit = false;
			//inPrison = false;
			outPrison = true;
			prisonBallCatchSource.Play();
			state = AIControl_Blue.State.SIDELANEEND;
		}
	}

	void OutPrison()
	{
		//SetStateText();
		//if (inPrison)
		//{

		//}
	}

	void StartState()
	{
		//SetStateText();
	}

	void Hide()
	{
		getHitBlue = false;
		SetStateText();
		if (getHit)
		{
			if (hasBall)
			{
				if (bb != null)
				{
					Destroy(bb);
				}
			}
			getHit = false;
			goPrison = true;
			state = AIControl_Blue.State.SIDELANE;
		}
		else if (inDanger)
		{
			GameObject hidePlace = findCloestLocation(hidePoint);
			MoveToPosition(hidePlace);
		}
		else {
			if (!hasBall)
			{
				state = AIControl_Blue.State.GETBALL;
			}
			if (hasBall)
			{
				state = AIControl_Blue.State.SHOOT;
			}
		}

	}

	GameObject findTeammate(GameObject[] teammates)
	{
		GameObject closest = null;//you will return this as the person you find.
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject teammate in teammates)//go through all players in map
		{
			var diff = (teammate.transform.position - position);
			var curDistance = diff.sqrMagnitude;
			if (curDistance < distance)//is this player closer than the last one?
			{
				//AIControl_Blue aiBlueCurrent = enemy.GetComponent<AIControl_Blue>();
				teammateCurrent = teammate.GetComponent<AIControl_Blue>();
				//bool currentTargetEnemyInPrison = aiBlueCurrent.inPrison;
				//currentTargetEnemyInPrison = aiBlueCurrent.goPrison;

				currentTeammateInPrison = teammateCurrent.goPrison;
				//bool currentTargetEnemyGoPrison = aiBlueCurrent.goPrison;
				//if (!currentTargetEnemyInPrison && !currentTargetEnemyGoPrison)
				if (currentTeammateInPrison)
				{
					closest = teammate;//this is the closest player
					distance = curDistance;//set the closest distance
				}
			}
		}
		if (closest != null)
		{
			return closest.gameObject;//this is the closest player	
		}
		else {
			return null;
		}
	}

	GameObject findTargetEnemy(GameObject[] enemies)
	{
		GameObject closest = null;//you will return this as the person you find.
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject enemy in enemies)//go through all players in map
		{
			var diff = (enemy.transform.position - position);
			var curDistance = diff.sqrMagnitude;
			if (curDistance < distance)//is this player closer than the last one?
			{
				//AIControl_Blue aiBlueCurrent = enemy.GetComponent<AIControl_Blue>();
				aiRedCurrent = enemy.GetComponent<AIControl>();
				//bool currentTargetEnemyInPrison = aiBlueCurrent.inPrison;
				currentTargetEnemyInPrison = aiRedCurrent.goPrison;
				//bool currentTargetEnemyGoPrison = aiBlueCurrent.goPrison;
				//if (!currentTargetEnemyInPrison && !currentTargetEnemyGoPrison)
				if (!currentTargetEnemyInPrison)
				{
					closest = enemy;//this is the closest player
					distance = curDistance;//set the closest distance
				}
			}
		}
		if (closest != null)
		{
			return closest.gameObject;//this is the closest player	
		}
		else {
			if (winNotSet)
			{
				winNotSet = false;
				winText.text = "Blue Win !";
				gameEndSource.Play();
				//return null;
			}
			return null;
		}
	}

	GameObject findCloestLocation(GameObject[] gos)
	{
		GameObject closest = null;//you will return this as the person you find.
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos)//go through all players in map
		{
			var diff = (go.transform.position - position);
			var curDistance = diff.sqrMagnitude;
			if (curDistance < distance)//is this player closer than the last one?
			{
				closest = go;//this is the closest player
				distance = curDistance;//set the closest distance
			}
		}
		return closest.gameObject;//this is the closest player
	}

	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}

	void OnAnimatorMove()
	{
		this.transform.position = anim.rootPosition; //npc only changes pos if mecanim says so 
		this.transform.rotation = anim.rootRotation; //npc only changes rotation if mecanim says so
		navMeshAgent.nextPosition = this.transform.position;
	}

	void SetStateText()
	{
		stateText.text = "State: " + state.ToString();
	}

	void Spawn()
	{
		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = UnityEngine.Random.Range(0, ballPoint.Length);

		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate(blueBall, ballPoint[spawnPointIndex].transform.position, ballPoint[spawnPointIndex].transform.rotation);
	}
}
