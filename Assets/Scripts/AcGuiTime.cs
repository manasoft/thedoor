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

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="vX"></param>
	/// <param name="vY"></param>
	/// <param name="vTime"></param>
	public AcGuiTime( float vX, float vY, float vTime )
		: base( vX, vY, vTime )
	{
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	//public static float getFrameW()
	//{
	//	return ( _FRAME_W );
	//}

	//public static float getFrameH()
	//{
	//	return ( _FRAME_H );
	//}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="vScale"></param>
	/// <param name="bRanking"></param>
	public void onGui( float vScale, bool bRanking )
	{
		float _x = m_vX - ( _FRAME_W * ( vScale - 1.0f ) / 2.0f );
		float _y = m_vY - ( _FRAME_H * ( vScale - 1.0f ) / 2.0f );

		float _padding_x = _PADDING_X * vScale;
		float _padding_y = _PADDING_Y * vScale;
		float _frame_w = _FRAME_W * vScale;
		float _frame_h = _FRAME_H * vScale;
		float _parts_w = _PARTS_W * vScale;
		float _parts_h = _PARTS_H * vScale;

		// ４桁
		int[] _values = new int[ 4 ];

		//
		if(m_vValueFloat < 0.0f)
		{
			_values[ 0 ] = _CHANGER_HYPHEN;
			_values[ 1 ] = _CHANGER_HYPHEN;
			_values[ 2 ] = _CHANGER_HYPHEN;
			_values[ 3 ] = _CHANGER_HYPHEN;
		}
		else
		{
			int _value = ( int ) ( m_vValueFloat * 100.0f ); // [1/100秒] 表示に変換するよ
			// 9999 以上は 9999 で表示
			if ( _value > 9999 )
			{
				_value = 9999;
			}
			//	
			_values[ 0 ] = _CHANGER_FIG_0 + (_value / 1000 % 10);
			_values[ 1 ] = _CHANGER_FIG_0 + (_value / 100 % 10);
			_values[ 2 ] = _CHANGER_FIG_0 + (_value / 10 % 10);
			_values[ 3 ] = _CHANGER_FIG_0 + (_value / 1 % 10);
		}

		////		int _value = ( int ) ( m_vValueInt * 100.0f / 60 ); // フレーム数を [1/100秒] 表示に変換するよ
		//int _value = ( int ) ( m_vValueFloat * 100.0f ); // [1/100秒] 表示に変換するよ
		//// 9999 以上は 9999 で表示
		//if ( _value > 9999 )
		//{
		//	_value = 9999;
		//}

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
			//_CHANGER_FIG_0 + ( _value / 1000 % 10 )
			_values[ 0 ]
		);
		add(
			_x + _padding_x + ( _parts_w * 1 ),
			_y + _padding_y,
			_parts_w,
			_parts_h,
			//_CHANGER_FIG_0 + ( _value / 100 % 10 )
			_values[ 1 ]
		);
		add(
			_x + _padding_x + ( _parts_w * 3 ),
			_y + _padding_y,
			_parts_w,
			_parts_h,
			//_CHANGER_FIG_0 + ( _value / 10 % 10 )
			_values[ 2 ]
		);
		add(
			_x + _padding_x + ( _parts_w * 4 ),
			_y + _padding_y,
			_parts_w,
			_parts_h,
			//_CHANGER_FIG_0 + ( _value / 1 % 10 )
			_values[ 3 ]
		);
		//
		draw();
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 背景はワンショットで・・・
	/// </summary>
	public static void onGui_Background()
	{
		/*
		 * 2014/12/01 変更
		 */
		//AcGuiBase.draw( new Rect( 0, 0, AcApp.SCREEN_W, AcApp.SCREEN_H ), _CHANGER_BACKGROUND_TIME );
		AcGuiBase.draw( new Rect( 0, AcRanking.OFFSET_Y, AcApp.SCREEN_W, AcApp.SCREEN_H ), _CHANGER_BACKGROUND_TIME );
	}

	// ========================================================================== //
	// ========================================================================== //
}
