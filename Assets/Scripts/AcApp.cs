using UnityEngine;
using System.Collections;

// Dictionary
using System.Collections.Generic;

/// <summary>
/// ゲームの定数とかシーンをまたぐ変数をまとめておくクラスだよ
/// </summary>
public class AcApp : Object
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

	/*
	 * レイヤー
	 * http://docs-jp.unity3d.com/Documentation/Components/Layers.html
	 * 
	 * http://www.wisdomsoft.jp/150.html
	 * 
	 * 2014/11/04
	 * カメラの重ねあわせとかで実験したけど取り敢えず使わない予定
	 */

	//public const int OBJECT_LAYER_GAME = 8;
	//public const int OBJECT_LAYER_GUI = 9;

	//public const int CAMERA_LAYERMASK_GAME = 1 << OBJECT_LAYER_GAME;
	//public const int CAMERA_LAYERMASK_GUI = 1 << OBJECT_LAYER_GUI;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// ゲーム用ポジション
	/// </summary>
	public static readonly Vector3 GamePosition = new Vector3( 0.0f, 10.0f, 0.0f );

	/// <summary>
	/// Title / Howtoplay / Ranking 用ポジション
	/// </summary>
	public static readonly Vector3 GuiPosition = new Vector3( 0.0f, 0.0f, 0.0f );

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 仮想画面の大きさ（GUI の位置はこれが基準になるので、途中で変更するとえらい作業が発生します）
	/// </summary>
	public const int SCREEN_W = 480;
	/// <summary>
	/// 仮想画面の大きさ（GUI の位置はこれが基準になるので、途中で変更するとえらい作業が発生します）
	/// </summary>
	public const int SCREEN_H = 800;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// ゲームモード：タイムアタックモード
	/// </summary>
	public const int GAMEMODE_TIMEATTACK = 0;

	/// <summary>
	/// ゲームモード：チャレンジモード
	/// </summary>
	public const int GAMEMODE_CHALLENGE = 1;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// クリアすべきドアの数
	/// </summary>
	public const int GAMERULE_TIMEATTACK_DOOR = 20;		// 暫定数値 = 20（2014/10/29 現在）

	/// <summary>
	/// プレイ時間（秒数です）
	/// </summary>
	public const float GAMERULE_CHALLENGE_TIME = 60.0f;		// 暫定数値 = 60.0f（2014/10/29 現在）

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public const string IMAGE_WALL = "wall";
	public const string IMAGE_DOOR = "door";
	public const string IMAGE_MARK = "mark";
	public const string IMAGE_NUMBER = "number";
	public const string IMAGE_TEX_S = "tex_s";
	public const string IMAGE_TEX_M = "tex_m";
	public const string IMAGE_TEX_L = "tex_l";

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// シングルトン
	/// </summary>
	private static AcApp m_vInstance = new AcApp();

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	///// <summary>
	///// タイトル画面の表示チェックに使用してます
	///// </summary>
	//private bool m_bBoot;

	///// <summary>
	///// オートモード中フラグ
	///// </summary>
	//private bool m_bAuto;

	///// <summary>
	///// 「オートプレイモード → 通常ゲームモード」要求フラグ
	///// </summary>
	//private bool m_bStopAuto;

	///// <summary>
	///// 「通常ゲームモード → オートプレイモード」要求フラグ
	///// </summary>
	//private bool m_bPlayAuto;

	/// <summary>
	/// タイムアタックモード / チャレンジモード
	/// </summary>
	private int m_vGameMode;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private AcImageManager m_vImageManager;
	private AcSoundManager m_vSoundManager;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private Dictionary<string, _ImageData> m_vImageData;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// デフォルトコンストラクタ
	/// </summary>
	private AcApp()
	{
		//m_bBoot = false;
		//
		//m_bAuto = true;
		//m_bPlayAuto = false;
		//m_bStopAuto = false;
		//
		m_vGameMode = GAMEMODE_TIMEATTACK;

		//
		_createImageManager();
		_createSoundManager();
	}

	// ========================================================================== //
	// ========================================================================== //

	private void _createImageManager()
	{
		var _tbl = new[]
		{
			//
			new { _entry = IMAGE_WALL,		_file = "tx_room",			_divX = 4,	_divY = 2,	 },
			new { _entry = IMAGE_DOOR,		_file = "tx_door",			_divX = 4,	_divY = 16,	 },
			new { _entry = IMAGE_MARK,		_file = "tx_128_kigou",		_divX = 8,	_divY = 4,	 },
			new { _entry = IMAGE_NUMBER,	_file = "tx_32_number",		_divX = 8,	_divY = 2,	 },
			new { _entry = IMAGE_TEX_S,		_file = "tx_256",			_divX = 4,	_divY = 4,	 },
			new { _entry = IMAGE_TEX_M,		_file = "tx_256_mid",		_divX = 1,	_divY = 4,	 },
			new { _entry = IMAGE_TEX_L,		_file = "tx_512_large",		_divX = 4,	_divY = 4,	 },
		};
		//
		m_vImageManager = AcImageManager.Create();
		m_vImageData = new Dictionary<string, _ImageData>();
		//
		foreach ( var _var in _tbl )
		{
			m_vImageManager.add( _var._entry, "groupName", new Rect( 0, 0, 1, 1 ), "Images" + AcUtil.getLanguageSuffix() + "/" + _var._file );
			//
			m_vImageData.Add( _var._entry, new _ImageData( _var._divX, _var._divY ) );
		}
	}

	private void _createSoundManager()
	{
		var _tbl = new[]
		{
			//
			new { _entry = "se_1",		_track = new string[] { "se1", "se2", "se3", },	_volume = 1.0f,	_pan = 0.0f,	_loop = false,	_next = "",			_file = "Seikai02-1" },
			new { _entry = "se_2",		_track = new string[] { "se1", },				_volume = 1.0f,	_pan = 0.0f,	_loop = false,	_next = "",			_file = "Huseikai02-4" },
			new { _entry = "se_3",		_track = new string[] { "se1", "se2", "se3" },	_volume = 1.0f,	_pan = 0.0f,	_loop = false,	_next = "",			_file = "Seikai02-1" },
			new { _entry = "bgm_1",		_track = new string[] { "bgm1", },				_volume = 0.1f,	_pan = 0.0f,	_loop = false,	_next = "bgm_2",	_file = "Encounter_loop" },
			new { _entry = "bgm_2",		_track = new string[] { "bgm1", },				_volume = 0.1f,	_pan = 0.0f,	_loop = false,	_next = "",			_file = "Top_Speed" },
			new { _entry = "se_cd_1",	_track = new string[] { "se1", "se2", "se3" },	_volume = 1.0f,	_pan = 0.0f,	_loop = false,	_next = "",			_file = "Accent Simple07-1" },
			new { _entry = "se_cd_2",	_track = new string[] { "se1", "se2", "se3" },	_volume = 1.0f,	_pan = 0.0f,	_loop = false,	_next = "",			_file = "Accent Simple06-1" },
		};
		//
		m_vSoundManager = AcSoundManager.Create();
		//
		foreach ( var _var in _tbl )
		{
			/*
			 * "<null> を、匿名型プロパティに割り当てる事はできません" と怒られました
			 */
			string _next = null;
			//
			if ( !_var._next.Equals( "" ) )
			{
				_next = _var._next;
			}
			//
			m_vSoundManager.add( _var._entry, "sound", _var._track, _var._volume, _var._pan, _var._loop, _next, "Sounds/" + _var._file );
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private static Rect _createImageRect( string vEntryName, int vIndex )
	{
		_ImageData _data = m_vInstance.m_vImageData[ vEntryName ];
		//
		return ( AcImageManager.createDividedRect( _data.m_vDivideW, _data.m_vDivideH, vIndex ) );
	}

	public static void imageRender( Renderer vRenderer, string vEntryName, int vIndex )
	{
		if ( m_vInstance.m_vImageData.ContainsKey( vEntryName ) )
		{
			//_ImageData _data = m_vInstance.m_vImageData[ vEntryName ];
			////
			//int _index_x = vIndex % _data.m_vDivideW;
			//int _index_y = vIndex / _data.m_vDivideH;
			////
			//float _w = 1.0f / _data.m_vDivideW;
			//float _h = 1.0f / _data.m_vDivideH;
			///*
			// * 画像の位置
			// * (0,1)---(1,1)
			// * |           |
			// * (0,0)---(1,0)
			// */
			//float _x = _w * _index_x;
			//float _y = 1.0f - ( _h * ( _index_y + 1 ) );
			////
			//m_vInstance.m_vImageManager.render( vRenderer, vEntryName, new Rect( _x, _y, _w, _h ) );
			m_vInstance.m_vImageManager.render( vRenderer, vEntryName, _createImageRect( vEntryName, vIndex ) );
		}
	}

	public static void imageRender( Renderer vRenderer, string vEntryName, Rect vRect )
	{
		m_vInstance.m_vImageManager.render( vRenderer, vEntryName, vRect );
	}

	public static void imageDraw( Rect vXywh, string vEntryName, int vIndex )
	{
		if ( m_vInstance.m_vImageData.ContainsKey( vEntryName ) )
		{
			m_vInstance.m_vImageManager.draw( vXywh, vEntryName, _createImageRect( vEntryName, vIndex ) );
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	public static string soundPlay( string vEntryName, float vFadeInTime = 0.0f )
	{
		return ( m_vInstance.m_vSoundManager.play( vEntryName, vFadeInTime ) );
	}

	public static void soundStop( string vTrackName, float vFadeOutTime = 0.0f )
	{
		m_vInstance.m_vSoundManager.stop( vTrackName, vFadeOutTime );
	}

	//public static void soundUpdate()
	//{
	//	m_vInstance.m_vSoundManager.update();
	//}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// ボタン押した時
	/// </summary>
	public static void soundPlay_Button()
	{
		soundPlay( "se_1" );
	}

	// ========================================================================== //
	// ========================================================================== //

	//public static void swBoot( bool bSw )
	//{
	//	m_vInstance.m_bBoot = bSw;
	//}

	//public static bool isBoot()
	//{
	//	return ( m_vInstance.m_bBoot );
	//}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//public static void playAuto()
	//{
	//	if ( !m_vInstance.m_bAutoPlay )
	//	{
	//		m_vInstance.m_bAutoPlay = true;
	//		m_vInstance.m_bAutoStop = false;
	//	}
	//}

	///// <summary>
	///// ゲームモードが選択された時に、オートプレイを終了させるためのフラグを立てる
	///// </summary>
	//public static void stopAuto()
	//{
	//	if ( m_vInstance.m_bAutoPlay )
	//	{
	//		//m_vInstance.m_bAuto = true;
	//		m_vInstance.m_bAutoStop = true;
	//	}
	//}

	///// <summary>
	///// オートモード状態変化
	///// </summary>
	///// <param name="bSw"></param>
	//public static void swAuto( bool bSw )
	//{
	//	m_vInstance.m_bAuto = bSw;
	//}

	///// <summary>
	///// オートモード状態確認
	///// </summary>
	///// <returns></returns>
	//public static bool isAuto()
	//{
	//	return ( m_vInstance.m_bAuto );
	//}

	///// <summary>
	///// リクエスト発行
	///// </summary>
	///// <param name="bSw"></param>
	//public static void swStopAuto( bool bSw )
	//{
	//	m_vInstance.m_bStopAuto = bSw;
	//}

	///// <summary>
	///// リクエスト確認
	///// </summary>
	///// <returns></returns>
	//public static bool isStopAuto()
	//{
	//	return ( m_vInstance.m_bStopAuto );
	//}

	///// <summary>
	///// リクエスト発行
	///// </summary>
	///// <param name="bSw"></param>
	//public static void swPlayAuto( bool bSw )
	//{
	//	m_vInstance.m_bPlayAuto = bSw;
	//}

	///// <summary>
	///// リクエスト確認
	///// </summary>
	///// <returns></returns>
	//public static bool isPlayAuto()
	//{
	//	return ( m_vInstance.m_bPlayAuto );
	//}


	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	public static void setGameMode( int vGameMode )
	{
		m_vInstance.m_vGameMode = vGameMode;
	}

	public static int getGameMode()
	{
		return ( m_vInstance.m_vGameMode );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	///// <summary>
	///// フレームレートを取得する
	///// と思ったがあんまり役に立たない？
	///// 
	///// とりあえず使用禁止！
	///// </summary>
	///// <returns></returns>
	//public static int getFrameRate()
	//{
	//	int _frameRate = Application.targetFrameRate;
	//	//
	//	if ( _frameRate < 0 )
	//	{
	//		_frameRate = 60;
	//	}
	//	//
	//	return ( _frameRate );
	//}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private class _ImageData
	{
		public int m_vDivideW;
		public int m_vDivideH;

		public _ImageData( int vDivideW, int vDivideH )
		{
			m_vDivideW = vDivideW;
			m_vDivideH = vDivideH;
		}
	}

	// ========================================================================== //
	// ========================================================================== //
}
