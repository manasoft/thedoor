using UnityEngine;
using System.Collections;

// Path / StreamWriter
using System.IO;

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

	/// <summary>
	/// nend / webview(asta) 切り替え
	/// </summary>
	private bool _IS_WEBVIEW = false;

	// ========================================================================== //
	// ========================================================================== //

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
	private static void _debugLog( string vString )
	{
		ScDebug.debugLog( typeof( AcAd ).FullName + " # " + vString );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

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

	/// <summary>
	/// html ファイルへのパス
	/// </summary>
	private string m_vFilePath;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// デバッグ用
	/// </summary>
	private string m_vGuiText = "";

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
		if ( _IS_WEBVIEW )
		{
			_setWebView();
		}
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

	/*
	 *		headタグ内に記載して下さい。
	 *		-------------------------------------↓
	 *		<!-- astrsk Icon広告用 css -->
	 *		<link type="text/css" rel="stylesheet" href="http://public.astrsk.net/mbget.css" />
	 *		<script type="text/javascript">Astrskfncstx_targetName="_self";</script>
	 *		-------------------------------------
	 *		3行目の「Astrskfncstx_targetName=」を設定することにより、任意のウインドウ、フレームで画面遷移させることができます。
	 *		新しいウインドウで開きたい場合は、「_self」を「_blank」に変更して下さい。
	 *		注）iOS向けのWebサイトで「_blank」を設定すると正常に画面遷移しない場合がございますのでご注意ください。
	 *		bodyタグ内の配信したい場所へ記載して下さい。
	 *		-------------------------------------↓
	 *		<!-- astrsk Icon広告 JS タグ  -->
	 *		<script type="text/javascript" src="http://public.astrsk.net/ast0200620wvq3ghyvt9/mab.js?size=73x75&amp;acCount=1" ><!--//--></script>
	 *		------------------------------------
	 *		数の広告を取得したい場合は、HTMLタグ内の「acCount」の値を取得したい数に変更して下さい。
	 *		例）4つ取得したい場合、「acCount=4」と書き換える。
	 */

	/*
	 * unity-webview で自前のHTMLを表示するサンプル - Qiita
	 * http://qiita.com/snaka/items/46042be015ad320d5f4b
	 */


	/*
	 * Unity WebView まとめ - UPSNAP
	 * http://upsnap.jp/archives/show/137
	 */

	/// <summary>
	/// 初期化ですな
	/// </summary>
	private void _setWebView()
	{
#if false
		/*
		 * PC 上にファイルを出力してみる場合
		 * 
		 */
		{
			// 作成する html ファイルへのパス
			const string _path = "ad/ad_astrsk.html";
			// html に書き込むスクリプト
			const string _html =
				"<html>" +
				"<head>" +
				"<!-- astrsk Icon広告用 css -->" +
				"<link type=\"text/css\" rel=\"stylesheet\" href=\"http://public.astrsk.net/mbget.css\" />" +
				"<script type=\"text/javascript\">Astrskfncstx_targetName=\"_self\";</script>" +
				//"<script type=\"text/javascript\">Astrskfncstx_targetName=\"_blank\";</script>" +
				"<title>Welcome</title>" +
				"</head>" +
				"<body>" +
				"<!-- astrsk Icon広告 JS タグ  -->" +
				"<script type=\"text/javascript\" src=\"http://public.astrsk.net/ast0200620wvq3ghyvt9/mab.js?size=73x75&amp;acCount=9\" ><!--//--></script>" +
				//"<script type=\"text/javascript\" src=\"http://public.astrsk.net/ast0200620wvq3ghyvt9/mab.js?size=73x75&amp;acCount=9\" ></script>" +
				"</body>" +
				"</html>";

			// Unity さんから、実機のフルパス名を教えていただく
			m_vFilePath = Path.Combine( Application.persistentDataPath, _path );

			// ファイルを作って保存する
			AcUtil.writeFile( m_vFilePath, _html, false );
		}
#endif

		switch ( Application.platform )
		{
			/*
			* 実験中！
			* PC のエミュレータ上だと落ちるので切り分ける必要があるみたいです
			*/
			case ( RuntimePlatform.Android ):
			case ( RuntimePlatform.IPhonePlayer ):

				// 作成する html ファイルへのパス
				const string _path = "ad/ad_astrsk.html";
				// html に書き込むスクリプト
				const string _html =
					"<html>" +
					"<head>" +
					"<!-- astrsk Icon広告用 css -->" +
					"<link type=\"text/css\" rel=\"stylesheet\" href=\"http://public.astrsk.net/mbget.css\" />" +
					"<script type=\"text/javascript\">Astrskfncstx_targetName=\"_self\";</script>" +
					//"<script type=\"text/javascript\">Astrskfncstx_targetName=\"_blank\";</script>" +
					"<title>Welcome</title>" +
					"</head>" +
					"<body>" +
					"<!-- astrsk Icon広告 JS タグ  -->" +
					"<script type=\"text/javascript\" src=\"http://public.astrsk.net/ast0200620wvq3ghyvt9/mab.js?size=73x75&amp;acCount=9\" ><!--//--></script>" +
					//"<script type=\"text/javascript\" src=\"http://public.astrsk.net/ast0200620wvq3ghyvt9/mab.js?size=73x75&amp;acCount=9\" ></script>" +
					"</body>" +
					"</html>";

				// Unity さんから、実機のフルパス名を教えていただく
				m_vFilePath = Path.Combine( Application.persistentDataPath, _path );

				// ファイルを作って保存する
				AcUtil.writeFile( m_vFilePath, _html, false );

				m_vWebView = this.gameObject.AddComponent<WebViewObject>();
				m_vWebView.Init();
				//m_vWebView.Init(
				//	( msg ) =>
				//		{
				//			m_vGuiText = msg;
				//			_debugLog( msg );
				//		}
				//);
				/*
				 * Unity で WebView を表示してみた。
				 * http://loumo.jp/wp/archive/20131115085810/
				 * init() の中での処理
				 * 
				 * javascriptでクリックしたリンクテキストを取得するには？ 【OKWave】
				 * http://okwave.jp/qa/q4434067.html
				 */

				//

				//float _scale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );
				//int _top = ( int ) ( 180 * _scale );
				//int _side = ( int ) ( 41 * _scale ); // 50
				//int _bottom = ( int ) ( 250 * _scale );

				////m_vWebView.LoadURL( "http://www.yahoo.co.jp/" );
				////m_vWebView.LoadURL( "http://www.manasoft.co.jp/crazydoor/ad/test.html" );
				//m_vWebView.LoadURL( "file://" + m_vFilePath );
				//m_vWebView.SetMargins( _side, _top, _side, _bottom ); // LoadURL 後じゃないとダメらしい http://scriptgraph.blogspot.jp/2013/08/unity-webview.html
				//m_vWebView.SetVisibility( true );
				//
				break;
		}
	}

	/// <summary>
	/// リロード処理って感じかな？
	/// </summary>
	/// <param name="bSw"></param>
	private void _setWebViewActive( bool bSw )
	{
		switch ( Application.platform )
		{
#if false
			case ( RuntimePlatform.Android ):
			case RuntimePlatform.OSXEditor:
		case RuntimePlatform.OSXPlayer:
		case RuntimePlatform.IPhonePlayer:
			//m_vWebView.LoadURL( "file://" + Application.dataPath + "/WebPlayerTemplates/unity-webview/" + Url );
			m_vWebView.LoadURL( "file://" + m_vFilePath );
			m_vWebView.SetMargins( 10, 50, 10, 100 );
			m_vWebView.EvaluateJS(
				"window.addEventListener('load', function() {" +
				"	window.Unity = {" +
				"		call:function(msg) {" +
				"			var iframe = document.createElement('IFRAME');" +
				"			iframe.setAttribute('src', 'unity:' + msg);" +
				"			document.documentElement.appendChild(iframe);" +
				"			iframe.parentNode.removeChild(iframe);" +
				"			iframe = null;" +
				"		}" +
				"	}" +
				"}, false);");
			m_vWebView.EvaluateJS(
				"window.addEventListener('load', function() {" +
				"	window.addEventListener('click', function() {" +
				"		Unity.call('clicked');" +
				"	}, false);" +
				"}, false);");

				//
				m_vWebView.SetVisibility( bSw );

				break;
		case RuntimePlatform.OSXWebPlayer:
		case RuntimePlatform.WindowsWebPlayer:
			//m_vWebView.LoadURL( Url );
			//m_vWebView.EvaluateJS(
			//	"parent.$(function() {" +
			//	"	window.Unity = {" +
			//	"		call:function(msg) {" +
			//	"			parent.unityWebView.sendMessage('WebViewObject', msg)" +
			//	"		}" +
			//	"	};" +
			//	"	parent.$(window).click(function() {" +
			//	"		window.Unity.call('clicked');" +
			//	"	});" +
			//	"});");
			break;
#else
			case ( RuntimePlatform.Android ):
			case ( RuntimePlatform.IPhonePlayer ):
				//
				if ( bSw )
				{
					float _scale = AcUtil.getScreenScaleY( AcApp.SCREEN_H );
					int _top = ( int ) ( 180 * _scale );
					int _side = ( int ) ( 41 * _scale ); // 50
					int _bottom = ( int ) ( 250 * _scale );
					//
					m_vWebView.LoadURL( "file://" + m_vFilePath );
					//m_vWebView.LoadURL( "https://play.google.com/store" );

					m_vWebView.SetMargins( _side, _top, _side, _bottom ); // LoadURL 後じゃないとダメらしい http://scriptgraph.blogspot.jp/2013/08/unity-webview.html

					const string js_1 =
					"window.addEventListener('load', function() {" +
					"   window.addEventListener('click', function() {" +
					"       Unity.call('clicked');" +
					"   }, false);" +
					"}, false);";

					const string _js_2 =
						"window.addEventListener('load', function() {" +
							"window.addEventListener('click', function (e) {" +
								"var o=e?e.target:event.srcElement;if(o.tagName=='A') Unity.call(o.innerHTML);" +
							"}, false);" +
						"}, false);";

					const string _js_3 =
						"document.body.onclick=mess;" +
						//	"function mess(e){var o=e?e.target:event.srcElement;if(o.tagName=='A') alert(o.innerHTML);}";
						"function (e){var o=e?e.target:event.srcElement;if(o.tagName=='A') Unity.call(o.innerHTML);}";

					const string _js_4 =
						"window.addEventListener('load', function() {" +
						"    window.Unity = {" +
						"       call:function(msg) {" +
						"           var iframe = document.createElement('IFRAME');" +
						"           iframe.setAttribute('src', 'unity:' + msg);" +
						"           document.documentElement.appendChild(iframe);" +
						"           iframe.parentNode.removeChild(iframe);" +
						"           iframe = null;" +
						"       }" +
						"    }" +
						"}, false);";

					const string _js_5 =
						"window.addEventListener('load', function() {" +
						"   window.addEventListener('click', function() {" +
						"       Unity.call('clicked');" +
						"   }, false);" +
						"}, false);";


					//m_vWebView.EvaluateJS( _js_4 );
					//m_vWebView.EvaluateJS( _js_5 );


				}
				//
				m_vWebView.SetVisibility( bSw );
				break;
#endif
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
		if ( _IS_WEBVIEW )
		{
			_setWebViewActive( bSw );
		}
		else
		{
			m_vManager.swNendIcon( bSw );
		}

		//if ( bSw )
		//{
		//	//_setRender();
		//}
		//else
		//{
		//}

		//if ( m_vWebView != null )
		//{
		//	m_vWebView.SetVisibility( bSw );
		//	//
		//	if ( bSw )
		//	{
		//	//	_setWebView();
		//	}
		//}
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
#if false
		GUI.Label( new Rect( 5, 5, 100, 40 ), "gui >> " + m_vGuiText );
#endif
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
