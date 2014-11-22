using UnityEngine;
using System.Collections;

/// <summary>
/// 動的に呼び出すオブジェクト用のスクリプトです
/// 
/// Ac じゃなく Sc でも行けそうだが、その場合はポジションを引数で受け取るようにしないとダメだな
/// </summary>
public class AcFade : MonoBehaviour
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

	/// <summary>
	/// トリガー（イベント発生時のトリガーです）
	/// </summary>
	public enum Trigger
	{
		FADING,
		FADED,
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	public enum Layer
	{
		OBJECT = 0,
		GUI,
		NUM,
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _OBJECT_CAMERA = 0;
	private const int _OBJECT_BACKGROUND = 1;
	private const int _OBJECT_NUM = 2;


	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private static readonly string[] _objectTbl =
	{
		"Camera",
		"Background",
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 
	/// </summary>
	/// <param name="vObjectId"></param>
	/// <returns></returns>
	private string _getObjectName( int vObjectId )
	{
		string[] _string = _objectTbl[ vObjectId ].Split( '/' );
		//
		return ( _string[ _string.Length - 1 ] );
	}

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

	/// <summary>
	/// 親
	/// </summary>
	private AcGameManager m_vManager;

	/// <summary>
	/// 
	/// </summary>
	private AiFadeTrigger m_vTrigger;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private GameObject[] m_vGameObject;

	/// <summary>
	/// オブジェクトだけなら保持する必要が無かったけど Gui で行う場合は保持する必要があるので持っとくよ！
	/// </summary>
	private Texture2D m_vTexture;

	private bool m_bGui;

	/// <summary>
	/// Gui 用・・・
	/// </summary>
	private Color m_vGuiColor;

	//private Texture2D m_vTexture;

	//private Color m_vColor;

	private IEnumerator m_vIEnumerator;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private _Lerp[] m_vLerp;

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// プレハブからインスタンスするよ
	/// </summary>
	/// <param name="vManager"></param>
	/// <param name="vTrigger"></param>
	/// <returns></returns>
	public static AcFade Create( AcGameManager vManager, AiFadeTrigger vTrigger )
	{
		GameObject _prefab = ( GameObject ) Resources.Load( "Prefabs/Fade" );
		//
		//		GameObject _object = ( GameObject ) Instantiate( _prefab, Vector3.zero, Quaternion.identity );
		GameObject _object = ( GameObject ) Instantiate( _prefab, AcApp.HidePosition, Quaternion.identity );
		//
		AcFade _class = _object.GetComponent<AcFade>();
		//
		_class._create( vManager, vTrigger );
		//
		return ( _class );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _create( AcGameManager vManager, AiFadeTrigger vTrigger )
	{
		_debugLog( "_create" );
		//
		m_vManager = vManager;
		m_vTrigger = vTrigger;
		//
		_setObject();
		_setRender();
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _setObject()
	{
		m_vGameObject = new GameObject[ _OBJECT_NUM ];
		//
		for ( int count = 0; count < _OBJECT_NUM; count++ )
		{
			m_vGameObject[ count ] = this.transform.FindChild( _objectTbl[ count ] ).gameObject;
		}
		
		/*
		 * C# - enumの項目数を取得する - Qiita
		 * http://qiita.com/kazuhirox/items/12319707ba2eb650e9e8
		 */
		//int _count = System.Enum.GetNames( typeof( Layer ) ).Length;

		var _tbl = new[]
		{
			new { _layer = Layer.OBJECT,	},
			new { _layer = Layer.GUI,		},
		};

		m_vLerp = new _Lerp[ _tbl.Length ];
		//
		for ( int _count = 0; _count < _tbl.Length; _count ++)
		{
			m_vLerp[ _count ] = new _Lerp( this, new _FadeTrigger( this, _tbl[ _count ]._layer ) );
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private void _setRender()
	{
		//var _tbl = new[]
		//{
		//	new { _object = _OBJECT_BACKGROUND,		_image = AcApp.IMAGE_TEX_L,		_index = ( AcApp.getGameMode() == AcApp.GAMEMODE_TIMEATTACK ) ? 9 : 8, },
		//	new { _object = _OBJECT_BUTTON_OK,		_image = AcApp.IMAGE_TEX_S,		_index = 4, },
		//};
		//
		//foreach ( var _var in _tbl )
		//{
		//	AcApp.imageRender( m_vGameObject[ _var._object ].renderer, _var._image, _var._index );
		//}

		/*
		 * unity_script_create_texture2d - FreeStyleWiki
		 * http://ft-lab.ne.jp/cgi-bin-unity/wiki.cgi?page=unity_script_create_texture2d
		 */
		/*
		 * 【Unity】オブジェクトの透明度を減算していくスクリプト。
		 * https://gist.github.com/Buravo46/8366967
		 */

		m_vGameObject[ _OBJECT_BACKGROUND ].SetActive( false );
		/*
		 * 1:大きさ 1x1 のテクスチャを作る
		 * 2:テクスチャの (0,0) の位置を ffffff にする
		 * 3:変更を反映させる
		 */
		//Texture2D _texture = new Texture2D( 1, 1, TextureFormat.RGB24, false );
		m_vTexture = new Texture2D( 1, 1, TextureFormat.RGB24, false );
		m_vTexture.SetPixel( 0, 0, Color.white );
		m_vTexture.Apply();
		//
		m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.mainTexture = m_vTexture;
		///*
		// * 後から変更しても反映される事を確認したよ（2014/11/21）
		// */
		//_texture.SetPixel( 0, 0, Color.white );
		//_texture.Apply();
		/*
		 * vRenderer.material.SetTextureOffset( "_MainTex", vRect.position );
		 * vRenderer.material.SetTextureScale( "_MainTex", vRect.size );
		 */
		//m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.color = Color.white;
		//	m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.color = new Color( 0.0f, 0.0f, 0.7f, 0.5f );

		//	white();
		//	black( 0.3f );

		//fade(
		//	new Color( 1.0f, 0.0f, 0.0f, 1.0f ),
		//	new Color( 0.0f, 1.0f, 0.0f, 0.0f ),
		//	10.0f
		//);

	}


	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public void setActive( bool bSw )
	{
		this.gameObject.SetActive( bSw );
	}

	/// <summary>
	/// カメラの depth 変更（複数カメラ時設定）
	/// 
	/// 使わないけど参考までに・・・
	/// しかも動作確認してない
	/// </summary>
	/// <param name="vDepth"></param>
	public void setCameraDepth( int vDepth )
	{
		m_vGameObject[ _OBJECT_BACKGROUND ].camera.depth = vDepth;
		//m_vGameObject[ _OBJECT_BACKGROUND ].camera.clearFlags = CameraClearFlags.Depth;
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	public void stop( AcFade.Layer vLayer )
	{
		m_vLerp[ ( int ) vLayer ].stop();
	}

	public void start( AcFade.Layer vLayer, Color vSrc, Color vDst, float vTime )
	{
		stop( vLayer );
		//
		switch ( vLayer )
		{
			case ( AcFade.Layer.OBJECT ):
				m_vGameObject[ _OBJECT_BACKGROUND ].SetActive( true );
				break;
			//
			case ( AcFade.Layer.GUI ):
				m_bGui = true;
				break;
		}
		//
		m_vLerp[ ( int ) vLayer ].start( vSrc, vDst, vTime );
	}

	public void color( AcFade.Layer vLayer, Color vColor )
	{
		stop( vLayer );
		//
		switch ( vLayer )
		{
			case ( AcFade.Layer.OBJECT ):
				m_vGameObject[ _OBJECT_BACKGROUND ].SetActive( true );
				m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.color = vColor;
				break;
			//
			case ( AcFade.Layer.GUI ):
				m_bGui = true;
				m_vGuiColor = vColor;
				break;
		}
	}

	public void clear( AcFade.Layer vLayer )
	{
		stop( vLayer );
		//
		switch ( vLayer )
		{
			case ( AcFade.Layer.OBJECT ):
				m_vGameObject[ _OBJECT_BACKGROUND ].SetActive( false );
				break;
			//
			case ( AcFade.Layer.GUI ):
				m_bGui = false;
				break;
		}
	}

	//public void clear()
	//{
	//	stop();
	//	//
	//	m_vGameObject[ _OBJECT_BACKGROUND ].SetActive( false );
	//}

	//public void fade( Color vSrc, Color vDst, float vTime )
	//{
	//	_stop();
	//	//
	//	m_vIEnumerator = _coroutine( vSrc, vDst, vTime );
	//	StartCoroutine( m_vIEnumerator );
	//}

	///// <summary>
	///// 色を直接セットする
	///// </summary>
	///// <param name="vColor"></param>
	//public void color( Color vColor )
	//{
	//	stop();
	//	//
	//	m_vGameObject[ _OBJECT_BACKGROUND ].SetActive( true );
	//	m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.color = vColor;
	//}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//public void white( float vAlpha = 1.0f )
	//{
	//	Color _color = Color.white;
	//	_color.a = vAlpha;
	//	color( _color );
	//}

	//public void black( float vAlpha = 1.0f )
	//{
	//	Color _color = Color.black;
	//	_color.a = vAlpha;
	//	color( _color );
	//}

	// ========================================================================== //
	// ========================================================================== //

	///// <summary>
	///// フェード中止
	///// </summary>
	//private void _stop()
	//{
	//	if ( m_vIEnumerator != null )
	//	{
	//		StopCoroutine( m_vIEnumerator );
	//		m_vIEnumerator = null;
	//	}
	//}

	////private void setCoroutine()
	////{
	////	if ( m_vIEnumerator != null )
	////	{
	////		StopCoroutine( m_vIEnumerator );
	////		m_vIEnumerator = null;
	////	}
	////	//
	////	m_vIEnumerator = _coroutine();
	////	StartCoroutine( m_vIEnumerator );
	////}


	////private IEnumerator _coroutine()
	////{
	////	int _alpha = 0;

	////	while ( true )
	////	{
	////		_alpha = ( _alpha + 1 ) % 100;
	////		m_vColor.a = _alpha / 100.0f;
	////		yield return null;
	////	}
	////}

	///// <summary>
	///// コルーチンをスタートするよ
	///// </summary>
	///// <param name="vSrc"></param>
	///// <param name="vDst"></param>
	///// <param name="vTime"></param>
	//public void fade( Color vSrc, Color vDst, float vTime )
	//{
	//	_stop();
	//	//
	//	m_vIEnumerator = _coroutine( vSrc, vDst, vTime );
	//	StartCoroutine( m_vIEnumerator );
	//}

	///// <summary>
	///// フェード処理するよ
	///// </summary>
	///// <param name="vSrc"></param>
	///// <param name="vDst"></param>
	///// <param name="vTime"></param>
	///// <returns></returns>
	//private IEnumerator _coroutine( Color vSrc, Color vDst, float vTime )
	//{
	//	m_vGameObject[ _OBJECT_BACKGROUND ].SetActive( true );
	//	//
	//	if ( vTime > 0.0f )
	//	{
	//		float _time = 0.0f;
	//		//Color _color = new Color();
	//		//
	//		while ( _time < vTime )
	//		{
	//			//_debugLog( "フェード中！" );

	//			//float _t = _time / vTime;
	//			//
	//			//_color.r = Mathf.Lerp( vSrc.r, vDst.r, _t );
	//			//_color.g = Mathf.Lerp( vSrc.g, vDst.g, _t );
	//			//_color.b = Mathf.Lerp( vSrc.b, vDst.b, _t );
	//			//_color.a = Mathf.Lerp( vSrc.a, vDst.a, _t );
	//			//
	//			//m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.color = _color;

	//			// ↑でやってたけど↓のやり方を発見！（2014/11/22）
	//			m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.color = Color.Lerp( vSrc, vDst, _time / vTime );

	//			// Gui では変数にとっておく必要があるよ
	//			//	m_vColor = Color.Lerp( vSrc, vDst, _time / vTime );

	//			_time += Time.deltaTime;
	//			//
	//			yield return null;
	//		}
	//	}
	//	//
	//	m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.color = vDst;
	//	//
	//	m_vTrigger.onTrigger( Trigger.FADED );
	//}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void _awake()
	{
	}

	private void _onGui()
	{
		if ( m_bGui )
		{
			//透明度を更新して黒テクスチャを描画
			//		GUI.color = new Color( 0, 0, 0, this.fadeAlpha );
			//		GUI.color = new Color( 0.5f, 0, 0, 0.2f );
			GUI.color = m_vGuiColor;
			//GUI.depth = 100000;
			//GUI.DrawTexture( new Rect( 0, 0, Screen.width / 2, Screen.height / 2 ), m_vTexture );
			GUI.DrawTexture( new Rect( 0, 0, Screen.width, Screen.height ), m_vTexture );
		}
	}


	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	void Awake()
	{
		_awake();
	}

	public void OnGUI()
	{
		_onGui();
	}

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 
	/// </summary>
	public interface AiFadeTrigger
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="vTrigger"></param>
		void onTrigger( AcFade.Trigger vTrigger );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private class _FadeTrigger : _Lerp._LerpTrigger
	{
		/// <summary>
		/// 親
		/// </summary>
		private AcFade m_vFade;

		/// <summary>
		/// 
		/// </summary>
		private AcFade.Layer m_vLayer;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="vFade"></param>
		/// <param name="vLayer"></param>
		public _FadeTrigger( AcFade vFade, AcFade.Layer vLayer )
		{
			m_vFade = vFade;
			m_vLayer = vLayer;
		}

		public void stop()
		{
			m_vFade.m_vLerp[ ( int ) m_vLayer ].stop();
		}

		public void start( Color vSrc, Color vDst, float vTime )
		{
			stop();
			//
			m_vFade.m_vLerp[ ( int ) m_vLayer ].start( vSrc, vDst, vTime );
		}

		public void onTrigger( _Lerp.Trigger vTrigger, Color vColor )
		{
			switch ( m_vLayer )
			{
				//
				case ( AcFade.Layer.OBJECT ):
					m_vFade.m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.color = vColor;
					break;
				//
				case ( AcFade.Layer.GUI ):
					m_vFade.m_vGuiColor = vColor;
					break;
			}
			//
			switch ( vTrigger )
			{
				//
				case ( _Lerp.Trigger.FADING ):
					m_vFade.m_vTrigger.onTrigger( AcFade.Trigger.FADING );
					break;
				//
				case ( _Lerp.Trigger.FADED ):
					m_vFade.m_vTrigger.onTrigger( AcFade.Trigger.FADED );
					break;
			}
		}
	}
	
	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 線形補間でフェード処理するクラスです
	/// </summary>
	private class _Lerp
	{
		/// <summary>
		/// トリガー（イベント発生時のトリガーです）
		/// </summary>
		public enum Trigger
		{
			FADING,
			FADED,
		};

		/// <summary>
		/// 親（コルーチン実行時に必要）
		/// </summary>
		private AcFade m_vFade;

		private _Lerp._LerpTrigger m_vTrigger;

		/// <summary>
		/// コルーチン制御用
		/// </summary>
		private IEnumerator m_vIEnumerator;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="vFade"></param>
		/// <param name="vTrigger"></param>
		public _Lerp( AcFade vFade, _LerpTrigger vTrigger )
		{
			m_vFade = vFade;
			m_vTrigger = vTrigger;
			m_vIEnumerator = null;
		}

		/// <summary>
		/// フェードを停止するよ
		/// </summary>
		public void stop()
		{
			if ( m_vIEnumerator != null )
			{
				m_vFade.StopCoroutine( m_vIEnumerator );
				m_vIEnumerator = null;
			}
		}

		/// <summary>
		/// フェードを開始するよ（コルーチンをスタートするよ）
		/// </summary>
		/// <param name="vSrc"></param>
		/// <param name="vDst"></param>
		/// <param name="vTime"></param>
		public void start( Color vSrc, Color vDst, float vTime )
		{
			stop();
			//
			m_vIEnumerator = _coroutine( vSrc, vDst, vTime );
			m_vFade.StartCoroutine( m_vIEnumerator );
		}

		/// <summary>
		/// フェード処理用のコルーチンです
		/// </summary>
		/// <param name="vSrc"></param>
		/// <param name="vDst"></param>
		/// <param name="vTime"></param>
		/// <returns></returns>
		private IEnumerator _coroutine( Color vSrc, Color vDst, float vTime )
		{
			if ( vTime > 0.0f )
			{
				float _time = 0.0f;
				//
				while ( _time < vTime )
				{
					m_vTrigger.onTrigger( Trigger.FADING, Color.Lerp( vSrc, vDst, _time / vTime ) );
					//
					_time += Time.deltaTime;
					//
					yield return null;
				}
			}
			//
			m_vTrigger.onTrigger( Trigger.FADED, vDst );
		}

		public interface _LerpTrigger
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="vTrigger"></param>
			void onTrigger( _Lerp.Trigger vTrigger, Color vColor );
		}
	}

	// ========================================================================== //
	// ========================================================================== //
}
