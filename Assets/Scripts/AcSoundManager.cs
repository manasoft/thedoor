using UnityEngine;
using System.Collections;

// Dictionary
using System.Collections.Generic;

/// <summary>
/// サウンドマネージャー
/// いつでもどこでも音が出せるよ！
/// ポジションとか指定しないので 2D ゲーム専用っぽいけど・・・
/// 
/// 一応 Ac で作るが Sc でも行ける感じで作ること！
/// </summary>
public class AcSoundManager : MonoBehaviour
{
	// ========================================================================== //
	// ========================================================================== //
	/*
	 * 継承の件
	 * [ : MonoBehaviour]
	 *		・new 出来ない 警告が出るよ
	 *			"You are trying to create a MonoBehaviour using the 'new' keyword.  This is not allowed.  MonoBehaviours can only be added using AddComponent().  Alternatively, your script can inherit from ScriptableObject or no base class at all"
	 *		・GameObject に追加してつかう
	 *		・Start() / Update() の処理が呼ばれる
	 * 
	 * [ : Object] (UnityEngine.Object)
	 *		・new は出来るよ、当たり前でしょ！
	 *		・Start() / Update() の処理が呼ばれない
	 *		
	 * 
	 * しかし基本的には Unity は new 禁止らしい・・・
	 * つーことは全部カラのオブジェクトにくっつける感じにすんのかな？
	 * 
	 */

	/*
	 * ファイルフォーマットとループ
	 * 2014/10/29時点
	 * ループさせたい場合に AudioSource で loop = true にするが ogg の場合、うまくいかない（wav は問題ないようだ）
	 * ループ時は isPlaying も true状態が続くようですか ogg の場合は終わってしまう
	 * で対応策として、NextEntryName に自分を指定してループさせている
	 * 
	 * ※注意
	 * これが本当にファイルフォーマットに問題があるかはよくわからないところです
	 * 単純に AudioSource にファイルをセットした場合はちゃんとループするので、何か設定に間違いがあるのかも
	 * 
	 * ※解決？
	 * プレイ後にループのフラグを立てるように修正したら直った！
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

	///// <summary>
	///// ゲームオブジェクトに付ける名前
	///// </summary>
	//private const string _NAME = "AcSoundManager";

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// ========================================================================== //
	// ========================================================================== //

	/// <summary>
	/// 音を鳴らすには AudioSource が必要です、AudioSource は GameObject にコンポーネントとしてくっつける必要があります
	/// </summary>
	//private GameObject m_vGameObject;

	/// <summary>
	/// 「サウンド」を保持します（サウンドで使う AudioClip を保持します）
	/// </summary>
	private Dictionary<string, _Sound> m_vSoundDictionary;

	/// <summary>
	/// 「登録データ」を保持します
	/// </summary>
	private Dictionary<string, _Entry> m_vEntryDictionary;

	/// <summary>
	/// 「登録データ」を元に作成した「トラックデータ」を保持します
	/// </summary>
	private Dictionary<string, _Track> m_vTrackDictionary;

	/// <summary>
	/// 「聞く人」を自分の中に作るよ！
	/// </summary>
	private AudioListener m_vAudioListener;

	///// <summary>
	///// Update() を自前で行おうとしたがうまくいかないので放置中・・・
	///// </summary>
	//private _MonoBehaviour m_vMonoBehaviour;

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
		//AcDebug.debugLog( vString );
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public static AcSoundManager Create()
	{
		GameObject _object = new GameObject();
		//
		AcSoundManager _class = ( AcSoundManager ) _object.AddComponent( ( typeof( AcSoundManager ) ) );
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
		if ( this == null )
		{
			_debugLog( "AcSoundManager の this == null らしい" );
		}
		else
		{
			_debugLog( "AcSoundManager の this != null らしい" );
		}
		//
		m_vSoundDictionary = new Dictionary<string, _Sound>();
		m_vEntryDictionary = new Dictionary<string, _Entry>();
		m_vTrackDictionary = new Dictionary<string, _Track>();
		/*
		 * "聞く人" も自分が行うよ！
		 */
		m_vAudioListener = this.gameObject.AddComponent<AudioListener>();
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //


	/// <summary>
	/// コンストラクタ
	/// </summary>
	private AcSoundManager()
	{
		//if ( this == null )
		//{
		//	_debugLog( "AcSoundManager の this == null らしい" );
		//}
		//else
		//{
		//	_debugLog( "AcSoundManager の this != null らしい" );
		//}

		//m_vGameObject = new GameObject( _NAME );
		////
		//m_vSoundDictionary = new Dictionary<string, _Sound>();
		//m_vEntryDictionary = new Dictionary<string, _Entry>();
		//m_vTrackDictionary = new Dictionary<string, _Track>();

		///*
		// * "聞く人" も自分が行うよ！
		// */
		//m_vAudioListener = m_vGameObject.AddComponent<AudioListener>();

		/*
		 * ゲームオブジェクトのコンポーネントに MonoBehaviour を継承したクラスを追加して自動的に Updata() してもらうよ！
		 * この処理は選択出来た方がいいかもね
		 */
		//		m_vMonoBehaviour = m_vGameObject.AddComponent<_MonoBehaviour>();
		//		m_vMonoBehaviour.m_vManager = this;
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	///// <summary>
	///// データを登録するよ！
	///// </summary>
	///// <param name="vEntryName"></param>
	///// <param name="vTrackName"></param>
	///// <param name="vSoundFile"></param>
	//public void add( string vEntryName, string[] vTrackName, string vSoundFile )
	//{
	//	/*
	//	 * 
	//	 */
	//	m_vEntryDictionary.Add( vEntryName, new _Entry( vEntryName, vTrackName, vSoundFile ) );

	//	/*
	//	 * コンポーネントの追加で失敗する事があるのか不明だがココで null チェックしているので _Track の中では null チェックが不要になります
	//	 */
	//	foreach ( string _trackName in vTrackName )
	//	{
	//		if ( !m_vTrackDictionary.ContainsKey( _trackName ) )
	//		{
	//			AudioSource _audioSource = m_vGameObject.AddComponent<AudioSource>();
	//			//
	//			if ( _audioSource != null )
	//			{
	//				m_vTrackDictionary.Add( _trackName, new _Track( this, _trackName, _audioSource ) );
	//			}
	//		}
	//	}
	//}

	/// <summary>
	/// データを追加します
	/// </summary>
	/// <param name="vEntryName"></param>
	/// <param name="vGroupName"></param>
	/// <param name="vTrackName"></param>
	/// <param name="vVolume"></param>
	/// <param name="vPan"></param>
	/// <param name="bLoop"></param>
	/// <param name="vNextEntryName"></param>
	/// <param name="vSoundFileName"></param>
	public void add( string vEntryName, string vGroupName, string[] vTrackName, float vVolume, float vPan, bool bLoop, string vNextEntryName, string vSoundFileName )
	{
		/*
		 * 同じ名前を登録しようとすると落ちるので注意して！
		 */
		if ( !m_vEntryDictionary.ContainsKey( vEntryName ) )
		{
			m_vEntryDictionary.Add( vEntryName, new _Entry( vEntryName, vGroupName, vTrackName, vVolume, vPan, bLoop, vNextEntryName, vSoundFileName ) );
			/*
			 * コンポーネントの追加で失敗する事があるのか不明だがココで null チェックしているので _Track の中では null チェックが不要になります
			 */
			foreach ( string _trackName in vTrackName )
			{
				if ( !m_vTrackDictionary.ContainsKey( _trackName ) )
				{
					//AudioSource _audioSource = m_vGameObject.AddComponent<AudioSource>();
					AudioSource _audioSource = this.gameObject.AddComponent<AudioSource>();
					//
					if ( _audioSource != null )
					{
						m_vTrackDictionary.Add( _trackName, new _Track( this, _trackName, _audioSource ) );
					}
				}
			}
			//
			if ( !m_vSoundDictionary.ContainsKey( vSoundFileName ) )
			{
				m_vSoundDictionary.Add( vSoundFileName, new _Sound( vSoundFileName ) );
			}
		}
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private void _awake()
	{
	}

	private void _start()
	{
	}

	/// <summary>
	/// 毎フレーム実行する処理です
	/// </summary>
	private void _update()
	{
		foreach ( _Track _track in m_vTrackDictionary.Values )
		{
			_track.update();
		}
	}

	/// <summary>
	/// ロードした AudioClip を捨てないとマズイんじゃないのかな？
	/// </summary>
	private void _onDestroy()
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
		//if ( m_vGameObject != null )
		//{
		//	GameObject.Destroy( m_vGameObject );
		//	m_vGameObject = null;
		//}

		GameObject.Destroy( this );
	}

	// ========================================================================== //
	// ========================================================================== //

	///// <summary>
	///// AcSoundManager : Object の場合、明示的に呼び出す update 処理です
	///// </summary>
	//public void update()
	//{
	//	_update();
	//}

	// ========================================================================== //
	// ========================================================================== //


	// ========================================================================== //
	// ========================================================================== //

	/*
	 * AcSoundManager : MonoBehaviour の場合の処理
	 * うまく行っていないので放置している（2014/10/28）
	 */

	void Awake()
	{
		_debugLog( "AcSoundManager # Awake" );

		_awake();
	}

	// Use this for initialization
	void Start()
	{
		_debugLog( "AcSoundManager # Start" );

		_start();
	}

	// Update is called once per frame
	void Update()
	{
		_debugLog( "AcSoundManager # Update" );

		_update();
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	void OnGUI()
	{
	}

	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	void OnApplicationQuit()
	{
		_debugLog( "AcSoundManager # OnApplicationQuit" );
	}

	void OnDestroy()
	{
		_debugLog( "AcSoundManager # OnDestroy" );
		//
		_onDestroy();
	}

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //


	public string play( string vEntryName, float vFadeInTime )
	{
		_debugLog( "AcSoundManager # play" );

		/*
		 * 登録されているか？
		 */
		if ( m_vEntryDictionary.ContainsKey( vEntryName ) )
		{
			_Entry _entry = m_vEntryDictionary[ vEntryName ];
			_Track _track = null;
			///*
			// * クリップをロードする
			// */
			//if ( _entry.m_vAudioClip == null )
			//{
			//	_entry.m_vAudioClip = ( AudioClip ) Resources.Load( _entry.m_vSoundFile );
			//}

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
						_track.play( _entry, vFadeInTime );
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
				_track.stop( 0.0f );
				_track.play( _entry, vFadeInTime );
				//
				return ( _track.m_vTrackName );
			}
		}
		//
		return ( null );
	}


	public void stop( string vTrackName, float vFadeOutTime )
	{
		if ( m_vTrackDictionary.ContainsKey( vTrackName ) )
		{
			_Track _track = m_vTrackDictionary[ vTrackName ];
			//
			_track.stop( vFadeOutTime );
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



	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	private class _Sound
	{
		/// <summary>
		/// サウンドファイル名
		/// </summary>
		public string m_vSoundFileName;

		/// <summary>
		/// リソースをロードして保持する
		/// </summary>
		public AudioClip m_vAudioClip;

		public _Sound( string vSoundFileName )
		{
			m_vSoundFileName = null;
			m_vAudioClip = null;
			//
			m_vSoundFileName = vSoundFileName;
			//
			load();
			//m_vAudioClip = ( AudioClip ) Resources.Load( vSoundFileName );
		}

		public AudioClip load()
		{
			if ( m_vAudioClip == null )
			{
				/*
				 * テクスチャーの場合は Instantiate() を使っていたけどオーディオクリップはそのままで良いらしい
				 * と思ったら、テクスチャーでも無くても良かった様です
				 */
				m_vAudioClip = ( AudioClip ) Resources.Load( m_vSoundFileName, typeof( AudioClip ) );
			}
			//
			return ( m_vAudioClip );
		}

		public void release()
		{
			if ( m_vAudioClip != null )
			{
				// 適当にやってみた、動作未確認です
				Object.Destroy( m_vAudioClip );
				m_vAudioClip = null;
			}
		}

		//public AudioClip getAudioClip()
		//{
		//	if ( m_vAudioClip == null )
		//	{
		//		m_vAudioClip = ( AudioClip ) Resources.Load( m_vSoundFileName );
		//	}
		//	//
		//	return ( m_vAudioClip );
		//}
	}

	// ========================================================================== //
	// ========================================================================== //

	/*
	 * 参考までに・・・
	 * 昔々 Android でやってた時のです
	 * private _ScSoundManager_Entry( String vEntry, String[] vGroup, int vPriority, float vVolumeL, float vVolumeR, boolean bLoop, String vNextEntry, String vResourceFile )
	 */

	/// <summary>
	/// 登録データを保持するクラス
	/// </summary>
	private class _Entry
	{
		/// <summary>
		/// エントリー名（登録した名前です、基本的にこの名前でアクセスする）
		/// </summary>
		public string m_vEntryName;

		/// <summary>
		/// グループ名（reload/release 等のコントロール用として使用する予定です）
		/// </summary>
		public string m_vGroupName;

		/// <summary>
		/// 使用するトラックの名前のリスト
		/// </summary>
		public string[] m_vTrackName;

		public float m_vVolume;		// 0.0f ~ 1.0f
		public float m_vPan;		// -1.0f ~ 0 ~ +1.0f
		public bool m_bLoop;		// false / true
		public string m_vNextEntryName;

		/// <summary>
		/// 音のファイル名
		/// </summary>
		public string m_vSoundFileName;


		///// <summary>
		///// 
		///// </summary>
		//public float m_vSoundTime;

		/// <summary>
		/// リソースをロードして保持する
		/// </summary>
		//public AudioClip m_vAudioClip;

		//public _Entry( string vEntryName, string[] vTrackName, string vSoundFile )
		//{
		//	m_vEntryName = vEntryName;
		//	m_vTrackName = vTrackName;
		//	m_vSoundFile = vSoundFile;
		//	//			m_vSoundTime = vSoundTime;
		//	m_vAudioClip = null;

		//	m_vVolume = 0.5f;
		//	m_vPan = 0.5f;
		//	m_bLoop = false;
		//	m_vNextEntryName = "se_2";


		//	//{
		//	//	// 実験
		//	//	AudioSource _s = new AudioSource();
		//	//	_s.pan = 1.0f;
		//	//	_s.volume = 1.0f;
		//	//	_s.loop = true;
		//	//}
		//}

		public _Entry( string vEntryName, string vGroupName, string[] vTrackName, float vVolume, float vPan, bool bLoop, string vNextEntryName, string vSoundFileName )
		{
			//if ( this == null )
			//{
			//	_debugLog( "_Entry の this == null らしい" );
			//}
			//else
			//{
			//	_debugLog( "_Entry の this != null らしい" );
			//}

			m_vEntryName = vEntryName;
			m_vGroupName = vGroupName;
			m_vTrackName = vTrackName;
			m_vSoundFileName = vSoundFileName;
			//
			m_vVolume = vVolume;
			m_vPan = vPan;
			m_bLoop = bLoop;
			m_vNextEntryName = vNextEntryName;
			//
			//			m_vSoundTime = vSoundTime;
			//m_vAudioClip = null;
			//
			//if ( m_vAudioClip == null )
			//{
			//	m_vAudioClip = ( AudioClip ) Resources.Load( m_vSoundFileName );
			//}


			//{
			//	// 実験
			//	AudioSource _s = new AudioSource();
			//	_s.pan = 1.0f;
			//	_s.volume = 1.0f;
			//	_s.loop = true;
			//}
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
		/// 親（NextEntryName 処理をする際に必要になる）
		/// </summary>
		public AcSoundManager m_vManager;

		/// <summary>
		/// 自分のトラック名
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

		public _Track( AcSoundManager vManager, string vTrackName, AudioSource vAudioSource )
		{
			m_vManager = vManager;
			//
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

			{
				//				m_vAudioSource.
			}
		}

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		//public bool isPlaying()
		//{
		//	return ( m_vAudioSource.isPlaying );
		//}

		public AudioClip getAudioClip()
		{
			AudioClip _audioClip = null;
			//
			if ( m_vEntry != null )
			{
				if ( m_vManager.m_vSoundDictionary.ContainsKey( m_vEntry.m_vSoundFileName ) )
				{
					//_audioClip = m_vManager.m_vSoundDictionary[ m_vEntry.m_vSoundFileName ].getAudioClip();
					_audioClip = m_vManager.m_vSoundDictionary[ m_vEntry.m_vSoundFileName ].load();
				}
			}
			//
			return ( _audioClip );
		}

		public bool isAutoWait()
		{
			if ( !m_vAudioSource.isPlaying )
			{
				_debugLog( "自動終了" );
				_debugLog( "トラック名 >> " + m_vTrackName );
				_debugLog( "エントリ名 >> " + m_vEntry.m_vEntryName );

				/*
				 * 
				 */
				if ( m_vAudioSource.loop )
				{
					_debugLog( "m_vAudioSource.loop なのに終了した" );
				}

				//
				if ( m_vEntry.m_vNextEntryName == null )
				{
					//m_vAudioSource.Stop();
					//m_vState = new _Wait( this );
					stop( m_vFadeOutTime );
				}
				else
				{
					_debugLog( "次へ >> " + m_vEntry.m_vNextEntryName );

					if ( m_vManager.m_vEntryDictionary.ContainsKey( m_vEntry.m_vNextEntryName ) )
					{
						_Entry _entry = m_vManager.m_vEntryDictionary[ m_vEntry.m_vNextEntryName ];
						//
						_debugLog( "連続再生" );
						//
						stop( 0.0f );
						play( _entry, 0.0f );
					}
				}
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

	/// <summary>
	/// 「全てのトラックが使用中の時に、先に発音したものから上書きされていく処理」のための比較用クラス
	/// </summary>
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
			//m_vTrack.m_vAudioSource.clip = m_vTrack.m_vEntry.m_vAudioClip;
			m_vTrack.m_vAudioSource.clip = m_vTrack.getAudioClip();
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
				//m_vTrack.m_vAudioSource.volume = m_vTime / m_vTrack.m_vFadeInTime;
				m_vTrack.m_vAudioSource.volume = m_vTrack.m_vEntry.m_vVolume * m_vTime / m_vTrack.m_vFadeInTime;
				//
				if ( m_vTime >= m_vTrack.m_vFadeInTime )
				{
					//m_vTrack.m_vAudioSource.volume = 1.0f;
					m_vTrack.m_vAudioSource.volume = m_vTrack.m_vEntry.m_vVolume;
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

			if ( !m_vTrack.m_vAudioSource.isPlaying )
			{
				/*
				 * クリップをロードする
				 */
				//if ( m_vTrack.m_vEntry.m_vAudioClip == null )
				//{
				//	m_vTrack.m_vEntry.m_vAudioClip = ( AudioClip ) Resources.Load( m_vTrack.m_vEntry.m_vSoundFileName );
				//}

				//m_vTrack.m_vAudioSource.clip = m_vTrack.m_vEntry.m_vAudioClip;
				m_vTrack.m_vAudioSource.clip = m_vTrack.getAudioClip();

				/*
				 * 2014/10/29
				 * 少なくとも loop は Play() 後に設定する必要があるらしい
				 * なので、全部 Play() 後の設定する様に変更したよ
				 */
				//
				//m_vTrack.m_vAudioSource.volume = 1.0f;
				//m_vTrack.m_vAudioSource.volume = m_vTrack.m_vEntry.m_vVolume;
				//m_vTrack.m_vAudioSource.pan = m_vTrack.m_vEntry.m_vPan;
				//m_vTrack.m_vAudioSource.loop = m_vTrack.m_vEntry.m_bLoop;
				//
				m_vTrack.m_vAudioSource.Play();
				//
				m_vTrack.m_vAudioSource.volume = m_vTrack.m_vEntry.m_vVolume;
				m_vTrack.m_vAudioSource.pan = m_vTrack.m_vEntry.m_vPan;
				m_vTrack.m_vAudioSource.loop = m_vTrack.m_vEntry.m_bLoop;

				//{
				//	/*
				//	 * 実験
				//	 */
				//	m_vTrack.m_vAudioSource.loop = true;
				//	m_vTrack.m_vAudioSource.pan = -1.0f; // -1.0f ← 0 → +1.0f
				//}
			}
			//play();
		}

		//public override void play()
		//{
		//	m_vTrack.m_vAudioSource.clip = m_vTrack.m_vEntry.m_vAudioClip;
		//	//
		//	m_vTrack.m_vAudioSource.volume = 1.0f;
		//	m_vTrack.m_vAudioSource.Play();
		//}

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
			if ( m_vTrack.m_vFadeOutTime > 0.0f )
			{
				// フェードアウトの時間が設定されていればフェードアウトする
				m_vTrack.m_vState = new _FadeOut( m_vTrack );
			}
			else
			{
				// 即、停止する
				m_vTrack.m_vState = new _Wait( m_vTrack );
			}
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

	// ========================================================================== //
	// ========================================================================== //

	//	private class _MonoBehaviour : MonoBehaviour
	private class _MonoBehaviour : Object
	{
		// ========================================================================== //
		// ========================================================================== //

		public AcSoundManager m_vManager;

		// ========================================================================== //
		// ========================================================================== //

		private void _awake()
		{
			_debugLog( "_MonoBehaviour # _awake" );
		}

		private void _start()
		{
			_debugLog( "_MonoBehaviour # _start" );
		}

		private void _update()
		{
			if ( m_vManager != null )
			{
				_debugLog( "_MonoBehaviour # _update" );

				m_vManager._update();
			}
		}

		// ========================================================================== //
		// ========================================================================== //

		/// <summary>
		/// ※注意
		/// ・Instantiate 直後に別スレッドで呼ばれちゃいみたいっす
		/// ・自分 this は参照出来るが、コンストラクタ内の途中で呼び出されるので注意が必要デス
		/// </summary>
		void Awake()
		{
			_awake();
		}

		// Use this for initialization
		void Start()
		{
			_start();
		}

		// Update is called once per frame
		void Update()
		{
			_update();
		}

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		void OnGUI()
		{
		}

		// -------------------------------------------------------------------------- //
		// -------------------------------------------------------------------------- //

		void OnApplicationQuit()
		{
			_debugLog( "_MonoBehaviour # OnApplicationQuit" );
		}

		void OnDestroy()
		{
			_debugLog( "_MonoBehaviour # OnDestroy" );
		}

		// ========================================================================== //
		// ========================================================================== //
	}

	// ========================================================================== //
	// ========================================================================== //
}
