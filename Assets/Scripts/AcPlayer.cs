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
		FINISH,		/// 通常終了
		//QUIT,		/// 強制終了（エスケープボタン？）
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
	/// なんか static readonly を使えばいいらしい
	/// </summary>
	private const float _PLAYER_POS_X = 0.0f;
	private const float _PLAYER_POS_Y = 0.0f;
	private const float _PLAYER_POS_Z = 0.0f;
	private static readonly Vector3 _PLAYER_POS = new Vector3( _PLAYER_POS_X, _PLAYER_POS_Y, _PLAYER_POS_Z );

	//
	private const float _ROOM_POS_X = 0.0f;
	private const float _ROOM_POS_Y = 0.0f;
	private const float _ROOM_POS_Z = 0.0f;
	//
	private const float _ROOM_SCALE_X = 1.0f;
	private const float _ROOM_SCALE_Y = 1.0f;
	private const float _ROOM_SCALE_Z = 2.0f;
	//
	private static readonly Vector3 _ROOM_POS_A = new Vector3( _ROOM_POS_X, _ROOM_POS_Y, _ROOM_POS_Z );
	private static readonly Vector3 _ROOM_POS_B = new Vector3( _ROOM_POS_X, _ROOM_POS_Y, _ROOM_POS_Z + _ROOM_SCALE_Z );

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
	/// 
	/// 2014/10/29
	/// イベントが呼ばれないとか不具合が多いので使わない方針に変更します
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

	private bool m_bQuit;

	/// <summary>
	/// ゲーム遷移を管理するフラグです
	/// </summary>
	private int m_vPhase;

	/// <summary>
	/// ゲームの終了フラグ（成功時のみフラグが立ちます）
	/// </summary>
	private bool m_bClear;

	/// <summary>
	/// 部屋のインデックス
	/// 現在の部屋（_ROOM_A/B）の意味になる
	/// </summary>
	private int m_vRoomIndex;

	/// <summary>
	/// ゲーム開始時に０クリアする（難易度とかに使える？）
	/// 今、自分のいる部屋を（クリアした部屋数）示す
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

	///// <summary>
	///// 移動データを保持
	///// Unity の Animation 機能でやり直し中なので使わなくなる予定です（2014/10/24）
	///// Animation が使えないので復活します！（2014/10/29）
	///// </summary>
	//private _Move m_vMove;

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

	/// <summary>
	/// 
	/// </summary>
	private string m_vSoundTrackBgm;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// デバッグログ
	/// </summary>
	/// <param name="vString"></param>
	private void _debugLog( string vString )
	{
		Debug.Log( vString );
	}

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
			m_vSoundTrackBgm = m_vManager.soundPlay( vEntryName, 1.0f );
		}
	}

	private void _stopBgm()
	{
		if ( m_vSoundTrackBgm != null )
		{
			m_vManager.soundStop( m_vSoundTrackBgm, 1.0f );
		}
	}

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// プレイヤー（カメラ）の移動ルートのデータ
	/// C# は struct が使えるので使ってみたよ
	/// が、やっぱ class にすっか？
	/// </summary>
	private class _Root
	{
		public Vector3 m_vVector;
		public int m_vFrame;

		public _Root( float vX, float vY, float vZ, int vFrame )
		{
			m_vVector = new Vector3( vX, vY, vZ );
			m_vFrame = vFrame;
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
	private static readonly _Root[][] _rootMoveNearTbl =
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
	//
	private static readonly _Root[][] _rootMovePassTbl =
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
	private static readonly _Root[][] _rootMoveBackTbl =
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

	/// <summary>
	/// _Root を処理して移動量を出力するクラス
	/// </summary>
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
		 * ref / out
		 * http://msdn.microsoft.com/ja-jp/library/szasx730.aspx
		 */
		public bool update( ref Vector3 vVector )
		{
			/*
			 * 配列の長さ
			 * http://www.ipentec.com/document/document.aspx?page=csharp-get-array-length
			 */
			if ( ( m_vMajorCount + 1 ) < m_vRoot.Length )
			{
				if ( m_vMinerCount < m_vRoot[ m_vMajorCount ].m_vFrame )
				{
					Vector3 _start = m_vRoot[ m_vMajorCount + 0 ].m_vVector;
					int _count = m_vRoot[ m_vMajorCount + 0 ].m_vFrame;
					Vector3 _end = m_vRoot[ m_vMajorCount + 1 ].m_vVector;
					//
					vVector = ( _end - _start ) / _count;
				}
				//
				if ( ( m_vMinerCount + 1 ) < m_vRoot[ m_vMajorCount ].m_vFrame )
				{
					m_vMinerCount++;
				}
				else
				{
					m_vMajorCount++;
					m_vMinerCount = 0;
				}
				//
				return ( true );
			}
			//
			return ( false );
		}
	}

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// プレハブからインスタンス化するよ
	/// </summary>
	/// <param name="vManager"></param>
	/// <param name="vTrigger"></param>
	/// <returns></returns>
	public static AcPlayer Create( AcGameManager vManager, AiPlayerTrigger vTrigger )
	{
		GameObject _prefab = ( GameObject ) Resources.Load( "Prefabs/Player" );
		//
		GameObject _object = ( GameObject ) Instantiate( _prefab, AcApp.GamePosition, Quaternion.identity );
		//
		AcPlayer _class = _object.GetComponent<AcPlayer>();
		//
		_class._create( vManager, vTrigger );
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
		float _timer = 4.0f; // "4" で初期設定してすぐ減算して "3.??" になって (int) キャストするよ！
		int _old_count = ( int ) _timer;
		//
		while ( _timer > 0.0f )
		{
			_timer -= Time.deltaTime;
			//
			int _new_count = ( int ) _timer;
			//
			m_vGuiGame.setReady( _new_count );
			//
			if ( _old_count != _new_count )
			{
				_old_count = _new_count;
				//
				if ( _new_count > 0 )
				{
					/*
					 * 実験
					 */
					_playSe( "se_cd_1" );
				}
				else
				{
					/*
					 * 実験
					 */
					_playSe( "se_cd_2" );
					_playBgm( "bgm_1" );
					//
					// コルーチンのスタート
					StartCoroutine( _coroutineTimer() );
					//
					m_vPhase = _PHASE_IDLE;
				}
			}
			//
			yield return null;
		}
		//
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
		m_bGuiTimerActive = true;
		//
		while ( m_bGuiTimerActive )
		{
			//float _deltaTime = Time.deltaTime;
			//Debug.Log( "デルタタイム >> " + _deltaTime.ToString() );

			switch ( AcApp.getGameMode() )
			{
				//
				case ( AcApp.GAMEMODE_TIMEATTACK ):
					m_vGuiTimeCount += Time.deltaTime;
					break;
				//
				case ( AcApp.GAMEMODE_CHALLENGE ):
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

		//	Application.LoadLevel( "Ranking" );
		{
			m_vTrigger.onTrigger( Trigger.FINISH );

			m_vGuiGame.swResultActive( false );

			_stopBgm();

			m_bAuto = true;
			m_vPhase = _PHASE_INI;
		}
	}

	/// <summary>
	/// 強制終了のチェック
	/// </summary>
	/// <returns></returns>
	private IEnumerator _coroutineQuit()
	{
		while ( true )
		{
			/*
			 * ゲームの終了とか
			 * http://motogeneralpurpose.blogspot.jp/2013/04/unity.html
			 * 上と同じとこですけどまとまっていたのでメモしときます
			 * http://motogeneralpurpose.blogspot.jp/search/label/Unity
			 */
			//		if ( Application.platform == RuntimePlatform.Android && Input.GetKey( KeyCode.Escape ) )
			if ( Input.GetKey( KeyCode.Escape ) )
			{
				if ( !m_bAuto )
				{
					requestPlayAuto();
					break;
				}
			}
			//
			yield return null;
		}
	}

	private IEnumerator _coroutineMove()
	{
		_Move _move;
		Vector3 _vector = new Vector3();
		//
		_move = new _Move( _rootMoveNearTbl[ m_vSelectDoor ] );
		//
		while ( _move.update( ref _vector ) )
		{
			_debugLog( "_coroutineMove / near" );

			this.transform.localPosition += _vector;
			//
			yield return null;
		}

		bool _is_unlock = _isUnlock();

		//
		if ( _is_unlock )
		{
			_move = new _Move( _rootMovePassTbl[ m_vSelectDoor ] );
		}
		else
		{
			_move = new _Move( _rootMoveBackTbl[ m_vSelectDoor ] );
		}

		//
		while ( _move.update( ref _vector ) )
		{
			_debugLog( "_coroutineMove / pass" );

			this.transform.localPosition += _vector;
			//
			yield return null;
		}

		//
		if ( _is_unlock )
		{
			_toNext();
		}
		else
		{
			_toFailure();
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

	//private void _startResult()
	//{
	//	//		m_bGuiTimerActive = false;
	//	//
	//	StartCoroutine( _coroutineResult() );
	//}

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

	//private void _setPass()
	//{
	//	this.transform.localPosition = _PLAYER_POS;
	//	// 
	//	m_vRoomCount++;
	//	//
	//	int _room_a = ( m_vRoomCount + 0 ) % _ROOM_NUM; // 次の部屋（入った部屋）
	//	int _room_b = ( m_vRoomCount + 1 ) % _ROOM_NUM; // 元の部屋（出た部屋 → 新しい部屋）
	//	//
	//	//m_vRoom[ _room_a ].transform.position = _ROOM_POS_A;
	//	//m_vRoom[ _room_b ].transform.position = _ROOM_POS_B;
	//	m_vRoom[ _room_a ].transform.localPosition = _ROOM_POS_A;
	//	m_vRoom[ _room_b ].transform.localPosition = _ROOM_POS_B;
	//	//
	//	_setRoom( m_vRoom[ _room_b ], m_vRoomCount + 1 );
	//}

	//private void _setBack()
	//{
	//	//		transform.position = _PLAYER_POS;
	//	////
	//	//int _room_a = ( m_vRoomCount + 0 ) % _ROOM_NUM; // 元の部屋
	//	//int _room_b = ( m_vRoomCount + 1 ) % _ROOM_NUM; // 次の部屋
	//	////
	//	//m_vRoom[ _room_a ].transform.position = _ROOM_POS_A;
	//	//m_vRoom[ _room_b ].transform.position = _ROOM_POS_B;
	//}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// フェイズ：初期設定
	/// </summary>
	private void _phaseIni()
	{
		/*
		 * 初期化しておくもの
		 */
		m_bQuit = false;
		m_bClear = false;
		m_vRoomCount = 0;

		//
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
			switch ( AcApp.getGameMode() )
			{
				//
				case ( AcApp.GAMEMODE_TIMEATTACK ):
					//
					m_vGuiTimeCount = 0;
					m_vGuiDoorCount = AcApp.GAMERULE_TIMEATTACK_DOOR;
					break;
				//
				case ( AcApp.GAMEMODE_CHALLENGE ):
					//
					m_vGuiTimeCount = AcApp.GAMERULE_CHALLENGE_TIME;
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
				m_vSelectDoor = m_vRoom[ m_vRoomIndex ].getUnlock();
				//
				m_vAnimator.SetBool( "bMove", true );
				m_vAnimator.SetInteger( "vRoot", m_vSelectDoor );
				//
				m_vPhase = _PHASE_MOVE;

				//
				StartCoroutine( _coroutineMove() );
			}
		}
		else
		{
			if ( m_bRequestPlayAuto )
			{
				/*
				 * 強制終了処理でコッチにくるよ！
				 */
				m_bRequestPlayAuto = false;
				m_bAuto = true;
				//
				//m_vPhase = _PHASE_INI;
				_toFailure();


				/*
				 * 
				 */
				//m_vTrigger.onTrigger( Trigger.QUIT );
			}
			else
			{
				if ( m_bClear )
				{
					_toSuccess();

					///*
					// * クリア時！
					// */
					//m_vPhase = _PHASE_SUCCESS;
					////
					////
					////m_vGui.swResultActive( true );
					////m_vGui.swResultType( true );
					//m_vGuiGame.swResultActive( true );
					//m_vGuiGame.swResultSuccess( true );

					///*
					// * ランキングへ登録する
					// */
					//switch ( AcApp.getGameMode() )
					//{
					//	//
					//	case ( AcApp.GAMEMODE_TIMEATTACK ):
					//		//
					//		AcSave.addTime( m_vGuiTimeCount );
					//		break;
					//	//
					//	case ( AcApp.GAMEMODE_CHALLENGE ):
					//		//
					//		AcSave.addDoor( m_vGuiDoorCount );
					//		break;
					//}

					//// コルーチンのスタート
					//_startResult();
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

								//
								StartCoroutine( _coroutineMove() );


								//{
								//	Debug.Log( "SELECT でアニメーション起動！" );
								//	/*
								//	 * アニメーションで動かしてみる
								//	 */
								//	m_vAnimator.SetBool( "bMove", true );
								//	m_vAnimator.SetInteger( "vRoot", _door_id );
								//}
							}

							{
								// 実験用タッチしたら音を出してみる
								//_playSe( "se_3" );
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


	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 正解判定
	/// true = 正解
	/// false = 不正解
	/// </summary>
	/// <returns>
	/// </returns>
	public bool _isUnlock()
	{
		/*
		 * 正解判定
		 */
		if ( m_vRoom[ m_vRoomIndex ].getUnlock() == m_vSelectDoor )
		{
			switch ( AcApp.getGameMode() )
			{
				//
				case ( AcApp.GAMEMODE_TIMEATTACK ):
					//
					m_vGuiDoorCount--;
					//
					if ( m_vGuiDoorCount == 0 )
					{
						m_bClear = true;
					}
					break;
				//
				case ( AcApp.GAMEMODE_CHALLENGE ):
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
			//
			return ( true );
		}
		else
		{
			/*
			 * 実験
			 */
			_playSe( "se_2" );
			//
			return ( false );
		}
	}

	private void _toNext()
	{
		this.transform.localPosition = _PLAYER_POS;

		//
		m_vRoomIndex = ( m_vRoomIndex + 1 ) % _ROOM_NUM;
		m_vRoomCount++;
		//
		int _room_a = ( m_vRoomIndex + 0 ) % _ROOM_NUM; // 次の部屋（入った部屋）
		int _room_b = ( m_vRoomIndex + 1 ) % _ROOM_NUM; // 元の部屋（出た部屋 → 新しい部屋）
		//
		m_vRoom[ _room_a ].transform.localPosition = _ROOM_POS_A;
		m_vRoom[ _room_b ].transform.localPosition = _ROOM_POS_B;
		//
		_setRoom( m_vRoom[ _room_b ], m_vRoomCount + 1 ); // 次の部屋の設定なので (+1) している

		//
		m_vPhase = _PHASE_IDLE;
	}

	private void _toSuccess()
	{
		this.transform.localPosition = _PLAYER_POS;

		// 結果表示
		m_vGuiGame.swResultActive( true );
		m_vGuiGame.swResultSuccess( true );

		// ランキングへ登録する
		switch ( AcApp.getGameMode() )
		{
			//
			case ( AcApp.GAMEMODE_TIMEATTACK ):
				//
				AcSave.addTime( m_vGuiTimeCount );
				break;
			//
			case ( AcApp.GAMEMODE_CHALLENGE ):
				//
				AcSave.addDoor( m_vGuiDoorCount );
				break;
		}

		// 成功画面へ
		m_vPhase = _PHASE_SUCCESS;

		// コルーチンのスタート
		StartCoroutine( _coroutineResult() );
	}

	private void _toFailure()
	{
		this.transform.localPosition = _PLAYER_POS;

		// 結果表示
		m_vGuiGame.swResultActive( true );
		m_vGuiGame.swResultSuccess( false );

		// 失敗画面へ
		m_vPhase = _PHASE_FAILURE;

		// コルーチンのスタート
		StartCoroutine( _coroutineResult() );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * アニメーションから呼び出してもらうメソッド
	 */

	/// <summary>
	/// アニメーションから呼び出してもらうメソッド
	/// </summary>
	public void onJudge()
	{
		Debug.Log( "onJudge" );


		m_vAnimator.SetBool( "bMove", false );


		if ( m_vRoom[ m_vRoomIndex ].getUnlock() == m_vSelectDoor )
		{
			switch ( AcApp.getGameMode() )
			{
				//
				case ( AcApp.GAMEMODE_TIMEATTACK ):
					//
					m_vGuiDoorCount--;
					//
					if ( m_vGuiDoorCount == 0 )
					{
						m_bClear = true;
					}
					break;
				//
				case ( AcApp.GAMEMODE_CHALLENGE ):
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

		_toNext();
		//		_setPass();

		/*
		 * 続くよ！
		 */
		//		m_vPhase = _PHASE_IDLE;
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
		//m_vPhase = _PHASE_FAILURE;
		////
		//m_vGuiGame.swResultActive( true );
		//m_vGuiGame.swResultSuccess( false );

		//// コルーチンのスタート
		//_startResult();

		_toFailure();
	}

	//private void _resetPlayer()
	//{
	//	this.transform.localPosition = _PLAYER_POS;
	//}

	//private void _updateRoom()
	//{
	//	m_vRoomCount++;
	//	//
	//	int _room_a = ( m_vRoomCount + 0 ) % _ROOM_NUM; // 次の部屋（入った部屋）
	//	int _room_b = ( m_vRoomCount + 1 ) % _ROOM_NUM; // 元の部屋（出た部屋 → 新しい部屋）
	//	//
	//	m_vRoom[ _room_a ].transform.localPosition = _ROOM_POS_A;
	//	m_vRoom[ _room_b ].transform.localPosition = _ROOM_POS_B;
	//	//
	//	_setRoom( m_vRoom[ _room_b ], m_vRoomCount + 1 );
	//}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void _awake()
	{
		_debugLog( "_awake()" );
	}

	private void _start()
	{
		// 親子関係
		this.transform.parent = m_vManager.transform;

		//
		m_vPhase = _PHASE_INI;

		//
		m_bQuit = false;

		// 初期時は強制的にオートにする
		m_bAuto = true;

		//
		m_bClear = false;

		//
		m_vRoomIndex = 0;
		m_vRoomCount = 0;

		//
		m_vSelectDoor = _DOOR_NONE;

		////
		//m_vMove = null;

		//
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

		Debug.Log( "Player アニメーション取得準備" );
		m_vAnimator = transform.GetComponent<Animator>();
		Debug.Log( "Player アニメーション取得完了？" );
	}

	private void _update()
	{
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

		if ( !m_bAuto )
		{
			if ( !m_bQuit )
			{
				if ( Input.GetKey( KeyCode.Escape ) )
				{
					requestPlayAuto();
					//
					m_bQuit = true;
				}
			}
		}
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
		_update();
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
		_debugLog( "AcPlayer # OnApplicationQuit()" );
	}

	void OnDestroy()
	{
		_debugLog( "AcPlayer # OnDestroy()" );
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
