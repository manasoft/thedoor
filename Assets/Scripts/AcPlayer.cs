using UnityEngine;
using System.Collections;

/// <summary>
/// 動的に呼び出すオブジェクト用のスクリプトです
/// 
/// プレイヤーと言うか視点と言うかカメラと言うか・・・
/// </summary>
public class AcPlayer : MonoBehaviour
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * 
	 */
	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //
	/*
	 * 
	 */
	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// トリガー
	/// </summary>
	public enum Trigger
	{
		END,
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private const int _PHASE_INI = 0;
	private const int _PHASE_READY = 1;
	private const int _PHASE_IDLE = 2;
	private const int _PHASE_MOVE = 3;
	private const int _PHASE_FAILURE = 4;
	private const int _PHASE_SUCCESS = 5;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _DOOR_NONE = -1;
	private const int _DOOR_L = 0;
	private const int _DOOR_C = 1;
	private const int _DOOR_R = 2;
	private const int _DOOR_NUM = 3;

	private const int _ROOM_A = 0;
	private const int _ROOM_B = 1;
	private const int _ROOM_NUM = 2;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/*
	 * ローカルポジションになるように親を設定しています
	 */

	/// <summary>
	/// プレイヤーの位置はアニメーションで行うので使わない事になりました
	/// Vector3 は const に出来ないとエラーが出ます
	/// </summary>
	//private const float _PLAYER_POS_X = 0.0f;
	//private const float _PLAYER_POS_Y = 0.0f;
	//private const float _PLAYER_POS_Z = 0.0f;
	//private Vector3 _PLAYER_POS = new Vector3( _PLAYER_POS_X, _PLAYER_POS_Y, _PLAYER_POS_Z );

	//
	private const float _ROOM_POS_X = 0.0f;
	private const float _ROOM_POS_Y = 0.0f;
	private const float _ROOM_POS_Z = 0.0f;
	//
	private const float _ROOM_SCALE_X = 1.0f;
	private const float _ROOM_SCALE_Y = 1.0f;
	private const float _ROOM_SCALE_Z = 2.0f;
	//
	private Vector3 _ROOM_POS_A = new Vector3( _ROOM_POS_X, _ROOM_POS_Y, _ROOM_POS_Z );
	private Vector3 _ROOM_POS_B = new Vector3( _ROOM_POS_X, _ROOM_POS_Y, _ROOM_POS_Z + _ROOM_SCALE_Z );

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 親
	/// </summary>
	private AcGameManager m_vManager;

	/// <summary>
	/// 
	/// </summary>
	private AiPlayerTrigger m_vTrigger;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// プレイヤー（カメラ）の移動をアニメーションで動かしてます
	/// </summary>
	private Animator m_vAnimator;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// オートプレイ状態？
	/// </summary>
	private bool m_bAuto;
	/// <summary>
	/// オートプレイを開始してよ！（直接変更しないで、メソッドを使って変更すること！）
	/// </summary>
	private bool m_bRequestPlayAuto;
	/// <summary>
	/// オートプレイを停止してよ！（直接変更しないで、メソッドを使って変更すること！）
	/// </summary>
	private bool m_bRequestStopAuto;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// ゲーム遷移を管理するフラグです
	/// </summary>
	private int m_vPhase;

	/// <summary>
	/// ゲームの終了フラグ（成功時のみフラグが立ちます）
	/// </summary>
	private bool m_bClear;

	/// <summary>
	/// 部屋の通し番号（加算しかしないよ）
	/// 現在の部屋（_ROOM_A/B）を求めるときはこの変数を２（_ROOM_NUM）で割った余りだよ！
	/// </summary>
	private int m_vRoomCount;

	///// <summary>
	///// 鍵のかかっていないドア番号
	///// </summary>
	//private int m_vUnlockDoor;

	/// <summary>
	/// 選択したドア番号
	/// </summary>
	private int m_vSelectDoor;

	/// <summary>
	/// 移動データを保持
	/// Unity の Animation 機能でやり直し中なので使わなくなる予定です（2014/10/24）
	/// </summary>
	private _Move m_vMove;

	/// <summary>
	/// ２つの部屋を交互に移動して表示するよ！
	/// </summary>
	private AcRoom[] m_vRoom;

	/// <summary>
	/// Gui の表示（time / door）
	/// </summary>
	private AcGuiGame m_vGuiGame;

	/// <summary>
	/// タイマーのオンオフ切り替えスイッチ
	/// </summary>
	private bool m_bGuiTimerActive;

	/// <summary>
	/// タイムを数えるよ（モードによって加減算するよ）
	/// </summary>
	private float m_vGuiTimeCount;

	/// <summary>
	/// ドアを数えるよ（モードによって加減算するよ）
	/// </summary>
	private int m_vGuiDoorCount;

	///// <summary>
	///// 実験中！
	///// </summary>
	//private AcSoundManager m_vSoundManager;


	private string m_vSoundTrackBgm;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 
	/// </summary>
	/// <param name="vEntryName"></param>
	/// <returns></returns>
	private void _playSe( string vEntryName )
	{
		if ( !m_bAuto )
		{
			m_vManager.soundPlay( vEntryName );
		}
	}

	private void _playBgm( string vEntryName )
	{
		if ( !m_bAuto )
		{
			m_vSoundTrackBgm = m_vManager.soundPlay( vEntryName );
		}
	}

	private void _stopBgm()
	{
		if(m_vSoundTrackBgm != null)
		{
			m_vManager.soundStop( m_vSoundTrackBgm );
		}
	}

	// ========================================================================== //
	// ========================================================================== //



	// ========================================================================== //
	// ========================================================================== //

	/*
	 * プレイヤー（カメラ）の移動ルートのデータ
	 * C# は struct が使えるので使ってみたよ
	 */
	private struct _Root
	{
		public Vector3 m_vVector;
		public int m_vCount;

		/*
		 * struct はパラメータのないコンストラクタが禁止？
		 */
		//		public MoveData()
		//		{
		//			m_vPos = new Vector3();
		//		}
		public _Root( float vX, float vY, float vZ, int vCount )
		{
			m_vVector = new Vector3( vX, vY, vZ );
			m_vCount = vCount;
		}
	}

	/*
	 * 実際の _Root のデータ
	 * 
	 * ジャグ配列？って言うのか？
	 * http://msdn.microsoft.com/ja-jp/library/2s05feca.aspx
	 */
	//private _Root[][] rootTbl =
	//{
	//	// _DOOR_L
	//	new _Root[]
	//	{
	//		new _Root(  0.0f,  0.0f,  0.0f, 5 ),
	//		new _Root( -0.3f,  0.0f,  1.0f, 5 ),
	//		new _Root(  0.0f,  0.0f,  2.0f, 0 ),
	//	},
	//	// _DOOR_C
	//	new _Root[]
	//	{
	//		new _Root(  0.0f,  0.0f,  0.0f, 10 ),
	//		new _Root(  0.0f,  0.0f,  2.0f, 0 ),
	//	},
	//	// _DOOR_R
	//	new _Root[]
	//	{
	//		new _Root(  0.0f,  0.0f,  0.0f, 5 ),
	//		new _Root( +0.3f,  0.0f,  1.0f, 5 ),
	//		new _Root(  0.0f,  0.0f,  2.0f, 0 ),
	//	},
	//};

	//
	private _Root[][] _rootMoveNearTbl =
	{
		// _DOOR_L
		new _Root[]
		{
			new _Root(  0.0f,  0.0f,  0.0f, 5 ),
			new _Root( -0.3f,  0.0f,  0.5f, 0 ),
		},
		// _DOOR_C
		new _Root[]
		{
			new _Root(  0.0f,  0.0f,  0.0f, 5 ),
			new _Root(  0.0f,  0.0f,  0.5f, 0 ),
		},
		// _DOOR_R
		new _Root[]
		{
			new _Root(  0.0f,  0.0f,  0.0f, 5 ),
			new _Root( +0.3f,  0.0f,  0.5f, 0 ),
		},
	};
	private _Root[][] _rootMovePassTbl =
	{
		// _DOOR_L
		new _Root[]
		{
			new _Root( -0.3f,  0.0f,  0.5f, 5 ),
			new _Root( -0.3f,  0.0f,  1.5f, 5 ),
			new _Root(  0.0f,  0.0f,  2.0f, 0 ),
		},
		// _DOOR_C
		new _Root[]
		{
			new _Root(  0.0f,  0.0f,  0.5f, 10 ),
			new _Root(  0.0f,  0.0f,  2.0f, 0 ),
		},
		// _DOOR_R
		new _Root[]
		{
			new _Root( +0.3f,  0.0f,  0.5f, 5 ),
			new _Root( +0.3f,  0.0f,  1.5f, 5 ),
			new _Root(  0.0f,  0.0f,  2.0f, 0 ),
		},
	};
	//
	private _Root[][] _rootMoveBackTbl =
	{
		// _DOOR_L
		new _Root[]
		{
			new _Root( -0.3f,  0.0f,  0.5f, 60 ),
			new _Root(  0.0f,  0.0f,  0.0f, 0 ),
		},
		// _DOOR_C
		new _Root[]
		{
			new _Root(  0.0f,  0.0f,  0.5f, 60 ),
			new _Root(  0.0f,  0.0f,  0.0f, 0 ),
		},
		// _DOOR_R
		new _Root[]
		{
			new _Root( +0.3f,  0.0f,  0.5f, 60 ),
			new _Root(  0.0f,  0.0f,  0.0f, 0 ),
		},
	};

	/*
	 * _Root を処理して移動量を出力するクラス
	 */
	private class _Move
	{
		_Root[] m_vRoot;
		int m_vMajorCount;
		int m_vMinerCount;

		public _Move( _Root[] vRoot )
		{
			m_vRoot = vRoot;
			m_vMajorCount = 0;
			m_vMinerCount = 0;
		}

		/*
		 * しかし null 非許容型って何のためにあるんだよ！
		 * Nullable 型
		 * http://ufcpp.net/study/csharp/sp2_nullable.html
		 */
		public Vector3? update()
		{
			Vector3? vector = null;
			/*
			 * 配列の長さ
			 * http://www.ipentec.com/document/document.aspx?page=csharp-get-array-length
			 */
			if ( ( m_vMajorCount + 1 ) < m_vRoot.Length )
			{
				if ( m_vMinerCount < m_vRoot[ m_vMajorCount ].m_vCount )
				{
					Vector3 start = m_vRoot[ m_vMajorCount + 0 ].m_vVector;
					int count = m_vRoot[ m_vMajorCount + 0 ].m_vCount;
					Vector3 end = m_vRoot[ m_vMajorCount + 1 ].m_vVector;
					//
					vector = ( end - start ) / count;
				}
				//
				if ( ( m_vMinerCount + 1 ) < m_vRoot[ m_vMajorCount ].m_vCount )
				{
					m_vMinerCount++;
				}
				else
				{
					m_vMajorCount++;
					m_vMinerCount = 0;
				}
			}
			//
			return ( vector );
		}
	}

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	/*
	 * プレハブからインスタンスするよ
	 */

	public static AcPlayer Create( AcGameManager vManager, AiPlayerTrigger vTrigger )
	{
		Debug.Log( "---1" );

		GameObject _prefab = ( GameObject ) Resources.Load( "Prefabs/Player" );
		Debug.Log( "---2" );
		//
		GameObject _object = ( GameObject ) Instantiate( _prefab, AcApp.GamePosition, Quaternion.identity );
		Debug.Log( "---3" );
		//
		AcPlayer _class = _object.GetComponent<AcPlayer>();
		Debug.Log( "---4" );
		//
		_class._create( vManager, vTrigger );
		Debug.Log( "---5" );
		//
		return ( _class );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _create( AcGameManager vManager, AiPlayerTrigger vTrigger )
	{
		m_vManager = vManager;
		m_vTrigger = vTrigger;
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// コルーチン
	/// ゲーム開始時に３２１０のカウントダウンをするよ
	/// ・カウントダウン処理する
	/// ・０表示でフェイズを変更
	/// ・表示を消す
	/// </summary>
	/// <returns></returns>
	private IEnumerator _coroutineReady()
	{
		bool _is_start = false;
		float _timer = 4.0f; // "4" で初期設定してすぐ減算して "3.??" になって (int) キャストするよ！
		//
		while ( _timer > 0.0f )
		{
			_timer -= Time.deltaTime;
			//
			int _count = ( int ) _timer;
			//
			m_vGuiGame.setReady( _count );
			//
			if ( !_is_start )
			{
				if ( _count == 0 )
				{
					_is_start = true;
					//
					// コルーチンのスタート
					StartCoroutine( _coroutineTimer() );
					//
					m_vPhase = _PHASE_IDLE;
					/*
					 * 実験
					 */
					_playBgm( "bgm_1" );
				}
			}
			//
			yield return null;
		}
		//
		//		m_vGui.swReadyActive( false );
		m_vGuiGame.swReadyActive( false );
	}

	/// <summary>
	/// コルーチン
	/// ・モードによってタイマーの増減をするよ
	/// ・チャレンジモードの場合は終了フラグを立てるよ
	/// </summary>
	/// <returns></returns>
	private IEnumerator _coroutineTimer()
	{
		//		int _frame = AcSetting.getFrameRate();
		//
		m_bGuiTimerActive = true;
		//
		while ( m_bGuiTimerActive )
		{
			//float _deltaTime = Time.deltaTime;
			//Debug.Log( "デルタタイム >> " + _deltaTime.ToString() );

			switch ( AcApp.getMode() )
			{
				//
				case ( AcApp.TIMEATTACK_MODE ):
					m_vGuiTimeCount += Time.deltaTime;
					break;
				//
				case ( AcApp.CHALLENGE_MODE ):
					m_vGuiTimeCount -= Time.deltaTime;
					//
					if ( m_vGuiTimeCount <= 0.0f )
					{
						m_vGuiTimeCount = 0.0f;
						m_bGuiTimerActive = false;
						m_bClear = true;
					}
					break;
			}
			//
			//			m_vGui.setTimer( m_vGuiTimeCount );
			m_vGuiGame.setTime( m_vGuiTimeCount );
			//
			yield return null;
		}
	}

	/// <summary>
	/// コルーチン
	/// ・ゲーム終了時の時間待ちするだけです
	/// </summary>
	/// <returns></returns>
	private IEnumerator _coroutineResult()
	{
		m_bGuiTimerActive = false;

		int _frame = 0;
		//
		while ( ( _frame++ ) < 60 * 2 )
		{
			yield return null;
		}
		//

		//	Application.LoadLevel( "Ranking" );
		{
			m_vTrigger.onTrigger( Trigger.END );


			m_vGuiGame.swResultActive( false );

			_stopBgm();

			//
			//			requestPlayAuto();

			m_bAuto = true;
			m_vPhase = _PHASE_INI;



			//AcApp.swPlayAuto( true );
			//AcApp.swStopAuto( false );
		}
	}


	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//private void _startReady()
	//{
	//	StartCoroutine( _coroutineReady() );
	//}

	//private void _startTimer()
	//{
	//	StartCoroutine( _coroutineTimer() );
	//}

	//private void _stopTimer()
	//{
	//	m_bGuiTimerActive = false;
	//}

	private void _startResult()
	{
		//		m_bGuiTimerActive = false;
		//
		StartCoroutine( _coroutineResult() );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _setRoom( AcRoom vRoom, int vCount )
	{
		vRoom.setRoom( 0 );
		// 正解
		//		m_vUnlockDoor = vCount % _DOOR_NONE;
	}

	//	private void _setIni()
	//	{
	////		transform.position = _PLAYER_POS;
	//		//
	//		//m_vRoom[ _ROOM_A ].transform.position = _ROOM_POS_A;
	//		//m_vRoom[ _ROOM_B ].transform.position = _ROOM_POS_B;
	//		////
	//		//_setRoom( m_vRoom[ _ROOM_A ], m_vRoomCount + 0 );
	//		//_setRoom( m_vRoom[ _ROOM_B ], m_vRoomCount + 1 );
	//	}

	private void _setPass()
	{
		//		transform.position = _PLAYER_POS;
		// 
		m_vRoomCount++;
		//
		int _room_a = ( m_vRoomCount + 0 ) % _ROOM_NUM; // 次の部屋（入った部屋）
		int _room_b = ( m_vRoomCount + 1 ) % _ROOM_NUM; // 元の部屋（出た部屋 → 新しい部屋）
		//
		//m_vRoom[ _room_a ].transform.position = _ROOM_POS_A;
		//m_vRoom[ _room_b ].transform.position = _ROOM_POS_B;
		m_vRoom[ _room_a ].transform.localPosition = _ROOM_POS_A;
		m_vRoom[ _room_b ].transform.localPosition = _ROOM_POS_B;
		//
		_setRoom( m_vRoom[ _room_b ], m_vRoomCount + 1 );
	}

	private void _setBack()
	{
		//		transform.position = _PLAYER_POS;
		////
		//int _room_a = ( m_vRoomCount + 0 ) % _ROOM_NUM; // 元の部屋
		//int _room_b = ( m_vRoomCount + 1 ) % _ROOM_NUM; // 次の部屋
		////
		//m_vRoom[ _room_a ].transform.position = _ROOM_POS_A;
		//m_vRoom[ _room_b ].transform.position = _ROOM_POS_B;
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// フェイズ：初期設定
	/// </summary>
	private void _phaseIni()
	{
		if ( m_bAuto )
		{
			m_vGuiGame.swReadyActive( false );
			m_vGuiGame.swTimeActive( false );
			m_vGuiGame.swDoorActive( false );
			//
			m_vPhase = _PHASE_IDLE;
		}
		else
		{
			m_vGuiGame.swReadyActive( true );
			m_vGuiGame.swTimeActive( true );
			m_vGuiGame.swDoorActive( true );
			//
			switch ( AcApp.getMode() )
			{
				//
				case ( AcApp.TIMEATTACK_MODE ):
					//
					m_vGuiTimeCount = 0;
					m_vGuiDoorCount = AcApp.TIMEATTACK_DOORS;
					break;
				//
				case ( AcApp.CHALLENGE_MODE ):
					//
					m_vGuiTimeCount = AcApp.CHALLENGE_TIME;
					m_vGuiDoorCount = 0;
					break;
			}
			//
			m_vGuiGame.setTime( m_vGuiTimeCount );
			m_vGuiGame.setDoor( m_vGuiDoorCount );
			//
			StartCoroutine( _coroutineReady() );
			//
			m_vPhase = _PHASE_READY;
		}
	}

	/// <summary>
	/// カウントダウン中
	/// </summary>
	private void _phaseReady()
	{
		// コルーチン待ちなので何もしません
	}

	/// <summary>
	/// 待機状態
	/// </summary>
	private void _phaseIdle()
	{
		if ( m_bAuto )
		{
			if ( m_bRequestStopAuto )
			{
				/*
				 * ゲームの開始時にコッチに来るハズ！
				 */
				m_bRequestStopAuto = false;
				m_bAuto = false;
				//
				m_vPhase = _PHASE_INI;
			}
			else
			{
				/*
				 * オートデモ中は自動的に正解を選ぶよ！
				 */
				m_vSelectDoor = m_vRoom[ m_vRoomCount % _ROOM_NUM ].getUnlock();
				//
				m_vAnimator.SetBool( "bMove", true );
				m_vAnimator.SetInteger( "vRoot", m_vSelectDoor );
				//
				m_vPhase = _PHASE_MOVE;
			}
		}
		else
		{
			if ( m_bRequestPlayAuto )
			{
				/*
				 * コッチは来ないハズ！
				 */
				m_bRequestPlayAuto = false;
				m_bAuto = true;
				//
				m_vPhase = _PHASE_INI;
			}
			else
			{
				if ( m_bClear )
				{
					/*
					 * クリア時！
					 */
					m_vPhase = _PHASE_SUCCESS;
					//
					//
					//m_vGui.swResultActive( true );
					//m_vGui.swResultType( true );
					m_vGuiGame.swResultActive( true );
					m_vGuiGame.swResultSuccess( true );

					/*
					 * ランキングへ登録する
					 */
					switch ( AcApp.getMode() )
					{
						//
						case ( AcApp.TIMEATTACK_MODE ):
							//
							AcSave.addTime( m_vGuiTimeCount );
							break;
						//
						case ( AcApp.CHALLENGE_MODE ):
							//
							AcSave.addDoor( m_vGuiDoorCount );
							break;
					}

					// コルーチンのスタート
					_startResult();
				}
				else
				{
					/*
					 * コッチはなんかコンパイルエラーになる
					 * http://qiita.com/JunSuzukiJapan@github/items/1698a21323558e246c4f
					 */

					/*
					 * http://bribser.co.jp/blog/tappobject/
					 * 
					 * 引数の out って何？
					 * http://msdn.microsoft.com/ja-jp/library/t3c3bfhx.aspx
					 * 参照渡しになるらしいです！
					 */
					if ( Input.GetMouseButtonDown( 0 ) )
					{
						Ray _ray = Camera.main.ScreenPointToRay( Input.mousePosition );
						RaycastHit _hit = new RaycastHit();
						//
						if ( Physics.Raycast( _ray, out _hit ) )
						{
							int _door_id = _DOOR_NONE;
							//
							GameObject _object = _hit.collider.gameObject;
							Debug.Log( _object.name );
							//
							if ( ( _object.name ).CompareTo( "DoorL" ) == ( 0 ) )
							{
								_door_id = _DOOR_L;
							}
							if ( ( _object.name ).CompareTo( "DoorC" ) == ( 0 ) )
							{
								_door_id = _DOOR_C;
							}
							if ( ( _object.name ).CompareTo( "DoorR" ) == ( 0 ) )
							{
								_door_id = _DOOR_R;
							}
							//
							if ( _door_id != _DOOR_NONE )
							{
								m_vSelectDoor = _door_id;
								//						m_vMove = new _Move( _rootMoveNearTbl[ m_vSelectDoor ] );
								//
								m_vPhase = _PHASE_MOVE;


								{
									Debug.Log( "SELECT でアニメーション起動！" );
									/*
									 * アニメーションで動かしてみる
									 */
									m_vAnimator.SetBool( "bMove", true );
									m_vAnimator.SetInteger( "vRoot", _door_id );
								}
							}

							{
								/// 実験用タッチしたら音を出してみる
								//	m_vSoundManager.play( "se_1" );
							}
						}
					}
				}
			}
		}
	}



	/// <summary>
	/// 名前を "_phaseMove()" に変更予定っす
	/// </summary>
	private void _phaseMove()
	{
		/*
		 * アニメーションでプレイヤーを動かしている
		 * アニメーションから呼び出すメソッドでフェイズを切り替えてもらいます
		 * なのでココでは何もしません
		 */
	}

	private void _phaseSuccess()
	{
		//		Debug.Log( "Success" );
	}

	private void _phaseFailure()
	{
		//		Debug.Log( "Failure" );
	}

	// ========================================================================== //
	// ========================================================================== //

	///// <summary>
	///// アニメーションから呼び出してもらうメソッド
	///// </summary>
	//public void onIdle()
	//{
	//	m_vAnimator.SetBool( "bMove", false );
	//	m_vAnimator.SetBool( "bMiss", false );
	//}

	/// <summary>
	/// アニメーションから呼び出してもらうメソッド
	/// </summary>
	public void onJudge()
	{
		Debug.Log( "onJudge" );


		m_vAnimator.SetBool( "bMove", false );


		if ( m_vRoom[ m_vRoomCount % _ROOM_NUM ].getUnlock() == m_vSelectDoor )
		{
			switch ( AcApp.getMode() )
			{
				//
				case ( AcApp.TIMEATTACK_MODE ):
					//
					m_vGuiDoorCount--;
					//
					if ( m_vGuiDoorCount == 0 )
					{
						m_bClear = true;
					}
					break;
				//
				case ( AcApp.CHALLENGE_MODE ):
					//
					m_vGuiDoorCount++;
					break;
			}
			//
			m_vGuiGame.setDoor( m_vGuiDoorCount );

			/*
			 * 実験
			 */
			_playSe( "se_1" );
		}
		else
		{
			/*
			 * 失敗時
			 */
			m_vAnimator.SetBool( "bMiss", true );

			/*
			 * 実験
			 */
			_playSe( "se_2" );
		}

	}

	public void onMoveStart()
	{
		Debug.Log( "onMoveStart_1" );

		//m_vAnimator.SetBool( "bMove", false );
	}

	public void onMoveEnd()
	{
		Debug.Log( "onMoveEnd" );

		//m_vAnimator.SetBool( "bMove", false );
		//m_vAnimator.SetBool( "bMiss", false );

		/*
		 * オブジェクトの位置をリセットする
		 */
		_setPass();

		/*
		 * 続くよ！
		 */
		m_vPhase = _PHASE_IDLE;
	}

	public void onMissStart()
	{
		Debug.Log( "onMissStart" );

		m_vAnimator.SetBool( "bMiss", false );
	}

	public void onMissEnd()
	{
		Debug.Log( "onMissEnd" );

		//m_vAnimator.SetBool( "bMove", false );
		//m_vAnimator.SetBool( "bMiss", false );

		bool _debug = false;
		if ( _debug )
		{
			m_vPhase = _PHASE_IDLE;
			return;
		}

		/*
		 * オブジェクトの位置をリセットする
		 * プレイヤー位置だけなので多分不要！
		 */
		//		_setBack();
		/*
		 * 失敗画面へ！
		 */
		m_vPhase = _PHASE_FAILURE;
		//
		m_vGuiGame.swResultActive( true );
		m_vGuiGame.swResultSuccess( false );

		// コルーチンのスタート
		_startResult();
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// オートプレイを開始してほしい時に呼んでくれ！
	/// </summary>
	public void requestPlayAuto()
	{
		if ( !m_bAuto )
		{
			m_bRequestPlayAuto = true;
			m_bRequestStopAuto = false;
		}
	}

	/// <summary>
	/// オートプレイを停止してほしい時に呼んでくれ！
	/// </summary>
	public void requestStopAuto()
	{
		if ( m_bAuto )
		{
			m_bRequestPlayAuto = false;
			m_bRequestStopAuto = true;
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void _awake()
	{
		Debug.Log( "_awake()" );
	}

	private void _start()
	{
		// 親子関係
		this.transform.parent = m_vManager.transform;

		//
		m_vPhase = _PHASE_INI;
		//
		//		m_bAuto = true;
		//
		m_bClear = false;
		//
		m_vRoomCount = 0;
		//
		//		m_vUnlockDoor = _DOOR_NONE;
		m_vSelectDoor = _DOOR_NONE;
		m_vMove = null;

		//		m_vRoomId = _ROOM_A;
		m_vRoom = new AcRoom[ _ROOM_NUM ];

		/*
		 * キャスト出来ない件
		 * http://pengames.jp/2013/12/02/instantiate%E3%81%AE%E6%88%BB%E3%82%8A%E5%80%A4%E3%81%8Cgameobject%E3%81%AB%E3%82%AD%E3%83%A3%E3%82%B9%E3%83%88%E3%81%A7%E3%81%8D%E3%81%AA%E3%81%84/
		 * 
		 * 回避できる？
		 * http://nijibox.jp/smap/2012/05/project_prefab/
		 */

		//// プレハブを取得
		//AcRoom prefab = ( AcRoom ) Resources.Load( "Prefabs/Room", typeof( AcRoom ) );
		//// プレハブからインスタンスを生成
		//m_vRoom[ _ROOM_A ] = ( AcRoom ) Instantiate( prefab, new Vector3( 0.0f, 0.0f, 0.0f ), Quaternion.identity );
		//m_vRoom[ _ROOM_B ] = ( AcRoom ) Instantiate( prefab, new Vector3( 0.0f, 0.0f, 2.0f ), Quaternion.identity );
		////
		////		m_vRoom[_ROOM_A].transform.tag = _ROOM_TAG_A;
		////		m_vRoom[_ROOM_B].transform.tag = _ROOM_TAG_A;
		//m_vRoom[ _ROOM_A ].initialize( 0, 0 );
		//m_vRoom[ _ROOM_B ].initialize( 1, 1 );

		//AcRoom prefab = AcRoom.getPrefab();
		//m_vRoom[ _ROOM_A ] = AcRoom.Create( prefab, new Vector3( 0.0f, 0.0f, 0.0f ), Quaternion.identity, 0, 0 );
		//m_vRoom[ _ROOM_B ] = AcRoom.Create( prefab, new Vector3( 0.0f, 0.0f, 2.0f ), Quaternion.identity, 1, 1 );
		GameObject _prefab = AcRoom.getPrefab();
		//m_vRoom[ _ROOM_A ] = AcRoom.Create( prefab, new Vector3( 0.0f, 0.0f, 0.0f ), Quaternion.identity, 0, 0 );
		//m_vRoom[ _ROOM_B ] = AcRoom.Create( prefab, new Vector3( 0.0f, 0.0f, 2.0f ), Quaternion.identity, 1, 1 );
		m_vRoom[ _ROOM_A ] = AcRoom.Create( _prefab );
		m_vRoom[ _ROOM_B ] = AcRoom.Create( _prefab );
		/*
		 * この辺がよくわからんとこなのですが
		 * コンポーネントのスクリプトに対してオブジェクトの処理が出来ちゃうのって何か違和感があるのですが、
		 * なんとなくやったら出来たので、こういうものなのかと・・・
		 */
		m_vRoom[ _ROOM_A ].transform.parent = this.transform.parent;
		m_vRoom[ _ROOM_B ].transform.parent = this.transform.parent;
		//
		//m_vRoom[ _ROOM_A ].transform.position = AcApp.GamePosition;
		//m_vRoom[ _ROOM_B ].transform.position = AcApp.GamePosition;
		//
		m_vRoom[ _ROOM_A ].transform.localPosition = _ROOM_POS_A;
		m_vRoom[ _ROOM_B ].transform.localPosition = _ROOM_POS_B;
		//
		_setRoom( m_vRoom[ _ROOM_A ], m_vRoomCount + 0 );
		_setRoom( m_vRoom[ _ROOM_B ], m_vRoomCount + 1 );

		//
		m_vGuiGame = new AcGuiGame();


		/*
		 * 初期時は強制的にオートにする
		 */
		m_bAuto = true;

		//m_vSoundManager = new AcSoundManager();
		//m_vSoundManager.add( "se_1", new string[] { "se1", "se2", "se3" }, "Sounds/Seikai02-1" );
		//m_vSoundManager.add( "se_2", new string[] { "se1" }, "Sounds/Huseikai02-4" );
		//m_vSoundManager.add( "bgm_1", new string[] { "bgm1" }, "Sounds/Encounter_loop" );
		//m_vSoundManager.add( "se_1", "Sounds/Seikai02-1", false );
		//m_vSoundManager.add( "se_2", "Sounds/Huseikai02-4", false );
		//m_vSoundManager.add( "bgm_1", "Sounds/Encounter_loop", true );

		Debug.Log( "Player アニメーション取得準備" );
		m_vAnimator = transform.GetComponent<Animator>();
		Debug.Log( "Player アニメーション取得完了？" );
	}

	private void _update()
	{
	}

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// ※注意
	/// ・Instantiate 直後に別スレッドで呼ばれちゃいみたいっす
	/// ・自分 this は参照出来るが、コンストラクタ内の途中で呼び出されるので注意が必要デス
	/// </summary>
	void Awake()
	{
		_awake();
	}

	// Use this for initialization
	void Start()
	{
		_start();
	}

	// Update is called once per frame
	void Update()
	{
		//m_vSoundManager.update();

		switch ( m_vPhase )
		{
			case ( _PHASE_INI ):
				_phaseIni();
				break;
			//
			case ( _PHASE_READY ):
				_phaseReady();
				break;
			//
			case ( _PHASE_IDLE ):
				_phaseIdle();
				break;
			//
			case ( _PHASE_MOVE ):
				_phaseMove();
				break;
			//
			case ( _PHASE_SUCCESS ):
				_phaseSuccess();
				break;
			//
			case ( _PHASE_FAILURE ):
				_phaseFailure();
				break;
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	void OnGUI()
	{
		m_vGuiGame.onGUI();
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/*
	 * OnApplicationQuit / OnDestroy 
	 * http://dymn.hatenablog.com/entry/2014/02/21/205409
	 * 
	 */

	void OnApplicationQuit()
	{
		Debug.Log( "AcPlayer >> OnApplicationQuit()" );
		//
		//		m_vSoundManager.destroy();
	}

	void OnDestroy()
	{
		Debug.Log( "AcPlayer >> OnDestroy()" );
		//
		//		m_vSoundManager.destroy();
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 
	/// </summary>
	public interface AiPlayerTrigger
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="vTrigger"></param>
		void onTrigger( AcPlayer.Trigger vTrigger );
	}

	// ========================================================================== //
	// ========================================================================== //
}
