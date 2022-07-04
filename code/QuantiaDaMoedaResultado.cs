using Godot;
using System;

public class QuantiaDaMoedaResultado : HBoxContainer
{
    public void SetQuantiaExibida(int newQuantia)
    {
        GetNode<Label>("Label").Text = "x " + newQuantia;
        Visible = newQuantia > 0;
    }
}
