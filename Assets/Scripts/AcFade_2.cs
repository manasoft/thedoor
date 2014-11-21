using UnityEngine;
using System.Collections;

public class AcFade_2 : MonoBehaviour
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

	/*
	 * 【Unity】シーン遷移時のフェードイン・フェードアウトを実装してみた  naichilab - Android iOSアプリ開発メモ
	 * http://naichilab.blogspot.jp/2013/12/unity.html
	 */

	/// <summary>暗転用黒テクスチャ</summary>
	private Texture2D blackTexture;
	/// <summary>フェード中の透明度</summary>
	private float fadeAlpha = 0;
	/// <summary>フェード中かどうか</summary>
	private bool isFading = false;

	private Color m_vColor;

	private IEnumerator m_vIEnumerator;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public static AcFade_2 Create()
	{
		GameObject _object = new GameObject();
		//
		AcFade_2 _class = ( AcFade_2 ) _object.AddComponent( ( typeof( AcFade_2 ) ) );
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

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void _awake()
	{
		/*
		 * Unity - Unity Manual
		 * http://docs-jp.unity3d.com/Documentation/ScriptReference/Texture2D.html
		 */



		//this.blackTexture = new Texture2D( 32, 32, TextureFormat.RGB24, false );
		//this.blackTexture.ReadPixels( new Rect( 0, 0, 32, 32 ), 0, 0, false );
		//this.blackTexture.SetPixel( 0, 0, Color.white );
		//this.blackTexture.Apply();
		this.blackTexture = new Texture2D( 1, 1, TextureFormat.RGB24, false );
		this.blackTexture.SetPixel( 0, 0, Color.white );	// 多分これで ffffff にして color で掛け合わせる？
		//this.blackTexture.SetPixel( 0, 0, Color.blue );	// 色つけちゃうと 00ff00 とかになって掛けあわせてもソコしか色がつかない
		this.blackTexture.Apply();

		// 元が ffffff なので好きな色を付けられるよ
		//m_vColor = new Color( 0.0f, 0.1f, 0.0f );
		m_vColor = Color.white;

		setCoroutine();
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void setCoroutine()
	{
		if ( m_vIEnumerator != null )
		{
			StopCoroutine( m_vIEnumerator );
			m_vIEnumerator = null;
		}
		//
		m_vIEnumerator = _coroutine();
		StartCoroutine( m_vIEnumerator );
	}


	private IEnumerator _coroutine()
	{
		int _alpha = 0;

		while ( true )
		{
			_alpha = ( _alpha + 1 ) % 100;
			m_vColor.a = _alpha / 100.0f;
			yield return null;
		}
	}

	// ========================================================================== //
	// ========================================================================== //


	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	void Awake()
	{
		_awake();
	}

	public void OnGUI()
	{
		//if ( !this.isFading )
		//	return;

		//透明度を更新して黒テクスチャを描画
		//		GUI.color = new Color( 0, 0, 0, this.fadeAlpha );
		//		GUI.color = new Color( 0.5f, 0, 0, 0.2f );
		GUI.color = m_vColor;
		GUI.depth = 100000;
		GUI.DrawTexture( new Rect( 0, 0, Screen.width / 2, Screen.height / 2 ), this.blackTexture );
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
}
