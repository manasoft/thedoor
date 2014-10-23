using UnityEngine;
using System.Collections;

//public class AcGui : MonoBehaviour
public class AcGui : object
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

	//private const int _GUI_TIMER_MODE_STOP = 0;
	//private const int _GUI_TIMER_MODE_INC = 1;
	//private const int _GUI_TIMER_MODE_DEC = 2;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private AcTextureChanger m_vChanger;

	private int m_vMode;

//	private bool m_bPlayable;

	private bool m_bReadyActive;
	private int m_vReadyCounter;

	private bool m_bTimerActive;
	//	private bool m_bTimerEnable;
	private int m_vTimerCounter;

	private bool m_bScoreActive;
	private int m_vScoreCounter;

	private bool m_bResultActive;
	private bool m_bResultType;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	//private AcTextureChanger.Data[] _guiChangerTbl =
	//{
	//	//
	//	new AcTextureChanger.Data(
	//		"Images/Gui/gui_1", 8, 8, 4,
	//		new int[] {  0,  1,  2,  3, },		// 0
	//		new int[] {  4,  5,  6,  7, },		// 1
	//		new int[] {  8,  9, 10, 11, },		// 2
	//		new int[] { 12, 13, 14, 15, },		// 3
	//		new int[] { 16, 17, 18, 19, },		// 4
	//		new int[] { 20, 21, 22, 23, },		// 5
	//		new int[] { 24, 25, 26, 27, },		// 6
	//		new int[] { 28, 29, 30, 31, },		// 7
	//		new int[] { 32, 33, 34, 35, },		// 8
	//		new int[] { 36, 37, 38, 39, },		// 9
	//		new int[] { 40, 41, 42, 43, },		// :
	//		new int[] { 48, 49, 50, 51, },		// 赤フレーム
	//		new int[] { 56, 57, 58, 59, },		// 黄プレーム:
	//		new int[] { 44, 45, 46, 47, },		// 成功
	//		new int[] { 52, 53, 54, 55, }		// 失敗
	//	),
	//};

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public static AcGui Create()
	{
		AcGui gui = new AcGui();

		return ( gui );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * 仮想画面の大きさに対するスケールを求める
	 * メモ：
	 * ・x/yは同じスケールにした方がいいでしょ？ → じゃｘに合わせますってかんじです
	 */
	private static float _getScale()
	{
		float _scale = 0.0f;
		//
		_scale = AcUtil.getScreenScaleX( AcSetting.SCREEN_W );
		//
		return ( _scale );
	}


	private static float _scale2pixelX( float vScale )
	{
		float _pixel = 0;
		//
		_pixel = AcSetting.SCREEN_W * vScale * _getScale();
		//
		return ( _pixel );
	}

	private static float _scale2pixelY( float vScale )
	{
		float _pixel = 0;
		//
		_pixel = AcSetting.SCREEN_H * vScale * _getScale();
		//
		return ( _pixel );
	}

	private static float _scale2pixelW( float vScale )
	{
		return ( _scale2pixelX( vScale ) );
	}

	private static float _scale2pixelH( float vScale )
	{
		return ( _scale2pixelY( vScale ) );
	}

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	/*
	 * デフォルトコンストラクタ
	 */
	public AcGui()
	{
//		m_vChanger = new AcTextureChanger( _guiChangerTbl[ 0 ] );

//		m_bPlayable = false;

		m_bReadyActive = false;
		m_vReadyCounter = 0;

		m_bTimerActive = false;
		//		m_bTimerEnable = false;
		m_vTimerCounter = 0;

		m_bScoreActive = false;
		m_vScoreCounter = 0;

		m_bResultActive = false;
		m_bResultType = false;
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private struct _Param
	{
		internal Rect m_vXywh;
		internal Rect m_vUvwh;

		//public _Param( float vX, float vY, float vW, float vH, float vScaleX, float vScaleY, Vector2 vUv, Vector2 vWh )
		//{
		//	//float pivot_x = -vX / vW;
		//	//float pivot_y = -vY / vH;
		//	//
		//	m_vXywh = new Rect( vX * vScaleX, vY * vScaleY, vW * vScaleX, vH * vScaleY );
		//	m_vUvwh = new Rect( vUv.x, vUv.y, vWh.x, vWh.y );
		//}

		//public _Param( float vX, float vY, float vW, float vH, float vScale, Vector2 vUv, Vector2 vWh )
		//{
		//	//float pivot_x = -vX / vW;
		//	//float pivot_y = -vY / vH;
		//	//
		//	m_vXywh = new Rect( vX * vScale, vY * vScale, vW * vScale, vH * vScale );
		//	m_vUvwh = new Rect( vUv.x, vUv.y, vWh.x, vWh.y );
		//}

		public _Param( float vX, float vY, float vW, float vH, Vector2 vUv, Vector2 vWh )
		{
			float _scale_x = AcUtil.getScreenScaleX( AcSetting.SCREEN_W );
			float _scale_y = AcUtil.getScreenScaleY( AcSetting.SCREEN_H );
			/*
			 * 注意！
			 * 位置 x/y は scale_x/y で補正する
			 * 大きさ w/h は scale_x のみで補正する
			 */
			m_vXywh = new Rect( vX * _scale_x, vY * _scale_y, vW * _scale_x, vH * _scale_x );
			m_vUvwh = new Rect( vUv.x, vUv.y, vWh.x, vWh.y );
		}

	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	//private static Rect _makeRectByScale( float vX, float vY, float vW, float vH, float vScaleX, float vScaleY, float vPivotX, float vPivotY )
	//{
	//	return ( new Rect( vX + ( vW * vPivotX * vScaleX ), vY + ( vH * vPivotY * vScaleY ), vW * vScaleX, vH * vScaleY ) );
	//}

	//private static Rect _makeRectByScale( Vector2 vXy, Vector2 vWh, float vScaleX, float vScaleY, float vPivotX, float vPivotY )
	//{
	//	return ( _makeRectByScale( vXy.x, vXy.y, vWh.x, vWh.y, vScaleX, vScaleY, vPivotX, vPivotY ) );
	//}

	//private void _guiReady( int vCount )
	//{
	//	if ( m_bReadyActive )
	//	{
	//		/*
	//		 * サイズを決めてセンタリングしてみる
	//		 */
	//		float _w = 200.0f;
	//		float _h = 100.0f;
	//		float _x = ( AcSetting.SCREEN_W - _w ) / 2;
	//		float _y = 50.0f;
	//		//
	//		int[] _changerTbl = { _CHANGER_FIG_3, _CHANGER_FIG_2, _CHANGER_FIG_1, _CHANGER_FIG_0, };
	//		int _frameRate = AcSetting.getFrameRate();
	//		//
	//		int _index = m_vReadyCounter / _frameRate;
	//		//
	//		if ( _index < _changerTbl.Length )
	//		{
	//			/*
	//			 * ArrayList クラス
	//			 * http://msdn.microsoft.com/ja-jp/library/system.collections.arraylist(v=vs.110).aspx
	//			 */
	//			ArrayList _params = new ArrayList();
	//			//
	//			_params.Add( new _Param( _x, _y, _w, _h, m_vChanger.getUV( vCount, _changerTbl[ _index ], 0, 0 ), m_vChanger.getWH() ) );
	//			//
	//			foreach ( _Param _param in _params )
	//			{
	//				GUI.DrawTextureWithTexCoords( _param.m_vXywh, m_vChanger.getTexture(), _param.m_vUvwh );
	//			}
	//		}
	//		else
	//		{
	//			m_bReadyActive = false;
	//		}
	//		//
	//		if ( m_vReadyCounter == ( 3 * _frameRate ) )
	//		{
	//			m_bPlayable = true;
	//		}
	//		//
	//		m_vReadyCounter++;
	//	}
	//}

	//private void _guiTimer( int vCount )
	//{
	//	if ( m_bTimerActive || m_bScoreActive )
	//	{
	//		const float _marginX = 10.0f;
	//		const float _marginY = 10.0f;
	//		const float _paddingX = 4.0f;
	//		const float _paddingY = 4.0f;
	//		//
	//		const float _partsW = 16.0f;
	//		const float _partsH = 32.0f;
	//		const float _frameW = ( _paddingX * 2 ) + ( _partsW * ( 2 + 1 + 2 ) );
	//		const float _frameH = ( _paddingY * 2 ) + ( _partsH * ( 1 ) );
	//		//
	//		const float _lFrameX = _marginX;
	//		const float _lFrameY = _marginY;
	//		const float _rFrameX = AcSetting.SCREEN_W - ( _marginX + _frameW );
	//		const float _rFrameY = _marginY;


	//		/*
	//		 * ArrayList クラス
	//		 * http://msdn.microsoft.com/ja-jp/library/system.collections.arraylist(v=vs.110).aspx
	//		 */
	//		ArrayList _params = new ArrayList();
	//		//
	//		if ( m_bTimerActive )
	//		{
	//			// タイマーフレーム
	//			_params.Add( new _Param( _lFrameX, _lFrameY, _frameW, _frameH, m_vChanger.getUV( vCount, _CHANGER_FRAME_1, 0, 0 ), m_vChanger.getWH() ) );
	//			// タイマーのコロン（フレームに描き込めばいい気もする）
	//			_params.Add( new _Param( _lFrameX + _paddingX + ( _partsW * 2 ), _lFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_COLON, 0, 0 ), m_vChanger.getWH() ) );
	//			// タイマー
	//			_params.Add( new _Param( _lFrameX + _paddingX + ( _partsW * 0 ), _lFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_FIG_0 + ( m_vTimerCounter / 1000 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
	//			_params.Add( new _Param( _lFrameX + _paddingX + ( _partsW * 1 ), _lFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_FIG_0 + ( m_vTimerCounter / 100 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
	//			_params.Add( new _Param( _lFrameX + _paddingX + ( _partsW * 3 ), _lFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_FIG_0 + ( m_vTimerCounter / 10 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
	//			_params.Add( new _Param( _lFrameX + _paddingX + ( _partsW * 4 ), _lFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_FIG_0 + ( m_vTimerCounter / 1 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
	//		}
	//		//
	//		if ( m_bScoreActive )
	//		{
	//			// スコアーフレーム
	//			_params.Add( new _Param( _rFrameX, _rFrameY, _frameW, _frameH, m_vChanger.getUV( vCount, _CHANGER_FRAME_2, 0, 0 ), m_vChanger.getWH() ) );
	//			// スコアー
	//			_params.Add( new _Param( _rFrameX + _paddingX + ( _partsW * 1 ), _rFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_FIG_0 + ( m_vScoreCounter / 100 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
	//			_params.Add( new _Param( _rFrameX + _paddingX + ( _partsW * 2 ), _rFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_FIG_0 + ( m_vScoreCounter / 10 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
	//			_params.Add( new _Param( _rFrameX + _paddingX + ( _partsW * 3 ), _rFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_FIG_0 + ( m_vScoreCounter / 1 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
	//		}
	//		/*
	//		 * イテレータ
	//		 * http://ufcpp.net/study/csharp/sp2_iterator.html
	//		 */
	//		foreach ( _Param _param in _params )
	//		{
	//			GUI.DrawTextureWithTexCoords( _param.m_vXywh, m_vChanger.getTexture(), _param.m_vUvwh );
	//		}
	//		//
	//		switch ( AcSetting.getMode() )
	//		{
	//			//
	//			case ( AcSetting.TIMEATTACK_MODE ):
	//				//
	//				m_vTimerCounter--;
	//				//
	//				if ( m_vTimerCounter == 0 )
	//				{
	//					stop();
	//				}
	//				break;
	//			//
	//			case ( AcSetting.CHALLENGE_MODE ):
	//				m_vTimerCounter++;
	//				break;
	//		}
	//	}
	//}

	private void _guiReady( int vCount )
	{
		if ( m_bReadyActive )
		{
			/*
			 * サイズを決めてセンタリングしてみる
			 */
			float _w = 200.0f;
			float _h = 100.0f;
			float _x = ( AcSetting.SCREEN_W - _w ) / 2;
			float _y = 50.0f;
			//
			/*
			 * ArrayList クラス
			 * http://msdn.microsoft.com/ja-jp/library/system.collections.arraylist(v=vs.110).aspx
			 */
			ArrayList _params = new ArrayList();
			//
//			_params.Add( new _Param( _x, _y, _w, _h, m_vChanger.getUV( vCount, _CHANGER_FIG_0 + ( m_vReadyCounter % 10 ), 0, 0 ), m_vChanger.getWH() ) );
			//
			foreach ( _Param _param in _params )
			{
			//	GUI.DrawTextureWithTexCoords( _param.m_vXywh, m_vChanger.getTexture(), _param.m_vUvwh );
			}
		}
	}

	private void _guiTimer( int vCount )
	{
		if ( m_bTimerActive || m_bScoreActive )
		{
			const float _marginX = 10.0f;
			const float _marginY = 10.0f;
			const float _paddingX = 4.0f;
			const float _paddingY = 4.0f;
			//
			const float _partsW = 16.0f;
			const float _partsH = 32.0f;
			const float _frameW = ( _paddingX * 2 ) + ( _partsW * ( 2 + 1 + 2 ) );
			const float _frameH = ( _paddingY * 2 ) + ( _partsH * ( 1 ) );
			//
			const float _lFrameX = _marginX;
			const float _lFrameY = _marginY;
			const float _rFrameX = AcSetting.SCREEN_W - ( _marginX + _frameW );
			const float _rFrameY = _marginY;


			/*
			 * ArrayList クラス
			 * http://msdn.microsoft.com/ja-jp/library/system.collections.arraylist(v=vs.110).aspx
			 */
			ArrayList _params = new ArrayList();
			//
			//if ( m_bTimerActive )
			//{
			//	// タイマーフレーム
			//	_params.Add( new _Param( _lFrameX, _lFrameY, _frameW, _frameH, m_vChanger.getUV( vCount, _CHANGER_FRAME_1, 0, 0 ), m_vChanger.getWH() ) );
			//	// タイマーのコロン（フレームに描き込めばいい気もする）
			//	_params.Add( new _Param( _lFrameX + _paddingX + ( _partsW * 2 ), _lFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_COLON, 0, 0 ), m_vChanger.getWH() ) );
			//	// タイマー
			//	_params.Add( new _Param( _lFrameX + _paddingX + ( _partsW * 0 ), _lFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_FIG_0 + ( m_vTimerCounter / 1000 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
			//	_params.Add( new _Param( _lFrameX + _paddingX + ( _partsW * 1 ), _lFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_FIG_0 + ( m_vTimerCounter / 100 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
			//	_params.Add( new _Param( _lFrameX + _paddingX + ( _partsW * 3 ), _lFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_FIG_0 + ( m_vTimerCounter / 10 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
			//	_params.Add( new _Param( _lFrameX + _paddingX + ( _partsW * 4 ), _lFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_FIG_0 + ( m_vTimerCounter / 1 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
			//}
			////
			//if ( m_bScoreActive )
			//{
			//	// スコアーフレーム
			//	_params.Add( new _Param( _rFrameX, _rFrameY, _frameW, _frameH, m_vChanger.getUV( vCount, _CHANGER_FRAME_2, 0, 0 ), m_vChanger.getWH() ) );
			//	// スコアー
			//	_params.Add( new _Param( _rFrameX + _paddingX + ( _partsW * 1 ), _rFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_FIG_0 + ( m_vScoreCounter / 100 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
			//	_params.Add( new _Param( _rFrameX + _paddingX + ( _partsW * 2 ), _rFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_FIG_0 + ( m_vScoreCounter / 10 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
			//	_params.Add( new _Param( _rFrameX + _paddingX + ( _partsW * 3 ), _rFrameY + _paddingY, _partsW, _partsH, m_vChanger.getUV( vCount, _CHANGER_FIG_0 + ( m_vScoreCounter / 1 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
			//}
			///*
			// * イテレータ
			// * http://ufcpp.net/study/csharp/sp2_iterator.html
			// */
			//foreach ( _Param _param in _params )
			//{
			//	GUI.DrawTextureWithTexCoords( _param.m_vXywh, m_vChanger.getTexture(), _param.m_vUvwh );
			//}
		}
	}


	private void _guiResult( int vCount )
	{
		if ( m_bResultActive )
		{
			/*
			 * サイズを決めてセンタリングしてみる
			 */
			float _w = 300.0f;
			float _h = 200.0f;
			float _x = ( AcSetting.SCREEN_W - _w ) / 2;
			float _y = 150.0f;
			//
			/*
			 * ArrayList クラス
			 * http://msdn.microsoft.com/ja-jp/library/system.collections.arraylist(v=vs.110).aspx
			 */
			ArrayList _params = new ArrayList();
			//
			//if(m_bResultType)
			//{
			//	_params.Add( new _Param( _x, _y, _w, _h, m_vChanger.getUV( vCount, _CHANGER_SUCCESS, 0, 0 ), m_vChanger.getWH() ) );
			//}
			//else
			//{
			//	_params.Add( new _Param( _x, _y, _w, _h, m_vChanger.getUV( vCount, _CHANGER_FAILURE, 0, 0 ), m_vChanger.getWH() ) );
			//}
			////
			//foreach ( _Param _param in _params )
			//{
			//	GUI.DrawTextureWithTexCoords( _param.m_vXywh, m_vChanger.getTexture(), _param.m_vUvwh );
			//}
		}
	}


	//void OnGUI()
	//{
	//	int _count = Time.frameCount;

	//	_guiReady( _count );
	//	_guiTimer( _count );
	//}

	/*
	 * 誰かの OnGUI() で呼び出してもらうよ！
	 */
	public void update()
	{
		int _count = Time.frameCount;

		_guiReady( _count );
		_guiTimer( _count );
		_guiResult( _count );
	}

	// ========================================================================== //
	// ========================================================================== //

	///*
	// * 操作出来ますか？（カウントダウン中じゃないか？）
	// */
	//public bool isPlayable()
	//{
	//	return ( m_bPlayable );
	//}


	///*
	// * 用意！
	// */
	//public void ready()
	//{
	//	m_bReadyActive = true;
	//	m_bTimerActive = true;
	//	m_bScoreActive = true;
	//	//
	//	switch ( AcSetting.getMode() )
	//	{
	//		//
	//		case ( AcSetting.TIMEATTACK_MODE ):
	//			m_vTimerCounter = 0;
	//			m_vScoreCounter = AcSetting.TIMEATTACH_DOORS;
	//			break;
	//		//
	//		case ( AcSetting.CHALLENGE_MODE ):
	//			m_vTimerCounter = AcSetting.CHALLENGE_TIME;
	//			m_vScoreCounter = 0;
	//			break;
	//	}
	//}

	///*
	// * タイマースタート
	// */
	//public void start()
	//{
	//	m_bTimerEnable = true;
	//}

	///*
	// * タイマーストップ
	// */
	//public void stop()
	//{
	//	m_bTimerEnable = false;
	//	m_bPlayable = false;
	//}

	///*
	// * ドアをクリア！
	// */
	//public void next()
	//{
	//	switch ( AcSetting.getMode() )
	//	{
	//		//
	//		case ( AcSetting.TIMEATTACK_MODE ):
	//			//
	//			m_vScoreCounter++;
	//			break;
	//		//
	//		case ( AcSetting.CHALLENGE_MODE ):
	//			//
	//			m_vScoreCounter--;
	//			//
	//			if ( m_vScoreCounter == 0 )
	//			{
	//				stop();
	//			}
	//			break;
	//	}
	//}

	//public int getTimer()
	//{
	//	return ( m_vTimerCounter );
	//}

	//public int getScore()
	//{
	//	return ( m_vScoreCounter );
	//}


	public void swReadyActive( bool bSw )
	{
		m_bReadyActive = bSw;
	}

	public void swTimerActive( bool bSw )
	{
		m_bTimerActive = bSw;
	}

	public void swScoreActive( bool bSw )
	{
		m_bScoreActive = bSw;
	}

	public void swResultActive( bool bSw )
	{
		m_bResultActive = bSw;
	}

	public void setReady( int vReady )
	{
		m_vReadyCounter = vReady;
	}

	public void setTimer( int vTimer )
	{
		m_vTimerCounter = vTimer;
	}

	public void setScore( int vScore )
	{
		m_vScoreCounter = vScore;
	}

	public void swResultType( bool bSw )
	{
		m_bResultType = bSw;
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	//public interface OnTrigger
	//{
	//	public abstract void OnStart();
	//	public abstract void OnTimeUp();
	//	public abstract void OnTimeUp();
	//}

	//public interface OnClearListener
	//{
	//}

	//public interface OnClearListener
	//{
	//}

	// ========================================================================== //
	// ========================================================================== //


	//// use this for initialization
	//void Start()
	//{

	//}

	//// update is called once per frame
	//void Update()
	//{

	//}

	// ========================================================================== //
	// ========================================================================== //

}
