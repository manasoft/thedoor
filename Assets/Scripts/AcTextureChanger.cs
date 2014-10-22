using UnityEngine;
using System.Collections;

/// <summary>
/// テクスチャの uv 値を切り替えて自動的にアニメーションさせるクラスだよ
/// 
/// テクスチャをインスタンス化する Instantiate() が UnityEngine.Object なので Object を継承している
/// </summary>
public class AcTextureChanger : Object
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

	/*
	 * AcTextureChanger.Data を保持
	 */
	private Data m_vData;
	/*
	 * インスタンスが重くなりそうなのでテクスチャを保持しておく
	 */
	private Texture m_vTexture;
	/*
	 * サイズ
	 */
	private float m_vSizeW;
	private float m_vSizeH;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * コンストラクタ
	 */
	public AcTextureChanger( Data vData )
	{
		m_vData = vData;
		//
		/*
		 * 2014/10/02 問題なく動いていたが typeof(Texture) を追加してみる
		 */
		m_vTexture = ( Texture ) Instantiate( Resources.Load( m_vData.m_vFile, typeof(Texture) ) );
		m_vSizeW = 1.0f / m_vData.m_vGridX;
		m_vSizeH = 1.0f / m_vData.m_vGridY;
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public void initialize( Renderer vRenderer )
	{
		vRenderer.material.mainTexture = m_vTexture;
		//
		vRenderer.material.SetTextureOffset( "_MainTex", new Vector2( 0.0f, 0.0f ) );
		vRenderer.material.SetTextureScale( "_MainTex", new Vector2( m_vSizeW, m_vSizeH ) );
	}

	/*
	 * vIndex テーブルどのデータを使うか？
	 * vStart 開始位置（オフセット）
	 * vRange 繰り返す幅
	 */
	public void update( Renderer vRenderer, int vFrame, int vIndex, int vStart, int vRange )
	{
		//		int index = m_vData.m_vIndex[ vIndex ][ ( vFrame / m_vData.m_vFrame ) % m_vData.m_vIndex[ vIndex ].Length ];
		//		int indexX = index % m_vData.m_vGridX;
		//		int indexY = index / m_vData.m_vGridX;
		//float sizeW = 1.0f / m_vData.m_vGridX;
		//float sizeH = 1.0f / m_vData.m_vGridY;
		//
		//Debug.Log( "index >> " + index );
		//Debug.Log( "indexX >> " + indexX );
		//Debug.Log( "indexY >> " + indexY );
		//Debug.Log( "sizeW >> " + sizeW );
		//Debug.Log( "sizeH >> " + sizeH );
		/*
		 * 画像の位置
		 * (0,1)---(1,1)
		 * |           |
		 * (0,0)---(1,0)
		 */
		//		vRenderer.material.SetTextureOffset( "_MainTex", new Vector2( m_vSizeW * indexX, 1.0f - m_vSizeH * (indexY + 1) ) );

		//		vRenderer.material.SetTextureScale( "_MainTex", new Vector2( sizeW, sizeH ) );
		//		vRenderer.material.SetTextureOffset( "_MainTex", new Vector2( 0.5f, 0.5f ) );
		//		vRenderer.material.SetTextureScale( "_MainTex", new Vector2( 0.5f, 0.5f ) );

		vRenderer.material.SetTextureOffset( "_MainTex", getUV( vFrame, vIndex, vStart, vRange ) );
	}

	public Texture getTexture()
	{
		return ( m_vTexture );
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

	public Vector2 getUV( int vFrame, int vIndex, int vStart, int vRange )
	{
		if ( (vRange == (0)) || ( vRange > m_vData.m_vIndex[ vIndex ].Length ) )
		{
			vRange = m_vData.m_vIndex[ vIndex ].Length;
		}
		//
		int index = m_vData.m_vIndex[ vIndex ][ ( vStart + ( vFrame / m_vData.m_vFrame ) ) % vRange ];
		int indexX = index % m_vData.m_vGridX;
		int indexY = index / m_vData.m_vGridX;
		/*
		 * 画像の位置
		 * (0,1)---(1,1)
		 * |           |
		 * (0,0)---(1,0)
		 */
		return ( new Vector2( m_vSizeW * indexX, 1.0f - m_vSizeH * ( indexY + 1 ) ) );
	}

	public Vector2 getWH()
	{
		return ( new Vector2( m_vSizeW, m_vSizeH ) );
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	public int getDataLength()
	{
		return ( m_vData.m_vIndex.Length );
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

	public struct Data
	{
		internal string m_vFile;
		internal int m_vGridX;
		internal int m_vGridY;
		internal int m_vFrame;
		internal int[][] m_vIndex;

		/*
		 * 可変長引数
		 * http://ufcpp.net/study/csharp/sp_params.html
		 */

		public Data( string vFile, int vGridX, int vGridY, int vFrame, params int[][] vIndex )
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
