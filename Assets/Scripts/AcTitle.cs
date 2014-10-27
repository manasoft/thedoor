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
	/// トリガー（それぞれのボタンが押された時用です）
	/// </summary>
	public enum Trigger
	{
		TIMEATTACK,
		CHALLENGE,
		RANKING,
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _OBJECT_CAMERA = 0;
	private const int _OBJECT_TITLE = 1;
	private const int _OBJECT_BUTTON_START = 2;
	private const int _OBJECT_BUTTON_TIMEATTACK = 3;
	private const int _OBJECT_BUTTON_CHALLENGE = 4;
	private const int _OBJECT_BUTTON_RANKING = 5;
	private const int _OBJECT_NUM = 6;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _CHENGER_BACKGROUND = 0;
	private const int _CHENGER_BUTTON_START = 1;
	private const int _CHENGER_BUTTON_TIMEATTACK = 2;
	private const int _CHENGER_BUTTON_CHALLENGE = 3;
	private const int _CHENGER_BUTTON_RANKING = 4;
	private const int _CHENGER_NUM = 5;

	// ========================================================================== //
	// ========================================================================== //

	private const int _PHASE_INI = 0;
	private const int _PHASE_START = 1;
	private const int _PHASE_MENU = 2;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private string[] _objectTbl =
	{
		"Camera",
		"Title",
		"SelectButtons/Start",
		"SelectButtons/TimeAttack",
		"SelectButtons/Challenge",
		"SelectButtons/Ranking",
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 
	/// </summary>
	/// <param name="vObjectId"></param>
	/// <returns></returns>
	private string getObjectName( int vObjectId )
	{
		string[] _string = _objectTbl[ vObjectId ].Split( '/' );
		//
		return ( _string[ _string.Length - 1 ] );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

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

	private AcTextureChanger m_vChanger;

	private int m_vPhase;

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

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * プレハブからインスタンスするよ
	 */
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
		m_vManager = vManager;
		m_vTrigger = vTrigger;
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void _setPhase( int vPhase )
	{
		Debug.Log( "_setPhase >> " + vPhase );

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
			//
			case ( _PHASE_INI ):
				break;
			//
			case ( _PHASE_START ):
				//
				m_vGameObject[ _OBJECT_BUTTON_START ].SetActive( true );
				break;
			//
			case ( _PHASE_MENU ):
				//
				m_vGameObject[ _OBJECT_BUTTON_TIMEATTACK ].SetActive( true );
				m_vGameObject[ _OBJECT_BUTTON_CHALLENGE ].SetActive( true );
				m_vGameObject[ _OBJECT_BUTTON_RANKING ].SetActive( true );
				break;
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private IEnumerator _coroutineNext( int vFrame, int vPhase )
	{
		int _count = 0;
		//
		while ( _count++ < vFrame )
		{
			yield return null;
		}
		//
		Debug.Log( "コルーチンからフェイズ切り替え" );
		//
		_setPhase( vPhase );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void _updateIni( int vTimer )
	{
		////
		//AcRanking.onStart();

		////
		//if ( AcApp.isBoot() )
		//{
		//	_setPhase( _PHASE_MENU );
		//}
		//else
		//{
		//	AcApp.swBoot( true );
		//	//
		//	//			StartCoroutine( _coroutineNext( 60 * 4, _PHASE_START ) );
		//	//
		//	_setPhase( _PHASE_START );
		//}

		_setPhase( _PHASE_START );
	}

	private void _updateStart( int vTimer )
	{
		//		m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_START ].renderer, vFrameCount, _CHENGER_START, 0, 0 );
		m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_START ].renderer, vTimer, _CHENGER_BUTTON_START, 0 );

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
				if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_BUTTON_START ) ) == ( 0 ) )
				{
					m_vAnimator.SetBool( "bMove", true );
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

	private void _updateMenu( int vTimer )
	{
		//m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_TIMEATTACK ].renderer, vFrameCount, _CHENGER_TIMEATTACK, 0, 0 );
		//m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_CHALLENGE ].renderer, vFrameCount, _CHENGER_CHALLENGE, 0, 0 );
		//m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_RANKING ].renderer, vFrameCount, _CHENGER_RANKING, 0, 0 );
		m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_TIMEATTACK ].renderer, vTimer, _CHENGER_BUTTON_TIMEATTACK, 0 );
		m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_CHALLENGE ].renderer, vTimer, _CHENGER_BUTTON_CHALLENGE, 0 );
		m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_RANKING ].renderer, vTimer, _CHENGER_BUTTON_RANKING, 0 );

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
				if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_BUTTON_TIMEATTACK ) ) == ( 0 ) )
				{
					//
					m_vTrigger.onTrigger( Trigger.TIMEATTACK );
				}
				if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_BUTTON_CHALLENGE ) ) == ( 0 ) )
				{
					//
					m_vTrigger.onTrigger( Trigger.CHALLENGE );
				}
				if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_BUTTON_RANKING ) ) == ( 0 ) )
				{
					//
					m_vTrigger.onTrigger( Trigger.RANKING );
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
		switch ( m_vPhase )
		{
			//
			case ( _PHASE_START ):
				_setPhase( _PHASE_MENU );
				break;
			//
			case ( _PHASE_MENU ):
				_setPhase( _PHASE_START );
				break;
		}
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
		Debug.Log( "AcTitle # Awake" );
	}

	private void _start()
	{
		m_vChanger = new AcTextureChanger();
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 0, 1, 2, 3, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 8, 9, 10, 11, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 16, 17, 18, 19, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 24, 25, 26, 27, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 32, 33, 34, 35, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 40, 41, 42, 43, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 48, 49, 50, 51, } );
		//
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 4, 5, 6, 7, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4, new int[] { 12, 13, 14, 15, 20, 21, 22, 23, 28, 29, 30, 31, 36, 37, 38, 39, } );

		//
		m_vGameObject = new GameObject[ _OBJECT_NUM ];

		//
		for ( int count = 0; count < _OBJECT_NUM; count++ )
		{
			m_vGameObject[ count ] = this.transform.FindChild( _objectTbl[ count ] ).gameObject;
		}
		//
		m_vAnimator = transform.FindChild( "SelectButtons" ).GetComponent<Animator>();
		//
		_setPhase( _PHASE_INI );
	}

	private void _update()
	{
		int _timer = Time.frameCount;
		/*
		 * 共通のバックグラウンド書き換え
		 */
		//		m_vChanger.update( m_vGameObject[ _OBJECT_BACKGROUND ].renderer, frameCount, _CHENGER_BACKGROUND, 0, 0 );
		m_vChanger.update( m_vGameObject[ _OBJECT_TITLE ].renderer, _timer, _CHENGER_BACKGROUND, 0 );
		//
		switch ( m_vPhase )
		{
			//
			case ( _PHASE_INI ):
				_updateIni( _timer );
				break;
			//
			case ( _PHASE_START ):
				_updateStart( _timer );
				break;
			//
			case ( _PHASE_MENU ):
				_updateMenu( _timer );
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

		//this.gameObject.SetActive( false );
		////
		//m_vTrigger.hoge();
		////
		//m_vTrigger.onTrigger( Trigger.TIMEATTACK );
		//m_vTrigger.onTrigger( Trigger.CHALLENGE );
		//m_vTrigger.onTrigger( Trigger.RANKING );
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
		Debug.Log( "AcTitle >> OnApplicationQuit()" );
	}

	void OnDestroy()
	{
		Debug.Log( "AcTitle >> OnDestroy()" );
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
