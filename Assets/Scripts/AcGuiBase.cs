using UnityEngine;
using System.Collections;

/// <summary>
/// AcGuiTime と AcGuiDoor の基底クラス
/// </summary>
public class AcGuiBase
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

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	protected const int _CHANGER_FIG_0 = 0;
	protected const int _CHANGER_FIG_1 = 1;
	protected const int _CHANGER_FIG_2 = 2;
	protected const int _CHANGER_FIG_3 = 3;
	protected const int _CHANGER_FIG_4 = 4;
	protected const int _CHANGER_FIG_5 = 5;
	protected const int _CHANGER_FIG_6 = 6;
	protected const int _CHANGER_FIG_7 = 7;
	protected const int _CHANGER_FIG_8 = 8;
	protected const int _CHANGER_FIG_9 = 9;
	protected const int _CHANGER_COLON = 10;
	protected const int _CHANGER_FRAME_1 = 11;
	protected const int _CHANGER_FRAME_2 = 12;
	protected const int _CHANGER_SUCCESS = 13;
	protected const int _CHANGER_FAILURE = 14;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * [.NET]C#でconst配列？ 
	 * 
	 * http://genz0.blogspot.jp/2009/12/netcconst.html
	 * ReadOnlyCollection<string> arrayData = Array.AsReadOnly<string>( new string[] { "111", "222" } );
	 */


	//private static readonly AcTextureChanger.Data[] _changerTbl =
	//{
	//	//
	//	new AcTextureChanger.Data(
	//		"Images/Gui/gui_1", 8, 8, 4,
	//		new int[] { 0, 1, 2, 3, },		// 0
	//		new int[] { 4, 5, 6, 7, },		// 1
	//		new int[] { 8, 9, 10, 11, },		// 2
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

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	////private const float _MARGIN_X = 0.0f;
	////private const float _MARGIN_Y = 0.0f;
	//private const float _PADDING_X = 4.0f;
	//private const float _PADDING_Y = 4.0f;
	////
	//private const float _PARTS_W = 16.0f;
	//private const float _PARTS_H = 32.0f;
	//private const float _FRAME_W = ( _PADDING_X * 2 ) + ( _PARTS_W * ( 2 + 1 + 2 ) );
	//private const float _FRAME_H = ( _PADDING_Y * 2 ) + ( _PARTS_H * ( 1 ) );

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	protected int m_vValueInt;
	protected float m_vValueFloat;
	//
	protected float m_vX;
	protected float m_vY;
	//protected float m_vBaseScale;
	//protected float m_vSizeScale;

	/// <summary>
	/// 同じチェンジャーを共有出来るように中に置くのではなく外に置けるようにする（同じテクスチャのメモリを持たないようにするためだよ）
	/// </summary>
	protected AcTextureChanger m_vChanger;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 初期化
	/// </summary>
	private void _ini()
	{
		m_vValueInt = 0;
		m_vValueFloat = 0.0f;
		m_vX = 0.0f;
		m_vY = 0.0f;
		//m_vBaseScale = 1.0f;
		//m_vSizeScale = 1.0f;
		m_vChanger = null;
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	///// <summary>
	///// デフォルトコンストラクタ
	///// ※デフォルトコンストラクタが無いクラスは継承が出来ないっぽいっす
	///// </summary>
	//public AcGuiBase()
	//{
	//	_ini();
	//}

	///// <summary>
	///// コンストラクタ
	///// </summary>
	///// <param name="vValue"></param>
	///// <param name="vX"></param>
	///// <param name="vY"></param>
	///// <param name="vScale"></param>
	///// <param name="vChanger"></param>
	//public AcGuiBase( int vValue, float vX, float vY, float vScale, AcTextureChanger vChanger )
	//{
	//	_ini();
	//	//
	//	m_vValue = vValue;
	//	m_vX = vX;
	//	m_vY = vY;
	//	m_vBaseScale = vScale;
	//	m_vChanger = vChanger;
	//}

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="vValue"></param>
	/// <param name="vX"></param>
	/// <param name="vY"></param>
	/// <param name="vChanger"></param>
	public AcGuiBase( int vValue, float vX, float vY, AcTextureChanger vChanger )
	{
		_ini();
		//
		m_vValueInt = vValue;
		m_vX = vX;
		m_vY = vY;
		m_vChanger = vChanger;
	}
	public AcGuiBase( float vValue, float vX, float vY, AcTextureChanger vChanger )
	{
		_ini();
		//
		m_vValueFloat = vValue;
		m_vX = vX;
		m_vY = vY;
		m_vChanger = vChanger;
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * 
	 * 
	 */

	//public virtual void onGUI()
	//{
	//}

	/// <summary>
	/// オーバーライドしてね！
	/// </summary>
	/// <param name="vBaseScale"></param>
	/// <param name="vSizeScale"></param>
	public virtual void onGUI( float vBaseScale, float vSizeScale )
	{
	}

	//public virtual float getFrameW()
	//{
	//	return ( 0.0f );
	//}

	//public virtual float getFrameH()
	//{
	//	return ( 0.0f );
	//}

	// ========================================================================== //
	// ========================================================================== //

	//public static AcTextureChanger.Data getTextureChangerData()
	//{
	//	AcTextureChanger.Data[] _changerTbl =
	//	{
	//		//
	//		new AcTextureChanger.Data(
	//			"Images/Gui/gui_1", 8, 8, 4,
	//			new int[] { 0, 1, 2, 3, },		// 0
	//			new int[] { 4, 5, 6, 7, },		// 1
	//			new int[] { 8, 9, 10, 11, },		// 2
	//			new int[] { 12, 13, 14, 15, },		// 3
	//			new int[] { 16, 17, 18, 19, },		// 4
	//			new int[] { 20, 21, 22, 23, },		// 5
	//			new int[] { 24, 25, 26, 27, },		// 6
	//			new int[] { 28, 29, 30, 31, },		// 7
	//			new int[] { 32, 33, 34, 35, },		// 8
	//			new int[] { 36, 37, 38, 39, },		// 9
	//			new int[] { 40, 41, 42, 43, },		// :
	//			new int[] { 48, 49, 50, 51, },		// 赤フレーム
	//			new int[] { 56, 57, 58, 59, },		// 黄プレーム:
	//			new int[] { 44, 45, 46, 47, },		// 成功
	//			new int[] { 52, 53, 54, 55, }		// 失敗
	//		),
	//	};

	//	return ( _changerTbl[ 0 ] );
	//}

	public static AcTextureChanger getTextureChanger()
	{
		AcTextureChanger _textureChanger = new AcTextureChanger();
		//
		_textureChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4, new int[] { 0, 1, 2, 3, } );			// 0
		_textureChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4, new int[] { 4, 5, 6, 7, } );			// 1
		_textureChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4, new int[] { 8, 9, 10, 11, } );			// 2
		_textureChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4, new int[] { 12, 13, 14, 15, } );		// 3
		_textureChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4, new int[] { 16, 17, 18, 19, } );		// 4
		_textureChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4, new int[] { 20, 21, 22, 23, } );		// 5
		_textureChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4, new int[] { 24, 25, 26, 27, } );		// 6
		_textureChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4, new int[] { 28, 29, 30, 31, } );		// 7
		_textureChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4, new int[] { 32, 33, 34, 35, } );		// 8
		_textureChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4, new int[] { 36, 37, 38, 39, } );		// 9
		_textureChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4, new int[] { 40, 41, 42, 43, } );		// :
		_textureChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4, new int[] { 48, 49, 50, 51, } );		// 赤フレーム
		_textureChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4, new int[] { 56, 57, 58, 59, } );		// 黄プレーム:
		_textureChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4, new int[] { 44, 45, 46, 47, } );		// 成功
		_textureChanger.add( "Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4, new int[] { 52, 53, 54, 55, } );		// 失敗
		//
		return ( _textureChanger );
	}

	//public static AcTextureChanger.Data getTextureChangerData()
	//{
	//	return (
	//		new AcTextureChanger.Data(
	//			"Images" + AcUtil.getLanguageSuffix() + "/gui_1", 8, 8, 4,
	//			new int[] { 0, 1, 2, 3, },		// 0
	//			new int[] { 4, 5, 6, 7, },		// 1
	//			new int[] { 8, 9, 10, 11, },		// 2
	//			new int[] { 12, 13, 14, 15, },		// 3
	//			new int[] { 16, 17, 18, 19, },		// 4
	//			new int[] { 20, 21, 22, 23, },		// 5
	//			new int[] { 24, 25, 26, 27, },		// 6
	//			new int[] { 28, 29, 30, 31, },		// 7
	//			new int[] { 32, 33, 34, 35, },		// 8
	//			new int[] { 36, 37, 38, 39, },		// 9
	//			new int[] { 40, 41, 42, 43, },		// :
	//			new int[] { 48, 49, 50, 51, },		// 赤フレーム
	//			new int[] { 56, 57, 58, 59, },		// 黄プレーム:
	//			new int[] { 44, 45, 46, 47, },		// 成功
	//			new int[] { 52, 53, 54, 55, }		// 失敗
	//		)
	//	);
	//	//		return ( _changerTbl[ 0 ] );
	//}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 描画情報のデータを持たせる構造体だよ
	/// </summary>
	protected class _Data
	{
		public Texture m_vTexture;
		public Rect m_vXywh;
		public Rect m_vUvwh;

		//public _Data( float vX, float vY, float vW, float vH, float vScale, Vector2 vUv, Vector2 vWh )
		//{
		//	m_vXywh = new Rect( vX * vScale, vY * vScale, vW * vScale, vH * vScale );
		//	m_vUvwh = new Rect( vUv.x, vUv.y, vWh.x, vWh.y );
		//}

		//public _Data( float vX, float vY, float vW, float vH, float vBaseScale, float vSizeScale, Vector2 vUv, Vector2 vWh )
		//{
		//	m_vXywh = new Rect(
		//		( vX * vBaseScale ) - ( vW * vBaseScale * ( vSizeScale - 1.0f ) / 2.0f ),
		//		( vY * vBaseScale ) - ( vH * vBaseScale * ( vSizeScale - 1.0f ) / 2.0f ),
		//		vW * vBaseScale * vSizeScale,
		//		vH * vBaseScale * vSizeScale );
		//	m_vUvwh = new Rect( vUv.x, vUv.y, vWh.x, vWh.y );
		//}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="vX"></param>
		/// <param name="vY"></param>
		/// <param name="vW"></param>
		/// <param name="vH"></param>
		/// <param name="vUv"></param>
		/// <param name="vWh"></param>
		//public _Data( float vX, float vY, float vW, float vH, Vector2 vUv, Vector2 vWh )
		//{
		//	m_vXywh = new Rect( vX, vY, vW, vH );
		//	m_vUvwh = new Rect( vUv.x, vUv.y, vWh.x, vWh.y );
		//}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="vTexture"></param>
		/// <param name="vX"></param>
		/// <param name="vY"></param>
		/// <param name="vW"></param>
		/// <param name="vH"></param>
		/// <param name="vUv"></param>
		/// <param name="vWh"></param>
		public _Data( Texture vTexture, float vX, float vY, float vW, float vH, Vector2 vUv, Vector2 vWh )
		{
			m_vTexture = vTexture;
			m_vXywh = new Rect( vX, vY, vW, vH );
			m_vUvwh = new Rect( vUv.x, vUv.y, vWh.x, vWh.y );
		}
	}

	// ========================================================================== //
	// ========================================================================== //
}
