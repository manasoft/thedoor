using UnityEngine;
using System.Collections;

// Dictionary
//using System.Collections.Generic;

/// <summary>
/// 動的に呼び出すオブジェクト用のスクリプトです
/// </summary>
public class AcHowtoplay : MonoBehaviour
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
		YES,
		NO,
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _OBJECT_CAMERA = 0;
	private const int _OBJECT_TITLE = 1;
	private const int _OBJECT_BUTTON_YES = 2;
	private const int _OBJECT_BUTTON_NO = 3;
	private const int _OBJECT_NUM = 4;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//private const int _CHENGER_TITLE_TIMEATTACK = 0;
	//private const int _CHENGER_TITLE_CHALLENGE = 1;
	//private const int _CHENGER_BUTTON_YES = 2;
	//private const int _CHENGER_BUTTON_NO = 3;
	//private const int _CHENGER_NUM = 4;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private static readonly string[] _objectTbl =
	{
		"Camera",
		"Title",
		//"DialogButtons/Yes",
		//"DialogButtons/No",
		"Yes",
		"No",
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
		ScDebug.debugLog( this.GetType().FullName + " # " + vString );
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
	private AiHowtoplayTrigger m_vTrigger;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private GameObject[] m_vGameObject;

	//private Animator m_vAnimator;

	//private AcTextureChanger m_vChanger;

	//private int m_vPhase;

	//private Thread m_vThread;

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
	public static AcHowtoplay Create( AcGameManager vManager, AiHowtoplayTrigger vTrigger )
	{
		GameObject _prefab = ( GameObject ) Resources.Load( "Prefabs/Howtoplay" );
		//
		//		GameObject _object = ( GameObject ) Instantiate( _prefab, Vector3.zero, Quaternion.identity );
		GameObject _object = ( GameObject ) Instantiate( _prefab, AcApp.GuiPosition, Quaternion.identity );
		//
		AcHowtoplay _class = _object.GetComponent<AcHowtoplay>();
		//
		_class._create( vManager, vTrigger );
		//
		return ( _class );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _create( AcGameManager vManager, AiHowtoplayTrigger vTrigger )
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
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _setRender()
	{
		var _tbl = new[]
		{
			new { _object = _OBJECT_TITLE,			_image = AcApp.IMAGE_TEX_L,		_index = ( AcApp.getGameMode() == AcApp.GAMEMODE_TIMEATTACK ) ? 1 : 2, },
			new { _object = _OBJECT_BUTTON_YES,		_image = AcApp.IMAGE_TEX_S,		_index = 4, },
			new { _object = _OBJECT_BUTTON_NO,		_image = AcApp.IMAGE_TEX_S,		_index = 5, },
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
			_setRender();
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void _awake()
	{
		_debugLog( "_awake" );

		////
		//m_vGameObject = new GameObject[ _OBJECT_NUM ];

		////
		//for ( int count = 0; count < _OBJECT_NUM; count++ )
		//{
		//	m_vGameObject[ count ] = this.transform.FindChild( _objectTbl[ count ] ).gameObject;
		//}
	}

	private void _start()
	{

		_debugLog( "_start" );

		//m_vChanger = new AcTextureChanger();
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/howtoplay_1", 8, 8, 4, new int[] { 0, 1, } );
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/howtoplay_1", 8, 8, 4, new int[] { 8, 9, } );
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/howtoplay_1", 8, 8, 4, new int[] { 16, 17, } );
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/howtoplay_1", 8, 8, 4, new int[] { 24, 25, } );

		////
		//m_vGameObject = new GameObject[ _OBJECT_NUM ];

		////
		//for ( int count = 0; count < _OBJECT_NUM; count++ )
		//{
		//	m_vGameObject[ count ] = this.transform.FindChild( _objectTbl[ count ] ).gameObject;
		//}

		//List<object> _list = new List<object>();
		////
		//_list.Add( new
		//{
		//	_object = _OBJECT_TITLE,
		//	_image = AcApp.IMAGE_TEX_L,
		//	_index = ( AcApp.getGameMode() == AcApp.GAMEMODE_TIMEATTACK ) ? 0 : 1,
		//} );
		//_list.Add( new
		//{
		//	_object = _OBJECT_BUTTON_YES,
		//	_image = AcApp.IMAGE_TEX_S,
		//	_index = 0,
		//} );
		//_list.Add( new
		//{
		//	_object = _OBJECT_BUTTON_NO,
		//	_image = AcApp.IMAGE_TEX_S,
		//	_index = 1,
		//} );


	}

	private void _update()
	{
		//		int _timer = Time.frameCount;

		////
		//switch ( AcApp.getGameMode() )
		//{
		//	case ( AcApp.GAMEMODE_TIMEATTACK ):
		//		m_vChanger.update( m_vGameObject[ _OBJECT_TITLE ].renderer, _timer, _CHENGER_TITLE_TIMEATTACK, 0 );
		//		break;
		//	//
		//	case ( AcApp.GAMEMODE_CHALLENGE ):
		//		m_vChanger.update( m_vGameObject[ _OBJECT_TITLE ].renderer, _timer, _CHENGER_TITLE_CHALLENGE, 0 );
		//		break;
		//}

		//m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_YES ].renderer, _timer, _CHENGER_BUTTON_YES, 0 );
		//m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_NO ].renderer, _timer, _CHENGER_BUTTON_NO, 0 );

		//
		if ( Input.GetMouseButtonDown( 0 ) )
		{
			Ray _ray = m_vGameObject[ _OBJECT_CAMERA ].camera.ScreenPointToRay( Input.mousePosition );
			RaycastHit _hit = new RaycastHit();
			//
			if ( Physics.Raycast( _ray, out _hit ) )
			{
				GameObject _object = _hit.collider.gameObject;
				//
				if ( ( _object.name ).CompareTo( _getObjectName( _OBJECT_BUTTON_YES ) ) == ( 0 ) )
				{
					//
					m_vTrigger.onTrigger( Trigger.YES );
				}
				if ( ( _object.name ).CompareTo( _getObjectName( _OBJECT_BUTTON_NO ) ) == ( 0 ) )
				{
					//
					m_vTrigger.onTrigger( Trigger.NO );
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
			//
			m_vTrigger.onTrigger( Trigger.NO );
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
	public interface AiHowtoplayTrigger
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="vTrigger"></param>
		void onTrigger( AcHowtoplay.Trigger vTrigger );
	}

	// ========================================================================== //
	// ========================================================================== //
}
