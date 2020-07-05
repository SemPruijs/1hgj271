using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
      //Input
      private float _moveHorizontal;
      private float _moveVertical;

      private Vector2 _movement;
      
      //Speed
      public float moveSpeed;
      public float jumpForce;
      
      //Data
      public int amountOfJumps;
      private int _jumpsLeft;

      public GameObject[] workers;
    
      
      public bool turnAroundAnimation;
      public bool topDown;

      //Components
      private Rigidbody2D _rb2d;
      private AudioSource _audioSource;
      
      //Sound
      [Header("AudioClips")]
      public AudioClip jumpSound;

      public AudioClip pickUpCoin;

      void Start()
      {
          _rb2d = GetComponent<Rigidbody2D>();
          _audioSource = GetComponent<AudioSource>();
      }
  
      void Update()
      {
          _moveHorizontal = Input.GetAxis("Horizontal");

          if (topDown)
          {
              _moveVertical = Input.GetAxis("Vertical");   
              
              _movement = new Vector2 (_moveHorizontal, _moveVertical);
          }
          else
          {
              _movement = new Vector2 (_moveHorizontal, 0);
          }
          

          if (!topDown)
          {
              if (Input.GetKeyDown("space") || Input.GetKeyDown("w") || Input.GetKeyDown("up"))
              {
                  if (_jumpsLeft > 0)
                  {
                      _jumpsLeft--;
                      Jump();    
                  }
              }
          }
          
          //animation
          if (turnAroundAnimation && GameManager.Instance.state == GameManager.State.InGame)
          {
              if (_moveHorizontal < 0)
              {
                  transform.rotation = Quaternion.Euler(0, 180, 0);
              } else if (_moveHorizontal > 0)
              {
                  transform.rotation = Quaternion.Euler(0, 0, 0);
              }
          }
      }

      private void OnCollisionEnter2D(Collision2D other)
      {
          if (!topDown)
          {
              if (other.gameObject.CompareTag("Ground"))
              {
                  _jumpsLeft = amountOfJumps;
              }
          }
          
          if (other.gameObject.CompareTag("School"))
          {
              if (GameManager.Instance.coins > 9)
              {
                  GetWorker();    
              }
          }

          if (other.gameObject.CompareTag("Finish"))
          {
              if (GameManager.Instance.coins > 100)
              {
                  GameManager.Instance.state = GameManager.State.PlayAgain;
              }
          }
      }

      private void OnTriggerEnter2D(Collider2D other)
      {
          if (other.gameObject.CompareTag("Coin"))
          {
              GameManager.Instance.coins++;
              _audioSource.PlayOneShot(pickUpCoin);
              Destroy(other.gameObject);
          }
      }

      private void FixedUpdate()
      {
          if (GameManager.Instance.state == GameManager.State.InGame)
          {
              _rb2d.AddForce (_movement * moveSpeed);
          }
      }

      private void Jump()
      {
          if (GameManager.Instance.state == GameManager.State.InGame)
          {
              _rb2d.AddForce(new Vector2(_rb2d.velocity.x, jumpForce));
              _audioSource.PlayOneShot(jumpSound);
          }
      }

      public void GetWorker()
      {
          foreach (var VARIABLE in workers)
          {
              VARIABLE.SetActive(true);
          }
      }
}
