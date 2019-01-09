using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum PlayerType
{
    Ghost = 0,
    Enemy1 = 1,
    Enemy2 = 2
}

public class PlayerClick : MonoBehaviour
{
    [SerializeField]
    private GameObject NormalModel = null;
    [SerializeField]
    private GameObject Enemy1Model = null;
    [SerializeField]
    private GameObject Enemy2Model = null;
    [SerializeField]
    private Camera MainCam = null;
    [SerializeField]
    public float WalkSpeed = 10.0f;
    [SerializeField]
    private GameObject BulletPrefab = null;
    [SerializeField]
    private GameObject GunLoc = null;
    [SerializeField]
    private Text HPtext = null;
    [SerializeField]
    private Text AMMOtext = null;
    [SerializeField]
    private Text Score = null;
    public float EnemyWalkSpeed = 0;
    [SerializeField]
    private List<Sprite> sprite;
    [SerializeField]
    private Image ImgBar = null;
    [SerializeField]
    private GameObject GameOver = null;
    [SerializeField]
    private bool IsTutorial = false;

    private PlayerType _type = PlayerType.Ghost;
    private int _score = 0;
    private int _maxHp = 3;
    private bool _isDead = false;
    private int _maxEnergy = 4;
    private int _powerCost = 1;
    private int _currentEnergy = 0;
    private int _currentEnemyHp = 0;
    private int _currentEnemyAmmo = 0;



    public PlayerType GetType() { return _type; }



    void Start()
    {
        _currentEnergy = _maxEnergy;
        SetPlayerType(PlayerType.Ghost);
    }

    public void Damage()
    {
        if (IsTutorial) return;
        if (_type == PlayerType.Ghost)
            _maxHp--;
        else
        {
            _currentEnemyHp -= 25;
            if (_currentEnemyHp <= 0)
            {
                SetPlayerType(PlayerType.Ghost);
            }
        }
    }

    private void ReloadGame()
    {
        SceneManager.LoadScene("Load");
    }

    void Update()
    {
        if (_maxHp <= 0)
        {
            _isDead = true;
            UpdateText();
            GameOver.SetActive(true);
            this.transform.position = new Vector3(1000, 1000, 1000);
            Invoke("ReloadGame", 5.0f);
        }



        if (_isDead) return;
        if (!IsTutorial) UpdateText();


        if (_type == PlayerType.Ghost && Input.GetMouseButtonDown(0) && (_currentEnergy >= _powerCost))
        {
            RaycastHit hit;
            Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                var tag = hit.transform.gameObject.tag;
                if (tag == "Untagged")
                    return;

                var e = hit.transform.gameObject.GetComponent<Enemy>();
                _currentEnemyHp = e.HP;
                _currentEnemyAmmo = e.AMMO;
                EnemyWalkSpeed = e.MOVEMENT;
                transform.position = hit.transform.position;
                e.ISDEAD = true;
                e.IGNORE = true;
                if (tag == "Enemy1")
                {
                    SetPlayerType(PlayerType.Enemy1);
                    _currentEnergy -= _powerCost;
                }
                else if (tag == "Enemy2")
                {
                    SetPlayerType(PlayerType.Enemy2);
                    _currentEnergy -= _powerCost;
                }
            }
        }
        else if (_type == PlayerType.Enemy1 && Input.GetMouseButtonDown(0) && _currentEnemyAmmo > 0)
        {
            Shoot(0);
        }
        else if (_type == PlayerType.Enemy2 && Input.GetMouseButtonDown(0) && _currentEnemyAmmo > 0)
        {
            Shoot(0);
            Shoot(-45);
            Shoot(45);
        }
        else if (_type != PlayerType.Ghost && Input.GetButtonDown("Jump"))
        {
                SetPlayerType(PlayerType.Ghost);
        }


    }

    public void ResetEnergy()
    {
        _currentEnergy = 4;
    }

    private void Shoot(float angle)
    {
        var bullet = (GameObject)Instantiate(BulletPrefab, GunLoc.transform.position, GunLoc.transform.rotation);
        bullet.tag = "Player";
        bullet.transform.Rotate(Vector3.up, angle);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 12;
        Destroy(bullet, 2.0f);
        _currentEnemyAmmo--;

    }

    public void AddScore()
    {
        _score += Random.Range(0, 4);
    }

    private void UpdateText()
    {
        switch (_type)
        {
            case PlayerType.Ghost:
                AMMOtext.text = "0";
                HPtext.text = _maxHp.ToString();
                break;
            case PlayerType.Enemy1:
                AMMOtext.text = _currentEnemyAmmo.ToString();
                HPtext.text = _currentEnemyHp.ToString();
                break;
            case PlayerType.Enemy2:
                AMMOtext.text = _currentEnemyAmmo.ToString();
                HPtext.text = _currentEnemyHp.ToString();
                break;
            default:
                break;
        }
        if (_currentEnergy > 4) _currentEnergy = 4;
        ImgBar.sprite = sprite[_currentEnergy];
        Score.text = _score.ToString();
    }


    public void SetPlayerType(PlayerType type)
    {
        _type = type;
        switch (type)
        {
            case PlayerType.Ghost:
                NormalModel.SetActive(true);
                Enemy2Model.SetActive(false);
                Enemy1Model.SetActive(false);
                //AMMOtext.text = "0";
                //HPtext.text = _maxHp.ToString();
                break;
            case PlayerType.Enemy1:
                Enemy1Model.SetActive(true);
                Enemy2Model.SetActive(false);
                NormalModel.SetActive(false);
                //AMMOtext.text = _currentEnemyAmmo.ToString();
                //HPtext.text = _currentEnemyHp.ToString();
                break;
            case PlayerType.Enemy2:
                Enemy2Model.SetActive(true);
                Enemy1Model.SetActive(false);
                NormalModel.SetActive(false);
                //AMMOtext.text = _currentEnemyAmmo.ToString();
                //HPtext.text = _currentEnemyHp.ToString();
                break;
            default:
                break;
        }
    }
}
