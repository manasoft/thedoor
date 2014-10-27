using UnityEngine;
using System.Collections;

// Dictionary
using System.Collections.Generic;

/// <summary>
/// サウンドマネージャー
/// いつでもどこでも音が出せるよ！
/// ポジションとか指定しないので 2D ゲーム専用っぽいけど・・・
/// </summary>
public class AcSoundManager : Object
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * 
	 * 
	 * 
	 */

	/*
	 * C# アクセス修飾子
	 * http://msdn.microsoft.com/ja-jp/library/ba0a1yw2.aspx
	 * 
	 * public  
	 * アクセスの制限はありません。  
	 * 
	 * protected  
	 * アクセスは、コンテナー クラス、またはコンテナー クラスから派生した型に制限されます。  
	 * 
	 * internal  
	 * アクセスは現在のアセンブリに制限されます。  
	 * 
	 * protected  internal  
	 * 現在のアセンブリ、またはコンテナー クラスから派生した型に制限されるアクセス  
	 * 
	 * private  
	 * アクセスはコンテナー型に制限されます。 
	 * 
	 *
	 * アセンブリとグローバル アセンブリ キャッシュ (C# および Visual Basic)
	 * http://msdn.microsoft.com/ja-jp/library/ms173099.aspx
	 */

	/*
	 * Java アクセス修飾子
	 * 
	 * private
	 * 同じクラス内からしか呼び出せないが、同じクラスから作られたオブジェクト同士であれば、相互の private メンバーにアクセスできる。 
	 * 
	 * 省略
	 * 同じパッケージ内からしか呼び出せない。 
	 * 
	 * protected
	 * 同じパッケージか、そのサブクラスからしか呼び出せない。 
	 * 
	 * public
	 * どこからでも呼び出せる。 
	 */


	/*
	 * ココのプログラムはこのサイトを参考にしていますよ
	 * http://marupeke296.com/UNI_main.html
	 * http://marupeke296.com/UNI_SND_No3_SoundPlayer.html
	 * 
	 * Unityで音を再生する
	 * オブジェクトにくっつけるやり方
	 * http://qiita.com/amano-kiyoyuki/items/43fa92cce1a44a8030b5
	 * 
	 */

	/*
	 * Dictionary
	 * http://msdn.microsoft.com/ja-jp/library/xfhwa508(v=vs.110).aspx
	 * 
	 * HashTable と Dictionary
	 * http://tenmon.g.hatena.ne.jp/waka0529/20090203/1233642871
	 */
	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// ゲームオブジェクトに付ける名前
	/// </summary>
	private const string _NAME = "AcSoundManager";

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 音を鳴らすには AudioSource が必要です、AudioSource は GameObject にコンポーネントとしてくっつける必要があります
	/// </summary>
	private GameObject m_vGameObject;

	/// <summary>
	/// 「登録データ」を保持します
	/// </summary>
	private Dictionary<string, _Entry> m_vEntryDictionary;

	/// <summary>
	/// 「登録データ」を元に作成した「トラックデータ」を保持します
	/// </summary>
	private Dictionary<string, _Track> m_vTrackDictionary;
	
	/// <summary>
	/// 
	/// </summary>
	private AudioListener m_vAudioListener;

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// デバッグ表示
	/// </summary>
	/// <param name="vString"></param>
	private static void _debugLog( string vString )
	{
		//Debug.Log( vString );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// コンストラクタ
	/// </summary>
	public AcSoundManager()
	{
		m_vGameObject = new GameObject( _NAME );
		//
		m_vEntryDictionary = new Dictionary<string, _Entry>();
		m_vTrackDictionary = new Dictionary<string, _Track>();

		//
		m_vAudioListener = m_vGameObject.AddComponent<AudioListener>();
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// データを登録するよ！
	/// </summary>
	/// <param name="vEntryName"></param>
	/// <param name="vTrackName"></param>
	/// <param name="vSoundFile"></param>
	public void add( string vEntryName, string[] vTrackName, string vSoundFile )
	{
		/*
		 * 
		 */
		m_vEntryDictionary.Add( vEntryName, new _Entry( vEntryName, vTrackName, vSoundFile ) );

		/*
		 * コンポーネントの追加で失敗する事があるのか不明だがココで null チェックしているので _Track の中では null チェックが不要になります
		 */
		foreach ( string _trackName in vTrackName )
		{
			if ( !m_vTrackDictionary.ContainsKey( _trackName ) )
			{
				AudioSource _audioSource = m_vGameObject.AddComponent<AudioSource>();
				//
				if ( _audioSource != null )
				{
					m_vTrackDictionary.Add( _trackName, new _Track( _trackName, _audioSource ) );
				}
			}
		}
	}

	/// <summary>
	/// 毎フレーム実行する処理です
	/// </summary>
	public void update()
	{
		foreach ( _Track _track in m_vTrackDictionary.Values )
		{
			_track.update();
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //


	public string play( string vEntryName, float vFedeInTime )
	{
		/*
		 * 登録されているか？
		 */
		if ( m_vEntryDictionary.ContainsKey( vEntryName ) )
		{
			_Entry _entry = m_vEntryDictionary[ vEntryName ];
			_Track _track = null;
			/*
			 * クリップをロードする
			 */
			if ( _entry.m_vAudioClip == null )
			{
				_entry.m_vAudioClip = ( AudioClip ) Resources.Load( _entry.m_vSoundFile );
			}

			/*
			 * 待機中のトラックを探す
			 */
			ArrayList _arrayList = new ArrayList();
			//
			foreach ( string _trackName in _entry.m_vTrackName )
			{
				if ( m_vTrackDictionary.ContainsKey( _trackName ) )
				{
					_track = m_vTrackDictionary[ _trackName ];
					//
					//					if ( _track.isWaiting() )
					if ( !_track.isBusy() )
					{
						_debugLog( "空きトラック再生" );
						//
						_track.play( _entry, vFedeInTime );
						//
						return ( _track.m_vTrackName );
					}
					//
					_arrayList.Add( _track );
				}
			}
			//
			_arrayList.Sort( new _TrackComparer() );
			//
			if ( _arrayList.Count > 0 )
			{
				_debugLog( "上書き再生" );
				//
				_track = ( _Track ) _arrayList[ 0 ];
				//
				_track.play( _entry, vFedeInTime );
				//
				return ( _track.m_vTrackName );
			}
		}
		//
		return ( null );
	}


	public void stop( string vTrackName, float vFedeOutTime )
	{
		if ( m_vTrackDictionary.ContainsKey( vTrackName ) )
		{
			_Track _track = m_vTrackDictionary[ vTrackName ];
			//
			_track.stop( vFedeOutTime );
		}
	}

	public string play( string vEntryName )
	{
		return ( play( vEntryName, 0.0f ) );
	}

	public void stop( string vTrackName )
	{
		stop( vTrackName, 0.0f );
	}

	// ========================================================================== //
	// ========================================================================== //


	/// <summary>
	/// ロードした AudioClip を捨てないとマズイんじゃないのかな？
	/// </summary>
	public void destroy()
	{
		/*
		 * Dictionary の中身を列挙する！
		 * http://www.ipentec.com/document/document.aspx?page=csharp-generics-dictionary-manage-key-value-data
		 */

		//		m_vAudioSource.Stop();

		/*
		 * これが正しい処理なのかは不明です
		 * どうも間違いらしい・・・
		 */
		//foreach ( _Data _data in m_vDictionary.Values )
		//{
		//	/*
		//	* テクスチャ更新
		//	* 破棄？
		//	* http://ft-lab.ne.jp/cgi-bin-unity/wiki.cgi?page=unity_script_texture2d_save_png_file
		//	 * 
		//	 * 2014/10/15
		//	 * サウンドでコレやったら Unity 上の表示の左に出てるクリップ？のアイコンが消えてファイルとして読み込めなくなったぞ！
		//	 * 
		//	 * やっちゃダメなんじゃないの？
		//	 * http://www.wallflux.com/link/167184853341463/1346967861
		//	*/
		//	UnityEngine.Object.DestroyImmediate( _data.m_vAudioClip, true );
		//}

		/*
		 * 改良版！
		 * 
		 * 2014/10/15
		 * 動作未確認です！
		 */
		if ( m_vGameObject != null )
		{
			GameObject.Destroy( m_vGameObject );
			m_vGameObject = null;
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 登録データを保持するクラス
	/// </summary>
	private class _Entry
	{
		/// <summary>
		/// 登録した名前
		/// </summary>
		public string m_vEntryName;

		/// <summary>
		/// 使用するトラックの名前のリスト
		/// </summary>
		public string[] m_vTrackName;

		/// <summary>
		/// 音のファイル名
		/// </summary>
		public string m_vSoundFile;

		///// <summary>
		///// 
		///// </summary>
		//public float m_vSoundTime;

		/// <summary>
		/// リソースをロードして保持する
		/// </summary>
		internal AudioClip m_vAudioClip;

		public _Entry( string vEntryName, string[] vTrackName, string vSoundFile )
		{
			m_vEntryName = vEntryName;
			m_vTrackName = vTrackName;
			m_vSoundFile = vSoundFile;
			//			m_vSoundTime = vSoundTime;
			m_vAudioClip = null;
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// トラック
	/// １個の AudioSource を持っているよ
	/// </summary>
	private class _Track
	{
		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		/// <summary>
		/// 
		/// </summary>
		public string m_vTrackName;

		/// <summary>
		/// 親の AcSoundManager の GameObject にコンポーネントとして追加して保持します
		/// </summary>
		public AudioSource m_vAudioSource;

		/// <summary>
		/// _State をオーバーライドしたクラスを保持して音の遷移状態に合わせた処理を行います
		/// </summary>
		public _State m_vState;

		/// <summary>
		/// 再生すべき音の登録データを、再生時に受け取って保持します（このトラックが使用中であるフラグにもなっている）
		/// </summary>
		public _Entry m_vEntry;

		///// <summary>
		///// 
		///// </summary>
		//public float m_vVolume;

		//		public bool m_bPlaying;

		/// <summary>
		/// 
		/// </summary>
		public int m_vTimeStamp;

		/// <summary>
		/// 再生時間
		/// </summary>
		//		public float m_vPlayTime;

		/// <summary>
		/// フェードイン用時間
		/// </summary>
		public float m_vFadeInTime;

		/// <summary>
		/// フェードアウト用時間
		/// </summary>
		public float m_vFadeOutTime;

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		//public _Track( AcSoundManager vSoundManager )
		//{
		//	m_vAudioSource = vSoundManager.m_vGameObject.AddComponent<AudioSource>();
		//	//
		//	m_vState = new _Wait( this );
		//	//
		//	m_vEntry = null;
		//	//
		//	//			m_vPlayTime = 0.0f;
		//	//
		//	m_vTimeStamp = 0;
		//	//
		//	m_vFadeInTime = 0.0f;
		//	m_vFadeOutTime = 0.0f;
		//}

		public _Track( string vTrackName, AudioSource vAudioSource )
		{
			m_vTrackName = vTrackName;
			//
			m_vAudioSource = vAudioSource;
			//
			m_vState = new _Wait( this );
			//
			m_vEntry = null;
			//
			//			m_vPlayTime = 0.0f;
			//
			m_vTimeStamp = 0;
			//
			m_vFadeInTime = 0.0f;
			m_vFadeOutTime = 0.0f;
		}

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		//public bool isPlaying()
		//{
		//	return ( m_vAudioSource.isPlaying );
		//}

		public bool isAutoWait()
		{
			if ( !m_vAudioSource.isPlaying )
			{
				_debugLog( "自動終了" );
				_debugLog( "トラック名 >> " + m_vTrackName );
				_debugLog( "エントリ名 >> " + m_vEntry.m_vEntryName );

				//				m_vAudioSource.Stop();
				m_vState = new _Wait( this );
				//
				return ( true );
			}
			//
			return ( false );
		}

		public bool isBusy()
		{
			return ( m_vEntry != null );
		}

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		public void play( _Entry vEntry, float vFadeInTime )
		{
			if ( m_vAudioSource != null )
			{
				m_vEntry = vEntry;
				//				m_vPlayTime = 0.0f;
				//
				//				m_vTimeStamp = Time.realtimeSinceStartup; // http://docs-jp.unity3d.com/Documentation/ScriptReference/Time.html
				m_vTimeStamp = Time.frameCount;
				//
				m_vFadeInTime = vFadeInTime;
				m_vFadeOutTime = 0.0f;

				m_vState.play();

				//m_vAudioSource.clip = vEntry.m_vAudioClip;

				////
				//if ( vFadeInTime > 0.0f )
				//{
				//	m_vFadeInTime = vFadeInTime;
				//	m_vAudioSource.volume = 0.0f;
				//}
				//else
				//{
				//	m_vAudioSource.volume = 1.0f;
				//}
				////
				//m_vAudioSource.Play();

				//
				_debugLog( "トラック名 >> " + m_vTrackName );
				_debugLog( "エントリ名 >> " + m_vEntry.m_vEntryName );
			}
		}

		//public void play( _Entry vData )
		//{
		//	play( vData, 0.0f );
		//}

		public void pause()
		{
			if ( m_vEntry != null )
			{
				m_vAudioSource.Pause();
			}

			//if ( m_vAudioSource != null )
			//{
			//	m_vState.pause();
			//}
		}

		public void stop( float vFadeOutTime )
		{
			if ( m_vAudioSource != null )
			{
				m_vFadeOutTime = vFadeOutTime;
				m_vState.stop();
			}
		}

		public void update()
		{
			if ( m_vAudioSource != null )
			{
				m_vState.update();
			}
		}

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //
	}

	// ========================================================================== //
	// ========================================================================== //

	private class _TrackComparer : IComparer
	{
		int IComparer.Compare( object x, object y )
		{
			return ( ( ( _Track ) x ).m_vTimeStamp - ( ( _Track ) y ).m_vTimeStamp );
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * _State のやり方？
	 * http://marupeke296.com/UNI_SND_No4_BGM.html
	 */

	/// <summary>
	/// 状態毎の各クラスの基底クラスです
	/// </summary>
	private class _State
	{
		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		/// <summary>
		/// トラックを保持するよ
		/// </summary>
		public _Track m_vTrack;

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		/// <summary>
		/// コンストラクタ（親であるトラックを受け取ります）
		/// </summary>
		/// <param name="vTrack"></param>
		public _State( _Track vTrack )
		{
			m_vTrack = vTrack;
		}

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		//public bool isPlaying()
		//{
		//	if ( !m_vTrack.m_vAudioSource.isPlaying )
		//	{
		//		Debug.Log( "自動終了 >> " + m_vTrack.m_vEntry.m_vEntryName );

		//		m_vTrack.m_vAudioSource.Stop();
		//		m_vTrack.m_vState = new _Wait( m_vTrack );
		//		//
		//		return ( false );
		//	}
		//	//
		//	return ( true );
		//}

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		//
		//public virtual bool isWaiting()
		//{
		//	return ( false );
		//}
		//
		public virtual void play()
		{
		}
		//
		public virtual void stop()
		{
		}
		//
		public virtual void pause()
		{
		}
		//
		public virtual void update()
		{
		}

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //
	}

	// ========================================================================== //
	// ========================================================================== //

	class _Wait : _State
	{
		public _Wait( _Track vTrack )
			: base( vTrack )
		{
			m_vTrack.m_vAudioSource.Stop();
			m_vTrack.m_vEntry = null;
		}

		//public override bool isWaiting()
		//{
		//	return ( true );
		//}

		public override void play()
		{
			if ( m_vTrack.m_vFadeInTime > 0.0f )
			{
				// フェードインの時間が設定されていればフェードインする
				m_vTrack.m_vState = new _FadeIn( m_vTrack );
			}
			else
			{
				// 即、再生する
				m_vTrack.m_vState = new _Play( m_vTrack );
			}
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	class _FadeIn : _State
	{
		/// <summary>
		/// 経過時間
		/// </summary>
		private float m_vTime;

		public _FadeIn( _Track vTrack )
			: base( vTrack )
		{
			m_vTrack.m_vAudioSource.clip = m_vTrack.m_vEntry.m_vAudioClip;
			//
			m_vTrack.m_vAudioSource.Play();
			m_vTrack.m_vAudioSource.volume = 0.0f;
			//
			m_vTime = 0.0f;
		}

		public override void pause()
		{
			m_vTrack.m_vState = new _Pause( m_vTrack, this );
		}

		public override void stop()
		{
			m_vTrack.m_vState = new _FadeOut( m_vTrack );
		}

		public override void update()
		{
			if ( !m_vTrack.isAutoWait() )
			{
				m_vTime += Time.deltaTime;
				m_vTrack.m_vAudioSource.volume = m_vTime / m_vTrack.m_vFadeInTime;
				//
				if ( m_vTime >= m_vTrack.m_vFadeInTime )
				{
					m_vTrack.m_vAudioSource.volume = 1.0f;
					m_vTrack.m_vState = new _Play( m_vTrack );
				}
			}
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	class _Play : _State
	{
		public _Play( _Track vTrack )
			: base( vTrack )
		{
			//Debug.Log( "isPlaying" );
			//if ( m_vSoundManager.m_vAudioSource.isPlaying == false )
			//{
			//	Debug.Log( "isPlaying 2" );

			//	m_vSoundManager.m_vAudioSource.clip = m_vSoundManager.m_vData.m_vAudioClip;
			//	//
			//	m_vSoundManager.m_vAudioSource.volume = 1.0f;
			//	m_vSoundManager.m_vAudioSource.Play();

			//}
			play();
		}

		public override void play()
		{
			m_vTrack.m_vAudioSource.clip = m_vTrack.m_vEntry.m_vAudioClip;
			//
			m_vTrack.m_vAudioSource.volume = 1.0f;
			m_vTrack.m_vAudioSource.Play();
		}

		//public override void play()
		//{
		//	Debug.Log( "isPlaying" );
		//	if ( m_vTrack.m_vAudioSource.isPlaying == false )
		//	{
		//		Debug.Log( "isPlaying 2" );

		//		m_vTrack.m_vAudioSource.clip = m_vTrack.m_vEntry.m_vAudioClip;
		//		//
		//		m_vTrack.m_vAudioSource.volume = 1.0f;
		//		m_vTrack.m_vAudioSource.Play();

		//	}
		//}

		public override void pause()
		{
			m_vTrack.m_vState = new _Pause( m_vTrack, this );
		}

		public override void stop()
		{
			m_vTrack.m_vState = new _FadeOut( m_vTrack );
		}

		public override void update()
		{
			m_vTrack.isAutoWait();
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	class _Pause : _State
	{
		_State m_vPreState;

		public _Pause( _Track vTrack, _State preState )
			: base( vTrack )
		{
			m_vPreState = preState;
			vTrack.m_vAudioSource.Pause();
		}

		public override void stop()
		{
			m_vTrack.m_vAudioSource.Stop();
			m_vTrack.m_vState = new _Wait( m_vTrack );
		}

		public override void play()
		{
			m_vTrack.m_vState = m_vPreState;
			m_vTrack.m_vAudioSource.Play();
		}
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	class _FadeOut : _State
	{
		/// <summary>
		/// フェードアウト開始時のボーリュームの値
		/// </summary>
		float m_vVolume;

		/// <summary>
		/// 経過時間
		/// </summary>
		float m_vTime;

		public _FadeOut( _Track vTrack )
			: base( vTrack )
		{
			m_vVolume = m_vTrack.m_vAudioSource.volume;
			m_vTime = 0.0f;
		}

		public override void pause()
		{
			m_vTrack.m_vState = new _Pause( m_vTrack, this );
		}

		public override void update()
		{
			if ( !m_vTrack.isAutoWait() )
			{
				m_vTime += Time.deltaTime;
				m_vTrack.m_vAudioSource.volume = m_vVolume * ( 1.0f - m_vTime / m_vTrack.m_vFadeOutTime );
				//
				if ( m_vTime >= m_vTrack.m_vFadeOutTime )
				{
					m_vTrack.m_vAudioSource.volume = 0.0f;
					m_vTrack.m_vAudioSource.Stop();
					m_vTrack.m_vState = new _Wait( m_vTrack );
				}
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //
}
