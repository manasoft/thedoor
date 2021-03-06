using UnityEngine;
using System.Collections;

/// <summary>
/// 動的に呼び出すオブジェクト用のスクリプトです
/// </summary>
public class AcTitle : MonoBehaviour
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
		//TIMEATTACK,
		//CHALLENGE,
		GAME,
		RANKING,
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 遷移管理
	/// </summary>
	private enum _Phase
	{
		//_INI,
		_START,
		_MENU,
		_RANKING,
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _OBJECT_CAMERA = 0;
	private const int _OBJECT_TITLE = 1;
	private const int _OBJECT_BUTTON_START = 2;
	private const int _OBJECT_BUTTON_TIMEATTACK = 3;
	private const int _OBJECT_BUTTON_CHALLENGE = 4;
	private const int _OBJECT_BUTTON_RANKING = 5;
	private const int _OBJECT_BUTTON_OK = 6;
	private const int _OBJECT_NUM = 7;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//private const int _CHENGER_BACKGROUND = 0;
	//private const int _CHENGER_BUTTON_START = 1;
	//private const int _CHENGER_BUTTON_TIMEATTACK = 2;
	//private const int _CHENGER_BUTTON_CHALLENGE = 3;
	//private const int _CHENGER_BUTTON_RANKING = 4;
	//private const int _CHENGER_NUM = 5;

	// ========================================================================== //
	// ========================================================================== //

	//private const int _PHASE_INI = 0;
	//private const int _PHASE_START = 1;
	//private const int _PHASE_MENU = 2;


	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private static readonly string[] _objectTbl =
	{
		"Camera",
		"Title",
		"SelectButtons/Start",
		"SelectButtons/TimeAttack",
		"SelectButtons/Challenge",
		"SelectButtons/Ranking",
		"SelectButtons/Ok",
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 
	/// </summary>
	/// <param name="vObjectId"></param>
	/// <returns></returns>
	private string _getObjectName( int vObjectId )
	{
		string[] _string = _objectTbl[ vObjectId ].Split( '/' );
		//
		return ( _string[ _string.Length - 1 ] );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 
	/// </summary>
	/// <param name="vString"></param>
	private void _debugLog( string vString )
	{
		AcDebug.debugLog( this.GetType().FullName + " # " + vString );
	}

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
	private AiTitleTrigger m_vTrigger;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private GameObject[] m_vGameObject;

	private Animator m_vAnimator;

	//	private AcTextureChanger m_vChanger;

	private _Phase m_vPhase;
	private _Phase m_vPhaseOnNext;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// プレハブからインスタンスするよ
	/// </summary>
	/// <param name="vManager"></param>
	/// <param name="vTrigger"></param>
	/// <returns></returns>
	public static AcTitle Create( AcGameManager vManager, AiTitleTrigger vTrigger )
	{
		GameObject _prefab = ( GameObject ) Resources.Load( "Prefabs/Title" );
		//
		//		GameObject _object = ( GameObject ) Instantiate( _prefab, Vector3.zero, Quaternion.identity );
		GameObject _object = ( GameObject ) Instantiate( _prefab, AcApp.GuiPosition, Quaternion.identity );
		//
		AcTitle _class = _object.GetComponent<AcTitle>();
		//
		_class._create( vManager, vTrigger );
		//
		return ( _class );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _create( AcGameManager vManager, AiTitleTrigger vTrigger )
	{
		_debugLog( "_create" );
		//
		m_vManager = vManager;
		m_vTrigger = vTrigger;
		//
		_setObject();
		_setRender();
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
		//
		m_vAnimator = transform.FindChild( "SelectButtons" ).GetComponent<Animator>();
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _setRender()
	{
		var _tbl = new[]
		{
			new { _object = _OBJECT_TITLE,				_image = AcApp.IMAGE_TEX_L,		_index = 0, },
			new { _object = _OBJECT_BUTTON_START,		_image = AcApp.IMAGE_TEX_S,		_index = 0, },
			new { _object = _OBJECT_BUTTON_TIMEATTACK,	_image = AcApp.IMAGE_TEX_S,		_index = 1, },
			new { _object = _OBJECT_BUTTON_CHALLENGE,	_image = AcApp.IMAGE_TEX_S,		_index = 2, },
			new { _object = _OBJECT_BUTTON_RANKING,		_image = AcApp.IMAGE_TEX_S,		_index = 3, },
			new { _object = _OBJECT_BUTTON_OK,			_image = AcApp.IMAGE_TEX_S,		_index = 5, },
		};
		//
		foreach ( var _var in _tbl )
		{
			AcApp.imageRender( m_vGameObject[ _var._object ].renderer, _var._image, _var._index );
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public void setActive( bool bSw )
	{
		this.gameObject.SetActive( bSw );
		//
		if ( bSw )
		{
			//_setRender();
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void _setPhase( _Phase vPhase )
	{
		//_debugLog( "_setPhase >> " + vPhase );

		m_vPhase = vPhase;
		/*
		 * 初期化として全てのオブジェクトを消すよ！
		 */
		foreach ( GameObject _object in m_vGameObject )
		{
			_object.SetActive( false );
		}
		/*
		 * カメラと背景は常に表示するよ！
		 */
		m_vGameObject[ _OBJECT_CAMERA ].SetActive( true );
		m_vGameObject[ _OBJECT_TITLE ].SetActive( true );
		/*
		 * 必要なオブジェクトを表示するよ！
		 */
		switch ( vPhase )
		{
			////
			//case ( _PHASE_INI ):
			//	break;
			//
			case ( _Phase._START ):
				//
				m_vGameObject[ _OBJECT_BUTTON_START ].SetActive( true );
				break;
			//
			case ( _Phase._MENU ):
				//
				m_vGameObject[ _OBJECT_BUTTON_TIMEATTACK ].SetActive( true );
				m_vGameObject[ _OBJECT_BUTTON_CHALLENGE ].SetActive( true );
				m_vGameObject[ _OBJECT_BUTTON_RANKING ].SetActive( true );
				break;
			//
			case ( _Phase._RANKING ):
				//
				m_vGameObject[ _OBJECT_BUTTON_TIMEATTACK ].SetActive( true );
				m_vGameObject[ _OBJECT_BUTTON_CHALLENGE ].SetActive( true );
				m_vGameObject[ _OBJECT_BUTTON_OK ].SetActive( true );
				break;
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private IEnumerator _coroutineNext( int vFrame, _Phase vPhase )
	{
		int _count = 0;
		//
		while ( _count++ < vFrame )
		{
			yield return null;
		}
		//
		//_debugLog( "コルーチンからフェイズ切り替え" );
		//
		_setPhase( vPhase );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	//private void _updateIni( int vTimer )
	//{
	//	////
	//	//AcRanking.onStart();

	//	////
	//	//if ( AcApp.isBoot() )
	//	//{
	//	//	_setPhase( _PHASE_MENU );
	//	//}
	//	//else
	//	//{
	//	//	AcApp.swBoot( true );
	//	//	//
	//	//	//			StartCoroutine( _coroutineNext( 60 * 4, _PHASE_START ) );
	//	//	//
	//	//	_setPhase( _PHASE_START );
	//	//}

	//	_setPhase( _Phase._START );
	//}

	private void _updateStart()
	{
		//		m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_START ].renderer, vFrameCount, _CHENGER_START, 0, 0 );
		//		m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_START ].renderer, vTimer, _CHENGER_BUTTON_START, 0 );

		if ( Input.GetMouseButtonDown( 0 ) )
		{
			//			Ray _ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			/*
			 * カメラへのキャスト
			 * http://kan-kikuchi.hatenablog.com/entry/2013/11/19/165526
			 */
			//			Ray _ray = ((Camera) m_vGameObject[ _OBJECT_CAMERA ].camera).ScreenPointToRay( Input.mousePosition );
			Ray _ray = m_vGameObject[ _OBJECT_CAMERA ].camera.ScreenPointToRay( Input.mousePosition );

			RaycastHit _hit = new RaycastHit();
			//
			if ( Physics.Raycast( _ray, out _hit ) )
			{
				GameObject _object = _hit.collider.gameObject;
				//
				if ( ( _object.name ).CompareTo( _getObjectName( _OBJECT_BUTTON_START ) ) == ( 0 ) )
				{
					m_vAnimator.SetBool( "bMove", true );
					m_vPhaseOnNext = _Phase._MENU;
				}
			}
		}

		/*
		 * ゲームの終了とか
		 * http://motogeneralpurpose.blogspot.jp/2013/04/unity.html
		 * 上と同じとこですけどまとまっていたのでメモしときます
		 * http://motogeneralpurpose.blogspot.jp/search/label/Unity
		 */
		if ( Application.platform == RuntimePlatform.Android && Input.GetKey( KeyCode.Escape ) )
		{
			Application.Quit();
		}
	}

	private void _updateMenu()
	{
		//m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_TIMEATTACK ].renderer, vFrameCount, _CHENGER_TIMEATTACK, 0, 0 );
		//m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_CHALLENGE ].renderer, vFrameCount, _CHENGER_CHALLENGE, 0, 0 );
		//m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_RANKING ].renderer, vFrameCount, _CHENGER_RANKING, 0, 0 );
		//m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_TIMEATTACK ].renderer, vTimer, _CHENGER_BUTTON_TIMEATTACK, 0 );
		//m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_CHALLENGE ].renderer, vTimer, _CHENGER_BUTTON_CHALLENGE, 0 );
		//m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_RANKING ].renderer, vTimer, _CHENGER_BUTTON_RANKING, 0 );

		if ( Input.GetMouseButtonDown( 0 ) )
		{
			//			Ray _ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			Ray _ray = m_vGameObject[ _OBJECT_CAMERA ].camera.ScreenPointToRay( Input.mousePosition );
			RaycastHit _hit = new RaycastHit();
			//
			if ( Physics.Raycast( _ray, out _hit ) )
			{
				GameObject _object = _hit.collider.gameObject;
				//
				if ( ( _object.name ).CompareTo( _getObjectName( _OBJECT_BUTTON_TIMEATTACK ) ) == ( 0 ) )
				{
					AcApp.setGameMode( AcApp.GAMEMODE_TIMEATTACK );
					//
					//m_vTrigger.onTrigger( Trigger.TIMEATTACK );
					m_vTrigger.onTrigger( Trigger.GAME );
				}
				if ( ( _object.name ).CompareTo( _getObjectName( _OBJECT_BUTTON_CHALLENGE ) ) == ( 0 ) )
				{
					AcApp.setGameMode( AcApp.GAMEMODE_CHALLENGE );
					//
					//m_vTrigger.onTrigger( Trigger.CHALLENGE );
					m_vTrigger.onTrigger( Trigger.GAME );
				}
				if ( ( _object.name ).CompareTo( _getObjectName( _OBJECT_BUTTON_RANKING ) ) == ( 0 ) )
				{
					m_vAnimator.SetBool( "bMove", true );
					m_vPhaseOnNext = _Phase._RANKING;

					//
					//m_vTrigger.onTrigger( Trigger.RANKING );
				}
			}
		}

		/*
		 * ゲームの終了とか
		 * http://motogeneralpurpose.blogspot.jp/2013/04/unity.html
		 * 上と同じとこですけどまとまっていたのでメモしときます
		 * http://motogeneralpurpose.blogspot.jp/search/label/Unity
		 */
		//		if ( Application.platform == RuntimePlatform.Android && Input.GetKey( KeyCode.Escape ) )
		if ( Input.GetKey( KeyCode.Escape ) )
		{
			m_vAnimator.SetBool( "bMove", true );
			m_vPhaseOnNext = _Phase._START;
		}
	}

	private void _updateRanking()
	{
		if ( Input.GetMouseButtonDown( 0 ) )
		{
			Ray _ray = m_vGameObject[ _OBJECT_CAMERA ].camera.ScreenPointToRay( Input.mousePosition );
			RaycastHit _hit = new RaycastHit();
			//
			if ( Physics.Raycast( _ray, out _hit ) )
			{
				GameObject _object = _hit.collider.gameObject;
				//
				if ( ( _object.name ).CompareTo( _getObjectName( _OBJECT_BUTTON_TIMEATTACK ) ) == ( 0 ) )
				{
					AcApp.setGameMode( AcApp.GAMEMODE_TIMEATTACK );
					//
					m_vTrigger.onTrigger( Trigger.RANKING );
				}
				if ( ( _object.name ).CompareTo( _getObjectName( _OBJECT_BUTTON_CHALLENGE ) ) == ( 0 ) )
				{
					AcApp.setGameMode( AcApp.GAMEMODE_CHALLENGE );
					//
					m_vTrigger.onTrigger( Trigger.RANKING );
				}
				if ( ( _object.name ).CompareTo( _getObjectName( _OBJECT_BUTTON_OK ) ) == ( 0 ) )
				{
					m_vAnimator.SetBool( "bMove", true );
					m_vPhaseOnNext = _Phase._MENU;
				}
			}
		}

		/*
		 * ゲームの終了とか
		 * http://motogeneralpurpose.blogspot.jp/2013/04/unity.html
		 * 上と同じとこですけどまとまっていたのでメモしときます
		 * http://motogeneralpurpose.blogspot.jp/search/label/Unity
		 */
		//		if ( Application.platform == RuntimePlatform.Android && Input.GetKey( KeyCode.Escape ) )
		if ( Input.GetKey( KeyCode.Escape ) )
		{
			m_vAnimator.SetBool( "bMove", true );
			m_vPhaseOnNext = _Phase._MENU;
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public void onSelectButtonsBegin()
	{
	}

	public void onSelectButtonsTurn()
	{
		m_vAnimator.SetBool( "bMove", false );
		//
		_setPhase( m_vPhaseOnNext );

		//switch ( m_vPhase )
		//{
		//	//
		//	case ( _Phase._START ):
		//		_setPhase( _Phase._MENU );
		//		break;
		//	//
		//	case ( _Phase._MENU ):
		//		_setPhase( _Phase._START );
		//		_setPhase( _Phase._RANKING );
		//		break;
		//	//
		//	case ( _Phase._RANKING ):
		//		_setPhase( _Phase._MENU );
		//		break;
		//}
	}

	public void onSelectButtonsEnd()
	{
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void _awake()
	{
		_debugLog( "_awake" );
	}

	private void _start()
	{
		_debugLog( "_start" );

		//m_vChanger = new AcTextureChanger();
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 0, 1, 2, 3, } );
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 8, 9, 10, 11, } );
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 16, 17, 18, 19, } );
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 24, 25, 26, 27, } );
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 32, 33, 34, 35, } );
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 40, 41, 42, 43, } );
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 48, 49, 50, 51, } );
		////
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 4, 5, 6, 7, } );
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 12, 13, 14, 15, 20, 21, 22, 23, 28, 29, 30, 31, 36, 37, 38, 39, } );

		////
		//m_vGameObject = new GameObject[ _OBJECT_NUM ];

		////
		//for ( int count = 0; count < _OBJECT_NUM; count++ )
		//{
		//	m_vGameObject[ count ] = this.transform.FindChild( _objectTbl[ count ] ).gameObject;
		//}
		////
		//m_vAnimator = transform.FindChild( "SelectButtons" ).GetComponent<Animator>();


		//var _tbl = new[]
		//{
		//	new { _object = _OBJECT_TITLE,				_image = AcApp.IMAGE_TEX_L,		_index = 0, },
		//	new { _object = _OBJECT_BUTTON_START,		_image = AcApp.IMAGE_TEX_S,		_index = 0, },
		//	new { _object = _OBJECT_BUTTON_TIMEATTACK,	_image = AcApp.IMAGE_TEX_S,		_index = 1, },
		//	new { _object = _OBJECT_BUTTON_CHALLENGE,	_image = AcApp.IMAGE_TEX_S,		_index = 2, },
		//	new { _object = _OBJECT_BUTTON_RANKING,		_image = AcApp.IMAGE_TEX_S,		_index = 3, },
		//};
		////
		//foreach ( var _var in _tbl )
		//{
		//	AcApp.imageRender( m_vGameObject[ _var._object ].renderer, _var._image, _var._index );
		//}

		//
		_setPhase( _Phase._START );
	}

	private void _update()
	{
		//int _timer = Time.frameCount;
		/*
		 * 共通のバックグラウンド書き換え
		 */
		//		m_vChanger.update( m_vGameObject[ _OBJECT_BACKGROUND ].renderer, frameCount, _CHENGER_BACKGROUND, 0, 0 );
		//		m_vChanger.update( m_vGameObject[ _OBJECT_TITLE ].renderer, _timer, _CHENGER_BACKGROUND, 0 );
		//
		switch ( m_vPhase )
		{
			////
			//case ( _Phase._INI ):
			//	_updateIni( _timer );
			//	break;
			//
			case ( _Phase._START ):
				_updateStart();
				break;
			//
			case ( _Phase._MENU ):
				_updateMenu();
				break;
			//
			case ( _Phase._RANKING ):
				_updateRanking();
				break;
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

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

	void OnApplicationQuit()
	{
		_debugLog( "OnApplicationQuit" );
	}

	void OnDestroy()
	{
		_debugLog( "OnDestroy" );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 
	/// </summary>
	public interface AiTitleTrigger
	{
		/*
		 * インターフェースのメソッドに public とかつけらんないっぽいっす
		 * でも実装したクラスは public 付けないとダメらしい
		 * わけわからんな！
		 */

		/// <summary>
		/// 
		/// </summary>
		/// <param name="vTrigger"></param>
		void onTrigger( AcTitle.Trigger vTrigger );
	}

	// ========================================================================== //
	// ========================================================================== //
}
