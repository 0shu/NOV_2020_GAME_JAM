using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceScript : MonoBehaviour
{
    // Public so that they may be dragged in the inspector on Unity :)
    public AudioClip intro, Loop;
    public AudioSource source;
    // Start is called before the first frame update
    void Start() // When the Scene starts (loads for the first time)
    {
        source.clip = intro; // Set the current audio clip to the intro.
        source.Play(); // Play the intro.
        Debug.Log("Playing intro"); // Debug for testing
        StartCoroutine(MusicLoop()); // Go to the coroutine
    }
    IEnumerator MusicLoop () // This makes the loop section repeat until the scene is unloaded or otherwise.
    {
        yield return new WaitForSeconds(source.clip.length);
        source.loop = true; // Set the audio to loop.
        source.clip = Loop; // Change to the loop song file.
        source.Play(); // Play the loop.
        Debug.Log("Playing loop"); // Debug for testing.
        
    }

}
