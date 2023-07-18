using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour {

    [SerializeField] private float offset = 0.1f;

    private Weapon weapon;
    private Player player;

    private void Start () {
        weapon = GetComponentInChildren<Weapon>();
        player = transform.parent.gameObject.GetComponent<Player>();
    }

    private void Update () {
        float rotation_z = GetRotationValue();
        RotateWeapon(rotation_z);
        ChangeScaleAndFlipPlayerSprite(rotation_z);
        ChangeSortingLayerInWeaponChild(rotation_z);
    }

    private float GetRotationValue() {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        return rotation_z;
    }

    private void RotateWeapon(float rotation) {
        transform.rotation = Quaternion.Euler(0f, 0f, rotation + offset);
    }

    private void ChangeScaleAndFlipPlayerSprite(float rotation) {
        Vector2 scale = transform.localScale;

        if (Mathf.Abs(rotation) > 90) {
            player.FlipSprite(-1f);
            scale.y = -1f;
        }
        else if (Mathf.Abs(rotation) < 90) {
            player.FlipSprite(1f);
            scale.y = 1f;
        }
        transform.localScale = scale;
    }

    private void ChangeSortingLayerInWeaponChild(float rotation) {
        weapon.ChangeSortingLayer(rotation);
    }

}
