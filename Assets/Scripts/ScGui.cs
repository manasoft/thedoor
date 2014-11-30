using UnityEngine;
using System.Collections;

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

	public static void createDialog( string vTitle, string vPositiveText, string vNegativeText )
	{
		//ScGui _gui = ScGui.Create();
		//
		new _Dialog( vTitle, vPositiveText, vNegativeText );
		//
		//return ( _gui );
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

	private class _Box : _Base
	{
		public _Box( Rect vRect, string vText )
			: base( vRect, new GUIContent( vText ), null )
		{
		}

		public override void onGui()
		{
			GUI.Box( m_vRect, m_vContent );
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

		public override void onGui()
		{
			if ( GUI.Button( m_vRect, m_vContent ))//, m_vStyle ) )
			{
				onTrigger();
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	private class _Dialog : _Base
	{
		ScGui m_vBox;
		ScGui m_vPositiveButton;
		ScGui m_vNegativeButton;

		public _Dialog( string vTitle, string vPositiveText, string vNegativeText )
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
	private abstract class _Base
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
