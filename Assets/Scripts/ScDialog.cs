using UnityEngine;
using System.Collections;

public class ScDialog : MonoBehaviour
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

	//private GUIContent[] m_vContent;

	// ========================================================================== //
	// ========================================================================== //

	// ========================================================================== //
	// ========================================================================== //

	public static ScDialog Create()
	{
		GameObject _object = new GameObject();
		//
		ScDialog _class = ( ScDialog ) _object.AddComponent( ( typeof( ScDialog ) ) );
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

	// ========================================================================== //
	// ========================================================================== //

	private void _onGui()
	{
		//GUI.Window()



		float x = 10.0f;
		float y = 10.0f;

		GUI.Box(  new Rect( x, y, 150, 150 ), "this is Dialog" );




		if ( GUI.Button( new Rect( x + 25, y + 30, 100, 50 ), "YES" ) )
		{

			ScDebug.debugLog( "bottun pushed" );

			Destroy( this );

		};

		if ( GUI.Button( new Rect( x + 25, y + 90, 100, 50 ), "NO" ) )
		{

			ScDebug.debugLog( "bottun pushed" );

		};
	}
	
	// -------------------------------------------------------------------------- //
	// -------------------------------------------------------------------------- //

	// Use this for initialization
	void Start()
	{
		ScDebug.debugLog( "ほげ" );
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnGUI()
	{
		_onGui();
	}

	// ========================================================================== //
	// ========================================================================== //
}
