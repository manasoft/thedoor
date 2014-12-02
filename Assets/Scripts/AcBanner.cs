using UnityEngine;
using System.Collections;

// C# のスレッドを使う！
using System.Threading;

// C# の WebClient / HttpWebRequest 用です
using System.Net;

// Uri
using System;

// AsyncCompletedEventArgs
//using System.ComponentModel;

// FileStream
using System.IO;

/// <summary>
/// 広告を管理するクラス
/// 自前処理（スレッドで Web から画像読み込みしたり）でバナー広告を出す処理
/// 
/// しかし、広告は、そういった専門の会社が出しているパッケージ（？）を使うことにしたので使わなくなりました
/// </summary>
public class AcBanner
//public class AcAd : object
//public class AcAd : MonoBehaviour
//public class AcAd : UnityEngine.Object
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * スレッドを使うよ！
	 * using System.Threading;
	 * が必要！
	 * 
	 * 保存パス
	 * ↓ windows 上での結果です
	 * Debug.Log( Application.dataPath );				// C:/workshop/project/Unity/TheDoor/Assets
	 * Debug.Log( Application.persistentDataPath );		// C:/Users/sirasawa/AppData/LocalLow/Manasoft/TheDoor
	 * Debug.Log( Application.temporaryCachePath );		// C:/Users/sirasawa/AppData/Local/Temp/Manasoft/TheDoor
	 * 
	 * ※注意：上記のパス取得メソッドはメインスレッドじゃないとエラーになります！
	 * ※重要：コンストラクタは別スレッドらしい
	 * 
	 */

	/*
	 * C# の機能で画像のダウンロード
	 * 
	 * WebClient
	 * http://www.ipentec.com/document/document.aspx?page=csharp-web-download-image-file-using-webclient&culture=ja-jp
	 * http://www.woodensoldier.info/computer/csharptips/153.htm
	 * HttpWebRequest
	 * http://www.ipentec.com/document/document.aspx?page=csharp-web-download-image-file-using-httpwebrequest
	 */

	/*
	 * Unity の機能で画像のダウンロード
	 * Unity には WWW ってクラスがあるけど別スレッドには出来ない → 処理が止まっちゃう！
	 * WWW www = new WWW( "http://ec3.images-amazon.com/images/I/616aLDtPn3L.jpg" );
	 * テクスチャ  = www.texture
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
	 * コルーチンのまとめ？
	 * http://izmiz.hateblo.jp/entry/2014/08/19/212641
	 * 
	 * コルーチン、別の呼び出し方
	 * http://posposi.blog.fc2.com/blog-entry-227.html
	 */

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// ウェブから読み込むテキストの代用品
	/// </summary>
	private string _textTbl =
		//
		"http://www.honda.co.jp/motor-lineup/img/btn_bike_gw.jpg" + "," +
		"http://www.honda.co.jp/GOLDWING/" + "," +
		//
		"http://www.honda.co.jp/motor-lineup/img/btn_bike_cb1300sb.jpg" + "," +
		"http://www.honda.co.jp/CB1300/" + "," +
		//
		"http://www.honda.co.jp/motor-lineup/img/btn_bike_nc750x.jpg" + "," +
		"http://www.honda.co.jp/NC750X/";

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * ダブルバッファー
	 */
	//private const int _BUFFER_A = 0;
	//private const int _BUFFER_B = 1;
	//private const int _BUFFER_NUM = 2;

	//	private const string _FILE_PATH = Application.temporaryCachePath;

	/// <summary>
	/// サブディレクトリ名（m_vPath はここまでを保持する）
	/// </summary>
	private const string _SUBDIR = "/ad";

	/// <summary>
	/// 保存するファイル名
	/// </summary>
	private const string _SAVEFILE = "/ad.dat";

	/// <summary>
	/// 保存する広告画像のファイル名（の前半）
	/// </summary>
	private const string _PREFIX = "/ad_";

	/// <summary>
	/// 保存する広告画像のファイル名（の後半）
	/// </summary>
	private const string _SUFFIX = ".png";

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// シングルトン
	/// </summary>
	private static AcBanner m_vInstance = null;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 保存ディレクトリまでのパスを保持する
	/// </summary>
	private string m_vPath;

	/// <summary>
	/// ネットにつながっているか？
	/// </summary>
	//	private bool m_bOnLine;

	/// <summary>
	/// スレッドによるロードが終了したら true になる、データの取得が出来ない場合も true になるので注意！
	/// </summary>
	private bool m_bLoaded;

	/// <summary>
	/// データの読み込みをスレッドで行う
	/// </summary>
	private Thread m_vThread;

	/// <summary>
	/// 広告画像のテクスチャを保持する
	/// </summary>
	private Texture m_vTexture;

	/// <summary>
	/// 広告のエリア（一応動的な変化に対応）
	/// </summary>
	private Rect m_vRect;

	/// <summary>
	/// 表示している広告
	/// </summary>
	private int m_vIndex;

	/// <summary>
	/// 広告の情報 _Data を管理する
	/// </summary>
	//	private ArrayList m_vArrayList;

	/// <summary>
	/// 
	/// </summary>
	private _Save m_vSave;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 変数の初期化
	/// </summary>
	private void _ini()
	{
		m_vPath = null;
		//
		//		m_bOnLine = false;
		m_bLoaded = false;
		//
		m_vThread = null;
		//
		m_vTexture = null;
		m_vRect = new Rect(); // null 非許容型です
		//
		m_vIndex = 0;
		//		m_vArrayList = new ArrayList();
		//
		m_vSave = null;

		{
			/*
			 * 2014/10/14
			 * WWW を使えばネットに繋がるか試してみる
			 * ダメだった！
			 */
			//	WWW _www = new WWW( "" );
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="vPath"></param>
	private AcBanner( string vPath )
	{
		_ini();
		//
		m_vPath = vPath;
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * http://blogs.yahoo.co.jp/nanashi_hippie/52586913.html
	 */

	/// <summary>
	/// パスからテクスチャを取得する
	/// AcUtil にいれた方がいいな
	/// </summary>
	/// <param name="vPath"></param>
	/// <returns></returns>
	public static Texture getTexture( string vPath )
	{
		FileStream _fileStream = new FileStream( vPath, FileMode.Open );
		BinaryReader _binaryReader = new BinaryReader( _fileStream );
		byte[] _bytes = _binaryReader.ReadBytes( ( int ) _binaryReader.BaseStream.Length );
		_binaryReader.Close();
		//
		Texture2D _texture = new Texture2D( 0, 0 );
		_texture.LoadImage( _bytes );
		//
		return ( _texture );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// ファイルを読み込むスレッド
	/// </summary>
	private void _thread()
	{
		m_vSave = new _Save();

		//
		AcUtil.deleteDirectory( m_vPath );
		AcUtil.createDirectory( m_vPath );

		//
		string[] _lines = _textTbl.Split( ',' );
		//
		for ( int count = 0; count < _lines.Length; count += 2 )
		{
			ScDebug.debugLog( _lines[ count + 0 ] + " / " + _lines[ count + 1 ] );

			//					m_vArrayList.Add( new _Data( _lines[ count + 0 ], _lines[ count + 1 ], m_vPath + _FILE + count + ".png" ) );
			m_vSave.add( new _Data( _lines[ count + 0 ], _lines[ count + 1 ], m_vPath + _PREFIX + count + _SUFFIX ) );
		}
		//
		AcUtil.writeObject( m_vPath + _SAVEFILE, m_vSave );

		//
		WebClient _webClient = new WebClient();
		//
		//				foreach ( _Data _data in m_vArrayList )
		foreach ( _Data _data in m_vSave.m_vArrayList )
		{
			ScDebug.debugLog( _data.m_vIconUrl + ">>" + _data.m_vIconPath );
			_webClient.DownloadFile( _data.m_vIconUrl, _data.m_vIconPath );
		}

		/*
		 * スレッド終了時にロードフラグを書き換える
		 */
		m_bLoaded = true;
	}

	/// <summary>
	/// 一定時間ごとにテクスチャを切り替えるコルーチン
	/// </summary>
	/// <returns></returns>
	private IEnumerator _coroutineRender()
	{
		const int _limit = 60 * 5;
		int _count = 0;
		_Data _data = null;
		//
		while ( true )
		{
			if ( m_bLoaded )
			{
				/*
				 * m_vSave が null になる条件は？
				 * ・初期化直後
				 * ・通信が出来なくて＆セーブしたデータもない場合
				 */
				if ( m_vSave != null )
				{
					/*
					 * 画面の大きさが変わったてもいいように再計算するよ！
					 * 
					 * この座標系
					 * 0 → +
					 * ↓
					 * +
					 */
					float _w = 480.0f;
					float _h = 80.0f;
					float _scale = Screen.width / _w;
					//
					_w *= _scale;
					_h *= _scale;
					//
					float _x = 0.0f;
					float _y = Screen.height - _h;

					m_vRect.x = _x;
					m_vRect.y = _y;
					m_vRect.width = _w;
					m_vRect.height = _h;

					/*
					 * 画像を変更するよ！
					 */
					if ( ( _count % _limit ) == 0 )
					{
						/*
						 * インデックス等更新
						 */
						_count = 0;
						//					m_vArrayIndex = ( m_vArrayIndex + 1 ) % m_vArrayList.Count;
						m_vIndex = ( m_vIndex + 1 ) % m_vSave.size();

						/*
						 * 
						 */
						//					_data = ( _Data ) m_vArrayList[ m_vArrayIndex ];
						_data = m_vSave.get( m_vIndex );

						/*
						 * テクスチャ更新
						 * 破棄？
						 * http://ft-lab.ne.jp/cgi-bin-unity/wiki.cgi?page=unity_script_texture2d_save_png_file
						 * 
						 * サウンドマネージャーでバグったので処理するのやめておきます
						 * でタイトルで放置してたら画面がグチャグチャっぽくなったりしたので復帰
						 */
						UnityEngine.Object.DestroyImmediate( m_vTexture );
						m_vTexture = getTexture( _data.m_vIconPath );
					}

					/*
					 * 広告の範囲がタッチされていれば url にジャンプするよ！
					 */
					if ( _data != null )
					{
						// http://buravo46.tumblr.com/post/70380413072/unity-prefab
						//
						if ( Input.GetMouseButtonDown( 0 ) )
						{
							//			m_bInput = true;
							/*
							 * この時の座標は
							 * +
							 * ↑
							 * 0 → +
							 */
							Vector3 _vector3 = Input.mousePosition;

							ScDebug.debugLog( "x = " + _vector3.x );
							ScDebug.debugLog( "y = " + _vector3.y );
							ScDebug.debugLog( "z = " + _vector3.z );

							/*
							 * ブラウザを開く
							 * http://narudesign.com/devlog/unity-openurl/
							 */
							float _new_x = _vector3.x;
							float _new_y = Screen.height - _vector3.y;

							if ( ( _x <= _new_x ) && ( _new_x <= ( _x + _w ) ) && ( _y <= _new_y ) && ( _new_y <= ( _y + _h ) ) )
							{
								Application.OpenURL( _data.m_vOpenUrl );
							}
						}
					}
					//
					_count++;
				}
				//
				yield return null;
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //


	private void _dataByThread()
	{
		// スレッドで読み込みを開始する
		m_vThread = new Thread( new ThreadStart( _thread ) );
		m_vThread.Start();
	}

	private void _dataByStrage()
	{
		// ロード済みのデータを使うことにする
		m_vSave = ( _Save ) AcUtil.readObject( m_vPath + _SAVEFILE );
		/*
		 * 以下は "m_vSave == null" でデータがない場合は描画コルーチンを抜ける様にしたので不要です
		 */
		//if ( m_vSave == null )
		//{
		//	// ロードできない場合はカラを付けておく
		//	m_vSave = new _Save();
		//}
	}

	private void _startCoroutine( MonoBehaviour vMonoBehaviour )
	{
		vMonoBehaviour.StartCoroutine( _coroutineRender() );
	}
	// ========================================================================== //
	// ========================================================================== //

	public static bool isLoaded()
	{
		return ( m_vInstance.m_bLoaded );
	}

	public static void onStart( MonoBehaviour vMonoBehaviour )
	{
		/*
		 * 最初の１回だけ行うこと
		 * ・判定はインスタンスは null かどうかでチェックする
		 */
		if ( m_vInstance == null )
		{
			string _path = Application.persistentDataPath + _SUBDIR;
			//
			m_vInstance = new AcBanner( _path );
		}

		//
		if ( !m_vInstance.m_bLoaded )
		{
			// 通信できるか？
			if ( AcUtil.isOnLine() )
			{
				m_vInstance._dataByThread();
				//
				// ロードフラグの設定はスレッド内で行っている
			}
			else
			{
				// コレを失敗しても対処出来ない・・・
				m_vInstance._dataByStrage();
				// ロードフラグの設定
				m_vInstance.m_bLoaded = true;
			}
		}
		//
		m_vInstance._startCoroutine( vMonoBehaviour );
	}

	/// <summary>
	/// OnGUI() で呼び出してもらう
	/// </summary>
	public static void onGui()
	{
		if ( m_vInstance.m_vTexture != null )
		{
			GUI.DrawTextureWithTexCoords( m_vInstance.m_vRect, m_vInstance.m_vTexture, new Rect( 0, 0, 1, 1 ) );
		}
	}

	public static void onApplicationQuit()
	{
		if ( m_vInstance.m_vThread != null )
		{
			ScDebug.debugLog( "スレッド終了するよ" );
			m_vInstance.m_vThread.Abort();
			m_vInstance.m_vThread = null;
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //







	//	private void _thread()
	//	{
	//		string[][] _webTbl = 
	//		{
	//			new string[] {"http://ec3.images-amazon.com/images/I/616aLDtPn3L.jpg","http://ec3.images-amazon.com/images/I/616aLDtPn3L.jpg",},
	//			new string[] {"http://www.woodensoldier.info/images/WsLogoLarge.gif","http://www.woodensoldier.info/images/WsLogoLarge.gif",},
	//		};

	//		int _index = 0;

	//		//WebClient _webClient = new WebClient {
	//		//};
	//		//
	//		while ( true )
	//		{
	////			_webClient.DownloadDataAsync


	//			//	Debug.Log( "スレッド実行" );
	//			//
	//			Thread.Sleep( 1000 );
	//		}
	//	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //


	/*
	 * <summry>
	 * 時間毎にテクスチャを切り替えるコルーチン
	 */


	/*
	 * コメントの書き方
	 * http://www.atmarkit.co.jp/fdotnet/teamdev/teamdev02/teamdev02_01.html
	 */

	/// <summary>
	/// コメントの書き方実験 hoge_1 です
	/// <paramref name=">"/>
	/// </summary>
	private void _hoge_1()
	{
	}

	/// <summary>
	/// Calculatorクラスの概要･･･▼
	/// 
	/// <code>
	/// あああ<br/>
	/// いいい
	/// </code>
	/// <newpara>
	/// 四則演算を行うためのクラスです。
	/// </newpara>
	/// <newpara>
	/// 四則演算を行うためのクラスです。
	/// </newpara>
	/// </summary>
	/// <remarks>
	/// Calculatorクラスの解説･･･▼
	/// <newpara>
	/// 四則演算の足し算［＋］、引き算［－］、掛け算［×］、割り算［÷］を行うメソッドがあります。
	/// </newpara>
	/// </remarks>
	private void _hoge_2()
	{
		_hoge_1();
		_hoge_2();
	}




	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//private IEnumerator _coroutineRead()
	//{
	//	const int _limit = 60 * 5;
	//	int _count = 0;
	//	//
	//	while ( true )
	//	{
	//		if ( (_count % _limit) == 0 )
	//		{
	//			_count = 0;
	//			m_vArrayIndex = ( m_vArrayIndex + 1 ) % m_vArrayList.Count;
	//			//
	//			_Data _data = ( _Data ) m_vArrayList[ m_vArrayIndex ];
	//			//
	//			m_vArrayData[ m_vArrayIndex % _BUFFER_NUM ] = _data;
	//			//
	//			WebClient _webClient = new WebClient();
	//			Uri _uri = new Uri( _data.m_vIconUrl );

	//			// Specify that the DownloadFileCallback method gets called
	//			// when the download completes.
	//			m_vWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler( ( DownloadFileCallback2 ) );
	//			// Specify a progress notification handler.
	//			//		m_vWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler( DownloadProgressCallback );
	//			m_vWebClient.DownloadFileAsync( new Uri( _data.m_vIconUrl ), _data.m_vOpenUrl );

	//			//
	//		}
	//		//
	//		_count++;
	//		//
	//		yield return null;
	//	}
	//}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	//private void DownloadFileCallback2(
	//	System.Object sender,
	//AsyncCompletedEventArgs e
	//	)
	//{
	//	Debug.Log( "読み込んだよ！" );

	//	//		m_bLoaded = true;

	//	// "Images/Door/tx_door"

	//	//		Texture _texture = ( Texture ) Instantiate( Resources.Load( m_vHogePass ));

	//	//Texture2D _texture = new Texture2D(0,0);
	//	//_texture.LoadImage(new Loa)
	//}

	// Use this for initialization
	//	void Start()
	//	{
	//		m_vThread = new Thread( new ThreadStart( _thread ) );
	//		m_vThread.Start();
	//		//
	////		m_vTexture = null;
	//		m_vTexture = ( Texture ) Instantiate( Resources.Load( "Images/Door/tx_door", typeof( Texture ) ) );

	//		//
	//		m_vAdRect = new Rect();

	//		m_vHogePass = Application.temporaryCachePath + "/hoge.png";

	//		m_vWebClient = new WebClient();
	//		Uri uri = new Uri( "http://ec3.images-amazon.com/images/I/616aLDtPn3L.jpg" );

	//		// Specify that the DownloadFileCallback method gets called
	//		// when the download completes.
	//		m_vWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(  ( DownloadFileCallback2 ) );
	//		// Specify a progress notification handler.
	////		m_vWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler( DownloadProgressCallback );
	//		m_vWebClient.DownloadFileAsync( uri, m_vHogePass );

	//		m_bLoaded = false;
	//	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	//private void _onStart( MonoBehaviour vMonoBehaviour )
	//{
	//	Debug.Log( "AcAd # Start()" );

	//	if ( !m_bLoaded )
	//	{
	//		// 通信できるか？
	//		if ( AcUtil.isOnLine() )
	//		{
	//			// スレッドで読み込みを開始する
	//			m_vThread = new Thread( new ThreadStart( _thread ) );
	//			m_vThread.Start();
	//		}
	//		else
	//		{
	//			// ロード済みのデータを使うことにする
	//			m_vSave = ( _Save ) AcUtil.readObject( m_vPath + _SAVEFILE );
	//		}
	//	}
	//	//
	//	vMonoBehaviour.StartCoroutine( _coroutineRender() );
	//}

	//private void _onGUI()
	//{
	//	if ( m_vTexture != null )
	//	{
	//		GUI.DrawTextureWithTexCoords( m_vRect, m_vTexture, new Rect( 0, 0, 1, 1 ) );
	//	}
	//}

	//private void _onApplicationQuit()
	//{
	//	if ( m_vThread != null )
	//	{
	//		Debug.Log( "スレッド終了するよ" );
	//		m_vThread.Abort();
	//		m_vThread = null;
	//	}
	//}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	//// Use this for initialization
	//void Start()
	//{
	//	_onStart( this );
	//}

	//// Update is called once per frame
	//void Update()
	//{
	//	Debug.Log( "AcAd # Update()" );
	//}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/*
	 * OnGUI() の中では描画以外はしないほうが良いみたい
	 * Update() １回にたいして２～４回行われたりする
	 */

	//void OnGUI()
	//{
	//	_onGUI();
	//}

	//void OnApplicationQuit()
	//{
	//	_onApplicationQuit();
	//}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	[Serializable]
	private class _Save
	{
		internal ArrayList m_vArrayList;

		public _Save()
		{
			m_vArrayList = new ArrayList();
		}

		public void add( _Data vData )
		{
			m_vArrayList.Add( vData );
		}

		public _Data get( int vIndex )
		{
			return ( ( _Data ) m_vArrayList[ vIndex ] );
		}

		public int size()
		{
			return ( m_vArrayList.Count );
		}

	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * struct でやりたいんだけど null 非許容になってしまった
	 * マイクロソフトのサイトでは struct は "null 許容型" って行ってるのになんで？
	 * 仕方ないので class にしてます
	 * 
	 * 解決？
	 * http://detail.chiebukuro.yahoo.co.jp/qa/question_detail/q10111379685
	 */
	[Serializable]
	private class _Data
	{
		internal string m_vIconUrl;
		internal string m_vOpenUrl;
		internal string m_vIconPath;

		public _Data( string vIconUrl, string vOpenUrl, string vIconPath )
		{
			m_vIconUrl = vIconUrl;
			m_vOpenUrl = vOpenUrl;
			//
			m_vIconPath = vIconPath;
		}
	}


	//public struct CoOrds
	//{
	//	public int x, y;

	//	public CoOrds( int p1, int p2 )
	//	{
	//		x = p1;
	//		y = p2;
	//	}
	//}

	// ========================================================================== //
	// ========================================================================== //

}
