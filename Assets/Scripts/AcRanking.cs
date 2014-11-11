using UnityEngine;
using System.Collections;

/// <summary>
/// 動的に呼び出すオブジェクト用のスクリプトです
/// </summary>
public class AcRanking : MonoBehaviour
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
		OK,
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _OBJECT_CAMERA = 0;
	private const int _OBJECT_TITLE = 1;
	private const int _OBJECT_BUTTON_OK = 2;
	private const int _OBJECT_NUM = 3;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//private const int _CHENGER_TITLE = 0;
	//private const int _CHENGER_BUTTON_OK = 1;
	//private const int _CHENGER_NUM = 2;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private static readonly string[] _objectTbl =
	{
		"Camera",
		"Title",
		"Ok",
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
	private AiRankingTrigger m_vTrigger;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private GameObject[] m_vGameObject;

	//	private Animator m_vAnimator;

	//private AcTextureChanger m_vChanger;
	//private AcTextureChanger m_vChangerGui;

	//private int m_vPhase;

	//private Thread m_vThread;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// カーソルを出すか？
	/// </summary>
	private bool m_bCursorActive;

	/// <summary>
	/// カーソルの位置＋大きさ
	/// </summary>
	private Rect m_vCursorRect;

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
	public static AcRanking Create( AcGameManager vManager, AiRankingTrigger vTrigger )
	{
		GameObject _prefab = ( GameObject ) Resources.Load( "Prefabs/Ranking" );
		//
		//		GameObject _object = ( GameObject ) Instantiate( _prefab, Vector3.zero, Quaternion.identity );
		GameObject _object = ( GameObject ) Instantiate( _prefab, AcApp.GuiPosition, Quaternion.identity );
		//
		AcRanking _class = _object.GetComponent<AcRanking>();
		//
		_class._create( vManager, vTrigger );
		//
		return ( _class );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _create( AcGameManager vManager, AiRankingTrigger vTrigger )
	{
		_debugLog( "_create" );
		//
		m_vManager = vManager;
		m_vTrigger = vTrigger;
		//
		m_bCursorActive = false;
		m_vCursorRect = new Rect();
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
			new { _object = _OBJECT_TITLE,			_image = AcApp.IMAGE_TEX_L,		_index = ( AcApp.getGameMode() == AcApp.GAMEMODE_TIMEATTACK ) ? 9 : 8, },
			new { _object = _OBJECT_BUTTON_OK,		_image = AcApp.IMAGE_TEX_S,		_index = 4, },
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

	public void setActive( bool bSw, bool bCursor )
	{
		this.gameObject.SetActive( bSw );
		//
		m_bCursorActive = false;
		//
		if ( bSw )
		{
			_setRender();
			//
			if ( bCursor )
			{
				m_bCursorActive = bCursor;
				//
				int _rank = 0;
				//
				switch ( AcApp.getGameMode() )
				{
					case ( AcApp.GAMEMODE_TIMEATTACK ):
						_rank = AcSave.getTimesRank();
						break;
					//
					case ( AcApp.GAMEMODE_CHALLENGE ):
						_rank = AcSave.getDoorsRank();
						break;
				}
				//
				StartCoroutine( _coroutine( _rank ) );
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private IEnumerator _coroutine( int vRank )
	{
		if ( vRank >= 0 )
		{
			float _w = 400.0f;
			float _h = 39.0f;
			float _x = ( AcApp.SCREEN_W - _w ) / 2;
			float _y = 71.0f + ( 55.0f * vRank );
			//
			m_vCursorRect.Set( _x, _y, _w, _h );
			//
			int _count = 0;
			//
			while ( _count < 60 )
			{
				//AcDebug.debugLog( "ランキングのる～う" );

				_count++;
				m_vCursorRect.y = _y + AcApp.SCREEN_H - ( _count * AcApp.SCREEN_H / 60 );
				//
				yield return null;
			}
			//
			//m_vCursor.y = 71.0f + ( 55.0f * vRank );
		}
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
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/ranking_1", 8, 8, 4, new int[] { 0, 1, 2, 3, } );
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/ranking_1", 8, 8, 4, new int[] { 0, 1, 2, 3, } );
		//m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/ranking_1", 8, 8, 4, new int[] { 8, 9, 10, 11, } );
		//
		//m_vChangerGui = AcGuiBase.getTextureChanger();
	}

	private void _update()
	{
		//int _timer = Time.frameCount;

		////
		//m_vChanger.update( m_vGameObject[ _OBJECT_TITLE ].renderer, _timer, _CHENGER_TITLE, 0 );
		//m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_OK ].renderer, _timer, _CHENGER_BUTTON_OK, 0 );

		if ( Input.GetMouseButtonDown( 0 ) )
		{
			Ray _ray = m_vGameObject[ _OBJECT_CAMERA ].camera.ScreenPointToRay( Input.mousePosition );
			RaycastHit _hit = new RaycastHit();
			//
			if ( Physics.Raycast( _ray, out _hit ) )
			{
				GameObject _object = _hit.collider.gameObject;
				//
				if ( ( _object.name ).CompareTo( _getObjectName( _OBJECT_BUTTON_OK ) ) == ( 0 ) )
				{
					m_vTrigger.onTrigger( Trigger.OK );
				}
			}
		}

		////		if ( Application.platform == RuntimePlatform.Android && Input.GetKey( KeyCode.Escape ) )
		//if ( Input.GetKey( KeyCode.Escape ) )
		//{
		//	//
		//}
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
		float _baseScale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );
		float _sizeScale = 1.0f;

		//float _margin_top = 180.0f;
		//float _margin_side = 60.0f;

		switch ( AcApp.getGameMode() )
		{
			case ( AcApp.GAMEMODE_TIMEATTACK ):
				//
				float[] _times = AcSave.getTimes();
				//
				for ( int _count = 0; _count < _times.Length; _count++ )
				{
					//AcGuiTime _guiTime = new AcGuiTime( _times[ _count ], 80, 200 + ( _count * 48 ), _baseScale, m_vChanger[ _CHENGER_GUI ] );
					//AcGuiTime _guiTime = new AcGuiTime( _times[ _count ], _margin_side, _margin_top + ( _count * ( AcGuiTime.getFrameH() + 2 ) ), m_vChangerGui );
					//AcGuiTime _guiTime = new AcGuiTime( _times[ _count ], ( AcApp.SCREEN_W - AcGuiTime.getFrameW() ) / 2, _margin_top + ( _count * ( AcGuiTime.getFrameH() + 2 ) ), m_vChangerGui );
					//AcGuiTime _guiTime = new AcGuiTime( _times[ _count ], ( AcApp.SCREEN_W - AcGuiTime.getFrameW() ) / 2, _margin_top + ( _count * ( AcGuiTime.getFrameH() + 2 ) ) );
					//AcGuiTime _guiTime = new AcGuiTime( _times[ _count ], 84.0f, 71.0f + ( 55.0f * _count ) );
					AcGuiTime _guiTime = new AcGuiTime(
						84.0f,
						71.0f + ( 55.0f * _count ),
						_times[ _count ]
					);
					_guiTime.onGUI( _baseScale, _sizeScale, true );
				}
				break;
			//
			case ( AcApp.GAMEMODE_CHALLENGE ):
				//
				int[] _doors = AcSave.getDoors();
				//
				for ( int _count = 0; _count < _doors.Length; _count++ )
				{
					//AcGuiDoor _guiDoor = new AcGuiDoor( _doors[ _count ], 480 - 80 - 88, 200 + ( _count * 48 ), _baseScale, m_vChanger[ _CHENGER_GUI ] );
					//AcGuiDoor _guiDoor = new AcGuiDoor( _doors[ _count ], AcApp.SCREEN_W - AcGuiDoor.getFrameW() - _margin_side, _margin_top + ( _count * ( AcGuiDoor.getFrameH() + 2 ) ), m_vChangerGui );
					//AcGuiDoor _guiDoor = new AcGuiDoor( _doors[ _count ], ( AcApp.SCREEN_W - AcGuiDoor.getFrameW() ) / 2, _margin_top + ( _count * ( AcGuiDoor.getFrameH() + 2 ) ), m_vChangerGui );
					//AcGuiDoor _guiDoor = new AcGuiDoor( _doors[ _count ], ( AcApp.SCREEN_W - AcGuiDoor.getFrameW() ) / 2, _margin_top + ( _count * ( AcGuiDoor.getFrameH() + 2 ) ) );
					//AcGuiDoor _guiDoor = new AcGuiDoor( _doors[ _count ], 84.0f + ( 26.0f * 4 ), 71.0f + ( 55.0f * _count ) );
					AcGuiDoor _guiDoor = new AcGuiDoor(
						84.0f + ( 26.0f * 4 ),
						71.0f + ( 55.0f * _count ),
						_doors[ _count ]
					);
					_guiDoor.onGUI( _baseScale, _sizeScale, true );
				}
				break;
		}

		if ( m_bCursorActive )
		{
			float _scale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );
			//
			Rect _rect = new Rect( m_vCursorRect );
			_rect.x *= _scale;
			_rect.y *= _scale;
			_rect.width *= _scale;
			_rect.height *= _scale;
			//
			AcApp.imageDraw( _rect, AcApp.IMAGE_TEX_M, 2 );
		}
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
	public interface AiRankingTrigger
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="vTrigger"></param>
		void onTrigger( AcRanking.Trigger vTrigger );
	}

	// ========================================================================== //
	// ========================================================================== //
}
