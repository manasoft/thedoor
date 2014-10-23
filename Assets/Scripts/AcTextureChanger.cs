using UnityEngine;
using System.Collections;

// Dictionary
using System.Collections.Generic;

/// <summary>
/// テクスチャの uv 値を切り替えて自動的にアニメーションさせるクラスだよ
/// 
/// テクスチャをインスタンス化する Instantiate() が UnityEngine.Object なので Object を継承している
/// 明示的に継承しないとダメっぽいっす！
/// </summary>
public class AcTextureChanger : Object
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

	///*
	// * AcTextureChanger.Data を保持
	// */
	//private Data m_vData;
	///*
	// * インスタンスが重くなりそうなのでテクスチャを保持しておく
	// */
	//private Texture m_vTexture;
	///*
	// * サイズ
	// */
	//private float m_vSizeW;
	//private float m_vSizeH;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// _Data を保持する
	/// </summary>
	private ArrayList m_vArrayList;

	/// <summary>
	/// ファイル名 → テクスチャーのテーブルです
	/// </summary>
	private Dictionary<string, Texture> m_vDictionary;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	///*
	// * コンストラクタ
	// */
	///// <summary>
	///// コンストラクタ（2014/10/23 使わない予定になりました）
	///// </summary>
	///// <param name="vData"></param>
	//public AcTextureChanger( Data vData )
	//{
	//	m_vData = vData;
	//	//
	//	/*
	//	 * 2014/10/02 問題なく動いていたが typeof(Texture) を追加してみる
	//	 */
	//	m_vTexture = ( Texture ) Instantiate( Resources.Load( m_vData.m_vFile, typeof( Texture ) ) );
	//	m_vSizeW = 1.0f / m_vData.m_vGridX;
	//	m_vSizeH = 1.0f / m_vData.m_vGridY;
	//}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// コンストラクタ
	/// </summary>
	public AcTextureChanger()
	{
		m_vArrayList = new ArrayList();
		m_vDictionary = new Dictionary<string, Texture>();
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	public void add( string vFile, int vGridX, int vGridY, int vFrame, int[] vIndex )
	{
		Debug.Log( "データを追加（ファイル）>> " + vFile );
		//
		m_vArrayList.Add( new _Data( vFile, vGridX, vGridY, vFrame, vIndex ) );
		//
		if ( ! m_vDictionary.ContainsKey( vFile ) )
		{
			Debug.Log( "テクスチャ追加（ファイル）>> " + vFile );

			m_vDictionary.Add( vFile, ( Texture ) Instantiate( Resources.Load( vFile, typeof( Texture ) ) ) );

			Debug.Log( "テクスチャ追加完了" );
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //


	private _Data _getData( int vIndex )
	{
		_Data _data = null;
		//
		if ( vIndex < m_vArrayList.Count )
		{
			_data = ( _Data ) m_vArrayList[ vIndex ];
		}
		//
		return ( _data );
	}

	public Texture getTexture( int vIndex )
	{
		Texture _texture = null;
		//
		_Data _data = _getData( vIndex );
		//
		if ( _data != null )
		{
			if ( m_vDictionary.ContainsKey( _data.m_vFile ) )
			{
				_texture = m_vDictionary[ _data.m_vFile ];
			}
		}
		//
		return ( _texture );
	}


	//public Vector2 getUV( int vIndex, int vStart, int vFrame )
	//{
	//	int index = m_vData.m_vIndex[ vIndex ][ ( vStart + ( vFrame / m_vData.m_vFrame ) ) % m_vData.m_vIndex[ vIndex ].Length ];
	//	int indexX = index % m_vData.m_vGridX;
	//	int indexY = index / m_vData.m_vGridX;
	//	/*
	//	 * 画像の位置
	//	 * (0,1)---(1,1)
	//	 * |           |
	//	 * (0,0)---(1,0)
	//	 */
	//	return ( new Vector2( m_vSizeW * indexX, 1.0f - m_vSizeH * ( indexY + 1 ) ) );
	//}

	public Vector2 getUV( int vIndex, int vFrame )
	{
		Vector2 _vector = new Vector2();
		//
		_Data _data = _getData( vIndex );
		//
		if ( _data != null )
		{
			int _index = _data.m_vIndex[ ( vFrame / _data.m_vFrame ) % _data.m_vIndex.Length ];
			int _index_x = _index % _data.m_vGridX;
			int _index_y = _index / _data.m_vGridX;
			//
			Vector2 _wh = getWH( vIndex );
			/*
			 * 画像の位置
			 * (0,1)---(1,1)
			 * |           |
			 * (0,0)---(1,0)
			 */
			_vector.x = _wh.x * _index_x;
			_vector.y = 1.0f - _wh.y * ( _index_y + 1 );
		}
		//
		return ( _vector );
	}

	public Vector2 getWH( int vIndex )
	{
		Vector2 _vector = new Vector2();
		//
		_Data _data = _getData( vIndex );
		//
		if ( _data != null )
		{
			_vector.x = 1.0f / _data.m_vGridX;
			_vector.y = 1.0f / _data.m_vGridY;
		}
		//
		return ( _vector );
	}


	public void update( Renderer vRenderer, int vFrame, int vIndex )
	{
		Texture _texture = getTexture( vIndex );
		//
		if((vRenderer.material.mainTexture == null) || (! vRenderer.material.mainTexture.Equals( _texture ) ) )
		{
			// この処理は結構重いみたいです（実はメソッド的な処理が行われている？）
			vRenderer.material.mainTexture = _texture;
		}
		//
		vRenderer.material.SetTextureOffset( "_MainTex", getUV( vIndex, vFrame ) );
		vRenderer.material.SetTextureScale( "_MainTex", getWH( vIndex ) );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	//public void initialize( Renderer vRenderer )
	//{
	//	vRenderer.material.mainTexture = m_vTexture;
	//	//
	//	vRenderer.material.SetTextureOffset( "_MainTex", new Vector2( 0.0f, 0.0f ) );
	//	vRenderer.material.SetTextureScale( "_MainTex", new Vector2( m_vSizeW, m_vSizeH ) );
	//}

	///*
	// * vIndex テーブルどのデータを使うか？
	// * vStart 開始位置（オフセット）
	// * vRange 繰り返す幅
	// */
	//public void update( Renderer vRenderer, int vFrame, int vIndex, int vStart, int vRange )
	//{
	//	//		int index = m_vData.m_vIndex[ vIndex ][ ( vFrame / m_vData.m_vFrame ) % m_vData.m_vIndex[ vIndex ].Length ];
	//	//		int indexX = index % m_vData.m_vGridX;
	//	//		int indexY = index / m_vData.m_vGridX;
	//	//float sizeW = 1.0f / m_vData.m_vGridX;
	//	//float sizeH = 1.0f / m_vData.m_vGridY;
	//	//
	//	//Debug.Log( "index >> " + index );
	//	//Debug.Log( "indexX >> " + indexX );
	//	//Debug.Log( "indexY >> " + indexY );
	//	//Debug.Log( "sizeW >> " + sizeW );
	//	//Debug.Log( "sizeH >> " + sizeH );
	//	/*
	//	 * 画像の位置
	//	 * (0,1)---(1,1)
	//	 * |           |
	//	 * (0,0)---(1,0)
	//	 */
	//	//		vRenderer.material.SetTextureOffset( "_MainTex", new Vector2( m_vSizeW * indexX, 1.0f - m_vSizeH * (indexY + 1) ) );

	//	//		vRenderer.material.SetTextureScale( "_MainTex", new Vector2( sizeW, sizeH ) );
	//	//		vRenderer.material.SetTextureOffset( "_MainTex", new Vector2( 0.5f, 0.5f ) );
	//	//		vRenderer.material.SetTextureScale( "_MainTex", new Vector2( 0.5f, 0.5f ) );

	//	vRenderer.material.SetTextureOffset( "_MainTex", getUV( vFrame, vIndex, vStart, vRange ) );
	//}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //


	//public Texture getTexture()
	//{
	//	return ( m_vTexture );
	//}


	//public Vector2 getUV( int vIndex, int vStart, int vFrame )
	//{
	//	int index = m_vData.m_vIndex[ vIndex ][ ( vStart + ( vFrame / m_vData.m_vFrame ) ) % m_vData.m_vIndex[ vIndex ].Length ];
	//	int indexX = index % m_vData.m_vGridX;
	//	int indexY = index / m_vData.m_vGridX;
	//	/*
	//	 * 画像の位置
	//	 * (0,1)---(1,1)
	//	 * |           |
	//	 * (0,0)---(1,0)
	//	 */
	//	return ( new Vector2( m_vSizeW * indexX, 1.0f - m_vSizeH * ( indexY + 1 ) ) );
	//}

	//public Vector2 getUV( int vFrame, int vIndex, int vStart, int vRange )
	//{
	//	if ( ( vRange == ( 0 ) ) || ( vRange > m_vData.m_vIndex[ vIndex ].Length ) )
	//	{
	//		vRange = m_vData.m_vIndex[ vIndex ].Length;
	//	}
	//	//
	//	int index = m_vData.m_vIndex[ vIndex ][ ( vStart + ( vFrame / m_vData.m_vFrame ) ) % vRange ];
	//	int indexX = index % m_vData.m_vGridX;
	//	int indexY = index / m_vData.m_vGridX;
	//	/*
	//	 * 画像の位置
	//	 * (0,1)---(1,1)
	//	 * |           |
	//	 * (0,0)---(1,0)
	//	 */
	//	return ( new Vector2( m_vSizeW * indexX, 1.0f - m_vSizeH * ( indexY + 1 ) ) );
	//}

	//public Vector2 getWH()
	//{
	//	return ( new Vector2( m_vSizeW, m_vSizeH ) );
	//}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	//public int getDataLength()
	//{
	//	return ( m_vData.m_vIndex.Length );
	//}
	public int getDataLength()
	{
		return ( m_vArrayList.Count );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	//public struct GridData
	//{
	//	private string m_vFile;
	//	private int m_vGridX;
	//	private int m_vGridY;

	//	public GridData(string vFile, int vGridX, int vGridY)
	//	{
	//		m_vFile = vFile;
	//		m_vGridX = vGridX;
	//		m_vGridY = vGridY;
	//	}
	//}

	/*
	 * internal ってなんだよ！
	 * http://msdn.microsoft.com/ja-jp/library/ba0a1yw2.aspx
	 */

	//public struct Data
	//{
	//	internal string m_vFile;
	//	internal int m_vGridX;
	//	internal int m_vGridY;
	//	internal int m_vFrame;
	//	internal int[][] m_vIndex;

	//	/*
	//	 * 可変長引数
	//	 * http://ufcpp.net/study/csharp/sp_params.html
	//	 */

	//	public Data( string vFile, int vGridX, int vGridY, int vFrame, params int[][] vIndex )
	//	{
	//		m_vFile = vFile;
	//		m_vGridX = vGridX;
	//		m_vGridY = vGridY;
	//		m_vFrame = vFrame;
	//		m_vIndex = vIndex;
	//	}
	//}


	private class _Data
	{
		public string m_vFile;
		public int m_vGridX;
		public int m_vGridY;
		public int m_vFrame;
		public int[] m_vIndex;

		public _Data( string vFile, int vGridX, int vGridY, int vFrame, int[] vIndex )
		{
			m_vFile = vFile;
			m_vGridX = vGridX;
			m_vGridY = vGridY;
			m_vFrame = vFrame;
			m_vIndex = vIndex;
		}

	}

	// ========================================================================== //
	// ========================================================================== //

}
