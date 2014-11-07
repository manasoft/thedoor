using UnityEngine;
using System.Collections;

// LINQ
using System.Linq;

// Dictionary
using System.Collections.Generic;

/// <summary>
/// イメージマネージャー
/// 
/// 一応 Ac で作るが Sc でも行ける感じで作ること！
/// </summary>
//public class AcImageManager : MonoBehaviour
public class AcImageManager : Object
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * 
	 */
	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //
	/*
	 * ■コレクション
	 * 
	 * 使わなくなった機能・新しい機能 (C# によるプログラミング入門)
	 * http://ufcpp.net/study/csharp/ap_modern.html#non-generic
	 */
	/*
	 * ■LINQ
	 * 
	 * LINQ (C# によるプログラミング入門)
	 * http://ufcpp.net/study/csharp/sp3_linq.html
	 * 標準クエリ演算子（クエリ式関係） (C# によるプログラミング入門)
	 * http://ufcpp.net/study/csharp/sp3_stdquery.html
	 * 
	 * 特集：C#プログラマーのためのLINQ超入門（前編）：LINQ（リンク）の基礎知識 (1-4) - ＠IT
	 * http://www.atmarkit.co.jp/ait/articles/0803/25/news150.html
	 * 
	 * C# - ListからDictionary作る時もLINQを使おうぜ！ILookupも便利だぜ！ - Qiita
	 * http://qiita.com/RyotaMurohoshi/items/ba4ade6c6c9dc40b6217
	 * Unity の場合うまく動かない事があるらしいっす！
	 * 
	 * C# やるなら LINQ を使おう  プログラマーズ雑記帳
	 * http://yohshiy.blog.fc2.com/blog-entry-274.html
	 * ↑これがまぁ参考になるかも
	 */
	/*
	 * Unity - Unity Manual
	 * Resources
	 * http://docs-jp.unity3d.com/Documentation/ScriptReference/Resources.html
	 * 
	 * スクリプトからアセットを読み込む - Neareal
	 * http://neareal.net/index.php?ComputerGraphics%2FUnity%2FTips%2FScript%2FDynamicAssetsLoading
	 * 
	 * Unity 基礎#04 〜 フォルダの理解(Resources, StreamingAssets) 〜 - UPSNAP
	 * http://upsnap.jp/archives/show/126
	 * 
	 * Unity でリソースの解放を行う  Lonely Mobiler
	 * http://loumo.jp/wp/archive/20140419000115/
	 * 
	 */
	/*
	 * 【Unity Action】使用メモリ量を常に視覚化しよう！ Karasuのアプリ奮闘記
	 * http://hideapp.cocolog-nifty.com/blog/2012/11/unity-action-31.html
	 * 
	 */
	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	/// <summary>
	/// 「イメージ」を保持します（イメージで使う Texture を保持します）
	/// </summary>
	private Dictionary<string, _Image> m_vImageDictionary;

	/// <summary>
	/// 「登録データ」を保持します
	/// </summary>
	private Dictionary<string, _Entry> m_vEntryDictionary;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// コンストラクタ
	/// </summary>
	public AcImageManager()
	{
		m_vImageDictionary = new Dictionary<string, _Image>();
		m_vEntryDictionary = new Dictionary<string, _Entry>();
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public void add( string vEntryName, string vGroupName, Rect vRect, string vImageFileName )
	{
		if ( !m_vEntryDictionary.ContainsKey( vEntryName ) )
		{
			m_vEntryDictionary.Add( vEntryName, new _Entry( vEntryName, vGroupName, vRect, vImageFileName ) );
			//
			if ( !m_vImageDictionary.ContainsKey( vImageFileName ) )
			{
				m_vImageDictionary.Add( vImageFileName, new _Image( vImageFileName ) );
			}
		}
	}

	/// <summary>
	/// 画像の書き込み
	/// ファイルが無かった場合にテクスチャーが null になっている場合があるよ！（その場合は前の状態のママで変化しないよ）
	/// </summary>
	/// <param name="vRenderer"></param>
	/// <param name="vEntryName"></param>
	public void render( Renderer vRenderer, string vEntryName )
	{
		_Entry _enrty = m_vEntryDictionary[ vEntryName ];
		_Image _resource = m_vImageDictionary[ _enrty.m_vImageFileName ];
		//
		Texture _texture = _resource.m_vTexture;
		//
		if ( _texture != null )
		{
			if ( ( vRenderer.material.mainTexture == null ) || ( !vRenderer.material.mainTexture.Equals( _texture ) ) )
			{
				/*
				 * この処理は結構重いみたいです（実はメソッド的な処理が行われている？）
				 * 参照を渡すだけじゃなくて、内部的にコピーしてんじゃないの？
				 */
				vRenderer.material.mainTexture = _texture;
			}
			/*
			 * "_MainTex" の意味がさっぱりわからんが・・・
			 */
			vRenderer.material.SetTextureOffset( "_MainTex", _enrty.m_vOffset );
			vRenderer.material.SetTextureScale( "_MainTex", _enrty.m_vScale );
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * このアタリの処理は試験的なもので必要性があるかも疑問です（2014/11/06）
	 */

	/// <summary>
	/// 画像をロード
	/// </summary>
	/// <param name="vEntryName"></param>
	public void load( string vEntryName )
	{
		if ( m_vEntryDictionary.ContainsKey( vEntryName ) )
		{
			m_vImageDictionary[ m_vEntryDictionary[ vEntryName ].m_vImageFileName ].load();
		}
	}

	/// <summary>
	/// 画像をリリース
	/// </summary>
	/// <param name="vEntryName"></param>
	public void release( string vEntryName )
	{
		if ( m_vEntryDictionary.ContainsKey( vEntryName ) )
		{
			m_vImageDictionary[ m_vEntryDictionary[ vEntryName ].m_vImageFileName ].release();
		}
	}

	/// <summary>
	/// 画像をロード（グループ指定）
	/// </summary>
	/// <param name="vGroupName"></param>
	public void loadGroup( string vGroupName )
	{
		var _var = m_vEntryDictionary.Where( _param => _param.Value.m_vGroupName.Equals( vGroupName ) ).Select( _param => _param.Value ); //.ToList<_Entry>;
		//
		foreach ( var _entry in _var )
		{
			//			m_vImageDictionary[ _entry.m_vImageFileName ].getTexture();
			m_vImageDictionary[ _entry.m_vImageFileName ].load();
		}
	}

	/// <summary>
	/// 画像をリリース（グループ指定）
	/// </summary>
	/// <param name="vGroupName"></param>
	public void releaseGroup( string vGroupName )
	{
		/*
		 * 今のところ未完成です、LINQ の実験のためにやってみた感じです
		 * 
		 * 目的：
		 * グループ名で画像を一括リリースする！
		 * LINQ がスゲーらしいので使ってみる（動的にグループ名のリストを作って処理するので、グループ名管理のコレクションを保持する必要が無い）
		 * 
		 * 問題点：
		 * 指定グループ以外のエントリーが同じ画像を使っていた場合画像がなくなるのでバグるな
		 * なので、指定グループの使用画像をチェックして他が使っていないかも判定する必要がある
		 * 画像を必要としている状態をフラグ化して必要としていない画像をチェックして一括削除するとかすると良いかも
		 */

		Debug.Log( "AcImageManager # release >> " + vGroupName );

		//		var _var = m_vEntryDictionary;

		var _var = m_vEntryDictionary.Where( _param => _param.Value.m_vGroupName.Equals( vGroupName ) ).Select( _param => _param.Value ); //.ToList<_Entry>;
		//
		foreach ( var _entry in _var )
		{
			Debug.Log( ( ( _Entry ) _entry ).m_vEntryName );
			//
			m_vImageDictionary[ _entry.m_vImageFileName ].release();
		}

		//List<_Entry> _list = ( List<_Entry> ) ( m_vEntryDictionary.Where( _Entry => _Entry.Value.m_vGroupName.Equals( vGroup ) ).Select( _Entry => _Entry ) );//.ToList<_Entry>;
		////
		//foreach ( _Entry _entry in _list )
		//{
		//	Debug.Log( _entry.m_vImageFileName );
		//}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// リソースを管理します
	/// 今のところ（2014/11/05 現在）テクスチャーを保持しているだけ
	/// </summary>
	private class _Image
	{
		/// <summary>
		/// イメージファイル名
		/// </summary>
		public string m_vImageFileName;

		/// <summary>
		/// リソースをロードして保持する
		/// </summary>
		public Texture m_vTexture;

		public _Image( string vImageFileName )
		{
			m_vImageFileName = null;
			m_vTexture = null;
			//
			m_vImageFileName = vImageFileName;
			/*
			 * 最初にインスタンス化するか、呼び出し時にインスタンス化するか？
			 */
			load();
			//m_vTexture = ( Texture ) Instantiate( Resources.Load( vImageFileName, typeof( Texture ) ) );
		}

		public Texture load()
		{
			if ( m_vTexture == null )
			{
				/*
				 * 2014/11/07
				 * Assets 内のファイルが有るかどうかの判別方法がわからない
				 */
				//if ( AcUtil.isFileExists( m_vImageFileName ) )
				//{
				//	Debug.Log( "ファイルが有るよ！ >> " + m_vImageFileName );
				//}
				//else
				//{
				//	Debug.Log( "ファイルが無いよ！ >> " + m_vImageFileName );
				//}

				/*
				 * ファイルが無いと null が返るらしいっす！
				 * http://docs-jp.unity3d.com/Documentation/ScriptReference/Resources.Load.html
				 */
				Object _object = Resources.Load( m_vImageFileName, typeof( Texture ) );
				//
				if ( _object != null )
				{
					m_vTexture = ( Texture ) Instantiate( _object );
					Debug.Log( "ファイルが有るよ！ >> " + m_vImageFileName );
				}
				else
				{
					Debug.Log( "ファイルが無いよ！ >> " + m_vImageFileName );
				}
				//m_vTexture = ( Texture ) Instantiate( Resources.Load( m_vImageFileName, typeof( Texture ) ) );
			}
			//
			return ( m_vTexture );
		}

		public void release()
		{
			if ( m_vTexture != null )
			{
				// 適当にやってみた、動作未確認です
				Object.Destroy( m_vTexture );
				m_vTexture = null;
			}
		}

		/// <summary>
		/// 最初にインスタンス化しない場合はこれを使う（2014/11/05 時点では最初にインスタンス化するので、使う予定は無いです）
		/// </summary>
		/// <returns></returns>
		//public Texture getTexture()
		//{
		//	if ( m_vTexture == null )
		//	{
		//		m_vTexture = ( Texture ) Instantiate( Resources.Load( m_vImageFileName, typeof( Texture ) ) );
		//	}
		//	//
		//	return ( m_vTexture );
		//}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * 参考までに
	 * public ScImage( String vEntry, String vGroup, int vGraphicsType, String vResourceFile )
	 */

	private class _Entry
	{
		/// <summary>
		/// エントリー名（登録した名前です、基本的にこの名前でアクセスする）
		/// </summary>
		public string m_vEntryName;

		/// <summary>
		/// グループ名（load/release 等のコントロール用として使用する予定です）
		/// </summary>
		public string m_vGroupName;

		//		public Rect m_vRect;

		/// <summary>
		/// renderer.material.SetTextureOffset() 用
		/// </summary>
		public Vector2 m_vOffset;

		/// <summary>
		/// renderer.material.SetTextureScale() 用
		/// </summary>
		public Vector2 m_vScale;

		/// <summary>
		/// イメージファイル名（画像ファイルへのパスです）
		/// </summary>
		public string m_vImageFileName;

		//public _Entry( string vEntryName, float vX, float vY, float vW, float vH, string vImageFileName )
		//{
		//	m_vEntryName = vEntryName;
		//	m_vOffset = new Vector2( vX, vY );
		//	m_vScale = new Vector2( vW, vH );
		//	m_vImageFileName = vImageFileName;
		//}

		public _Entry( string vEntryName, string vGroupName, Rect vRect, string vImageFileName )
		{
			m_vEntryName = vEntryName;
			m_vGroupName = vGroupName;
			m_vOffset = vRect.position;
			m_vScale = vRect.size;
			m_vImageFileName = vImageFileName;
		}
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

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public static void test()
	{

		AcImageManager _manager = new AcImageManager();

		_manager.add( "entry_10", "group_1", new Rect(), "Images" + AcUtil.getLanguageSuffix() + "/tx_room" );
//		_manager.add( "entry_10", "group_1", new Rect(), "Assets/Resources/Images" + AcUtil.getLanguageSuffix() + "/tx_room" );
		_manager.add( "entry_11", "group_1", new Rect(), "Images" + AcUtil.getLanguageSuffix() + "/tx_room2" );
		_manager.add( "entry_12", "group_1", new Rect(), "Images" + AcUtil.getLanguageSuffix() + "/tx_room3" );
		_manager.add( "entry_13", "group_1", new Rect(), "Images" + AcUtil.getLanguageSuffix() + "/tx_room4" );

		_manager.add( "entry_20", "group_2", new Rect(), "" );
		_manager.add( "entry_21", "group_2", new Rect(), "" );
		_manager.add( "entry_22", "group_2", new Rect(), "" );
		_manager.add( "entry_23", "group_2", new Rect(), "" );
		_manager.add( "entry_24", "group_2", new Rect(), "" );

		_manager.add( "entry_30", "group_3", new Rect(), "" );
		_manager.add( "entry_31", "group_3", new Rect(), "" );
		_manager.add( "entry_32", "group_3", new Rect(), "" );
		_manager.add( "entry_33", "group_3", new Rect(), "" );
		_manager.add( "entry_34", "group_3", new Rect(), "" );
		_manager.add( "entry_35", "group_3", new Rect(), "" );

		_manager.release( "group_1" );
		_manager.release( "group_2" );
		_manager.release( "group_3" );
	}

	// ========================================================================== //
	// ========================================================================== //
}
