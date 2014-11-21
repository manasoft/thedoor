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
	/// トリガー（イベント発生時のトリガーです）
	/// </summary>
	public enum Trigger
	{
		READY_GO,
		RESULT_END,		/// 通常終了
		//QUIT,		/// 強制終了（エスケープボタン？）
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 遷移管理
	/// </summary>
	private enum _Phase
	{
		_INI,
		_READY,
		_IDLE,
		_MOVE,
		_RESULT,
		//_FAILURE,
		//_SUCCESS,
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _OBJECT_CAMERA = 0;
	private const int _OBJECT_NUM = 1;

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
	/// プレイヤーの位置
	/// アニメーションで行うので使わない事になりました
	/// Vector3 は const に出来ないとエラーが出ます
	/// なんか static readonly を使えばいいらしい
	/// 
	/// アニメーションは中止したので復活！
	/// </summary>
	private const float _PLAYER_POS_X = 0.0f;
	private const float _PLAYER_POS_Y = 0.0f;
	private const float _PLAYER_POS_Z = 0.0f;
	private static readonly Vector3 _PLAYER_POS = new Vector3( _PLAYER_POS_X, _PLAYER_POS_Y, _PLAYER_POS_Z );

	/// <summary>
	/// 部屋の位置
	/// </summary>
	private const float _ROOM_POS_X = 0.0f;
	private const float _ROOM_POS_Y = 0.0f;
	private const float _ROOM_POS_Z = 0.0f;
	/// <summary>
	/// 部屋の大きさ
	/// </summary>
	private const float _ROOM_SCALE_X = 1.0f;
	private const float _ROOM_SCALE_Y = 1.0f;
	private const float _ROOM_SCALE_Z = 2.0f;
	/// <summary>
	/// 部屋の位置
	/// </summary>
	private static readonly Vector3 _ROOM_POS_A = new Vector3( _ROOM_POS_X, _ROOM_POS_Y, _ROOM_POS_Z );
	private static readonly Vector3 _ROOM_POS_B = new Vector3( _ROOM_POS_X, _ROOM_POS_Y, _ROOM_POS_Z + _ROOM_SCALE_Z );

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private static readonly string[] _objectTbl =
	{
		"Main Camera",
		//"Camera",
	};

	// ========================================================================== //
	// ========================================================================== //

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

	private GameObject[] m_vGameObject;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// プレイヤー（カメラ）の移動をアニメーションで動かしてます
	/// 
	/// 2014/10/29
	/// イベントが呼ばれないとか不具合が多いので使わない方針に変更します
	/// </summary>
	//private Animator m_vAnimator;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	///  プレイ中？
	/// </summary>
	private bool m_bPlay;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //


	///// <summary>
	///// オートプレイ状態？
	///// </summary>
	//private bool m_bAuto;
	///// <summary>
	///// オートプレイを開始してよ！（直接変更しないで、メソッドを使って変更すること！）
	///// </summary>
	//private bool m_bRequestPlayAuto;
	///// <summary>
	///// オートプレイを停止してよ！（直接変更しないで、メソッドを使って変更すること！）
	///// </summary>
	//private bool m_bRequestStopAuto;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private bool m_bQuit;

	/// <summary>
	/// ゲーム遷移を管理するフラグです
	/// </summary>
	private _Phase m_vPhase;

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

	/// <summary>
	/// 選択したドア番号
	/// </summary>
	private int m_vSelectDoor;

	/// <summary>
	/// ２つの部屋を交互に移動して表示するよ！
	/// </summary>
	private AcRoom[] m_vRoom;

	/// <summary>
	/// Gui の表示（time / door）
	/// </summary>
	private AcGuiGame m_vGuiGame;

	/// <summary>
	/// BGM のトラックを保持（stop 用）
	/// </summary>
	private string m_vSoundTrackBgm;


	///// <summary>
	///// 2014/11/21 追加
	///// </summary>
	//private AcFade m_vFade;

//	private AcHide m_vHide;


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
		//AcDebug.debugLog( vString );
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
		//if ( !m_bAuto )
		{
			AcApp.soundPlay( vEntryName );
		}
	}

	private void _playBgm( string vEntryName )
	{
		//if ( !m_bAuto )
		{
			m_vSoundTrackBgm = AcApp.soundPlay( vEntryName, 1.0f );
		}
	}

	private void _stopBgm()
	{
		if ( m_vSoundTrackBgm != null )
		{
			AcApp.soundStop( m_vSoundTrackBgm, 1.0f );
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
		//
		_setObject();
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _setObject()
	{
		m_vGameObject = new GameObject[ _OBJECT_NUM ];
		//
		for ( int count = 0; count < _OBJECT_NUM; count++ )
		{
			m_vGameObject[ count ] = this.transform.FindChild( _objectTbl[ count ] ).gameObject;
		}
	}


	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	private void _iniRoom()
	{
		m_vRoomIndex = 0;
		m_vRoomCount = 0;
		//
		m_vSelectDoor = _DOOR_NONE;
		//
		m_vRoom[ _ROOM_A ].transform.localPosition = _ROOM_POS_A;
		m_vRoom[ _ROOM_B ].transform.localPosition = _ROOM_POS_B;
		//
		_setRoom( m_vRoom[ _ROOM_A ], m_vRoomCount + 0 );
		_setRoom( m_vRoom[ _ROOM_B ], m_vRoomCount + 1 );
	}

	/// <summary>
	/// ゲーム開始
	/// </summary>
	public void play()
	{
		if ( !m_bPlay )
		{
			m_bPlay = true;
			//
			m_bClear = false;
			//
			m_vGuiGame.swReadyActive( true );
			/*
			 * 2014/11/21
			 * ゲーム画面を隠していたのが GUI だったのを AcHide のオブジェクトに変えたので
			 * ↑レディをオフった時に表示するタイミングに移動します
			 */
			//m_vGuiGame.swTimeActive( true );
			//m_vGuiGame.swDoorActive( true );
			//
			_iniRoom();
			//
			m_vPhase = _Phase._READY;
		}
	}

	public void stop()
	{
		if ( m_bPlay )
		{
			m_bPlay = false;
			//
			m_vGuiGame.swReadyActive( false );
			m_vGuiGame.swTimeActive( false );
			m_vGuiGame.swDoorActive( false );
			//
			m_vPhase = _Phase._RESULT;
		}
	}

	///// <summary>
	///// キャンセル
	///// </summary>
	//public void cancel()
	//{
	//	if ( m_bPlay )
	//	{
	//		m_bPlay = false;
	//	}
	//}

	///// <summary>
	///// オートプレイを開始してほしい時に呼んでくれ！
	///// </summary>
	//public void requestPlayAuto()
	//{
	//	if ( !m_bAuto )
	//	{
	//		m_bRequestPlayAuto = true;
	//		m_bRequestStopAuto = false;
	//	}
	//}

	///// <summary>
	///// オートプレイを停止してほしい時に呼んでくれ！
	///// </summary>
	//public void requestStopAuto()
	//{
	//	if ( m_bAuto )
	//	{
	//		m_bRequestPlayAuto = false;
	//		m_bRequestStopAuto = true;
	//	}
	//}

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

	///// <summary>
	///// 強制終了のチェック
	///// </summary>
	///// <returns></returns>
	//private IEnumerator _coroutineQuit()
	//{
	//	while ( true )
	//	{
	//		/*
	//		 * ゲームの終了とか
	//		 * http://motogeneralpurpose.blogspot.jp/2013/04/unity.html
	//		 * 上と同じとこですけどまとまっていたのでメモしときます
	//		 * http://motogeneralpurpose.blogspot.jp/search/label/Unity
	//		 */
	//		//		if ( Application.platform == RuntimePlatform.Android && Input.GetKey( KeyCode.Escape ) )
	//		if ( Input.GetKey( KeyCode.Escape ) )
	//		{
	//			if ( !m_bAuto )
	//			{
	//				requestPlayAuto();
	//				break;
	//			}
	//		}
	//		//
	//		yield return null;
	//	}
	//}

	/// <summary>
	/// コルーチン処理（移動処理）
	/// </summary>
	/// <returns></returns>
	private IEnumerator _coroutineMove()
	{
		_Move _move;
		Vector3 _vector = new Vector3();
		/*
		 * 真ん中しか通らなく仕様にするそうです
		 */
		int _selectDoor = _DOOR_C;
		//
		_move = new _Move( _rootMoveNearTbl[ _selectDoor ] );
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
			_move = new _Move( _rootMovePassTbl[ _selectDoor ] );
		}
		else
		{
			_move = new _Move( _rootMoveBackTbl[ _selectDoor ] );
			//
			m_vGuiGame.stopTime();
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

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 部屋を作るよ！
	/// </summary>
	/// <param name="vRoom"></param>
	/// <param name="vRoomCount"></param>
	private void _setRoom( AcRoom vRoom, int vRoomCount )
	{
		//if ( m_bAuto )
		//{
		//	vRoomCount = 0;
		//}
		//
		vRoom.setRoom( vRoomCount );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// フェイズ：初期設定
	/// </summary>
	private void _phaseIni()
	{
		///*
		// * 初期化しておくもの
		// */
		//m_bQuit = false;
		//m_bClear = false;
		//m_vRoomCount = 0;

		////
		//if ( m_bAuto )
		//{
		//	m_vGuiGame.swReadyActive( false );
		//	m_vGuiGame.swTimeActive( false );
		//	m_vGuiGame.swDoorActive( false );
		//	//
		//	m_vPhase = _Phase._IDLE;
		//}
		//else
		//{
		//	m_vGuiGame.swReadyActive( true );
		//	m_vGuiGame.swTimeActive( true );
		//	m_vGuiGame.swDoorActive( true );
		//	//
		//	m_vPhase = _Phase._READY;
		//}

		/*
		 * ゲーム開始前/終了後はココにいるよ！
		 * 外部から ready に遷移させられるのを待っている
		 */
	}

	/// <summary>
	/// カウントダウン中
	/// </summary>
	private void _phaseReady()
	{
		// コルーチン待ちなので何もしません
	}

	/// <summary>
	/// 入力待機状態
	/// </summary>
	private void _phaseIdle()
	{
		//if ( m_bAuto )
		//{
		//	if ( m_bRequestStopAuto )
		//	{
		//		/*
		//		 * ゲームの開始時にコッチに来るハズ！
		//		 */
		//		m_bRequestStopAuto = false;
		//		m_bAuto = false;
		//		//
		//		m_vPhase = _Phase._INI;
		//	}
		//	/*
		//	 * 2014/11/17
		//	 * オートプレイを止めるそうなのでコメントアウトするよ
		//	 */
		//	//else
		//	//{
		//	//	/*
		//	//	 * オートデモ中は自動的に正解を選ぶよ！
		//	//	 */
		//	//	m_vSelectDoor = m_vRoom[ m_vRoomIndex ].getUnlock();
		//	//	//
		//	//	//m_vAnimator.SetBool( "bMove", true );
		//	//	//m_vAnimator.SetInteger( "vRoot", m_vSelectDoor );
		//	//	//
		//	//	m_vPhase = _Phase._MOVE;

		//	//	//
		//	//	StartCoroutine( _coroutineMove() );
		//	//}
		//}
		//else
		{
			//if ( m_bRequestPlayAuto )
			//{
			//	/*
			//	 * 強制終了処理でコッチにくるよ！
			//	 */
			//	m_bRequestPlayAuto = false;
			//	m_bAuto = true;
			//	//
			//	//m_vPhase = _PHASE_INI;
			//	_toFailure();


			//	/*
			//	 * 
			//	 */
			//	//m_vTrigger.onTrigger( Trigger.QUIT );
			//}
			//else
			{
				if ( m_bClear )
				{
					_toSuccess();
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
						Ray _ray = m_vGameObject[ _OBJECT_CAMERA ].camera.ScreenPointToRay( Input.mousePosition );
						//Ray _ray = Camera.main.ScreenPointToRay( Input.mousePosition );
						RaycastHit _hit = new RaycastHit();
						//
						if ( Physics.Raycast( _ray, out _hit ) )
						{
							int _door_id = _DOOR_NONE;
							//
							GameObject _object = _hit.collider.gameObject;
							_debugLog( _object.name );
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
								m_vPhase = _Phase._MOVE;

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


	private void _phaseResult()
	{
	}



	//private void _phaseSuccess()
	//{
	//	//		Debug.Log( "Success" );
	//}

	//private void _phaseFailure()
	//{
	//	//		Debug.Log( "Failure" );
	//}

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
			m_vGuiGame.addDoor();
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
		m_vPhase = _Phase._IDLE;
	}

	private void _toSuccess()
	{
		this.transform.localPosition = _PLAYER_POS;

		// 結果表示
		m_vGuiGame.swResultActive( true, true );

		// ランキングへ登録する
		switch ( AcApp.getGameMode() )
		{
			case ( AcApp.GAMEMODE_TIMEATTACK ):
				//
				AcSave.addTime( m_vGuiGame.getTime() );
				break;
			//
			case ( AcApp.GAMEMODE_CHALLENGE ):
				//
				AcSave.addDoor( m_vGuiGame.getDoor() );
				break;
		}

		// 成功画面へ
		//m_vPhase = _Phase._SUCCESS;
		stop();
	}

	private void _toFailure()
	{
		this.transform.localPosition = _PLAYER_POS;

		// 結果表示
		m_vGuiGame.swResultActive( true, false );

		// ランキングへ登録する
		switch ( AcApp.getGameMode() )
		{
			case ( AcApp.GAMEMODE_TIMEATTACK ):
				//
				AcSave.missTime();
				break;
			//
			case ( AcApp.GAMEMODE_CHALLENGE ):
				//
				AcSave.missDoor();
				break;
		}
		// 失敗画面へ
		//m_vPhase = _Phase._FAILURE;
		stop();
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
		//_debugLog( "onJudge" );


		//m_vAnimator.SetBool( "bMove", false );


		//if ( m_vRoom[ m_vRoomIndex ].getUnlock() == m_vSelectDoor )
		//{
		//	switch ( AcApp.getGameMode() )
		//	{
		//		//
		//		case ( AcApp.GAMEMODE_TIMEATTACK ):
		//			//
		//			m_vGuiDoorCount--;
		//			//
		//			if ( m_vGuiDoorCount == 0 )
		//			{
		//				m_bClear = true;
		//			}
		//			break;
		//		//
		//		case ( AcApp.GAMEMODE_CHALLENGE ):
		//			//
		//			m_vGuiDoorCount++;
		//			break;
		//	}
		//	//
		//	m_vGuiGame.setDoor( m_vGuiDoorCount );

		//	/*
		//	 * 実験
		//	 */
		//	_playSe( "se_1" );
		//}
		//else
		//{
		//	/*
		//	 * 失敗時
		//	 */
		//	m_vAnimator.SetBool( "bMiss", true );

		//	/*
		//	 * 実験
		//	 */
		//	_playSe( "se_2" );
		//}

	}

	public void onMoveStart()
	{
		_debugLog( "onMoveStart_1" );

		//m_vAnimator.SetBool( "bMove", false );
	}

	public void onMoveEnd()
	{
		_debugLog( "onMoveEnd" );

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
		_debugLog( "onMissStart" );

		//m_vAnimator.SetBool( "bMiss", false );
	}

	public void onMissEnd()
	{
		_debugLog( "onMissEnd" );

		//m_vAnimator.SetBool( "bMove", false );
		//m_vAnimator.SetBool( "bMiss", false );

		bool _debug = false;
		if ( _debug )
		{
			m_vPhase = _Phase._IDLE;
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
		m_vPhase = _Phase._INI;

		//
		m_bPlay = false;
		m_bQuit = false;

		// 初期時は強制的にオートにする
		//m_bAuto = true;

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
		m_vGuiGame = AcGuiGame.Create( this, new _GuiGameTrigger( this ) );

		//_debugLog( "Player アニメーション取得準備" );
		//m_vAnimator = transform.GetComponent<Animator>();
		//_debugLog( "Player アニメーション取得完了？" );

		//{
		//	/*
		//	 * 実験中
		//	 */
		//	//m_vFade = AcFade.Create();
		//	m_vHide = AcHide.Create( this, new _HideTrigger( this ) );
		//}
	}

	private void _update()
	{
		switch ( m_vPhase )
		{
			case ( _Phase._INI ):
				_phaseIni();
				break;
			//
			case ( _Phase._READY ):
				_phaseReady();
				break;
			//
			case ( _Phase._IDLE ):
				_phaseIdle();
				break;
			//
			case ( _Phase._MOVE ):
				_phaseMove();
				break;
			//
			case ( _Phase._RESULT ):
				_phaseResult();
				break;
			////
			//case ( _Phase._SUCCESS ):
			//	_phaseSuccess();
			//	break;
			////
			//case ( _Phase._FAILURE ):
			//	_phaseFailure();
			//	break;
		}

		//if ( !m_bAuto )
		//{
		//	if ( !m_bQuit )
		//	{
		//		if ( Input.GetKey( KeyCode.Escape ) )
		//		{
		//			requestPlayAuto();
		//			//
		//			m_bQuit = true;
		//		}
		//	}
		//}
	}

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// ※注意
	/// ・Instantiate 直後に別スレッドで呼ばれちゃうみたいっす
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

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// AcGuiGame からのトリガー処理です
	/// </summary>
	private class _GuiGameTrigger : AcGuiGame.AiGuiGameTrigger
	{
		private AcPlayer m_vPlayer;

		public _GuiGameTrigger( AcPlayer vPlayer )
		{
			m_vPlayer = vPlayer;
		}

		public void onTrigger( AcGuiGame.Trigger vTrigger )
		{
			switch ( vTrigger )
			{
				case ( AcGuiGame.Trigger.TIME_END ):
					//
					m_vPlayer.m_bClear = true;
					break;
				//
				case ( AcGuiGame.Trigger.DOOR_END ):
					//
					m_vPlayer.m_bClear = true;
					break;
				//
				case ( AcGuiGame.Trigger.READY_321 ):
					//
					m_vPlayer._playSe( "se_cd_1" );
					break;
				//
				case ( AcGuiGame.Trigger.READY_GO ):
					//
					m_vPlayer._playSe( "se_cd_2" );
					m_vPlayer._playBgm( "bgm_1" );
					//
					m_vPlayer.m_vGuiGame.swReadyActive( false );
					/*
					 * 2014/11/21
					 * コッチに移動します
					 */
					m_vPlayer.m_vGuiGame.swTimeActive( true );
					m_vPlayer.m_vGuiGame.swDoorActive( true );
					//
					m_vPlayer.m_vGuiGame.startTime();
					m_vPlayer.m_vPhase = _Phase._IDLE;
					//
					m_vPlayer.m_vTrigger.onTrigger( Trigger.READY_GO );
					break;
				//
				case ( AcGuiGame.Trigger.RESULT_END ):
					//
					m_vPlayer.m_vGuiGame.swResultActive( false, false );
					//
					m_vPlayer._stopBgm();
					//
					//m_vPlayer.m_bAuto = true;
					//m_vPlayer.m_vPhase = _Phase._INI;
					//
					m_vPlayer.m_vTrigger.onTrigger( Trigger.RESULT_END );
					break;
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //


	/// <summary>
	/// 
	/// </summary>
	private class _HideTrigger : AcFade.AiFadeTrigger
	{
		private AcPlayer m_vPlayer;

		public _HideTrigger( AcPlayer vPlayer )
		{
			m_vPlayer = vPlayer;
		}

		public void onTrigger( AcFade.Trigger vTrigger )
		{
		}
	}

	// ========================================================================== //
	// ========================================================================== //
}
