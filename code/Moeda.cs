using Godot;
using System;


[Tool]
public class Moeda : Control
{
    public enum TIPO_MOEDA{
        UM_CENTAVO,
        CINCO_CENTAVOS,
        DEZ_CENTAVOS,
        VINTE_E_CINCO_CENTAVOS,
        CINQUENTA_CENTAVOS,
        UM_REAL,
        /*
        DOIS_REAIS,
        CINCO_REAIS,
        DEZ_REAIS,
        VINTE_REAIS,
        CINQUENTA_REAIS,
        CEM_REAIS,
        DUZENTOS_REAIS,
        */
    }

    public enum LADO_MOEDA{
            CARA,
            COROA,
    }

    [Export]
    public TIPO_MOEDA tipo_moeda = TIPO_MOEDA.UM_REAL;
    [Export]
    public LADO_MOEDA lado = LADO_MOEDA.COROA;
    [Export(PropertyHint.Range, "0,100,1,or_greater")]
    public int quantia = 0;

    public override void _Ready()
    {
        GetNode<Label>("PanelContainer/VBoxContainer/LabelQuantia").Text = quantia.ToString();
        
        GetNode<TextureRect>("PanelContainer/VBoxContainer/MoedaTexture").Texture = ResourceLoader.Load(
            "res://sprites/" + tipo_moeda.ToString().ToLower().Replace("_", "-")
            + "-" + lado.ToString().ToLower() + ".png") as Texture;
    }
}
