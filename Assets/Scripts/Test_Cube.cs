using UnityEngine;
using System.Collections;

public class Test_Cube : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		this.gameObject.transform.Rotate( Vector3.one, 0.2f );

		if ( Input.GetMouseButtonDown( 0 ) )
		{
			Application.LoadLevel( "Game" );
		}
	}

	void OnGUI()
	{
		AcApp.imageDraw( new Rect( 0, 50, 100, 50 ), AcApp.IMAGE_NUMBER, 8 );
		AcApp.imageDraw( new Rect( 0, 100, 100, 50 ), AcApp.IMAGE_INFO, 0 );
		AcApp.imageDraw( new Rect( 0, 150, 100, 50 ), AcApp.IMAGE_INFO, 1 );

		GUI.Label( new Rect( 0, 0, Screen.width, 50 ), new GUIContent( "iOS 2014/12/10 18時02分" ) );
	}

}

