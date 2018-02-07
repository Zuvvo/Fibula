using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class hpbar : MonoBehaviour
{
    #region PRIVATE_VARIABLES
    private RectTransform targetkanwas;
    #endregion
    #region PUBLIC_REFERENCES
    public RectTransform healthBar;
    private Transform objectToFollow;
    #endregion
    #region PUBLIC_METHODS
    public void SetHealthBarData(Transform targetTransform, RectTransform healthBarPanel)
    {
        this.targetkanwas = healthBarPanel;
        healthBar = GetComponent<RectTransform>();
        objectToFollow = targetTransform;
        zmianapozycjipaska();
        healthBar.gameObject.SetActive(true);
    }
    public void OnHealthChanged(float healthFill)
    {
     //   objectToFollow.gameObject.GetComponent<asuka>().hp -= 10;
        healthBar.GetComponent<Image>().fillAmount = healthFill;
    }
    void Start()
    {
    }
    #endregion
    #region UNITY_CALLBACKS
    void Update()
    {
        zmianapozycjipaska();
    }
    #endregion
    #region PRIVATE_METHODS
    private void zmianapozycjipaska()
    {
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(objectToFollow.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * targetkanwas.sizeDelta.x)),
        ((ViewportPosition.y * targetkanwas.sizeDelta.y) +15));
        healthBar.anchoredPosition = WorldObject_ScreenPosition;
        if(objectToFollow.name == "asuka")
        {
            Vector3 scale = new Vector3(120, 20, 1);
            healthBar.localScale= scale;
        }

    }
    #endregion
}
//    Vector2 skala = gameObject.transform.localScale;
//    skala.y *= szybkosc;
//        skala.x *= szybkosc;
//        transform.localScale = skala;