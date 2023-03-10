using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Card Properties")]
    public string rankName;
    public string suit;
    public int value;
    public bool isFaceUp;
    public bool isSelected;

    [Header("Selection Properties")]
    public SelectionOutline selectionOutline;
    public Color selectionColour;

    private bool startFacing;
    private Renderer renderer;

    void Start()
    {
        selectionOutline = FindObjectOfType<SelectionOutline>();
        renderer = GetComponent<Renderer>();
        Initialize();
    }

    private void Update()
    {
        renderer.material.SetColor("_Color", isSelected ? selectionColour : Color.white);
    }

    public void Flip()
    {
        isFaceUp = !isFaceUp;
        transform.position = new Vector3(transform.position.x, 7.5f, transform.position.z);
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, (isFaceUp) ? 0.0f : 180.0f); // ternary operator
        isSelected = isFaceUp;
    }

    string toString()
    {
        return $"{rankName} of {suit}s";
    }

    private void Initialize()
    {
        isSelected = false;
        isFaceUp = false;

        var suitRankStrings = name.Split("_");

        var numberWords = new[]
        {
            "Zero", "Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King"
        };

        suit = suitRankStrings[0];

        switch (suitRankStrings[1])
        {
            case "A":
                value = 1;
                break;
            case "J":
                value = 11;
                break;
            case "Q":
                value = 12;
                break;
            case "K":
                value = 13;
                break;
            default:
                suit = suitRankStrings[0];
                Int32.TryParse(suitRankStrings[1], out value); // convert to an int

                break;
        }

        rankName = numberWords[value];

        startFacing = isFaceUp;
    }

    void OnMouseEnter()
    {
        EnableSelectionOutline();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Flip();
        }
    }

    void OnMouseExit()
    {
        DisableSelectionOutline();
    }

    // External Code

    private void EnableSelectionOutline()
    {
        Ray ray = selectionOutline.cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            selectionOutline.TargetRenderer = hit.transform.GetComponent<Renderer>();
            if (selectionOutline.lastTarget == null) selectionOutline.lastTarget = selectionOutline.TargetRenderer;
            if (selectionOutline.SelectionMode == SelMode.AndChildren)
            {
                if (selectionOutline.ChildrenRenderers != null)
                {
                    Array.Clear(selectionOutline.ChildrenRenderers, 0, selectionOutline.ChildrenRenderers.Length);
                }

                selectionOutline.ChildrenRenderers = hit.transform.GetComponentsInChildren<Renderer>();
            }


            if (selectionOutline.TargetRenderer != selectionOutline.lastTarget || !selectionOutline.Selected)
            {
                selectionOutline.SetTarget();
            }

            selectionOutline.lastTarget = selectionOutline.TargetRenderer;
        }
        else
        {
            selectionOutline.TargetRenderer = null;
            selectionOutline.lastTarget = null;
            if (selectionOutline.Selected)
            {
                selectionOutline.ClearTarget();
            }
        }
    }

    private void DisableSelectionOutline()
    {
        if (selectionOutline.Selected)
        {
            selectionOutline.ClearTarget();
        }
    }
}

