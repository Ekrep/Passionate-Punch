﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{

    private Vector3 _firstPos;
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(true);
        _firstPos=background.gameObject.transform.position;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.transform.position = _firstPos;
        base.OnPointerUp(eventData);
    }
}