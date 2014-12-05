using UnityEngine;
using System.Collections;

// C# List<T>
using System.Collections.Generic;

/// <summary>
/// Gui でダイアログ等、まとめようという作戦
/// </summary>
public class ScGui : MonoBehaviour
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

	/// <summary>
	/// デリゲートだよ
	/// 
	/// 今後 OnClick() とかに変更されるかもよ・・・
	/// </summary>
	public delegate void OnTrigger();

	// ========================================================================== //
	// ========================================================================== //

	private _Base m_vBase;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private Component _create( System.Type vType )
	{
		GameObject _object = new GameObject();
		//
		Component _component = _object.AddComponent( vType );
		//
		_object.name = _component.GetType().FullName;
		//
		return ( _component );
	}

	public static ScGui Create()
	{
		GameObject _object = new GameObject();
		//
		ScGui _class = ( ScGui ) _object.AddComponent( ( typeof( ScGui ) ) );
		//
		_object.name = _class.GetType().FullName;
		//
		_class._create();
		//
		return ( _class );
	}
	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _create()
	{
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 画面上のドット数を指定すると、それっぽいフォントサイズを返す（正確じゃないよ）
	/// コレ Unity で用意して欲しい関数だよな
	/// </summary>
	/// <param name="vHeight"></param>
	/// <returns></returns>
	public static int calcFontSize( float vHeight )
	{
		GUIStyle _style = new GUIStyle();
		//
		const int _limit = 32; // 一応安全のため最大値を設定しておく
		//
		for ( int _count = 0; _count < _limit; _count++ )
		{
			_style.fontSize = _count + 1;
			Vector2 _vector = _style.CalcSize( new GUIContent( "あ" ) );
			if ( _vector.y > vHeight )
			{
				return ( _count );
			}
		}
		//
		return ( _limit );
	}

	/// <summary>
	/// ・処理が重い！
	/// </summary>
	/// <param name="vString"></param>
	/// <param name="vWidth"></param>
	/// <param name="vFontSize"></param>
	/// <returns></returns>
	public static string autoLinefeed( string vString, float vWidth, int vFontSize )
	{
		//ScDebug.debugLog( "autoLinefeed / start" );
	
		string _string = "";
		//
		string[] _lines = vString.Split( '\n' );
		//
		GUIStyle _style = new GUIStyle();
		_style.fontSize = vFontSize;
		//
		foreach ( string _line in _lines )
		{
			string _temp = _line;
			//
			while ( _temp.Length > 0 )
			{
				for ( int _length = _temp.Length; _length > 0; _length-- )
				{
					Vector2 _vector = _style.CalcSize( new GUIContent( _temp.Substring( 0, _length ) ) );
					//
					if ( _vector.x < vWidth )
					{
						_string += _temp.Substring( 0, _length ) + "\n";
						_temp = _temp.Substring( _length );
						break;
					}
				}

	
				//for ( int _length = 1; _length < _temp.Length; _length++ )
				//{
				//	Vector2 _vector = _style.CalcSize( new GUIContent( _temp.Substring( 0, _length ) ) );
				//	//
				//	if ( _vector.x > vWidth )
				//	{
				//		_string += _temp.Substring( 0, _length - 1 ) + "\n";
				//		_temp = _temp.Substring( _length );
				//		break;
				//	}
				//}
			}
		}
		//
		//ScDebug.debugLog( "autoLinefeed / end" );
		//
		return ( _string );
	}

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	public static ScGui createBox( Rect vRect, string vText )
	{
		ScGui _gui = ScGui.Create();
		//
		_gui.m_vBase = new ScGui._Box( vRect, vText );
		//
		return ( _gui );
	}

	public static ScGui createButton( Rect vRect, string vText, ScGui.OnTrigger vOnTrigger )
	{
		ScGui _gui = ScGui.Create();
		//
		_gui.m_vBase = new _Button( vRect, vText, vOnTrigger );
		////
		//_gui.setOnTrigger( vOnTrigger );
		//
		return ( _gui );
	}

	//public static void createDialog( string vTitle, string vPositiveText, string vNegativeText )
	//{
	//	//ScGui _gui = ScGui.Create();
	//	//
	//	new _Dialog_3( vTitle, vPositiveText, vNegativeText );
	//	//
	//	//return ( _gui );
	//}

	public static ScGui.Dialog createDialog()
	{


		ScGui.Dialog _gui = new ScGui.Dialog();
		//
		return ( _gui );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	//public ScGui setOnTrigger( ScGui.SiGuiTrigger vTrigger )
	//{
	//	m_vBase.setOnTrigger( vTrigger );
	//	//
	//	return ( this );
	//}

	/// <summary>
	/// リスナーのセット
	/// 
	/// _Base の中で
	/// </summary>
	/// <param name="vOnTrigger"></param>
	/// <returns></returns>
	public ScGui setOnTrigger( ScGui.OnTrigger vOnTrigger )
	{
		m_vBase.m_vOnTrigger += vOnTrigger;
		//
		return ( this );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnGUI()
	{
		if ( m_vBase != null )
		{
			m_vBase.onGui();
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public class _Box : _Base
	{
		public _Box( Rect vRect, string vText )
			: base( vRect, new GUIContent( vText ), null )
		{
		}

		public _Box()
			: base()
		{
		}

		public _Box( Rect vRect, GUIContent vContent, GUIStyle vStyle )
			: base( vRect, vContent, vStyle )
		{
		}

		public override void onGui()
		{
			GUI.Box( m_vRect, m_vContent, m_vStyle );
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	private class _Button : _Base
	{
		public _Button( Rect vRect, string vText, ScGui.OnTrigger vOnTrigger )
			: base( vRect, new GUIContent( vText ), null )
		{
		}

		public _Button( Rect vRect, GUIContent vContent, GUIStyle vStyle, ScGui.OnTrigger vOnTrigger )
			: base( vRect, vContent, vStyle, vOnTrigger )
		{
		}

		public override void onGui()
		{
			if ( GUI.Button( m_vRect, m_vContent, m_vStyle ) )
			{
				onTrigger();
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	private class _Dialog_3 : _Base
	{
		ScGui m_vBox;
		ScGui m_vPositiveButton;
		ScGui m_vNegativeButton;

		public _Dialog_3( string vTitle, string vPositiveText, string vNegativeText )
		{
			// 画面サイズからのマージンを決める
			float _margin_w = 0.1f;
			float _margin_h = 0.4f;

			// マージンからダイアログの大きさを決める
			float _w = Screen.width * ( 1.0f - _margin_w );
			float _h = Screen.height * ( 1.0f - _margin_h );

			// ダイアログの位置
			float _x = ( Screen.width - _w ) / 2;
			float _y = ( Screen.height - _h ) / 2;

			int _button_num = 2;
			float _button_margin = 0.01f;

			float _button_w = ( (_w -(_w * _button_num * _button_margin)) / _button_num );
			float _button_h = ( _h / 3 ) * 0.9f;

			float _button_x_start = _x + _w * _button_margin;
			float _button_x_offset = _button_w + _w * _button_margin;
			
			//float _button_x = _x + (((_w / 2) - _button_w)/2);
			float _button_y = _y + (_h / 2);

			m_vBox = ScGui.createBox( new Rect( _x, _y, _w, _h ), vTitle );
			//
			m_vPositiveButton = ScGui.createButton(
				new Rect( _button_x_start, _button_y, _button_w, _button_h ),
				vPositiveText,
				() =>
				{
				}
			);
			m_vNegativeButton = ScGui.createButton(
				new Rect( _button_x_start + _button_x_offset * 1, _button_y, _button_w, _button_h ),
				vNegativeText,
				() =>
				{
				}
			);

		}

		public override void onGui()
		{
			m_vBox.OnGUI();
			m_vPositiveButton.OnGUI();
			m_vNegativeButton.OnGUI();
		}
	}

	//public class Dialog : _Base
	//{
	//	public Dialog()
	//		: base()
	//	{
	//		/*
	//		 * 基本画面サイズいっぱいに表示する
	//		 */
	//		// 画面サイズからのマージンを決める
	//		float _margin_w = 0.1f;
	//		float _margin_h = 0.4f;

	//		// マージンからダイアログの大きさを決める
	//		float _w = Screen.width * ( 1.0f - _margin_w );
	//		float _h = Screen.height * ( 1.0f - _margin_h );

	//		// ダイアログの位置
	//		float _x = ( Screen.width - _w ) / 2;
	//		float _y = ( Screen.height - _h ) / 2;

	//		m_vRect.Set( _x, _y, _w, _y );
	//	}

	//	public override void onGui()
	//	{
	//		//base.onGui();
	//	}
	//}
	public class Dialog : _Box
	{
		public Dialog()
		{
			/*
			 * 基本画面サイズいっぱいに表示する
			 */
			// 画面サイズからのマージンを決める
			float _margin_w = 0.1f;
			float _margin_h = 0.4f;

			// マージンからダイアログの大きさを決める
			float _w = Screen.width * ( 1.0f - _margin_w );
			float _h = Screen.height * ( 1.0f - _margin_h );

			// ダイアログの位置
			float _x = ( Screen.width - _w ) / 2;
			float _y = ( Screen.height - _h ) / 2;

			m_vRect.Set( _x, _y, _w, _h );
		}

		public override void onGui()
		{
			base.onGui();
		}
	}


	//private class _List : _Base
	//{
	//	ScGui m_vBox;

	//	public override void onGui()
	//	{

	//	}
	//}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// abstract のクラスを作ってみた
	/// 以下、うろ覚えの知識
	/// ・抽象クラスなのでインスタンス出来ない
	/// ・誰かに継承してもらう
	/// 等
	/// </summary>
	public abstract class _Base
	{
		//	public SiGuiTrigger m_vTrigger;

		public Rect m_vRect;
		public GUIContent m_vContent;
		public GUIStyle m_vStyle;

		/// <summary>
		/// インターフェースの代わりにデリゲートでイベントリスナー的な事をやってみる
		/// </summary>
		public ScGui.OnTrigger m_vOnTrigger;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public _Base()
		{
			//m_vTrigger = null;

			m_vRect = new Rect();
			m_vContent = null;
			m_vStyle = null;
			m_vOnTrigger = null;
		}

		public _Base( Rect vRect, GUIContent vContent, GUIStyle vStyle )
			: this()
		{
			m_vRect = vRect;
			m_vContent = vContent;
			m_vStyle = vStyle;
			m_vOnTrigger = null;
		}

		public _Base( Rect vRect, GUIContent vContent, GUIStyle vStyle, ScGui.OnTrigger vOnTrigger )
			: this( vRect, vContent, vStyle )
		{
			setOnTrigger( vOnTrigger );
		}

		/// <summary>
		/// リスナーのセット
		/// </summary>
		/// <param name="vOnTrigger"></param>
		/// <returns></returns>
		public ScGui._Base setOnTrigger( ScGui.OnTrigger vOnTrigger )
		{
			m_vOnTrigger += vOnTrigger;
			//
			return ( this );
		}

		//public _Base setOnTrigger( SiGuiTrigger vTrigger )
		//{
		//	//m_vTrigger = vTrigger;
		//	m_vOnTrigger += vTrigger.onTrigger;

		//	//
		//	return ( this );
		//}

		//public _Base setOnTrigger( OnTrigger vTrigger )
		//{
		//	//m_vTrigger = vTrigger;
		//	m_vOnTrigger += vTrigger;

		//	//
		//	return ( this );
		//}

		/// <summary>
		/// デリゲートの実行処理？
		/// </summary>
		/// <returns></returns>
		public _Base onTrigger()
		{
			//if (m_vTrigger !=null)
			//{
			//	//m_vTrigger.onTrigger();
			//	m_vTrigger.m_vTrigger();
			//}
			if ( m_vOnTrigger != null )
			{
				m_vOnTrigger();
			}
			//
			return ( this );
		}

		/// <summary>
		/// 描画処理は abstract なので必ず実装してね！
		/// </summary>
		public abstract void onGui();


	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	///// <summary>
	///// トリガーのインターフェースなんだけど C# で作っている関係でデリゲートを使った方がいいみたいで
	///// 結局、使わない？
	///// </summary>
	//public interface SiGuiTrigger
	//{
	//	//public delegate void onTrigger();
	//	void onTrigger();
	//	//onTrigger m_vTrigger;
	//}

	// ========================================================================== //
	// ========================================================================== //
}
