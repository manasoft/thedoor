using UnityEngine;
using System.Collections;

public class AcSceneManager2D : MonoBehaviour
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

	protected const int _OBJECT_NONE = -1;
	protected const int _OBJECT_BG = 0;
	protected const int _OBJECT_SELECT = 1;
	protected const int _OBJECT_SELECT_START = 2;
	protected const int _OBJECT_SELECT_TIMEATTACK = 3;
	protected const int _OBJECT_SELECT_CHALLENGE = 4;
	protected const int _OBJECT_SELECT_RANKING = 5;
	protected const int _OBJECT_DIALOG = 6;
	protected const int _OBJECT_DIALOG_YES = 7;
	protected const int _OBJECT_DIALOG_NO = 8;
	protected const int _OBJECT_NUM = 9;

	protected const int _CHENGER_NONE = -1;
	protected const int _CHENGER_BG_TITLE = 0;
	protected const int _CHENGER_BG_HOW2PLAY_TIMEATTACK = 1;
	protected const int _CHENGER_BG_HOW2PLAY_CHALLENGE = 2;
	protected const int _CHENGER_BG_RANKING = 3;
	protected const int _CHENGER_SELECT = 4;
	protected const int _CHENGER_SELECT_START = 5;
	protected const int _CHENGER_SELECT_TIMEATTACK = 6;
	protected const int _CHENGER_SELECT_CHALLENGE = 7;
	protected const int _CHENGER_SELECT_RANKING = 8;
	protected const int _CHENGER_DIALOG = 9;
	protected const int _CHENGER_DIALOG_YES = 10;
	protected const int _CHENGER_DIALOG_NO = 11;
	protected const int _CHENGER_NUM = 12;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private string[] _objectTbl =
	{
		"Background",
		"Select",
		"Select/Start",
		"Select/TimeAttack",
		"Select/Challenge",
		"Select/Ranking",
		"Dialog",
		"Dialog/Yes",
		"Dialog/No",
	};

	//private class _Object
	//{
	//	public string m_vPath;
	//	public string m_vName;
	//	public GameObject m_vGameObject;
	//	public int m_vChangerId;

	//	public _Object( string vPath, int vChangerId )
	//	{
	//		m_vPath = vPath;
	//		//			m_vName = "";
	//		m_vChangerId = vChangerId;

	//		string[] _string = vPath.Split( '/' );
	//		m_vName = _string[ _string.Length - 1 ];
	//	}
	//}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//private AcTextureChanger.Data[] _changerTbl =
	//	{
	//		new AcTextureChanger.Data(
	//			"Images/Exterior/title_1", 8, 8, 4,
	//			new int[] {  0,  1,  2,  3,},
	//			new int[] {  8,  9, 10, 11,},
	//			new int[] { 40, 41, 42, 43,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			//
	//			new int[] {  4,  5,  6,  7,},
	//			new int[] { 12, 13, 14, 15, 20, 21, 22, 23, 28, 29, 30, 31, 36, 37, 38, 39,}
	//		),
	//		//
	//		new AcTextureChanger.Data(
	//			AcUtil.getImageResourcePath() + "Exterior/title_1", 8, 8, 4,
	//			new int[] {  0,  1,  2,  3,},
	//			new int[] {  8,  9, 10, 11,},
	//			new int[] { 40, 41, 42, 43,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			new int[] { 48, 49, 50, 51,},
	//			//
	//			new int[] {  4,  5,  6,  7,},
	//			new int[] { 12, 13, 14, 15, 20, 21, 22, 23, 28, 29, 30, 31, 36, 37, 38, 39,}
	//		),
	//	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	protected GameObject getObject( int vObjectId )
	{
		return ( m_vGameObject[ vObjectId ] );
	}

	protected Renderer getObjectRenderer( int vObjectId )
	{
		return ( m_vGameObject[ vObjectId ].renderer );
	}

	protected string getObjectName( int vObjectId )
	{
		string[] _string = _objectTbl[ vObjectId ].Split( '/' );
		//
		return ( _string[ _string.Length - 1 ] );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	protected AcTextureChanger getTextureChanger()
	{
		return ( m_vChanger );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private GameObject[] m_vGameObject;

	private Animator m_vAnimator;

	private AcTextureChanger m_vChanger;

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	public void selectOnBottom()
	{
		m_vAnimator.SetBool( "bMove", false );

		bool _sw = m_vGameObject[ _OBJECT_SELECT_START ].activeInHierarchy;

		m_vGameObject[ _OBJECT_SELECT_START ].SetActive( !_sw );

		m_vGameObject[ _OBJECT_SELECT_TIMEATTACK ].SetActive( _sw );
		m_vGameObject[ _OBJECT_SELECT_CHALLENGE ].SetActive( _sw );
		m_vGameObject[ _OBJECT_SELECT_RANKING ].SetActive( _sw );
	}

	// ========================================================================== //
	// ========================================================================== //

	protected void _start_Title()
	{
		m_vAnimator = m_vGameObject[ _OBJECT_SELECT ].GetComponent<Animator>();

		//m_vChanger.initialize( m_vGameObject[ _OBJECT_BG ].renderer );
		//m_vChanger.initialize( m_vGameObject[ _OBJECT_SELECT_START ].renderer );
		//m_vChanger.initialize( m_vGameObject[ _OBJECT_SELECT_TIMEATTACK ].renderer );
		//m_vChanger.initialize( m_vGameObject[ _OBJECT_SELECT_CHALLENGE ].renderer );
		//m_vChanger.initialize( m_vGameObject[ _OBJECT_SELECT_RANKING ].renderer );

		m_vGameObject[ _OBJECT_SELECT_TIMEATTACK ].SetActive( false );
		m_vGameObject[ _OBJECT_SELECT_CHALLENGE ].SetActive( false );
		m_vGameObject[ _OBJECT_SELECT_RANKING ].SetActive( false );

		//Vector3 _scale = m_vGameObject[ _OBJECT_SELECT_START ].transform.localScale;
		//_scale.x *= 1.5f;

		//m_vGameObject[ _OBJECT_SELECT_START ].transform.localScale = _scale;
		//m_vGameObject[ _OBJECT_SELECT_TIMEATTACK ].transform.localScale = _scale;
		//m_vGameObject[ _OBJECT_SELECT_CHALLENGE ].transform.localScale = _scale;
		//m_vGameObject[ _OBJECT_SELECT_RANKING ].transform.localScale = _scale;

		m_vGameObject[ _OBJECT_DIALOG ].SetActive( false );
	}

	protected void _update_Title()
	{
		int _frame = Time.frameCount;

		//m_vChanger.update( m_vGameObject[ _OBJECT_BG ].renderer, _frame, _CHENGER_BG_TITLE, 0, 0 );
		//m_vChanger.update( m_vGameObject[ _OBJECT_SELECT_START ].renderer, _frame, _CHENGER_SELECT_START, 0, 0 );
		//m_vChanger.update( m_vGameObject[ _OBJECT_SELECT_TIMEATTACK ].renderer, _frame, _CHENGER_SELECT_TIMEATTACK, 0, 0 );
		//m_vChanger.update( m_vGameObject[ _OBJECT_SELECT_CHALLENGE ].renderer, _frame, _CHENGER_SELECT_CHALLENGE, 0, 0 );
		//m_vChanger.update( m_vGameObject[ _OBJECT_SELECT_RANKING ].renderer, _frame, _CHENGER_SELECT_RANKING, 0, 0 );
	}

	protected void _start_Howtoplay()
	{
		m_vGameObject[ _OBJECT_SELECT ].SetActive( false );

	}

	protected void _start_Ranking()
	{
		m_vGameObject[ _OBJECT_SELECT ].SetActive( false );
		m_vGameObject[ _OBJECT_DIALOG ].SetActive( false );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //


	protected void _update_Howtoplay()
	{
	}

	protected void _update_Ranking()
	{
	}

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	// Use this for initialization
	void Start()
	{
//		AcTextureChanger.Data[] _changerTbl =
////		_changerTbl = new AcTextureChanger.Data[]
//		{
//			new AcTextureChanger.Data(
//				"Images/Exterior/title_1", 8, 8, 4,
//				new int[] {  0,  1,  2,  3,},
//				new int[] {  8,  9, 10, 11,},
//				new int[] { 40, 41, 42, 43,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				//
//				new int[] {  4,  5,  6,  7,},
//				new int[] { 12, 13, 14, 15, 20, 21, 22, 23, 28, 29, 30, 31, 36, 37, 38, 39,}
//			),
//			//
//			new AcTextureChanger.Data(
//				"Images" + AcUtil.getLanguageSuffix() + "/Exterior/title_1", 8, 8, 4,
//				new int[] {  0,  1,  2,  3,},
//				new int[] {  8,  9, 10, 11,},
//				new int[] { 40, 41, 42, 43,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				new int[] { 48, 49, 50, 51,},
//				//
//				new int[] {  4,  5,  6,  7,},
//				new int[] { 12, 13, 14, 15, 20, 21, 22, 23, 28, 29, 30, 31, 36, 37, 38, 39,}
//			),
//		};

	//	m_vChanger = new AcTextureChanger( _changerTbl[ 0 ] );
		//
		m_vGameObject = new GameObject[ _OBJECT_NUM ];
		//
		for ( int count = 0; count < _OBJECT_NUM; count++ )
		{
			m_vGameObject[ count ] = transform.FindChild( _objectTbl[ count ] ).gameObject;
			//			m_vChanger.initialize( m_vGameObject[ count ].renderer );
		}

		//		m_vChanger.initialize( m_vGameObject[ _OBJECT_BG ].renderer );

		_start_Title();
	}

	// Update is called once per frame
	void Update()
	{
		_update_Title();


		if ( Input.GetMouseButtonDown( 0 ) )
		{
			Ray _ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit _hit = new RaycastHit();
			//
			if ( Physics.Raycast( _ray, out _hit ) )
			{
				GameObject _object = _hit.collider.gameObject;
				//
				if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_SELECT_START ) ) == ( 0 ) )
				{
					m_vAnimator.SetBool( "bMove", true );
//					Application.LoadLevel( "Game" );
				}
				//
				if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_SELECT_TIMEATTACK ) ) == ( 0 ) )
				{
					AcSetting.setMode( AcSetting.TIMEATTACK_MODE );
					//
					Application.LoadLevel( "Howtoplay" );
				}
				//
				if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_SELECT_CHALLENGE ) ) == ( 0 ) )
				{
					AcSetting.setMode( AcSetting.CHALLENGE_MODE );
					//
					Application.LoadLevel( "Howtoplay" );
				}
				//
				if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_SELECT_RANKING ) ) == ( 0 ) )
				{
//					Application.LoadLevel( "Ranking" );
					m_vAnimator.SetBool( "bMove", true );
				}
				//
				if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_DIALOG_YES ) ) == ( 0 ) )
				{
					Application.LoadLevel( "Game" );
				}
				//
				if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_DIALOG_NO ) ) == ( 0 ) )
				{
					Application.LoadLevel( "Title" );
				}
			}
		}

		//int _frame = Time.frameCount;

		//m_vChanger.update( m_vGameObject[ _OBJECT_BG ].renderer, _frame, _CHENGER_BG_TITLE, 0, 0 );

		//int _frame = Time.frameCount;

		//for ( int count = 0; count < _OBJECT_NUM; count++ )
		//{
		//	m_vChanger.update( m_vGameObject[ count ].renderer, _frame, count, 0, 0 );
		//}



		//int _frame = Time.frameCount;

		//switch ( AcSetting.getMode() )
		//{
		//	case ( AcSetting.TIMEATTACK_MODE ):
		//		m_vChanger.update( m_vGameObject[ _OBJECT_BACKGROUND ].renderer, _frame, _CHENGER_TIMEATTACK, 0, 0 );
		//		break;
		//	//
		//	case ( AcSetting.CHALLENGE_MODE ):
		//		m_vChanger.update( m_vGameObject[ _OBJECT_BACKGROUND ].renderer, _frame, _CHENGER_CHALLENGE, 0, 0 );
		//		break;
		//}
		////
		//m_vChanger.update( m_vGameObject[ _OBJECT_BTN_YES ].renderer, _frame, _CHENGER_YES, 0, 0 );
		//m_vChanger.update( m_vGameObject[ _OBJECT_BTN_NO ].renderer, _frame, _CHENGER_NO, 0, 0 );

		////for ( int _index = 0; _index < _OBJECT_NUM; _index ++)
		////{
		////	m_vChanger.update( m_vGameObject[ _index ].renderer, _frame, _index, 0, 0 );
		////}

		//if ( Input.GetMouseButtonDown( 0 ) )
		//{
		//	Ray _ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		//	RaycastHit _hit = new RaycastHit();
		//	//
		//	if ( Physics.Raycast( _ray, out _hit ) )
		//	{
		//		GameObject _object = _hit.collider.gameObject;
		//		//
		//		if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_BTN_YES ) ) == ( 0 ) )
		//		{
		//			Debug.Log( "yes!" );
		//			Application.LoadLevel( "Game" );
		//		}
		//		//
		//		if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_BTN_NO ) ) == ( 0 ) )
		//		{
		//			Debug.Log( "No!" );
		//			Application.LoadLevel( "Title" );

		//		}
		//	}
		//}

		///*
		// * ゲームの終了とか
		// * http://motogeneralpurpose.blogspot.jp/2013/04/unity.html
		// * 上と同じとこですけどまとまっていたのでメモしときます
		// * http://motogeneralpurpose.blogspot.jp/search/label/Unity
		// */
		//if ( Application.platform == RuntimePlatform.Android && Input.GetKey( KeyCode.Escape ) )
		//{
		//	Application.LoadLevel( "Title" );
		//}
	}

	// ========================================================================== //
	// ========================================================================== //
}
