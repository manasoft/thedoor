using UnityEngine;
using System.Collections;

public class AcHowtoplayManager : MonoBehaviour
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
	private const int _OBJECT_BUTTON_YES = 1;
	private const int _OBJECT_BUTTON_NO = 2;
	private const int _OBJECT_NUM = 3;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _CHENGER_TIMEATTACK = 0;
	private const int _CHENGER_CHALLENGE = 1;
	private const int _CHENGER_YES = 2;
	private const int _CHENGER_NO = 3;
	private const int _CHENGER_NUM = 4;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private string[] _objectTbl =
	{
		"SceneBase2D/Background",
		"DialogButtons/Yes",
		"DialogButtons/No",
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

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

	//	private Animator m_vAnimator;

	private AcTextureChanger m_vChanger;

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	// Use this for initialization
	void Start()
	{
		//
		//m_vChanger = new AcTextureChanger( new AcTextureChanger.Data(
		//	//
		//	"Images" + AcUtil.getLanguageSuffix() + "/howtoplay_1", 8, 8, 4,
		//	new int[] { 0, 1, },
		//	new int[] { 8, 9, },
		//	new int[] { 16,17, },
		//	new int[] { 24, 25, }
		//	//
		//) );

		//
		m_vChanger = new AcTextureChanger();
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/howtoplay_1", 8, 8, 4, new int[] { 0, 1, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/howtoplay_1", 8, 8, 4, new int[] { 8, 9, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/howtoplay_1", 8, 8, 4, new int[] { 16, 17, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/howtoplay_1", 8, 8, 4, new int[] { 24, 25, } );

		//
		m_vGameObject = new GameObject[ _OBJECT_NUM ];

		//
		for ( int count = 0; count < _OBJECT_NUM; count++ )
		{
			m_vGameObject[ count ] = transform.FindChild( _objectTbl[ count ] ).gameObject;
			//			m_vChanger.initialize( m_vGameObject[ count ].renderer );
		}
	}

	// Update is called once per frame
	void Update()
	{
		int _timer = Time.frameCount;

		switch ( AcApp.getGameMode() )
		{
			case ( AcApp.GAMEMODE_TIMEATTACK ):
				m_vChanger.update( m_vGameObject[ _OBJECT_BACKGROUND ].renderer, _timer, _CHENGER_TIMEATTACK, 0 );
				break;
			//
			case ( AcApp.GAMEMODE_CHALLENGE ):
				m_vChanger.update( m_vGameObject[ _OBJECT_BACKGROUND ].renderer, _timer, _CHENGER_CHALLENGE, 0 );
				break;
		}
		//
		m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_YES ].renderer, _timer, _CHENGER_YES, 0 );
		m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_NO ].renderer, _timer, _CHENGER_NO, 0 );

		//for ( int _index = 0; _index < _OBJECT_NUM; _index ++)
		//{
		//	m_vChanger.update( m_vGameObject[ _index ].renderer, _frame, _index, 0, 0 );
		//}

		if ( Input.GetMouseButtonDown( 0 ) )
		{
			Ray _ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit _hit = new RaycastHit();
			//
			if ( Physics.Raycast( _ray, out _hit ) )
			{
				GameObject _object = _hit.collider.gameObject;
				//
				if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_BUTTON_YES ) ) == ( 0 ) )
				{
					//					Debug.Log( "yes!" );
					Application.LoadLevel( "Game" );
				}
				//
				if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_BUTTON_NO ) ) == ( 0 ) )
				{
					//					Debug.Log( "No!" );
					Application.LoadLevel( "Title" );
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
			Application.LoadLevel( "Title" );
		}
	}

	// ========================================================================== //
	// ========================================================================== //
}
