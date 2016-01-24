using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneManager_script : MonoBehaviour {

	void Start () 
	{
        DontDestroyOnLoad( gameObject );
    }

    public void LoadScene( string name )
    {
        SceneManager.LoadScene( name );
    }
}
