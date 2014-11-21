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
		END,
	};

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	private const int _OBJECT_BACKGROUND = 0;
	private const int _OBJECT_NUM = 1;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private static readonly string[] _objectTbl =
	{
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

	//private Texture2D m_vTexture;

	//private Color m_vColor;

	private IEnumerator m_vIEnumerator;

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
		//
		Texture2D _texture = new Texture2D( 1, 1, TextureFormat.RGB24, false );
		_texture.SetPixel( 0, 0, Color.white );
		_texture.Apply();
		//
		m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.mainTexture = _texture;
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

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	public void clear()
	{
		_stop();
		//
		m_vGameObject[ _OBJECT_BACKGROUND ].SetActive( false );
	}

	public void color( Color vColor )
	{
		_stop();
		//
		m_vGameObject[ _OBJECT_BACKGROUND ].SetActive( true );
		m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.color = vColor;
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	public void white( float vAlpha = 1.0f )
	{
		Color _color = Color.white;
		_color.a = vAlpha;
		color( _color );
	}

	public void black( float vAlpha = 1.0f )
	{
		Color _color = Color.black;
		_color.a = vAlpha;
		color( _color );
	}

	// ========================================================================== //
	// ========================================================================== //

	private void _stop()
	{
		if ( m_vIEnumerator != null )
		{
			StopCoroutine( m_vIEnumerator );
			m_vIEnumerator = null;
		}
	}

	//private void setCoroutine()
	//{
	//	if ( m_vIEnumerator != null )
	//	{
	//		StopCoroutine( m_vIEnumerator );
	//		m_vIEnumerator = null;
	//	}
	//	//
	//	m_vIEnumerator = _coroutine();
	//	StartCoroutine( m_vIEnumerator );
	//}


	//private IEnumerator _coroutine()
	//{
	//	int _alpha = 0;

	//	while ( true )
	//	{
	//		_alpha = ( _alpha + 1 ) % 100;
	//		m_vColor.a = _alpha / 100.0f;
	//		yield return null;
	//	}
	//}

	public void fade( Color vSrc, Color vDst, float vTime )
	{
		_stop();
		//
		m_vIEnumerator = _coroutine( vSrc, vDst, vTime );
		StartCoroutine( m_vIEnumerator );
	}


	private IEnumerator _coroutine( Color vSrc, Color vDst, float vTime )
	{
		float _time = 0.0f;
		Color _color = new Color();
		//
		m_vGameObject[ _OBJECT_BACKGROUND ].SetActive( true );
		//
		while ( _time <= vTime )
		{
			_debugLog( "フェード中！" );

			float _t = _time / vTime;
			//
			_color.r = Mathf.Lerp( vSrc.r, vDst.r, _t );
			_color.g = Mathf.Lerp( vSrc.g, vDst.g, _t );
			_color.b = Mathf.Lerp( vSrc.b, vDst.b, _t );
			_color.a = Mathf.Lerp( vSrc.a, vDst.a, _t );
			//
			m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.color = _color;
			//
			_time += Time.deltaTime;
			//
			yield return null;
		}
		//
		m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.color = vDst;
	}


	// ========================================================================== //
	// ========================================================================== //

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//	Color _color = new Color( 0, 0, 0, 0.01f );

		//	m_vGameObject[ _OBJECT_BACKGROUND ].renderer.material.color -= _color;
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
}
