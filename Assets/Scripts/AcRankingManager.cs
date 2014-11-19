using UnityEngine;
using System.Collections;

public class AcRankingManager : MonoBehaviour
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

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _OBJECT_BACKGROUND = 0;
	private const int _OBJECT_NUM = 1;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//private const int _CHENGER_RANKING = 0;
	//private const int _CHENGER_GUI = 1;
	//private const int _CHENGER_NUM = 2;

	private const int _CHENGER_BACKGROUND = 0;
	private const int _CHENGER_NUM = 1;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private string[] _objectTbl =
	{
		"SceneBase2D/Background",
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

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private GameObject[] m_vGameObject;

	private AcTextureChanger m_vChanger;
	private AcTextureChanger m_vChangerGui;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// Use this for initialization
	void Start()
	{
		////
		//m_vChanger = new AcTextureChanger( new AcTextureChanger.Data(
		//	//
		//	"Images" + AcUtil.getLanguageSuffix() + "/ranking_1", 8, 8, 4,
		//	new int[] { 0, 1, 2, 3, },
		//	new int[] { 8, 9, 10, 11, }
		//) );
		//
		m_vChanger = new AcTextureChanger();
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/ranking_1", 8, 8, 4, new int[] { 0, 1, 2, 3, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/ranking_1", 8, 8, 4, new int[] { 8, 9, 10, 11, } );
		//
		//m_vChangerGui = AcGuiBase.getTextureChanger();

		//
		m_vGameObject = new GameObject[ _OBJECT_NUM ];

		//
		for ( int count = 0; count < _OBJECT_NUM; count++ )
		{
			m_vGameObject[ count ] = this.transform.FindChild( _objectTbl[ count ] ).gameObject;
//			m_vChanger.initialize( m_vGameObject[ count ].renderer );
		}
	}

	// Update is called once per frame
	void Update()
	{
		int _timer = Time.frameCount;

//		m_vChanger.update( m_vGameObject[ _OBJECT_BACKGROUND ].renderer, _count, _CHENGER_BACKGROUND, 0, 0 );
		m_vChanger.update( m_vGameObject[ _OBJECT_BACKGROUND ].renderer, _timer, _CHENGER_BACKGROUND, 0 );

		//		if ( Application.platform == RuntimePlatform.Android && Input.GetKey( KeyCode.Escape ) )
		if ( Input.GetKey( KeyCode.Escape ) )
		{
			Application.LoadLevel( "Title" );
		}
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
			//AcGuiTime _guiTime = new AcGuiTime( _times[ _count ], 80, 200 + ( _count * 48 ), _baseScale, m_vChanger[ _CHENGER_GUI ] );
			//AcGuiTime _guiTime = new AcGuiTime( _times[ _count ], 80, 200 + ( _count * ( AcGuiTime.getFrameH() + 2 ) ), m_vChangerGui );
			//AcGuiTime _guiTime = new AcGuiTime( _times[ _count ], 80, 200 + ( _count * ( AcGuiTime.getFrameH() + 2 ) ) );
			//_guiTime.onGUI( _baseScale, _sizeScale, true );
		}

		//
		int[] _doors = AcSave.getDoors();
		//
		for ( int _count = 0; _count < _doors.Length; _count++ )
		{
			//AcGuiDoor _guiDoor = new AcGuiDoor( _doors[ _count ], 480 - 80 - 88, 200 + ( _count * 48 ), _baseScale, m_vChanger[ _CHENGER_GUI ] );
			//AcGuiDoor _guiDoor = new AcGuiDoor( _doors[ _count ], AcApp.SCREEN_W - AcGuiDoor.getFrameW() - 80.0f, 200 + ( _count * ( AcGuiDoor.getFrameH() + 2 ) ), m_vChangerGui );
			//AcGuiDoor _guiDoor = new AcGuiDoor( _doors[ _count ], AcApp.SCREEN_W - AcGuiDoor.getFrameW() - 80.0f, 200 + ( _count * ( AcGuiDoor.getFrameH() + 2 ) ) );
			//_guiDoor.onGUI( _baseScale, _sizeScale, true );
		}
	}

	// ========================================================================== //
	// ========================================================================== //
}
