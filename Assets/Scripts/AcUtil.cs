using UnityEngine;
using System.Collections;
//using System.ComponentModel;

// C# FileStream / Stream
using System.IO;

// C# IFormatter
using System.Runtime.Serialization;

// C# BinaryFormatter
using System.Runtime.Serialization.Formatters.Binary;

//public static class AcUtil : object
//public static class AcUtil : System.Object
/// <summary>
/// 便利なスタティックメソッドをまとめるクラスだよ
/// "Ac" じゃなくて "Sc" にしてライブラリにするイメージで作るよ！
/// </summary>
public class AcUtil : object
{

	// ========================================================================== //
	// ========================================================================== //
	/*
	 * このクラスは、スタティックな便利ツールをまとめるよ
	 * 
	 * static クラスは MonoBehaviour では無く object を継承するようです (大文字の Object ではないらしい)
	 * MonoBehaviour にしたら Unity さんから object にしろってエラーが出ました
	 * でもコレって型の名前だろ？そもそも何も継承しない場合と何か違うのか？
	 * 多分 "System.Object" から継承するのが正しいみたい
	 * ただ "Object" と書くと "UnityEngine.Object" になるようです
	 * 
	 * 2014/11/07
	 * 多少認識が変わったのでメモします
	 * オブジェクト（？）にくっつけるのは MonoBehaviour
	 * くっつけないのは Object（UnityEngine.Object）
	 * object (System.Object) は基本使わんほうが良いみたい
	 * （Unity のソースコードは他で使わんから Unity の基底クラス？で作ったほうが余計な面倒が無さそう）
	 * static のクラスはそもそも作らない方が良いと思う
	 * （あまり意味が無いと思うし、わざわざ汎用性？を無くすことも無いと思います）
	 */

	/*
	 * http://code.msdn.microsoft.com/windowsdesktop/2-static-8c0cc826
	 * C# では static なクラスを普通に作れるらしいっす
	 * と言うか Java の様な「ファイル名＝クラス名」のルールが無いっぽいっす
	 * 
	 * クラスが static だとメソッドとかも自動的に static になるっぽいってサイトで見た気がしたけど
	 * 明示的に static を付けてないと Unity さんに怒られた
	 */

	/*
	 * 英語の対義語とか調べたいとき
	 * http://www.synonym.com/
	 * 類義語 ( Synonym ) と対義語 ( Antonym ) が調べられて便利。
	 */

	/*
	 * Unity開発者が複数人で開発を進める上で覚えておくと幸せになる９つの事 - テラシュールブログ
	 * http://tsubakit1.hateblo.jp/entry/20140613/1402670011
	 */
	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	///*
	// * シングルトン
	// */
	//private static AcUtil m_vInstance = new AcUtil();

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//private Random  m_vRandom;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	///*
	// * デフォルトコンストラクタ
	// */
	//private AcUtil()
	//{
	//	m_vRandom = new Random();
	//}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public static float getScreenScaleX( int vW )
	{
		return ( ( float ) Screen.width / vW );
	}

	public static float getScreenScaleY( int vH )
	{
		return ( ( float ) Screen.height / vH );
	}

	/*
	 * 
	 * 
	 * 
	 */
	//public static float getScreenScaleSmall( int vW, int vH )
	//{
	//	float scale_x = getScreenScaleX( vW );
	//	float scale_y = getScreenScaleY( vH );
	//	//
	//	return ( ( scale_x < scale_y ) ? scale_x : scale_y );
	//}

	//public static float getScreenScaleLarge( int vW, int vH )
	//{
	//	float scale_x = getScreenScaleX( vW );
	//	float scale_y = getScreenScaleY( vH );
	//	//
	//	return ( ( scale_x > scale_y ) ? scale_x : scale_y );
	//}


	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * ネットワーク接続の判定
	 * http://blog.doinet.co.jp/?eid=1031773
	 * 
	 * Application.internetReachability == NetworkReachability.NotReachable
	 * 
	 * NetworkReachability.NotReachable;//つながっていない
	 * NetworkReachability.ReachableViaCarrierDataNetwork;//つながっている
	 * NetworkReachability.ReachableViaLocalAreaNetwork;//つながっている(wifi)
	 * 
	 * うまくいかない場合
	 * http://spphire9.wordpress.com/2011/10/24/unity%E3%81%A7android%E3%81%8C%E3%83%8D%E3%83%83%E3%83%88%E3%83%AF%E3%83%BC%E3%82%AF%E3%81%AB%E3%81%A4%E3%81%AA%E3%81%8C%E3%81%A3%E3%81%A6%E3%81%84%E3%82%8B%E3%81%8B%E8%AA%BF%E3%81%B9%E3%82%8B/
	 * 
	 * マニフェストファイルのおき方
	 * http://nantara.jp/blog/?p=160
	 * 
	 * INTERNETパーミッションが勝手につく等，UnityとAndroidManifestのよくわからないところ 
	 * http://qiita.com/RyotaMurohoshi/items/c8aa78d5db13c31da152
	 * 
	 * うまくいかなかった原因
	 * 実機の wifi が切られてた！← 半日潰したよ！
	 */
	public static bool isOnLine()
	{
		return ( Application.internetReachability != NetworkReachability.NotReachable );
		//		return ( true );
	}


	/// <summary>
	/// メインスレッドからしか呼べない
	/// </summary>
	/// <returns></returns>
	public static string getLanguageSuffix()
	{
		string _string = null;

		/*
		 * http://pinbit.blog.jp/archives/archives/cat_246587.html/5450017.html
		 * 
		 * get_systemLanguage can only be called from the main thread.
		 */
		//Debug.Log( Application.systemLanguage.ToString() );


		/*
		 * 名前の付け方の参考
		 * http://ameblo.jp/miwawa08/entry-11440695530.html
		 * http://ameblo.jp/miwawa08/entry-11440695530.html
		 */
		switch ( Application.systemLanguage )
		{
			//
			case ( SystemLanguage.Japanese ):
				_string = "-ja"; // 日本は "jp" じゃないのね・・・
				break;
			//
			default:
				_string = "";
				break;
		}
		//
		//Debug.Log( _string );
		//
		return ( _string );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// １フレームが 1/60 って前提でフレーム数をミリセカンドに変換するよ！
	/// </summary>
	/// <param name="vFrame"></param>
	/// <returns></returns>
	public int frame2millisecond( int vFrame )
	{
		return ( ( int ) ( vFrame * 1000.0f * 1.0f / 60 ) );
	}

	// ========================================================================== //
	// ========================================================================== //




	// ========================================================================== //
	// ========================================================================== //

	/*
	 * ＠IT：.NET TIPS 確実な終了処理を行うには？ - C#
	 * http://www.atmarkit.co.jp/fdotnet/dotnettips/027dispose/dispose.html
	 * using(...){} の話
	 */

	/*
	 * StreamWriter クラス (System.IO)
	 * http://msdn.microsoft.com/ja-jp/library/system.io.streamwriter(v=vs.110).aspx
	 * 
	 * 方法 テキスト ファイルに書き込む (C# プログラミング ガイド)
	 * http://msdn.microsoft.com/ja-jp/library/8bh11f1k.aspx
	 * 
	 * テキスト・ファイルの内容を簡単に書き込むには？［2.0のみ、C#、VB］ － ＠IT
	 * http://www.atmarkit.co.jp/fdotnet/dotnettips/680filewriteall/filewriteall.html
	 */

	/// <summary>
	/// ファイルの書き込み
	/// </summary>
	/// <param name="vPath"></param>
	/// <param name="vText"></param>
	/// <param name="bAppend"></param>
	public static void writeFile( string vPath, string vText, bool bAppend )
	{
		FileInfo _fileInfo = new FileInfo( vPath );
		//
		Directory.CreateDirectory( _fileInfo.Directory.FullName );
		//
		using ( StreamWriter _writer = new StreamWriter( vPath, bAppend ) )
		{
			_writer.Write( vText );
		}

		//FileInfo _fileInfo = new FileInfo( vPath );
		////
		//Directory.CreateDirectory( _fileInfo.Directory.FullName );
		////
		//if ( bAppend )
		//{
		//	File.AppendAllText( vPath, vText );
		//}
		//else
		//{
		//	File.WriteAllText( vPath, vText );
		//}
	}

	/*
	 * StreamReader クラス (System.IO)
	 * http://msdn.microsoft.com/ja-jp/library/system.io.streamreader(v=vs.110).aspx
	 * 
	 * テキスト・ファイルの内容を読み込むには？［C#、VB］ － ＠IT
	 * http://www.atmarkit.co.jp/fdotnet/dotnettips/036fileread/fileread.html
	 */

	/// <summary>
	/// ファイルの読み込み
	/// 戻り値は null も有ります（この手のヤツは "" の方が良いのかって疑問が常にあるが・・・）
	/// </summary>
	/// <param name="vPath"></param>
	/// <returns></returns>
	public static string readFile( string vPath )
	{
		string _text = null;
		//
		using ( StreamReader _reader = new StreamReader( vPath) )
		{
			_text = _reader.ReadToEnd();
		}
		//
		return ( _text );
	}

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * シリアル化
	 * http://msdn.microsoft.com/ja-jp/library/4abbf6k0(v=vs.90).aspx
	 */

	/*
	 * using
	 * http://www.atmarkit.co.jp/fdotnet/dotnettips/027dispose/dispose.html
	 */

	/// <summary>
	/// シリアライズ（オブジェクト → ファイル）
	/// </summary>
	/// <param name="vFile"></param>
	/// <param name="vObject"></param>
	/// <returns></returns>
	public static object writeObject( string vFile, object vObject )
	{
		ScDebug.debugLog( "writeObject >> " + vFile );

		if ( vFile != null )
		{
			if ( vObject != null )
			{
				/*
				 * FileInfo
				 * http://jeanne.wankuma.com/tips/csharp/path/getdirectoryname.html
				 */
				FileInfo _fileInfo = new FileInfo( vFile );
				//
				Directory.CreateDirectory( _fileInfo.Directory.FullName );
				//
				using ( Stream _stream = new FileStream( vFile, FileMode.Create, FileAccess.Write, FileShare.None ) )
				{
					IFormatter _formatter = new BinaryFormatter();
					/*
					 * これでいいのか疑問
					 * http://homepage2.nifty.com/nonnon/SoftSample/CS.NET/SampleException.html
					 * http://devlights.hatenablog.com/entry/20120406/p2
					 */
					try
					{
						_formatter.Serialize( _stream, vObject );
					}
					catch ( SerializationException ex )
					{
						ScDebug.debugLog( "SerializationException >> " + ex.Message );
					}
				}
			}
		}
		//
		return ( vObject );
	}

	/// <summary>
	/// デシリアライズ（ファイル → オブジェクト）
	/// </summary>
	/// <param name="vFile"></param>
	/// <returns></returns>
	public static object readObject( string vFile )
	{
		ScDebug.debugLog( "readObject >> " + vFile );

		object _object = null;
		//
		if ( vFile != null )
		{
			if ( File.Exists( vFile ) )
			{
				using ( Stream _stream = new FileStream( vFile, FileMode.Open, FileAccess.Read, FileShare.Read ) )
				{
					IFormatter _formatter = new BinaryFormatter();
					/*
					 * これでいいのか疑問
					 * http://homepage2.nifty.com/nonnon/SoftSample/CS.NET/SampleException.html
					 * http://devlights.hatenablog.com/entry/20120406/p2
					 */
					try
					{
						_object = _formatter.Deserialize( _stream );
					}
					catch ( SerializationException ex )
					{
						ScDebug.debugLog( "SerializationException >> " + ex.Message );
						//
						_object = null;
					}
				}
			}
		}
		//
		return ( _object );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //


	/*
	 * FileInfo
	 * http://jeanne.wankuma.com/tips/csharp/path/getdirectoryname.html
	 */

	public static bool isFileExists( string vPath )
	{
		return ( File.Exists( vPath ) );
	}

	public static bool isDirectoryExists( string vPath )
	{
		return ( Directory.Exists( vPath ) );
	}

	public static string getDirectory( string vPath )
	{
		if ( isDirectoryExists( vPath ) )
		{
			return ( vPath );
		}
		//
		if ( isFileExists( vPath ) )
		{
			FileInfo _fileInfo = new FileInfo( vPath );
			//
			return ( _fileInfo.Directory.FullName );
		}
		//
		return ( null );
	}

	public static void createDirectory( string vPath )
	{
		if ( !isDirectoryExists( vPath ) && !isFileExists( vPath ) )
		{
			Directory.CreateDirectory( vPath );
		}
	}

	public static void deleteDirectory( string vPath )
	{
		if ( isDirectoryExists( vPath ) )
		{
			Directory.Delete( vPath, true ); // true しないとカラのフォルダしか削除出来ないそうです
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * ↓使わないよ
	 * try catch をやめて using を使おう！
	 */

	public static object writeObject2( string vFile, object vObject )
	{
		ScDebug.debugLog( "writeObject >> " + vFile );

		if ( vFile != null )
		{
			if ( vObject != null )
			{
				/*
				 * FileInfo
				 * http://jeanne.wankuma.com/tips/csharp/path/getdirectoryname.html
				 */
				FileInfo _fileInfo = new FileInfo( vFile );
				////
				//Debug.Log( "_fileInfo.DirectoryName >> " + _fileInfo.DirectoryName );
				//Debug.Log( "_fileInfo.Directory.FullName >> " + _fileInfo.Directory.FullName );
				////
				Directory.CreateDirectory( _fileInfo.Directory.FullName );

				//				createDirectory( vFile );

				//if (! File.Exists(vFile) )
				//{
				//	Debug.Log(vFile + "がないので作ってみる");
				//	File.Create( vFile );
				//	Debug.Log( vFile + "が出来たっぽい" );
				//}

				try
				{
					IFormatter _formatter = new BinaryFormatter();
					Stream _stream = new FileStream( vFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite ); //  FileShare.None 
					_formatter.Serialize( _stream, vObject );
					_stream.Close();
				}
				catch ( System.Exception e )
				{
					ScDebug.debugLog( "writeObjec で何かエラー" + e );
				}
			}
		}
		//
		return ( vObject );
	}


	public static object readObject2( string vFile )
	{
		ScDebug.debugLog( "readObject >> " + vFile );

		object _object = null;

		if ( vFile != null )
		{
			//	if ( File.Exists( vFile ) )
			{
				Stream _stream = null;
				try
				{
					IFormatter _formatter = new BinaryFormatter();
					_stream = new FileStream( vFile, FileMode.Open, FileAccess.Read, FileShare.Read );
					_object = _formatter.Deserialize( _stream );
				}
				catch ( System.Exception e )
				{
					ScDebug.debugLog( "readObject で何かエラー" + e );
				}
				finally
				{
					if ( _stream != null )
					{
						_stream.Close();
					}
				}
			}
		}
		//
		return ( _object );
	}


	// ========================================================================== //
	// ========================================================================== //

	//// Use this for initialization
	//static void Start()
	//{

	//}

	//// Update is called once per frame
	//static void Update()
	//{

	//}

	// ========================================================================== //
	// ========================================================================== //

}
