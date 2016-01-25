using UnityEngine;
using System.Collections;
using System;

public class PlatformVanisher : MonoBehaviour {

	public GameObject platform; // reference to the platform to vanish

    [Range(3, 7)]
    [Tooltip("How long the platform will remain solid or not.")]
	public float stateTime = 3f; // how long each state will be active

    private bool _visible = true;
    private SpriteRenderer _renderer;
    private BoxCollider2D _collider;
    private float _normalAlpha = 1f;
    private float _vanishedAlpha = 0.25f;

    // private variables

    private float _lastChangeStateTime;

	// Use this for initialization
	void Start () {
        _renderer = platform.GetComponent<SpriteRenderer>();
        _normalAlpha = _renderer.material.color.a;
        _vanishedAlpha = _normalAlpha * 0.25f;
        _collider = platform.GetComponent<BoxCollider2D>();
        _lastChangeStateTime = 0f;
	}
	
	// game loop
	void Update () {
        var currentTime = Time.time;
        if (currentTime >= (_lastChangeStateTime + stateTime)) {
            ChangeState();
            _lastChangeStateTime = currentTime;
        }
	}

    private void ChangeState()
    {
        _visible = !_visible;
        float newAlpha = _visible ? _normalAlpha : _vanishedAlpha;
        _renderer.material.color = new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, newAlpha);
        _collider.enabled = _visible;
    }
}
