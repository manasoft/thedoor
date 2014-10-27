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
	//	private const int _OBJECT_OK = 1;
	private const int _OBJECT_NUM = 2;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _CHENGER_TITLE = 0;
	//	private const int _CHENGER_OK = 1;
	private const int _CHENGER_NUM = 1;

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
//		"Ok",
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
	private AiRankingTrigger m_vTrigger;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private GameObject[] m_vGameObject;

	//	private Animator m_vAnimator;

	private AcTextureChanger m_vChanger;
	private AcTextureChanger m_vChangerGui;

	//private int m_vPhase;

	//private Thread m_vThread;

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




	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void _awake()
	{
		Debug.Log( "AcHowtoplay # Awake" );
	}

	private void _start()
	{
		m_vChanger = new AcTextureChanger();
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/ranking_1", 8, 8, 4, new int[] { 0, 1, 2, 3, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/ranking_1", 8, 8, 4, new int[] { 8, 9, 10, 11, } );
		//
		m_vChangerGui = AcGuiBase.getTextureChanger();

		//
		m_vGameObject = new GameObject[ _OBJECT_NUM ];

		//
		for ( int count = 0; count < _OBJECT_NUM; count++ )
		{
			m_vGameObject[ count ] = this.transform.FindChild( _objectTbl[ count ] ).gameObject;
		}
	}

	private void _update()
	{
		int _timer = Time.frameCount;

		//
		m_vChanger.update( m_vGameObject[ _OBJECT_TITLE ].renderer, _timer, _CHENGER_TITLE, 0 );

		//		if ( Application.platform == RuntimePlatform.Android && Input.GetKey( KeyCode.Escape ) )
		if ( Input.GetKey( KeyCode.Escape ) )
		{
			//
			m_vTrigger.onTrigger( Trigger.OK );
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
		float _baseScale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );
		float _sizeScale = 1.0f;

		//
		float[] _times = AcSave.getTimes();
		//
		for ( int _count = 0; _count < _times.Length; _count++ )
		{
			//			AcGuiTime _guiTime = new AcGuiTime( _times[ _count ], 80, 200 + ( _count * 48 ), _baseScale, m_vChanger[ _CHENGER_GUI ] );
			AcGuiTime _guiTime = new AcGuiTime( _times[ _count ], 80, 200 + ( _count * ( AcGuiTime.getFrameH() + 2 ) ), m_vChangerGui );
			_guiTime.onGUI( _baseScale, _sizeScale );
		}

		//
		int[] _doors = AcSave.getDoors();
		//
		for ( int _count = 0; _count < _doors.Length; _count++ )
		{
			//			AcGuiDoor _guiDoor = new AcGuiDoor( _doors[ _count ], 480 - 80 - 88, 200 + ( _count * 48 ), _baseScale, m_vChanger[ _CHENGER_GUI ] );
			AcGuiDoor _guiDoor = new AcGuiDoor( _doors[ _count ], AcApp.SCREEN_W - AcGuiDoor.getFrameW() - 80.0f, 200 + ( _count * ( AcGuiDoor.getFrameH() + 2 ) ), m_vChangerGui );
			_guiDoor.onGUI( _baseScale, _sizeScale );
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	void OnApplicationQuit()
	{
		Debug.Log( "AcRanking >> OnApplicationQuit()" );
	}

	void OnDestroy()
	{
		Debug.Log( "AcRanking >> OnDestroy()" );
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
