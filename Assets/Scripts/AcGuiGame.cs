using UnityEngine;
using System.Collections;


//public class AcGui : MonoBehaviour
/// <summary>
/// ゲーム中のタイム＆ドア＆その他を表示するクラスです
/// </summary>
public class AcGuiGame : object
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

	public const int GUI_TIMEATTACK_MODE = 0;
	public const int GUI_CHALLENGE_MODE = 1;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _CHANGER_FIG_0 = 0;
	private const int _CHANGER_FIG_1 = 1;
	private const int _CHANGER_FIG_2 = 2;
	private const int _CHANGER_FIG_3 = 3;
	private const int _CHANGER_FIG_4 = 4;
	private const int _CHANGER_FIG_5 = 5;
	private const int _CHANGER_FIG_6 = 6;
	private const int _CHANGER_FIG_7 = 7;
	private const int _CHANGER_FIG_8 = 8;
	private const int _CHANGER_FIG_9 = 9;
	private const int _CHANGER_COLON = 10;
	private const int _CHANGER_FRAME_1 = 11;
	private const int _CHANGER_FRAME_2 = 12;
	private const int _CHANGER_SUCCESS = 13;
	private const int _CHANGER_FAILURE = 14;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private AcTextureChanger m_vChanger;

	private int m_vMode;

	private bool m_bReadyActive;
	private int m_vReadyCounter;

	private bool m_bTimeActive;
	private float m_vTimerCounter;

	private bool m_bDoorActive;
	private int m_vDoorCounter;

	private bool m_bResultActive;
	private bool m_bResultSuccess;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public static AcGuiGame Create()
	{
		AcGuiGame _guiGame = new AcGuiGame();

		return ( _guiGame );
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
	public AcGuiGame()
	{
//		m_vChanger = new AcTextureChanger( AcGuiBase.getTextureChangerData() );
		m_vChanger = AcGuiBase.getTextureChanger();

		m_bReadyActive = false;
		m_vReadyCounter = 0;

		m_bTimeActive = false;
		m_vTimerCounter = 0.0f;

		m_bDoorActive = false;
		m_vDoorCounter = 0;

		m_bResultActive = false;
		m_bResultSuccess = false;
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


	private void _guiReady()
	{
		if ( m_bReadyActive )
		{
			_GuiReady _guiReady = new _GuiReady( m_vReadyCounter, 0, 0, m_vChanger );
			_guiReady._onGUI();
		}
	}

	private void _guiTime()
	{
		if ( m_bTimeActive )
		{
			float _baseScale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );
			float _sizeScale = 1.0f;
			//
			AcGuiTime _guiTime = new AcGuiTime( m_vTimerCounter, 10.0f, 10.0f, m_vChanger );
			_guiTime.onGUI( _baseScale, _sizeScale );
		}
	}

	private void _guiDoor()
	{
		if ( m_bDoorActive )
		{
			float _baseScale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );
			float _sizeScale = 1.0f;
			//
			AcGuiDoor _guiDoor = new AcGuiDoor( m_vDoorCounter, AcApp.SCREEN_W - AcGuiDoor.getFrameW() - 10.0f, 10.0f, m_vChanger );
			_guiDoor.onGUI( _baseScale, _sizeScale );
		}
	}


	private void _guiResult()
	{
		if ( m_bResultActive )
		{
			_GuiResult _guiResult = new _GuiResult( 0, 0, 0, m_vChanger );
			_guiResult._onGUI( m_bResultSuccess );
		}
	}

	/*
	 * 誰かの OnGUI() で呼び出してもらうよ！
	 */
	public void onGUI()
	{
		_guiReady();
		_guiTime();
		_guiDoor();
		_guiResult();
	}

	// ========================================================================== //
	// ========================================================================== //




	public void swReadyActive( bool bSw )
	{
		m_bReadyActive = bSw;
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
	/// 結果（成功/失敗）の表示を切り替える
	/// </summary>
	/// <param name="bSw"></param>
	public void swResultActive( bool bSw )
	{
		m_bResultActive = bSw;
	}

	public void setReady( int vReady )
	{
		m_vReadyCounter = vReady;
	}

	public void setTime( float vTime )
	{
		m_vTimerCounter = vTime;
	}

	public void setDoor( int vDoor )
	{
		m_vDoorCounter = vDoor;
	}

	public void swResultSuccess( bool bSw )
	{
		m_bResultSuccess = bSw;
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private class _GuiReady : AcGuiBase
	{
		public _GuiReady( int vValue, float vX, float vY, AcTextureChanger vChanger )
			: base( vValue, vX, vY, vChanger )
		{
		}

		public void _onGUI()
		{
			float _scale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );

			int _timer = Time.frameCount;

			/*
			 * サイズを決めてセンタリングしてみる
			 */
			float _w = 200.0f;
			float _h = 100.0f;
			float _x = ( AcApp.SCREEN_W - _w ) / 2;
			float _y = 50.0f;
			//
			/*
			 * ArrayList クラス
			 * http://msdn.microsoft.com/ja-jp/library/system.collections.arraylist(v=vs.110).aspx
			 */
			ArrayList _data = new ArrayList();
			//
			int _index = _CHANGER_FIG_0 + ( m_vValueInt % 10 );
			//
			//			_data.Add( new _Data( ( m_vX + _x ) * _scale, ( m_vY + _y ) * _scale, _w * _scale, _h * _scale, m_vChanger.getUV( _count, _CHANGER_FIG_0 + ( m_vValue % 10 ), 0, 0 ), m_vChanger.getWH() ) );
			_data.Add( new _Data(
				m_vChanger.getTexture( _index ),
				( m_vX + _x ) * _scale,
				( m_vY + _y ) * _scale,
				_w * _scale,
				_h * _scale,
				m_vChanger.getUV( _timer, _index, 0 ),
				m_vChanger.getWH( _index ) ) );
			//
			foreach ( _Data __data in _data )
			{
				GUI.DrawTextureWithTexCoords( __data.m_vXywh, m_vChanger.getTexture( _index ), __data.m_vUvwh );
				//				GUI.DrawTextureWithTexCoords( __data.m_vXywh, m_vChanger.getTexture(), __data.m_vUvwh );
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	private class _GuiResult : AcGuiBase
	{
		public _GuiResult( int vValue, float vX, float vY, AcTextureChanger vChanger )
			: base( vValue, vX, vY, vChanger )
		{
		}

		public void _onGUI( bool bSuccess )
		{
			float _scale = AcUtil.getScreenScaleX( AcApp.SCREEN_W );

			int _timer = Time.frameCount;

			/*
			 * サイズを決めてセンタリングしてみる
			 */
			float _w = 300.0f;
			float _h = 200.0f;
			float _x = ( AcApp.SCREEN_W - _w ) / 2;
			float _y = 150.0f;
			//
			/*
			 * ArrayList クラス
			 * http://msdn.microsoft.com/ja-jp/library/system.collections.arraylist(v=vs.110).aspx
			 */
			ArrayList _data = new ArrayList();
			//
			int _index;
			//
			if ( bSuccess )
			{
				_index = _CHANGER_SUCCESS;
				//
				//				_data.Add( new _Data( ( m_vX + _x ) * _scale, ( m_vY + _y ) * _scale, _w * _scale, _h * _scale, m_vChanger.getUV( _count, _CHANGER_SUCCESS, 0, 0 ), m_vChanger.getWH() ) );
				_data.Add( new _Data(
					m_vChanger.getTexture( _index ),
					( m_vX + _x ) * _scale,
					( m_vY + _y ) * _scale,
					_w * _scale,
					_h * _scale,
					m_vChanger.getUV( _timer, _index, 0 ),
					m_vChanger.getWH( _index ) ) );
			}
			else
			{
				_index = _CHANGER_FAILURE;
				//
				//				_data.Add( new _Data( ( m_vX + _x ) * _scale, ( m_vY + _y ) * _scale, _w * _scale, _h * _scale, m_vChanger.getUV( _count, _CHANGER_FAILURE, 0, 0 ), m_vChanger.getWH() ) );
				_data.Add( new _Data(
					m_vChanger.getTexture( _index ),
					( m_vX + _x ) * _scale,
					( m_vY + _y ) * _scale,
					_w * _scale,
					_h * _scale,
					m_vChanger.getUV( _timer, _index, 0 ),
					m_vChanger.getWH( _index ) ) );
			}
			//
			foreach ( _Data __data in _data )
			{
				//				GUI.DrawTextureWithTexCoords( __data.m_vXywh, m_vChanger.getTexture(), __data.m_vUvwh );
				GUI.DrawTextureWithTexCoords( __data.m_vXywh, __data.m_vTexture, __data.m_vUvwh );
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //
}
