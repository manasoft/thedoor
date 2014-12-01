using UnityEngine;
using System.Collections;

public class AcGuiDoor : AcGuiBase
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
	/// <param name="vDoor"></param>
	public AcGuiDoor( float vX, float vY, int vDoor )
		: base( vX, vY, vDoor )
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

		// ２桁
		int[] _values = new int[ 2 ];

		//
		if ( m_vValueInt < 0 )
		{
			_values[ 0 ] = _CHANGER_FIG_0;		// ← 表示しない
			_values[ 1 ] = _CHANGER_HYPHEN;
		}
		else
		{
			int _value = m_vValueInt;
			// 99 以上は 99 で表示
			if ( _value > 99 )
			{
				_value = 99;
			}
			//	
			_values[ 0 ] = _CHANGER_FIG_0 + ( _value / 10 % 10 );
			_values[ 1 ] = _CHANGER_FIG_0 + ( _value / 1 % 10 );
		}

		//
		clear();
		//
		if ( !bRanking )
		{
			// ドアフレーム
			add(
				_x,
				_y,
				_frame_w,
				_frame_h,
				_CHANGER_FRAME_2
			);
			//
			add(
				_x + _padding_x + ( _parts_w * 3 ),
				_y + _padding_y,
				_parts_w,
				_parts_h,
				_CHANGER_FIG_0 + ( AcApp.GAMERULE_TIMEATTACK_DOOR / 10 % 10 )
			);
			//
			add(
				_x + _padding_x + ( _parts_w * 4 ),
				_y + _padding_y,
				_parts_w,
				_parts_h,
				_CHANGER_FIG_0 + ( AcApp.GAMERULE_TIMEATTACK_DOOR / 1 % 10 )
			);
		}
		// ドアの数字
		//if ( ( _value / 10 % 10 ) > 0 )
		if ( _values[ 0 ] != _CHANGER_FIG_0 )
		{
			add(
				_x + _padding_x + ( _parts_w * 0 ),
				_y + _padding_y,
				_parts_w,
				_parts_h,
				//_CHANGER_FIG_0 + ( _value / 10 % 10 )
				_values[ 0 ]
			);
		}
		//
		add(
			_x + _padding_x + ( _parts_w * 1 ),
			_y + _padding_y,
			_parts_w,
			_parts_h,
			//_CHANGER_FIG_0 + ( _value / 1 % 10 )
			_values[ 1 ]
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
		//AcGuiBase.draw( new Rect( 0, 0, AcApp.SCREEN_W, AcApp.SCREEN_H ), _CHANGER_BACKGROUND_DOOR );
		AcGuiBase.draw( new Rect( 0, AcRanking.OFFSET_Y, AcApp.SCREEN_W, AcApp.SCREEN_H ), _CHANGER_BACKGROUND_DOOR );
	}

	// ========================================================================== //
	// ========================================================================== //
}
