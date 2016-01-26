using UnityEngine;
using System.Collections;
using System;

public class PlatformVanisher : MonoBehaviour {

	public GameObject platform; // reference to the platform to vanish

    [Range(5, 9)]
    [Tooltip("How long the platform will remain solid or not.")]
	public float stateTime = 5f; // how long each state will be active
    [Range(0, 3)]
    public int timeDelay = 0;

    private bool _visible = true;
    private bool _vanishing = false;
    private SpriteRenderer _renderer;
    private BoxCollider2D _collider;
    private float _normalAlpha = 1f;
    private float _vanishedAlpha = 0.25f;
    private Animator _animator;

    // private variables

    private float _lastChangeStateTime;

	// Use this for initialization
	void Start () {
        _collider = platform.GetComponent<BoxCollider2D>();
        _animator = platform.GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator component missing from this gameobject");
        }
        _lastChangeStateTime = 0f + timeDelay;
        _animator.SetBool("Solid", true);
        _animator.SetBool("Vanishing", false);
    }
	
	// game loop
	void Update () {
        var currentTime = Time.time;
        if (currentTime >= (_lastChangeStateTime + stateTime - 1.5f))
        {
            if (!_vanishing)
            {
                _animator.SetBool("Vanishing", true);
                _vanishing = true;
            }
        }
        if (currentTime >= (_lastChangeStateTime + stateTime))
        {
            ChangeState();
            _lastChangeStateTime = currentTime;
        }
	}

    private void ChangeState()
    {
        _visible = !_visible;
        _collider.enabled = _visible;
        _animator.SetBool("Vanishing", false);
        _vanishing = false;
        _animator.SetBool("Solid", _visible);
    }
}
