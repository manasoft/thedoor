using UnityEngine;
using System.Collections;

// List<T>
using System.Collections.Generic;

//public class AcGui : MonoBehaviour
/// <summary>
/// ゲーム中のタイム＆ドア＆その他を表示するクラスです
/// 
/// プレハブ無しで自前で GameObject を作って MonoBehaviour の処理を行わせる事に成功したよ！
/// </summary>
//public class AcGuiGame : Object
public class AcGuiGame : MonoBehaviour
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 *　UnityをC#で超入門してみる #4 GUIの章 - Qiita
	 * http://qiita.com/hiroyuki_hon/items/af0a52c1cb9e856f32b2
	 * 
	 * 
	 * 
	 * 
	 * 
	 * Texture _tex = ( Texture ) Instantiate( Resources.Load( "Images/Door/door_1" ) );
	 * 
	 * http://ustom.daa.jp/blog/2013/10/gui%E3%82%92%E4%BD%BF%E3%81%A3%E3%81%A6%E7%B0%A1%E5%8D%98%E3%81%ABhp%E3%83%90%E3%83%BC%E3%81%AE%E8%A1%A8%E7%A4%BA%E3%81%99%E3%82%8B%E6%96%B9%E6%B3%95unity/
	 * GUI.DrawTextureWithTexCoords(
	 * new Rect( 10.0f, 10.0f, 600.0f, 20.0f ),
	 * 	_tex,
	 * 		new Rect( 0.0f, 0.0f, 0.5f, 0.5f )
	 * 	);
	 * 	
	 * 
	 * 
	 * 画像サイズに合わせてスクリーンに表示する方法2 
	 * http://qiita.com/gc-j-lee/items/f32f9fcbce165c18e623
	 * GUI.DrawTexture(
	 * new Rect( Screen.width / 2 - 128, Screen.height / 2 - 128, _tex.width, _tex.width ),
	 *	_tex
	 * );
	 * 
	 * 画面の幅、高さ
	 * http://www40.atwiki.jp/spellbound/pages/1372.html
	 * 
	 *
	 * 
	 */
	/*
	 * 匿名構造体
	 * http://msdn.microsoft.com/ja-jp/library/bb397696.aspx
	 */

	/*
	 * MonoBehaviour を継承してもモデルにくっつけないと
	 * OnGUI() とか呼ばれないのか？
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
		COUNTDOWN,
		GO,
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//public const int GUI_TIMEATTACK_MODE = 0;
	//public const int GUI_CHALLENGE_MODE = 1;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//private const int _CHANGER_FIG_0 = 0;
	//private const int _CHANGER_FIG_1 = 1;
	//private const int _CHANGER_FIG_2 = 2;
	//private const int _CHANGER_FIG_3 = 3;
	//private const int _CHANGER_FIG_4 = 4;
	//private const int _CHANGER_FIG_5 = 5;
	//private const int _CHANGER_FIG_6 = 6;
	//private const int _CHANGER_FIG_7 = 7;
	//private const int _CHANGER_FIG_8 = 8;
	//private const int _CHANGER_FIG_9 = 9;
	//private const int _CHANGER_COLON = 10;
	//private const int _CHANGER_FRAME_1 = 11;
	//private const int _CHANGER_FRAME_2 = 12;
	//private const int _CHANGER_SUCCESS = 13;
	//private const int _CHANGER_FAILURE = 14;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 親
	/// </summary>
	private AcPlayer m_vPlayer;

	/// <summary>
	/// 
	/// </summary>
	private AiGuiGameTrigger m_vTrigger;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//	private AcTextureChanger m_vChanger;

	///// <summary>
	///// 削除予定なので使用禁止だよ！
	///// </summary>
	//[System.Obsolete]
	//private MonoBehaviour m_vMonoBehaviour;

	private int m_vMode;

	//private bool m_bReadyActive;
	//private int m_vReadyCounter;

	private _GuiReady m_vGuiReady;

	private bool m_bTimeActive;
	private float m_vTimerCounter;

	private bool m_bDoorActive;
	private int m_vDoorCounter;

	//private bool m_bResultActive;
	//private bool m_bResultSuccess;

	private _GuiResult m_vGuiResult;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

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

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public static AcGuiGame Create( AcPlayer vPlayer,AiGuiGameTrigger vTrigger )
	{
		
		/*
		 * Unity入門-コンポーネントの追加 - WisdomSoft
		 * http://www.wisdomsoft.jp/118.html
		 * 
		 * 各種ゲームオブジェクトをスクリプトから生成する  Lonely Mobiler
		 * http://loumo.jp/wp/archive/20140531004758/
		 */
		//AcGuiGame _guiGame = new AcGuiGame();
		//GameObject _object = new GameObject( _guiGame.GetType().FullName );

		//AcDebug.debugLog( typeof( AcGuiGame ).GetType().FullName ); // "System.MonoType"
		//
		GameObject _object = new GameObject();
		//
		//AcDebug.debugLog( "コンポーネントを add するよ" );
		//
		AcGuiGame _guiGame = ( AcGuiGame ) _object.AddComponent( ( typeof( AcGuiGame ) ) );
		//		_object.AddComponent( typeof( AcGuiGame ) ); // この時点でコンストラクタが実行されるようです
		//
		//AcDebug.debugLog( "コンポーネントを get するよ" );
		//
		//		AcGuiGame _guiGame = _object.GetComponent<AcGuiGame>();
		//
		//AcDebug.debugLog( "オブジェクトに名前を設定するよ" );
		//
		//AcDebug.debugLog( _object.name );
		//AcDebug.debugLog( _guiGame.GetType().FullName );
		//
		_object.name = _guiGame.GetType().FullName;
		//
		_guiGame._create( vPlayer, vTrigger );
		//
		return ( _guiGame );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _create( AcPlayer vPlayer, AiGuiGameTrigger vTrigger )
	{
		_debugLog( "_create" );
		//
		m_vPlayer = vPlayer;
		m_vTrigger = vTrigger;

		//m_bReadyActive = false;
		//m_vReadyCounter = 0;

		m_vGuiReady = new _GuiReady( this );

		m_bTimeActive = false;
		m_vTimerCounter = 0.0f;

		m_bDoorActive = false;
		m_vDoorCounter = 0;

		//m_bResultActive = false;
		//m_bResultSuccess = false;

		m_vGuiResult = new _GuiResult(this);
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	/*
	 * デフォルトコンストラクタ
	 */
	//public AcGuiGame()
	private AcGuiGame()
	{
		//		m_vChanger = new AcTextureChanger( AcGuiBase.getTextureChangerData() );
		//		m_vChanger = AcGuiBase.getTextureChanger();

		_debugLog( "コンストラクタ" );

		////m_bReadyActive = false;
		////m_vReadyCounter = 0;

		//m_vGuiReady = new _GuiReady();

		//m_bTimeActive = false;
		//m_vTimerCounter = 0.0f;

		//m_bDoorActive = false;
		//m_vDoorCounter = 0;

		////m_bResultActive = false;
		////m_bResultSuccess = false;

		//m_vGuiResult = new _GuiResult();

	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	//private struct _Param
	//{
	//	internal Rect m_vXywh;
	//	internal Rect m_vUvwh;

	//	//public _Param( float vX, float vY, float vW, float vH, float vScaleX, float vScaleY, Vector2 vUv, Vector2 vWh )
	//	//{
	//	//	//float pivot_x = -vX / vW;
	//	//	//float pivot_y = -vY / vH;
	//	//	//
	//	//	m_vXywh = new Rect( vX * vScaleX, vY * vScaleY, vW * vScaleX, vH * vScaleY );
	//	//	m_vUvwh = new Rect( vUv.x, vUv.y, vWh.x, vWh.y );
	//	//}

	//	//public _Param( float vX, float vY, float vW, float vH, float vScale, Vector2 vUv, Vector2 vWh )
	//	//{
	//	//	//float pivot_x = -vX / vW;
	//	//	//float pivot_y = -vY / vH;
	//	//	//
	//	//	m_vXywh = new Rect( vX * vScale, vY * vScale, vW * vScale, vH * vScale );
	//	//	m_vUvwh = new Rect( vUv.x, vUv.y, vWh.x, vWh.y );
	//	//}

	//	public _Param( float vX, float vY, float vW, float vH, Vector2 vUv, Vector2 vWh )
	//	{
	//		float _scale_x = AcUtil.getScreelScaleX( AcSetting.SCREEN_W );
	//		float _scale_y = AcUtil.getScreelScaleY( AcSetting.SCREEN_H );
	//		/*
	//		 * 注意！
	//		 * 位置 x/y は scale_x/y で補正する
	//		 * 大きさ w/h は scale_x のみで補正する
	//		 */
	//		m_vXywh = new Rect( vX * _scale_x, vY * _scale_y, vW * _scale_x, vH * _scale_x );
	//		m_vUvwh = new Rect( vUv.x, vUv.y, vWh.x, vWh.y );
	//	}

	//}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// ゲーム中のカウントダウン表示
	/// </summary>
	private void _onGUI_Ready()
	{
		//if ( m_bReadyActive )
		//{
		//	//_GuiReady _guiReady = new _GuiReady( m_vReadyCounter, 0, 0, m_vChanger );
		//	//_GuiReady _guiReady = new _GuiReady( m_vMonoBehaviour, 0, 0, m_vReadyCounter );
		//	_GuiReady _guiReady = new _GuiReady( 0, 0, m_vReadyCounter );
		//	_guiReady._onGUI();
		//}
		m_vGuiReady._onGUI();
	}

	/// <summary>
	/// ゲーム中のタイム表示
	/// </summary>
	private void _onGUI_Time()
	{
		if ( m_bTimeActive )
		{
			float _baseScale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );
			float _sizeScale = 1.0f;
			//
			//AcGuiTime _guiTime = new AcGuiTime( m_vTimerCounter, 10.0f, 10.0f, m_vChanger );
			//AcGuiTime _guiTime = new AcGuiTime( m_vMonoBehaviour, 84.0f, 10.0f, m_vTimerCounter );
			AcGuiTime _guiTime = new AcGuiTime( 84.0f, 10.0f, m_vTimerCounter );
			_guiTime.onGUI( _baseScale, _sizeScale, false );
		}
	}

	/// <summary>
	/// ゲーム中のドア枚数表示
	/// </summary>
	private void _onGUI_Door()
	{
		if ( m_bDoorActive )
		{
			float _baseScale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );
			float _sizeScale = 1.0f;
			//
			//AcGuiDoor _guiDoor = new AcGuiDoor( m_vDoorCounter, AcApp.SCREEN_W - AcGuiDoor.getFrameW() - 10.0f, 10.0f, m_vChanger );
			//AcGuiDoor _guiDoor = new AcGuiDoor( m_vDoorCounter, AcApp.SCREEN_W - AcGuiDoor.getFrameW() - 10.0f, 10.0f );
			//AcGuiDoor _guiDoor = new AcGuiDoor( m_vMonoBehaviour, 84.0f, 10.0f, m_vDoorCounter );
			AcGuiDoor _guiDoor = new AcGuiDoor( 84.0f, 10.0f, m_vDoorCounter );
			_guiDoor.onGUI( _baseScale, _sizeScale, false );
		}
	}

	/// <summary>
	/// ゲーム中の結果表示
	/// </summary>
	private void _onGUI_Result()
	{
		//if ( m_bResultActive )
		{
			//_GuiResult _guiResult = new _GuiResult( 0, 0, 0, m_vChanger );
			//_GuiResult _guiResult = new _GuiResult( m_vMonoBehaviour, 0, 0, 0 );
			//_guiResult._onGUI( m_bResultSuccess );
			m_vGuiResult._onGUI();
		}
	}

	///// <summary>
	///// ゲーム中の OnGUI 表示（誰かの OnGUI() で呼び出してもらうよ！）
	///// </summary>
	//public void onGUI()
	//{
	//	//_onGUI_Ready();
	//	//_onGUI_Time();
	//	//_onGUI_Door();
	//	//_onGUI_Result();
	//}

	public void OnGUI()
	{
		_onGUI_Ready();
		_onGUI_Time();
		_onGUI_Door();
		_onGUI_Result();
	}

	// ========================================================================== //
	// ========================================================================== //




	public void swReadyActive( bool bSw )
	{
		//m_bReadyActive = bSw;
		m_vGuiReady._swActive( this, bSw );
	}

	public void swTimeActive( bool bSw )
	{
		m_bTimeActive = bSw;
	}

	public void swDoorActive( bool bSw )
	{
		m_bDoorActive = bSw;
	}

	/// <summary>
	/// 結果表示
	/// </summary>
	/// <param name="bSw"></param>
	//public void swResultActive( bool bSw )
	//{
	//	m_bResultActive = bSw;
	//}

	public void swResultActive( bool bSw, bool bSuccess )
	{
		//m_bResultActive = bSw;
		//
		m_vGuiResult._swActive( this, bSw, bSuccess );
	}

	//public void setReady( int vReady )
	//{
	//	m_vReadyCounter = vReady;
	//}

	public void setTime( float vTime )
	{
		m_vTimerCounter = vTime;
	}

	public void setDoor( int vDoor )
	{
		m_vDoorCounter = vDoor;
	}

	///// <summary>
	///// 結果（成功/失敗）の表示を切り替える
	///// </summary>
	///// <param name="bSw"></param>
	//public void swResultSuccess( bool bSw )
	//{
	//	m_bResultSuccess = bSw;
	//}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private class _GuiReady : AcGuiBase
	{
		private AcGuiGame m_vGuiGame;
		//
		private bool m_bActive;
		private IEnumerator m_vIEnumerator;
	
		//public _GuiReady( int vValue, float vX, float vY, AcTextureChanger vChanger )
		//	: base( vValue, vX, vY, vChanger )
		//{
		//}

		//public _GuiReady( MonoBehaviour vMonoBehaviour, float vX, float vY, int vValue )
		//	: base( vMonoBehaviour, vX, vY, vValue )
		//{
		//}

		//public _GuiReady( float vX, float vY, int vValue )
		//	: base( vX, vY, vValue )
		//{
		//	m_bActive = false;
		//	m_vIEnumerator = null;
		//}

		public _GuiReady(AcGuiGame vGuiGame)
			: base()
		{
			m_vGuiGame = vGuiGame;
			m_bActive = false;
			m_vIEnumerator = null;
		}

		public void _swActive( MonoBehaviour vMonoBehaviour, bool bSw )
		{
			m_bActive = bSw;
			//
			if ( m_vIEnumerator != null )
			{
				vMonoBehaviour.StopCoroutine( m_vIEnumerator );
				m_vIEnumerator = null;
			}
			//
			if ( bSw )
			{
				m_vIEnumerator = _coroutine();
				vMonoBehaviour.StartCoroutine( m_vIEnumerator );
			}
		}

		/// <summary>
		/// 移動処理をコルーチンでやってみるよ
		/// </summary>
		/// <returns></returns>
		private IEnumerator _coroutine()
		{
			float _timer = 4.0f; // "4" で初期設定してすぐ減算して "3.??" になって (int) キャストするよ！
			int _old_count = ( int ) _timer;
			//
			while ( true )
			{
				//
				_timer -= Time.deltaTime;
				//
				int _new_count = ( int ) _timer;
				//
				if ( _old_count != _new_count )
				{
					_old_count = _new_count;
					//
					if ( _new_count > 0 )
					{
						/*
						 * 実験
						 */
						m_vGuiGame.m_vTrigger.onTrigger( AcGuiGame.Trigger.COUNTDOWN );
					}
					else
					{
						/*
						 * 実験
						 */
						//_playSe( "se_cd_2" );
						//_playBgm( "bgm_1" );
						//
						m_vGuiGame.m_vTrigger.onTrigger( AcGuiGame.Trigger.GO );
						break;
					}
				}
				//
				m_vValueInt = _new_count;
				//
				yield return null;
			}
			//
			//m_bActive = false;
		}


		//public void _onGUI_2()
		//{
		//	float _scale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );

		//	int _timer = Time.frameCount;

		//	/*
		//	 * サイズを決めてセンタリングしてみる
		//	 */
		//	float _w = 200.0f;
		//	float _h = 100.0f;
		//	float _x = ( AcApp.SCREEN_W - _w ) / 2;
		//	float _y = 50.0f;
		//	//
		//	/*
		//	 * ArrayList クラス
		//	 * http://msdn.microsoft.com/ja-jp/library/system.collections.arraylist(v=vs.110).aspx
		//	 */
		//	ArrayList _data = new ArrayList();
		//	//
		//	int _index = _CHANGER_FIG_0 + ( m_vValueInt % 10 );
		//	//
		//	//			_data.Add( new _Data( ( m_vX + _x ) * _scale, ( m_vY + _y ) * _scale, _w * _scale, _h * _scale, m_vChanger.getUV( _count, _CHANGER_FIG_0 + ( m_vValue % 10 ), 0, 0 ), m_vChanger.getWH() ) );
		//	_data.Add( new _ImageList(
		//		m_vChanger.getTexture( _index ),
		//		( m_vX + _x ) * _scale,
		//		( m_vY + _y ) * _scale,
		//		_w * _scale,
		//		_h * _scale,
		//		m_vChanger.getUV( _timer, _index, 0 ),
		//		m_vChanger.getWH( _index ) ) );
		//	//
		//	foreach ( _ImageList __data in _data )
		//	{
		//		GUI.DrawTextureWithTexCoords( __data.m_vXywh, m_vChanger.getTexture( _index ), __data.m_vUvwh );
		//		//				GUI.DrawTextureWithTexCoords( __data.m_vXywh, m_vChanger.getTexture(), __data.m_vUvwh );
		//	}
		//}

		public void _onGUI()
		{
			if ( m_bActive )
			{
				float _scale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );

				/*
				 * サイズを決めてセンタリングしてみる
				 */
				//float _w = 200.0f;
				//float _h = 100.0f;
				//float _x = ( AcApp.SCREEN_W - _w ) / 2;
				//float _y = 50.0f;
				float _w = AcApp.SCREEN_W;
				float _h = AcApp.SCREEN_H;
				float _x = ( AcApp.SCREEN_W - _w ) / 2;
				float _y = ( AcApp.SCREEN_H - _h ) / 2;
				//
				/*
				 * ArrayList クラス
				 * http://msdn.microsoft.com/ja-jp/library/system.collections.arraylist(v=vs.110).aspx
				 */
				List<_ImageList> _data = new List<_ImageList>();
				//
				//int _index = _CHANGER_FIG_0 + ( m_vValueInt % 10 );
				int _index = _CHANGER_COUNTDOWN_0 + ( m_vValueInt % 10 );
				//
				clear();
				//
				add(
					( m_vX + _x ) * _scale,
					( m_vY + _y ) * _scale,
					_w * _scale,
					_h * _scale,
					_index
				);
				//
				draw();
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	private class _GuiResult : AcGuiBase
	{
		//public _GuiResult( int vValue, float vX, float vY, AcTextureChanger vChanger )
		//	: base( vValue, vX, vY, vChanger )
		//{
		//}
		//public _GuiResult( MonoBehaviour vMonoBehaviour, float vX, float vY, int vValue )
		//	: base( vMonoBehaviour, vX, vY, vValue )
		//{
		//}

		private AcGuiGame m_vGuiGame;
		//
		private bool m_bActive;
		private IEnumerator m_vIEnumerator;

		public _GuiResult( AcGuiGame vGuiGame )
			: base()
		{
			m_vGuiGame = vGuiGame;
			m_bActive = false;
			m_vIEnumerator = null;
		}

		public void _swActive( MonoBehaviour vMonoBehaviour, bool bSw, bool bSuccess )
		{
			m_bActive = bSw;
			//
			if ( m_vIEnumerator != null )
			{
				vMonoBehaviour.StopCoroutine( m_vIEnumerator );
				m_vIEnumerator = null;
			}
			//
			if ( bSw )
			{
				m_vValueInt = bSuccess ? 1 : 0;
				//
				m_vIEnumerator = _coroutine();
				vMonoBehaviour.StartCoroutine( m_vIEnumerator );
			}
		}

		/// <summary>
		/// 移動処理をコルーチンでやってみるよ
		/// </summary>
		/// <returns></returns>
		private IEnumerator _coroutine()
		{
			//AcDebug.debugLog( "_coroutine()" );

			m_vX = AcApp.SCREEN_W / 2 + AcApp.SCREEN_W;
			m_vY = AcApp.SCREEN_H / 2;
			//
			while ( ( m_vX -= ( AcApp.SCREEN_W / 60 ) ) > AcApp.SCREEN_W / 2 )
			{
				//AcDebug.debugLog( "_coroutine() loop中・・・" );
				//
				yield return null;
			}
			//
			m_vX = AcApp.SCREEN_W / 2;
		}

		//public void _onGUI_2( bool bSuccess )
		//{
		//	float _scale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );

		//	int _timer = Time.frameCount;

		//	/*
		//	 * サイズを決めてセンタリングしてみる
		//	 */
		//	float _w = 300.0f;
		//	float _h = 200.0f;
		//	float _x = ( AcApp.SCREEN_W - _w ) / 2;
		//	float _y = 150.0f;
		//	//
		//	/*
		//	 * ArrayList クラス
		//	 * http://msdn.microsoft.com/ja-jp/library/system.collections.arraylist(v=vs.110).aspx
		//	 */
		//	ArrayList _list = new ArrayList();
		//	//
		//	int _index;
		//	//
		//	if ( bSuccess )
		//	{
		//		switch ( AcApp.getGameMode() )
		//		{
		//			//
		//			case ( AcApp.GAMEMODE_TIMEATTACK ):
		//				_index = _CHANGER_SUCCESS;
		//				break;
		//			//
		//			//case ( AcApp.GAMEMODE_CHALLENGE ):
		//			default:
		//				_index = _CHANGER_SUCCESS_CHALLENGE;
		//				break;
		//		}
		//		//
		//		//				_data.Add( new _Data( ( m_vX + _x ) * _scale, ( m_vY + _y ) * _scale, _w * _scale, _h * _scale, m_vChanger.getUV( _count, _CHANGER_SUCCESS, 0, 0 ), m_vChanger.getWH() ) );
		//		_list.Add( new _ImageList(
		//			m_vChanger.getTexture( _index ),
		//			( m_vX + _x ) * _scale,
		//			( m_vY + _y ) * _scale,
		//			_w * _scale,
		//			_h * _scale,
		//			m_vChanger.getUV( _timer, _index, 0 ),
		//			m_vChanger.getWH( _index ) ) );
		//	}
		//	else
		//	{
		//		_index = _CHANGER_FAILURE;
		//		//
		//		//				_data.Add( new _Data( ( m_vX + _x ) * _scale, ( m_vY + _y ) * _scale, _w * _scale, _h * _scale, m_vChanger.getUV( _count, _CHANGER_FAILURE, 0, 0 ), m_vChanger.getWH() ) );
		//		_list.Add( new _ImageList(
		//			m_vChanger.getTexture( _index ),
		//			( m_vX + _x ) * _scale,
		//			( m_vY + _y ) * _scale,
		//			_w * _scale,
		//			_h * _scale,
		//			m_vChanger.getUV( _timer, _index, 0 ),
		//			m_vChanger.getWH( _index ) ) );
		//	}
		//	//
		//	foreach ( _ImageList __data in _list )
		//	{
		//		//				GUI.DrawTextureWithTexCoords( __data.m_vXywh, m_vChanger.getTexture(), __data.m_vUvwh );
		//		GUI.DrawTextureWithTexCoords( __data.m_vXywh, __data.m_vTexture, __data.m_vUvwh );
		//	}
		//}

		public void _onGUI()
		{
			if ( m_bActive )
			{
				float _scale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );

				/*
				 * サイズを決めてセンタリングしてみる
				 */
				float _w = 300.0f;
				float _h = 200.0f;
				//float _x = ( AcApp.SCREEN_W - _w ) / 2;
				//float _y = 150.0f;
				float _x = m_vX - ( _w ) / 2;
				float _y = m_vY - ( _h ) / 2;

				//
				/*
				 * ArrayList クラス
				 * http://msdn.microsoft.com/ja-jp/library/system.collections.arraylist(v=vs.110).aspx
				 */
				List<_ImageList> _data = new List<_ImageList>();
				//
				clear();
				//
				int _index = _CHANGER_FAILURE;
				//
				switch ( m_vValueInt )
				{
					case ( 0 ):
						_index = _CHANGER_FAILURE;
						break;
					//
					//case ( 1 ):
					default:
						//
						switch ( AcApp.getGameMode() )
						{
							case ( AcApp.GAMEMODE_TIMEATTACK ):
								_index = _CHANGER_SUCCESS;
								break;
							//
							//case ( AcApp.GAMEMODE_CHALLENGE ):
							default:
								_index = _CHANGER_SUCCESS_CHALLENGE;
								break;
						}
						break;
				}
				//
				add(
					//( m_vX + _x ) * _scale,
					//( m_vY + _y ) * _scale,
					_x * _scale,
					_y * _scale,
					_w * _scale,
					_h * _scale,
					_index
				);
				//
				draw();
			}
		}

		//public void _onGUI( bool bSuccess )
		//{
		//	float _scale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );

		//	/*
		//	 * サイズを決めてセンタリングしてみる
		//	 */
		//	float _w = 300.0f;
		//	float _h = 200.0f;
		//	//float _x = ( AcApp.SCREEN_W - _w ) / 2;
		//	//float _y = 150.0f;
		//	float _x = m_vX - ( _w ) / 2;
		//	float _y = m_vY - ( _h ) / 2;

		//	//
		//	/*
		//	 * ArrayList クラス
		//	 * http://msdn.microsoft.com/ja-jp/library/system.collections.arraylist(v=vs.110).aspx
		//	 */
		//	List<_ImageList> _data = new List<_ImageList>();
		//	//
		//	clear();
		//	//
		//	int _index = _CHANGER_FAILURE;
		//	//
		//	if ( bSuccess )
		//	{
		//		switch ( AcApp.getGameMode() )
		//		{
		//			//
		//			case ( AcApp.GAMEMODE_TIMEATTACK ):
		//				_index = _CHANGER_SUCCESS;
		//				break;
		//			//
		//			//case ( AcApp.GAMEMODE_CHALLENGE ):
		//			default:
		//				_index = _CHANGER_SUCCESS_CHALLENGE;
		//				break;
		//		}
		//	}
		//	else
		//	{
		//		_index = _CHANGER_FAILURE;
		//	}
		//	//
		//	//
		//	add(
		//		_x * _scale,
		//		_y * _scale,
		//		_w * _scale,
		//		_h * _scale,
		//		_index
		//	);
		//	//
		//	draw();
		//}
	}

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 
	/// </summary>
	public interface AiGuiGameTrigger
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="vTrigger"></param>
		void onTrigger( AcGuiGame.Trigger vTrigger );
	}

	// ========================================================================== //
	// ========================================================================== //
}
