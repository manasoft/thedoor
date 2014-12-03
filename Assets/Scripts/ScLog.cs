using UnityEngine;
using System.Collections;

// List<>
using System.Collections.Generic;

/// <summary>
/// 実機上でしか動かないプラグインとか有りまして（広告とか web-view とか）そいつを画面に表示したいって事です
/// 
/// </summary>
public class ScLog : MonoBehaviour
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * 
	 */
	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //
	/*
	 * リファレンス
	 * http://docs-jp.unity3d.com/Documentation/ScriptReference/index.html
	 * 
	 * Unity3D 画面上に表示出来るログ機能（レベル付き）改修版 - Qiita
	 * http://qiita.com/dk4kd/items/24dd0f4c32a170a0ec12
	 * 
	 * Unity iOSやAndroid端末向けにフリック対応をする
	 * http://www.ko-tan.com/article/377752654.html
	 * 
	 * Unity3D - Debug.Logを開発環境以外で無効化 - Qiita
	 * http://qiita.com/rodostw/items/39183e62ed2a1f52f690
	 * デバッグモード判定の仕方
	 * UnityEngine.Debug.isDebugBuild
	 * 
	 */
	/*
	 * [Unity] ポーズ動作をTime.timeScale=0を使わずに実現する  ftvlog
	 * http://ftvoid.com/blog/post/660
	 * 
	 */
	/*
	 * 文字列、暗号化 .NET Tips C#, VB.NET
	 * http://dobon.net/vb/dotnet/string/index.html
	 */
	/*
	 * ActionScript入門Wiki - Unity - 入力情報を取得する
	 * http://www40.atwiki.jp/spellbound/pages/1333.html
	 */
	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// android の log の仕様に合わせたレベルです
	/// </summary>
	private enum _Level
	{
		Verbose,
		Debug,
		Information,
		Warning,
		Error,
	}

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// データをリストで持ってます
	/// </summary>
	private List<ScLog._Data> m_vList;

	/// <summary>
	/// ログを書き込んだらソコに表示を移動するフラグ
	/// </summary>
	private bool m_bLastPos;

	/// <summary>
	/// 画面のドラッグ？の移動量を保持する
	/// </summary>
	private Vector2 m_vDeltaPosition;

	/// <summary>
	/// スクロール位置
	/// </summary>
	private Vector2 m_vScrollPosition = Vector2.zero;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 
	/// </summary>
	private bool m_bActive;

	/// <summary>
	/// ウィンドウサイズ
	/// </summary>
	private Rect m_vWindowRect;

	/// <summary>
	/// 表示行数
	/// </summary>
	private int m_vRows;

	private int m_vFontSize;

	/// <summary>
	/// スクロールロック
	/// </summary>
	private bool m_bLock;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public static ScLog Create()
	{
		GameObject _object = new GameObject();
		//
		ScLog _class = ( ScLog ) _object.AddComponent( ( typeof( ScLog ) ) );
		//
		_object.name = _class.GetType().FullName;
		//
		_class._create();
		//
		return ( _class );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _create()
	{
		m_vList = new List<_Data>();

		m_bLastPos = false;


		m_vWindowRect = new Rect( 0, Screen.height / 2, Screen.width, Screen.height / 2 );
		m_vRows = 10;
		m_vFontSize = ScGui.calcFontSize( m_vWindowRect.y / m_vRows );
		//
		m_bLock = false;
		m_bActive = true;
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 表示切り替え
	/// </summary>
	/// <param name="bSw"></param>
	public void swActive( bool bSw )
	{
		m_bActive = bSw;
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 描画は GUI 上で行います
	/// </summary>
	private void _onGui()
	{

		/*
		 * 表示したい行数に合わせてフォントサイズを決める
		 * この処理は正確ではない、と言うより間違い（近い値にはなるが・・・）
		 */
		//int _fontSize = ( int ) ( m_vWindowRect.height / m_vRows );
		/*
		 * アホっぽいプログラムを作ってみた
		 */
		//int _fontSize = _calcFont( m_vWindowRect.height / m_vRows );

		/*
		 * 文字列を連結する
		 */
		string _text = "";
		//
		foreach ( _Data _data in m_vList )
		{
			string _string =
					_data.m_vDateTime.ToString( @"MM/dd HH:mm:ss | " ) +
					_data.m_vTag + " | " +
					_data.m_vMessage;
			//
			_text += _string + "\n";
		}

		/*
		 * データが増えると、すぐに処理が重くなる・・・
		 */
		//_text = ScGui.autoLinefeed( _text, Screen.width / 2, m_vFontSize );

		/*
		 * スタイルを決定して文字列を表示した場合に必要な大きさを計算する
		 */
		GUIStyle _style = new GUIStyle();
		_style.fontSize = m_vFontSize;
		_style.normal.textColor = Color.green;
		Vector2 _vector = _style.CalcSize( new GUIContent( _text ) );

		/*
		 * スクロールサイズが決定！
		 */
		Rect _scrollRect = new Rect( 0, 0, _vector.x, _vector.y );

		/*
		 * スクロールの最大値
		 */
		Vector2 _scrollLimit = new Vector2();
		//
		_scrollLimit.x = Mathf.Max( 0, _scrollRect.width - m_vWindowRect.width );
		_scrollLimit.y = Mathf.Max( 0, _scrollRect.height - m_vWindowRect.height );

		/*
		 * ドラッグ処理
		 * http://www.wisdomsoft.jp/258.html
		 */
		if ( Event.current.type == EventType.MouseDrag )
		{
			/*
			 * 何故かy座標が逆になる？
			 * 同じイベント（EventType.MouseDrag）で処理しているが、実際はタッチとマウスなので、そのの違いなのか？
			 */
			switch ( Application.platform )
			{
				case ( RuntimePlatform.Android ):
				case ( RuntimePlatform.IPhonePlayer ):
					m_vScrollPosition.x -= Event.current.delta.x;
					m_vScrollPosition.y += Event.current.delta.y;
					break;
				default:
					m_vScrollPosition -= Event.current.delta;
					break;
			}
		}
		/*
		 * 限界とか気にしないで値を突っ込んでも大丈夫だった
		 * 正しい限界値も勘で設定していたので「何もしない」でやる方法で行きます
		 */
		m_vScrollPosition += m_vDeltaPosition;
		//m_vScrollPosition.x = Mathf.Clamp( m_vScrollPosition.x + m_vDeltaPosition.x, 0, _scrollLimit.x );
		//m_vScrollPosition.y = Mathf.Clamp( m_vScrollPosition.y + m_vDeltaPosition.y, 0, _scrollLimit.y );
		//m_vDeltaPosition.x = 0;
		//m_vDeltaPosition.y = 0;

		//if ( m_bLastPos )
		//{
		//	m_bLastPos = false;
		//	m_vScrollPosition.y = _limit_y;
		//}

		// スクロールビューの開始位置を作成する
		m_vScrollPosition = GUI.BeginScrollView( m_vWindowRect, m_vScrollPosition, _scrollRect );
		//
		GUI.Label( _scrollRect, _text, _style );
		//
		GUI.EndScrollView();
	}


	///// <summary>
	///// 描画は GUI 上で行います
	///// </summary>
	//private void _onGui2()
	//{
	//	{
	//		//GUI.ModalWindow( 1, new Rect( 20, 50, 100, 100 ), _windowFunc, new GUIContent( "ModalWindow" ) );
	//		////GUI.ModalWindow( 2, new Rect( 20, 160, 100, 100 ), _windowFunc, new GUIContent( "ModalWindow2" ) );
	//	}


	//	/*
	//	 * ウィンドウのサイズ（引数で貰うべきだな）
	//	 */
	//	Rect _windowRect = new Rect( 0, 0, Screen.width, Screen.height );

	//	/*
	//	 * ウィンドウに表示したい行数
	//	 */
	//	const int _rows = 32;

	//	/*
	//	 * バーの太さ、取得する関数が無いっぽいので決め打ち
	//	 * 実際画面サイズにかかわらず一定のドッド？になるっぽい
	//	 */
	//	const float _bar_width = 0.0f;

	//	/*
	//	 * スクロールする画面のサイズ（高さはこれから決めるよ）
	//	 */
	//	Rect _scrollRect = new Rect( 0, 0, _windowRect.width - _bar_width, 0 );

	//	/*
	//	 * 表示したい行数に合わせてフォントサイズを決める
	//	 */
	//	float _fontSize = _windowRect.height / _rows;

	//	/*
	//	 * ウィンドウに表示できる列数
	//	 * が、残念ながら文字列からドッド数？を求める方法が無いっぽいのでこの処理は無意味かもしれない
	//	 */
	//	//int _columns = ( int ) ( _scrollRect.width / _fontSize );
	//	int _columns = 10000000;



	//	/*
	//	 * 文字列を成形する
	//	 */
	//	List<string> _list = new List<string>();
	//	string _text = "";
	//	//
	//	foreach ( _Data _data in m_vList )
	//	{
	//		string _string =
	//				_data.m_vDateTime.ToString( @"MM/dd HH:mm:ss | " ) +
	//				_data.m_vTag + " | " +
	//				_data.m_vMessage;
	//		//
	//		string[] _lines = _string.Split( '\n' );
	//		//
	//		foreach ( string _line in _lines )
	//		{
	//			string __line = _line;

	//			while ( __line.Length > 0 )
	//			{
	//				if ( __line.Length < _columns )
	//				{
	//					_list.Add( __line );
	//					_text += __line + "\n";
	//					break;
	//				}
	//				//
	//				_list.Add( __line.Substring( 0, _columns ) );
	//				_text += __line.Substring( 0, _columns ) + "\n";
	//				__line = __line.Substring( _columns );
	//			}
	//		}
	//	}
	//	//
	//	GUIStyle _style = new GUIStyle();
	//	_style.fontSize = ( int ) _fontSize - 1;
	//	_style.normal.textColor = Color.red;
	//	Vector2 _vector = _style.CalcSize( new GUIContent( _text ) );


	//	/*
	//	 * やっとスクロールサイズの高さが決定！
	//	 */
	//	//_scrollRect.height = _list.Count * _fontSize;
	//	_scrollRect.width = _vector.x;
	//	_scrollRect.height = _vector.y;
	//	//
	//	float _limit_y = Mathf.Max( 0, _scrollRect.height - _windowRect.height );


	//	m_vScrollPosition.y = Mathf.Clamp( m_vScrollPosition.y + m_vDeltaPosition.y, 0, _limit_y );

	//	m_vDeltaPosition.y = 0;

	//	if ( m_bLastPos )
	//	{
	//		m_bLastPos = false;

	//		//scrollViewVector.y = scrollViewAllRect.height - scrollViewRect.height;
	//		//if(scrollViewVector.y < 0)
	//		//{
	//		//	scrollViewVector.y = 0;
	//		//}

	//		m_vScrollPosition.y = _limit_y;

	//		ScDebug.debugLog( ">>>>>>>>>>>" + m_vScrollPosition.y.ToString() );

	//	}




	//	// スクロールビューの開始位置を作成する
	//	m_vScrollPosition = GUI.BeginScrollView( _windowRect, m_vScrollPosition, _scrollRect );

	//	{
	//		GUI.Label( _scrollRect, _text, _style );
	//	}

	//	//{
	//	//	string _string = "";
	//	//	//
	//	//	foreach ( string __string in _list )
	//	//	{
	//	//		_string += __string + "\n";
	//	//	}
	//	//	//
	//	//	//GUIStyle _style = new GUIStyle();
	//	//	//_style.fontSize = ( int ) _fontSize - 1;
	//	//	//_style.normal.textColor = Color.red;
	//	//	//
	//	//	GUI.Label( _scrollRect, _string, _style );
	//	//}



	//	//{
	//	//	//		scrollViewVector = GUILayout.BeginScrollView()

	//	//	// ボックスを作成

	//	//	//GUI.Box( new Rect( 50, 50, 200, 20 ), "Please select" );

	//	//	//foreach ( _Data _data in m_vList )
	//	//	for ( int _count = 0; _count < _item_num; _count++ )
	//	//	{
	//	//		GUIStyle _style = new GUIStyle();
	//	//		_style.fontSize = ( int ) _fontSize - 1;
	//	//		_style.normal.textColor = Color.red;

	//	//		//GUILayout.Label( _data.m_vMessage, new GUIStyle() );
	//	//		GUI.Label(
	//	//			new Rect( 0, _fontSize * _count, 100, _fontSize ),
	//	//			new GUIContent(
	//	//				m_vList[ _count ].m_vDateTime.ToString( @"MM/dd HH:mm:ss | " ) +
	//	//				m_vList[ _count ].m_vMessage ),
	//	//			_style
	//	//		);
	//	//	}

	//	//	//d.TimeStamp.ToString("MM/dd HH:mm:ss \t ") 

	//	//	//// Button

	//	//	//GUI.Button( new Rect( 50, 80, 100, 100 ), contents[ 0 ] );

	//	//	//GUI.Button( new Rect( 50, 180, 180, 100 ), contents[ 1 ] );

	//	//	//GUI.Button( new Rect( 50, 280, 180, 100 ), contents[ 2 ] );

	//	//	//GUI.Button( new Rect( 50, 380, 180, 100 ), contents[ 3 ] );

	//	//	//GUI.Button( new Rect( 50, 480, 180, 100 ), contents[ 4 ] );



	//	//	// スクロールビューの終了位置を作成する 

	//	//	//GUILayout.EndScrollView();
	//	//}
	//	GUI.EndScrollView();
	//}


	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private int _calcFont( float vHeight )
	{
		GUIStyle _style = new GUIStyle();
		//
		const int _limit = 32;
		//
		for ( int _count = 0; _count < _limit; _count++ )
		{
			_style.fontSize = _count + 1;
			Vector2 _vector = _style.CalcSize( new GUIContent( "あ" ) );
			if ( _vector.y > vHeight )
			{
				return ( _count );
			}
		}
		//
		return ( _limit );
	}

	private void _start()
	{
		{
			// 実験
			GUIStyle _style = new GUIStyle();
			_style.fontSize = 8;
			_style.normal.textColor = Color.green;
			Vector2 _vector = _style.CalcSize( new GUIContent( "あ\nあ" ) );


			ScDebug.debugLog( "文字の高さ > " + _vector.y.ToString() );
		}
	}

	/// <summary>
	/// 画面をタッチしたりしてレベルの変更とかしたいな
	/// </summary>
	private void _update()
	{
		if ( Input.GetMouseButtonDown( 0 ) )
		{
			V( "tab_2", "おされた！" );
		}


		if ( Input.touchCount > 0 )
		{
			//	V( "tab_2", "タッチされたよ" );

			//if ( Input.GetTouch( 0 ).phase == TouchPhase.Moved )
			{
				//V( "tab_2", "フリック" );

				//m_vDeltaPosition += Input.GetTouch( 0 ).deltaPosition;
				m_vDeltaPosition.x -= Input.GetTouch( 0 ).deltaPosition.x;
				m_vDeltaPosition.y += Input.GetTouch( 0 ).deltaPosition.y;

				/*
				 * 座標は右上にプラスのようです
				 * 1,0 / 1,1
				 * 0,0 / 0,1
				 */
				//V( "tab_3", "移動量 > " + m_vDeltaPosition.ToString() );
			}
		}

		//if ( Input.  GetMouseButtonDown( 0 ) )
		//{
		//	Debug.Log( Input.mousePosition );
		//}


	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //


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

	void OnGUI()
	{
		if ( m_bActive )
		{
			_onGui();
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// ログの追加の基本関数
	/// </summary>
	/// <param name="vLevel"></param>
	/// <param name="vTag"></param>
	/// <param name="vMessage"></param>
	private void _add( ScLog._Level vLevel, string vTag, string vMessage )
	{
		//ScDebug.debugLog( vMessage );
		//vMessage = ScGui.autoLinefeed( Screen.width / 2, vMessage, m_vFontSize );
		//ScDebug.debugLog( vMessage );

		//m_vList.Add( new _Data( vLevel, vTag, vMessage ) );
		m_vList.Insert( 0, new _Data( vLevel, vTag, vMessage ) );
		//m_bLastPos = true;
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	public void V( string vTag, string vMessage )
	{
		_add( ScLog._Level.Verbose, vTag, vMessage );
	}

	public void E( string vTag, string vMessage )
	{
		_add( ScLog._Level.Error, vTag, vMessage );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// ログのデータを保持するクラス
	/// </summary>
	private class _Data
	{
		public ScLog._Level m_vLevel;
		public string m_vTag;
		public string m_vMessage;
		public System.DateTime m_vDateTime;

		public _Data( ScLog._Level vLevel, string vTag, string vString )
		{
			m_vLevel = vLevel;
			m_vTag = vTag;
			m_vMessage = vString;
			m_vDateTime = System.DateTime.Now;
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 実験用
	/// </summary>
	public static void test()
	{
		ScDebug.debugLog( "Screen.width >> " + Screen.width.ToString() );
		ScDebug.debugLog( "Screen.height >> " + Screen.height.ToString() );

		ScLog _log = ScLog.Create();

		_log.V( "あああ", "          \n0123456789\n０１２３４５６７８９" );
		_log.V( "tab_1", "msg_長い文字--------------------------------------------------" );
		_log.V( "tab_1", "０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９" );


		//for ( int _count = 0; _count < 50; _count++ )
		//{
		//	_log.V( "tab_1", "msg_" + _count.ToString() );
		//}

		//_log.V( "tab_1", "msg_1" );
		//_log.V( "tab_1", "msg_2" );
		//_log.V( "tab_1", "msg_3" );
		//_log.V( "tab_1", "msg_4" );
		//_log.V( "tab_1", "msg_5" );
	}

	// ========================================================================== //
	// ========================================================================== //
}
