using UnityEngine;
using System.Collections;
using System.Threading;

/// <summary>
/// シーン "Title.unity" のマネージャークラスだよ
/// </summary>
public class AcTitleManager : MonoBehaviour
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * ビギナーのためのUnity超入門
	 * http://libro.tuyano.com/index2?id=2716003
	 * 
	 * Unity初心者の雑記  Unity
	 * http://pinbit.blog.jp/archives/cat_246587.html
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
	private const int _OBJECT_BUTTON_START = 1;
	private const int _OBJECT_BUTTON_TIMEATTACK = 2;
	private const int _OBJECT_BUTTON_CHALLENGE = 3;
	private const int _OBJECT_BUTTON_RANKING = 4;
	private const int _OBJECT_NUM = 5;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _CHENGER_BACKGROUND = 0;
	private const int _CHENGER_START = 1;
	private const int _CHENGER_TIMEATTACK = 2;
	private const int _CHENGER_CHALLENGE = 3;
	private const int _CHENGER_RANKING = 4;
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
		"SceneBase2D/Background",
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

	private GameObject[] m_vGameObject;

	private Animator m_vAnimator;

	private AcTextureChanger m_vChanger;

	private int m_vPhase;

	//	private Thread m_vThread;

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
		 * 背景は常に表示するよ！
		 */
		m_vGameObject[ _OBJECT_BACKGROUND ].SetActive( true );
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
		//
		AcRanking.onStart();

		//
		if ( AcSetting.isBoot() )
		{
			_setPhase( _PHASE_MENU );
		}
		else
		{
			AcSetting.swBoot( true );
			//
			//			StartCoroutine( _coroutineNext( 60 * 4, _PHASE_START ) );
			//
			_setPhase( _PHASE_START );
		}
	}

	private void _updateStart( int vTimer )
	{
//		m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_START ].renderer, vFrameCount, _CHENGER_START, 0, 0 );
		m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_START ].renderer, vTimer, _CHENGER_START, 0 );

		if ( Input.GetMouseButtonDown( 0 ) )
		{
			Ray _ray = Camera.main.ScreenPointToRay( Input.mousePosition );
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
		m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_TIMEATTACK ].renderer, vTimer, _CHENGER_TIMEATTACK,0 );
		m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_CHALLENGE ].renderer, vTimer, _CHENGER_CHALLENGE,0 );
		m_vChanger.update( m_vGameObject[ _OBJECT_BUTTON_RANKING ].renderer, vTimer, _CHENGER_RANKING, 0 );

		if ( Input.GetMouseButtonDown( 0 ) )
		{
			Ray _ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit _hit = new RaycastHit();
			//
			if ( Physics.Raycast( _ray, out _hit ) )
			{
				GameObject _object = _hit.collider.gameObject;
				//
				if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_BUTTON_TIMEATTACK ) ) == ( 0 ) )
				{
					AcSetting.setMode( AcSetting.TIMEATTACK_MODE );
					//
					Application.LoadLevel( "Howtoplay" );
				}
				if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_BUTTON_CHALLENGE ) ) == ( 0 ) )
				{
					AcSetting.setMode( AcSetting.CHALLENGE_MODE );
					//
					Application.LoadLevel( "Howtoplay" );
				}
				if ( ( _object.name ).CompareTo( getObjectName( _OBJECT_BUTTON_RANKING ) ) == ( 0 ) )
				{
					Application.LoadLevel( "Ranking" );
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

	// Use this for initialization
	void Start()
	{
		//m_vChanger = new AcTextureChanger( new AcTextureChanger.Data(
		//	//
		//	"Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4,
		//	new int[] { 0, 1, 2, 3, },
		//	new int[] { 8, 9, 10, 11, },
		//	new int[] { 16, 17, 18, 19, },
		//	new int[] { 24, 25, 26, 27, },
		//	new int[] { 32, 33, 34, 35, },
		//	new int[] { 40, 41, 42, 43, },
		//	new int[] { 48, 49, 50, 51, },
		//	//
		//	new int[] { 4, 5, 6, 7, },
		//	new int[] { 12, 13, 14, 15, 20, 21, 22, 23, 28, 29, 30, 31, 36, 37, 38, 39, }
		//) );
		m_vChanger = new AcTextureChanger();
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4,	new int[] { 0, 1, 2, 3, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4,	new int[] { 8, 9, 10, 11, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4,	new int[] { 16, 17, 18, 19, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4,	new int[] { 24, 25, 26, 27, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4,	new int[] { 32, 33, 34, 35, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4,	new int[] { 40, 41, 42, 43, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4,	new int[] { 48, 49, 50, 51, } );
		//
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4,	new int[] { 4, 5, 6, 7, } );
		m_vChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/title_1", 8, 8, 4,	new int[] { 12, 13, 14, 15, 20, 21, 22, 23, 28, 29, 30, 31, 36, 37, 38, 39, } );

		//
		m_vGameObject = new GameObject[ _OBJECT_NUM ];

		//
		for ( int count = 0; count < _OBJECT_NUM; count++ )
		{
			m_vGameObject[ count ] = this.transform.FindChild( _objectTbl[ count ] ).gameObject;
//			m_vChanger.initialize( m_vGameObject[ count ].renderer );
		}
		//
		m_vAnimator = transform.FindChild( "SelectButtons" ).GetComponent<Animator>();
		//
		_setPhase( _PHASE_INI );
		//		_setPhase( _PHASE_START );
		//		_setPhase( _PHASE_MENU );


		//		StartCoroutine( _coroutineLoad() );

		//{
		//													// ↓ windows 上での結果です
		//	Debug.Log( "パス！" );
		//	Debug.Log( Application.dataPath );				// C:/workshop/project/Unity/TheDoor/Assets
		//	Debug.Log( Application.persistentDataPath );	// C:/Users/sirasawa/AppData/LocalLow/Manasoft/TheDoor
		//	Debug.Log( Application.temporaryCachePath );	// C:/Users/sirasawa/AppData/Local/Temp/Manasoft/TheDoor
		//}

		//{
		//	/*
		//	 * スレッド実験
		//	 */
		//	m_vThread = new Thread( new ThreadStart( ThreadMethod ) );
		//	m_vThread.Start();
		//}
	}

	/*
	 * 別スレッドからのダウンロードは C# の処理を使わんとダメだ！（Unity はシングルスレッド！）
	 * C# のダウンロード処理
	 * http://www.ipentec.com/document/document.aspx?page=csharp-web-download-image-file-using-webclient&culture=ja-jp
	 * http://www.ipentec.com/document/document.aspx?page=csharp-web-download-image-file-using-httpwebrequest
	 * 
	 * Unity の保存場所
	 * http://qiita.com/bokkuri_orz/items/c37b2fd543458a189d4d
	 */

	private void ThreadMethod()
	{

		/*
		 * 2014/10/10
		 * WWW はメインスレッドじゃないと動かないって Unity に怒られるよ！
		 */
		WWW www = new WWW( "http://ec3.images-amazon.com/images/I/616aLDtPn3L.jpg" );


		m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.mainTexture = www.texture;
		m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.mainTextureOffset = Vector2.zero;
		m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.mainTextureScale = Vector2.one;


		//for ( int i = 0; i < 100; i++ )
		//{
		//	Thread.Sleep( 1000 );
		//	//Console.Write(" A ");
		//	Debug.Log( "スレッドだー" );
		//}
	}

	// Update is called once per frame
	void Update()
	{
		int _timer = Time.frameCount;
		/*
		 * 共通のバックグラウンド書き換え
		 */
//		m_vChanger.update( m_vGameObject[ _OBJECT_BACKGROUND ].renderer, frameCount, _CHENGER_BACKGROUND, 0, 0 );
		m_vChanger.update( m_vGameObject[ _OBJECT_BACKGROUND ].renderer, _timer, _CHENGER_BACKGROUND, 0 );
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

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	void OnGUI()
	{
	}

	void OnApplicationQuit()
	{
		//if( m_vThread != null )
		//{
		//	Debug.Log( "スレッド終了するよ" );
		//	m_vThread.Abort();
		//	m_vThread = null;
		//}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * 画像のロード
	 * 
	 * http://www40.atwiki.jp/spellbound/pages/1369.html
	 * 
	 * http://blog.be-style.jpn.com/article/53386594.html
	 * 
	 * http://neareal.net/index.php?ComputerGraphics%2FUnity%2FTips%2FScript%2FLoadFileByUsingWWW
	 * 
	 * http://westhillapps.blog.jp/tag/Unity
	 * 
	 * unity 外部画像の非同期ロード
	 * Pro専用？
	 * http://qiita.com/tsubaki_t1/items/d29775e37116f6040810
	 * 
	 * やっぱ Pro じゃないとダメ？
	 * http://detail.chiebukuro.yahoo.co.jp/qa/question_detail/q1286509792
	 * 
	 * 駄目な感じ？
	 * http://qiita.com/tsubaki_t1/items/d29775e37116f6040810
	 */

	/*
	 * WWW の実験だよ
	 * 使って無いよ！（2014/10/15）
	 */
	private IEnumerator _coroutineLoad()
	{
		WWW www = new WWW( "http://ec3.images-amazon.com/images/I/616aLDtPn3L.jpg" );

		if ( !www.isDone )
		{
			//			www.
			yield return null;
		}

		//		yield return www; // 一度中断。読み込みが完了したら再開。


		m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.mainTexture = www.texture;
		m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.mainTextureOffset = Vector2.zero;
		m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.mainTextureScale = Vector2.one;
		//		renderer.material.mainTexture = www.texture; // 読み込んだ画像をテクスチャとして貼り付ける
	}

	// ========================================================================== //
	// ========================================================================== //
}
