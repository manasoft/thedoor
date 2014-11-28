using UnityEngine;
using System.Collections;

// List<T>
using System.Collections.Generic;

/// <summary>
/// AcGuiTime と AcGuiDoor の基底クラス
/// </summary>
public class AcGuiBase : Object
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * 2014/11/28
	 * Gui の描画で少々わかった事・・・
	 * ・カメラが 2D/3D はあんまし関係ないよね
	 * ・大きさの違う画面サイズは縦方向のサイズを基準にして（保証されていて）横が増えたり減ったりする
	 * ・座標は左上から右下にプラス
	 * ・Screen.width / Screen.height は実際の液晶サイズ
	 * 
	 * 上記を考慮して描画すると
	 * ・位置の設定を簡単にするため仮想画面を設定する（例：480 x 800）
	 * ・実際の画面サイスを取得する（Screen.width / Screen.height）して・・・
	 * ・縦方向の画面サイズから、スケール（float）を求め、次に、横方向へのオフセット（float）を計算する
	 * 
	 * 注意点
	 * ・表示位置などは AcApp.SCREEN_W / AcApp.SCREEN_H を基準に指定するので、後から AcApp.SCREEN_W / AcApp.SCREEN_H を変更してはいけませんよ！
	 */

	/*
	 * [.NET]C#でconst配列？ 
	 * 
	 * http://genz0.blogspot.jp/2009/12/netcconst.html
	 * ReadOnlyCollection<string> arrayData = Array.AsReadOnly<string>( new string[] { "111", "222" } );
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
	protected const int _CHANGER_SUCCESS_CHALLENGE = 14;
	protected const int _CHANGER_FAILURE = 15;
	protected const int _CHANGER_COUNTDOWN_0 = 16;
	protected const int _CHANGER_COUNTDOWN_1 = 17;
	protected const int _CHANGER_COUNTDOWN_2 = 18;
	protected const int _CHANGER_COUNTDOWN_3 = 19;
	protected const int _CHANGER_BACKGROUND_TIME = 20;
	protected const int _CHANGER_BACKGROUND_DOOR = 21;
	protected const int _CHANGER_CURSOR = 22;
	protected const int _CHANGER_HYPHEN = 23;

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

	/// <summary>
	/// 2014/11/11
	/// コルーチン処理させたいので MonoBehaviour を "親" から貰います
	/// と思って実装してたんですけど、これは良くないなと・・・
	/// やるなら、
	/// １：AcGuiBase 自体がオブジェクトを持って（プレハブからインスタンス）
	/// ２：既に行っているように AcPlayer からコルーチン処理するためのメソッドを用意する（setResultPosition(x,y) 等）方が良い
	/// なのでこのメンバーは削除する予定です
	/// 使用禁止！
	/// </summary>
	//[System.Obsolete]
	//protected MonoBehaviour m_vMonoBehaviour;

	/// <summary>
	/// int 表示用の値（ドアの枚数、等）
	/// </summary>
	protected int m_vValueInt;

	/// <summary>
	/// float 表示用の値（時間、等）
	/// </summary>
	protected float m_vValueFloat;
	
	/// <summary>
	/// ポジション
	/// </summary>
	protected float m_vX;

	/// <summary>
	/// ポジション
	/// </summary>
	protected float m_vY;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 2014/11/09 追加
	/// </summary>
	private List<_ImageList> m_vImageList;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// スタティックな画像データ
	/// 配列の順番がインデックスになっているよ！
	/// </summary>
	private static readonly _ImageData[] m_vImageData =
	{
		new _ImageData( AcApp.IMAGE_NUMBER, 0 ),		// 0
		new _ImageData( AcApp.IMAGE_NUMBER, 1 ),		// 1
		new _ImageData( AcApp.IMAGE_NUMBER, 2 ),		// 2
		new _ImageData( AcApp.IMAGE_NUMBER, 3 ),		// 3
		new _ImageData( AcApp.IMAGE_NUMBER, 4 ),		// 4
		new _ImageData( AcApp.IMAGE_NUMBER, 5 ),		// 5
		new _ImageData( AcApp.IMAGE_NUMBER, 6 ),		// 6
		new _ImageData( AcApp.IMAGE_NUMBER, 7 ),		// 7
		new _ImageData( AcApp.IMAGE_NUMBER, 8 ),		// 8
		new _ImageData( AcApp.IMAGE_NUMBER, 9 ),		// 9
		new _ImageData( AcApp.IMAGE_NUMBER, 0 ),		// 10
		new _ImageData( AcApp.IMAGE_TEX_M, 1 ),			// 11
		new _ImageData( AcApp.IMAGE_TEX_M, 0 ),			// 12
		new _ImageData( AcApp.IMAGE_TEX_L, 4 ),			// 13
		new _ImageData( AcApp.IMAGE_TEX_L, 6 ),			// 14
		new _ImageData( AcApp.IMAGE_TEX_L, 5 ),			// 15
		//
		new _ImageData( AcApp.IMAGE_TEX_L, 0 ),			// 16
		new _ImageData( AcApp.IMAGE_TEX_L, 13 ),		// 17
		new _ImageData( AcApp.IMAGE_TEX_L, 12 ),		// 18
		new _ImageData( AcApp.IMAGE_TEX_L, 11 ),		// 19
		//
		new _ImageData( AcApp.IMAGE_TEX_L, 9 ),			// 20
		new _ImageData( AcApp.IMAGE_TEX_L, 8 ),			// 21
		//
		new _ImageData( AcApp.IMAGE_TEX_M, 2 ),			// 22
		//
		new _ImageData( AcApp.IMAGE_NUMBER, 10 ),		// 23
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //

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
		//
		m_vImageList = new List<_ImageList>();
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// コンストラクタ
	/// </summary>
	public AcGuiBase()
	{
		_ini();
	}

	public AcGuiBase( float vX, float vY )
		: this()
	{
		m_vX = vX;
		m_vY = vY;
	}

	public AcGuiBase( float vX, float vY, int vValue )
		: this( vX, vY )
	{
		m_vValueInt = vValue;
	}

	public AcGuiBase( float vX, float vY, float vValue )
		: this( vX, vY )
	{
		m_vValueFloat = vValue;
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public void clear()
	{
		m_vImageList.Clear();
	}

	public void add( Rect vRect , int vIndex )
	{
		m_vImageList.Add( new _ImageList( vRect, vIndex ) );
	}

	public void add( float vX, float vY, float vW, float vH, int vIndex )
	{
		m_vImageList.Add( new _ImageList( new Rect( vX, vY, vW, vH ), vIndex ) );
	}

	/// <summary>
	/// GUI は render じゃなくて draw の方がいいのかな？
	/// </summary>
	public void draw()
	{
		// 高さを基準にスケールを計算するよ（Unity は高さ基準っぽいので・・・）
		float _scale = ( float ) Screen.height / AcApp.SCREEN_H;
		// 横方向へのオフセットを計算するよ（高さを基準にしたので、高さのオフセットはありません）
		float _offset_x = ( Screen.width - ( AcApp.SCREEN_W * _scale ) ) / 2;
		//
		foreach ( _ImageList _list in m_vImageList )
		{
			Rect _rect = new Rect(
				_list.m_vXywh.x * _scale + _offset_x,
				_list.m_vXywh.y * _scale,
				_list.m_vXywh.width * _scale,
				_list.m_vXywh.height * _scale
			);
			//
			AcApp.imageDraw( _rect, _list.m_vEntryName, _list.m_vIndex );
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// ワンショット描画！
	/// </summary>
	/// <param name="vRect"></param>
	/// <param name="vIndex"></param>
	protected static void draw( Rect vRect, int vIndex )
	{
		AcGuiBase _base = new AcGuiBase();
		//
		_base.clear();
		_base.add( vRect, vIndex );
		_base.draw();
	}

	/// <summary>
	/// ワンショット描画でカーソルを表示するよ
	/// </summary>
	/// <param name="vRect"></param>
	public static void drawCursor( Rect vRect )
	{
		AcGuiBase _base = new AcGuiBase();
		//
		_base.clear();
		_base.add( vRect, _CHANGER_CURSOR );
		_base.draw();
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 描画情報のデータを持たせる構造体だよ
	/// </summary>
	protected class _ImageList
	{
		public Rect m_vXywh;
		public string m_vEntryName;
		public int m_vIndex;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="vRect"></param>
		/// <param name="vIndex"></param>
		public _ImageList( Rect vRect, int vIndex )
		{
			m_vXywh = vRect;
			m_vEntryName = m_vImageData[ vIndex ].m_vEntryName;
			m_vIndex = m_vImageData[ vIndex ].m_vIndex;
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 画像の管理を何でするか？
	/// AcImageManager は文字列で行っているが、数字を扱っているので数字の方が良くない？
	/// 
	/// 例えば 0 を表示したい場合 _IMAGE_ID_0 を int で 0 にしたら
	/// 0 から AcImageManager への変換処理が必要になるよなぁ
	/// そのための変換データを管理するクラスを作る
	/// 
	/// または AcImageManager で "gui_0" ~ "gui_9" とか名前を全部つけて別画像で管理してもらうか？
	/// この方法が王道っぽいのだが、ちと面倒な感じもあるんだよね・・・いや、どっちにしろ面倒かな？
	/// </summary>
	private class _ImageData
	{
		public string m_vEntryName;
		public int m_vIndex;

		public _ImageData( string vEntryName, int vIndex )
		{
			m_vEntryName = vEntryName;
			m_vIndex = vIndex;
		}
	}

	// ========================================================================== //
	// ========================================================================== //
}
