using UnityEngine;
using System.Collections;

/// <summary>
/// 動的に呼び出すオブジェクト用のスクリプトです
/// </summary>
public class AcAd : MonoBehaviour
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * 
	 */
	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //
	/*
	 * Unity で Web 画面を出すには？って事で、幾つか見つけました
	 * 
	 * 
	 * GREE
	 *		https://github.com/gree/unity-webview
	 * 
	 *		Unity用WebViewプラグインをオープンソースライセンスで公開!  GREE Engineers' Blog
	 *		http://labs.gree.jp/blog/2012/04/4772/
	 *		
	 *		unity-webview で自前のHTMLを表示するサンプル - Qiita
	 *		http://qiita.com/snaka/items/46042be015ad320d5f4b
	 *		
	 *		Unity で WebView を表示してみた。  Lonely Mobiler
	 *		http://loumo.jp/wp/archive/20131115085810/
	 *		
	 *		Unity で WebView を表示してみた。  Lonely MobilerUnity で WebView を表示してみた。  Lonely Mobiler
	 *		http://loumo.jp/wp/archive/20131115085810/
	 *		
	 *		Unity3D - Unity4.3 Android Admob 広告をクリック出来ない！ - Qiita
	 *		http://qiita.com/masao_mobile/items/87ec53a979eee08fb7c8
	 *		↑を確認したけど設定には問題はないようですが、クリック出来ない
	 *		↑クリック出来ないんじゃなくて、スゲー重いだけだった・・・
	 * 
	 * 
	 * appbankgames
	 *		https://github.com/appbankgames/unity-webview
	 * 
	 *		Unity上でWebViewを開く unity-webview - テラシュールブログ
	 *		http://tsubakit1.hateblo.jp/entry/20130523/1369316363
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
	private AiAdTrigger m_vTrigger;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private GameObject[] m_vGameObject;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private WebViewObject m_vWebView;

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
	public static AcAd Create( AcGameManager vManager, AiAdTrigger vTrigger )
	{
		GameObject _prefab = ( GameObject ) Resources.Load( "Prefabs/Ad" );
		//
		GameObject _object = ( GameObject ) Instantiate( _prefab, AcApp.GuiPosition, Quaternion.identity );
		//
		AcAd _class = _object.GetComponent<AcAd>();
		//
		_class._create( vManager, vTrigger );
		//
		return ( _class );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _create( AcGameManager vManager, AiAdTrigger vTrigger )
	{
		_debugLog( "_create" );
		//
		m_vManager = vManager;
		m_vTrigger = vTrigger;
		//
		_setObject();
		_setRender();
		//
		_setWebView();
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
			new { _object = _OBJECT_TITLE,			_image = AcApp.IMAGE_TEX_L,		_index = 3, },
			new { _object = _OBJECT_BUTTON_OK,		_image = AcApp.IMAGE_TEX_S,		_index = 4, },
		};
		//
		foreach ( var _var in _tbl )
		{
			AcApp.imageRender( m_vGameObject[ _var._object ].renderer, _var._image, _var._index );
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _setWebView()
	{
		switch ( Application.platform )
		{
			case ( RuntimePlatform.Android ):
			case ( RuntimePlatform.IPhonePlayer ):

				float _scale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );
				int _top = ( int ) ( 200 * _scale );
				int _side = ( int ) ( 35 * _scale ); // 50
				int _bottom = ( int ) ( 290 * _scale );
				/*
				 * 実験中！
				 * PC のエミュレータ上だと落ちるので切り分ける必要があるみたいです
				 */
				m_vWebView = this.gameObject.AddComponent<WebViewObject>();
				//m_vWebView.Init();
				m_vWebView.Init( ( msg ) =>
				{
					_debugLog( msg );
					//Debug.Log( msg );
				} );
				m_vWebView.LoadURL( "http://www.yahoo.co.jp/" );
				m_vWebView.SetMargins( _side, _top, _side, _bottom ); // LoadURL 後じゃないとダメらしい http://scriptgraph.blogspot.jp/2013/08/unity-webview.html
				m_vWebView.SetVisibility( true );
				//
				m_vWebView.EvaluateJS(
					"window.addEventListener('load', function() {" +
					"   window.addEventListener('click', function() {" +
					"       Unity.call('clicked');" +
					"   }, false);" +
					"}, false);");
				//
				break;
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
		//if ( bSw )
		//{
		//	//_setRender();
		//}
		//else
		//{

		//}

		if ( m_vWebView != null )
		{
			m_vWebView.SetVisibility( bSw );
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
	}

	private void _update()
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
				if ( ( _object.name ).CompareTo( _getObjectName( _OBJECT_BUTTON_OK ) ) == ( 0 ) )
				{
					m_vTrigger.onTrigger( Trigger.OK );
				}
			}
		}
		//
		if ( Input.GetKey( KeyCode.Escape ) )
		{
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
	public interface AiAdTrigger
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="vTrigger"></param>
		void onTrigger( AcAd.Trigger vTrigger );
	}

	// ========================================================================== //
	// ========================================================================== //
}
