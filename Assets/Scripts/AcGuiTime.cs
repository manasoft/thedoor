using UnityEngine;
using System.Collections;

public class AcGuiTime : AcGuiBase
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * 
	 * 
	 * 
	 */
	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//private const float _MARGIN_X = 0.0f;
	//private const float _MARGIN_Y = 0.0f;
	//private const float _PADDING_X = 120.0f;
	private const float _PADDING_X = 120.0f;
	private const float _PADDING_Y = -1.0f;
	//
	private const float _PARTS_W = 26.0f;
	private const float _PARTS_H = 39.0f;
	private const float _FRAME_W = 312.0f;	//( _PADDING_X * 2 ) + ( _PARTS_W * ( 2 + 1 + 2 ) );
	private const float _FRAME_H = 37.0f;	//( _PADDING_Y * 2 ) + ( _PARTS_H * ( 1 ) );

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //


	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	///// <summary>
	///// コンストラクタ
	///// </summary>
	///// <param name="vTime"></param>
	///// <param name="vX"></param>
	///// <param name="vY"></param>
	///// <param name="vScale"></param>
	///// <param name="vChanger"></param>
	//public AcGuiTime( int vTime, float vX, float vY, float vScale, AcTextureChanger vChanger )
	//	: base( vTime, vX, vY, vScale, vChanger )
	//{
	//}

	//public AcGuiTime( float vTime, float vX, float vY, AcTextureChanger vChanger )
	//	: base( vTime, vX, vY, vChanger )
	//{
	//}

	//public AcGuiTime( MonoBehaviour vMonoBehaviour, float vX, float vY, float vTime )
	//	: base( vMonoBehaviour, vX, vY, vTime )
	//{
	//}

	public AcGuiTime( float vX, float vY, float vTime )
		: base( vX, vY, vTime )
	{
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public static float getFrameW()
	{
		return ( _FRAME_W );
	}

	public static float getFrameH()
	{
		return ( _FRAME_H );
	}

	//public override void onGUI()
	//{
	//	int _count = Time.frameCount;

	//	ArrayList _data = new ArrayList();

	//	// タイマーフレーム
	//	_data.Add( new _Data( m_vX, m_vY, _FRAME_W, _FRAME_H, m_vBaseScale, m_vChanger.getUV( _count, _CHANGER_FRAME_1, 0, 0 ), m_vChanger.getWH() ) );
	//	// タイマーのコロン（フレームに描き込めばいい気もする）
	//	_data.Add( new _Data( m_vX + _PADDING_X + ( _PARTS_W * 2 ), m_vY + _PADDING_Y, _PARTS_W, _PARTS_H, m_vBaseScale, m_vChanger.getUV( _count, _CHANGER_COLON, 0, 0 ), m_vChanger.getWH() ) );
	//	// タイマー
	//	_data.Add( new _Data( m_vX + _PADDING_X + ( _PARTS_W * 0 ), m_vY + _PADDING_Y, _PARTS_W, _PARTS_H, m_vBaseScale, m_vChanger.getUV( _count, _CHANGER_FIG_0 + ( m_vValue / 1000 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
	//	_data.Add( new _Data( m_vX + _PADDING_X + ( _PARTS_W * 1 ), m_vY + _PADDING_Y, _PARTS_W, _PARTS_H, m_vBaseScale, m_vChanger.getUV( _count, _CHANGER_FIG_0 + ( m_vValue / 100 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
	//	_data.Add( new _Data( m_vX + _PADDING_X + ( _PARTS_W * 3 ), m_vY + _PADDING_Y, _PARTS_W, _PARTS_H, m_vBaseScale, m_vChanger.getUV( _count, _CHANGER_FIG_0 + ( m_vValue / 10 % 10 ), 0, 0 ), m_vChanger.getWH() ) );
	//	_data.Add( new _Data( m_vX + _PADDING_X + ( _PARTS_W * 4 ), m_vY + _PADDING_Y, _PARTS_W, _PARTS_H, m_vBaseScale, m_vChanger.getUV( _count, _CHANGER_FIG_0 + ( m_vValue / 1 % 10 ), 0, 0 ), m_vChanger.getWH() ) );

	//	foreach ( _Data __data in _data )
	//	{
	//		GUI.DrawTextureWithTexCoords( __data.m_vXywh, m_vChanger.getTexture(), __data.m_vUvwh );
	//	}
	//}

	//public void onGUI_2( float vBaseScale, float vSizeScale )
	//{
	//	int _timer = Time.frameCount;

	//	float _x = ( m_vX * vBaseScale ) - ( _FRAME_W * vBaseScale * ( vSizeScale - 1.0f ) / 2.0f );
	//	float _y = ( m_vY * vBaseScale ) - ( _FRAME_H * vBaseScale * ( vSizeScale - 1.0f ) / 2.0f );

	//	float _scale = vBaseScale * vSizeScale;

	//	float _padding_x = _PADDING_X * _scale;
	//	float _padding_y = _PADDING_Y * _scale;
	//	float _frame_w = _FRAME_W * _scale;
	//	float _frame_h = _FRAME_H * _scale;
	//	float _parts_w = _PARTS_W * _scale;
	//	float _parts_h = _PARTS_H * _scale;

	//	//		int _value = ( int ) ( m_vValueInt * 100.0f / 60 ); // フレーム数を [1/100秒] 表示に変換するよ
	//	int _value = ( int ) ( m_vValueFloat * 100.0f ); // [1/100秒] 表示に変換するよ
	//	// 9999 以上は 9999 で表示
	//	if ( _value > 9999 )
	//	{
	//		_value = 9999;
	//	}

	//	ArrayList _data = new ArrayList();

	//	int _index;

	//	// タイマーフレーム
	//	_index = _CHANGER_FRAME_1;
	//	_data.Add( new _ImageList(
	//		m_vChanger.getTexture( _index ),
	//		_x,
	//		_y,
	//		_frame_w,
	//		_frame_h,
	//		m_vChanger.getUV( _timer, _index, 0 ),
	//		m_vChanger.getWH( _index ) ) );

	//	// タイマーのコロン（フレームに描き込めばいい気もする）
	//	_index = _CHANGER_COLON;
	//	_data.Add( new _ImageList(
	//		m_vChanger.getTexture( _index ),
	//		_x + _padding_x + ( _parts_w * 2 ),
	//		_y + _padding_y,
	//		_parts_w,
	//		_parts_h,
	//		m_vChanger.getUV( _timer, _index, 0 ),
	//		m_vChanger.getWH( _index ) ) );
	//	// タイマー
	//	_index = _CHANGER_FIG_0 + ( _value / 1000 % 10 );
	//	_data.Add( new _ImageList(
	//		m_vChanger.getTexture( _index ),
	//		_x + _padding_x + ( _parts_w * 0 ),
	//		_y + _padding_y,
	//		_parts_w,
	//		_parts_h,
	//		m_vChanger.getUV( _timer, _index, 0 ),
	//		m_vChanger.getWH( _index ) ) );
	//	//
	//	_index = _CHANGER_FIG_0 + ( _value / 100 % 10 );
	//	_data.Add( new _ImageList(
	//		m_vChanger.getTexture( _index ),
	//		_x + _padding_x + ( _parts_w * 1 ),
	//		_y + _padding_y,
	//		_parts_w,
	//		_parts_h,
	//		m_vChanger.getUV( _timer, _index, 0 ),
	//		m_vChanger.getWH( _index ) ) );
	//	//
	//	_index = _CHANGER_FIG_0 + ( _value / 10 % 10 );
	//	_data.Add( new _ImageList(
	//		m_vChanger.getTexture( _index ),
	//		_x + _padding_x + ( _parts_w * 3 ),
	//		_y + _padding_y,
	//		_parts_w,
	//		_parts_h,
	//		m_vChanger.getUV( _timer, _index, 0 ),
	//		m_vChanger.getWH( _index ) ) );
	//	//
	//	_index = _CHANGER_FIG_0 + ( _value / 1 % 10 );
	//	_data.Add( new _ImageList(
	//		m_vChanger.getTexture( _index ),
	//		_x + _padding_x + ( _parts_w * 4 ),
	//		_y + _padding_y,
	//		_parts_w,
	//		_parts_h,
	//		m_vChanger.getUV( _timer, _index, 0 ),
	//		m_vChanger.getWH( _index ) ) );

	//	//
	//	foreach ( _ImageList __data in _data )
	//	{
	//		//			GUI.DrawTextureWithTexCoords( __data.m_vXywh, m_vChanger.getTexture(), __data.m_vUvwh );
	//		GUI.DrawTextureWithTexCoords( __data.m_vXywh, __data.m_vTexture, __data.m_vUvwh );
	//	}
	//}

	public override void onGUI( float vBaseScale, float vSizeScale, bool bRanking )
	{
		float _x = ( m_vX * vBaseScale ) - ( _FRAME_W * vBaseScale * ( vSizeScale - 1.0f ) / 2.0f );
		float _y = ( m_vY * vBaseScale ) - ( _FRAME_H * vBaseScale * ( vSizeScale - 1.0f ) / 2.0f );

		float _scale = vBaseScale * vSizeScale;

		float _padding_x = _PADDING_X * _scale;
		float _padding_y = _PADDING_Y * _scale;
		float _frame_w = _FRAME_W * _scale;
		float _frame_h = _FRAME_H * _scale;
		float _parts_w = _PARTS_W * _scale;
		float _parts_h = _PARTS_H * _scale;

		//		int _value = ( int ) ( m_vValueInt * 100.0f / 60 ); // フレーム数を [1/100秒] 表示に変換するよ
		int _value = ( int ) ( m_vValueFloat * 100.0f ); // [1/100秒] 表示に変換するよ
		// 9999 以上は 9999 で表示
		if ( _value > 9999 )
		{
			_value = 9999;
		}

		//
		clear();
		//
		if ( !bRanking )
		{
			// タイマーフレーム
			add(
				_x,
				_y,
				_frame_w,
				_frame_h,
				_CHANGER_FRAME_1
			);
		}
		// タイマーの数字
		add(
			_x + _padding_x + ( _parts_w * 0 ),
			_y + _padding_y,
			_parts_w,
			_parts_h,
			_CHANGER_FIG_0 + ( _value / 1000 % 10 )
		);
		add(
			_x + _padding_x + ( _parts_w * 1 ),
			_y + _padding_y,
			_parts_w,
			_parts_h,
			_CHANGER_FIG_0 + ( _value / 100 % 10 )
		);
		add(
			_x + _padding_x + ( _parts_w * 3 ),
			_y + _padding_y,
			_parts_w,
			_parts_h,
			_CHANGER_FIG_0 + ( _value / 10 % 10 )
		);
		add(
			_x + _padding_x + ( _parts_w * 4 ),
			_y + _padding_y,
			_parts_w,
			_parts_h,
			_CHANGER_FIG_0 + ( _value / 1 % 10 )
		);
		//
		draw();
	}

	public void onGui_Background( float vBaseScale, float vSizeScale )
	{
		float _scale = vBaseScale * vSizeScale;
		//
		clear();
		//
		add(
			0,
			0,
			AcApp.SCREEN_W * _scale,
			AcApp.SCREEN_H * _scale,
			_CHANGER_BACKGROUND_TIME
		);
		//
		draw();
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //
}
