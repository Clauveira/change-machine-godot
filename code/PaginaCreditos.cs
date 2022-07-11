using Godot;
using System;

public class PaginaCreditos : Control
{
    CPUParticles2D particulas;
    RandomNumberGenerator rand = new RandomNumberGenerator();

    PackedScene shaderPackedScene = (PackedScene)ResourceLoader.Load("res://scenes/ShaderCreditos.tscn");
    PackedScene orbitParticlesPackedScene = (PackedScene)ResourceLoader.Load("res://scenes/efeitos/orbit_coins_particles.tscn");
    PackedScene gravityParticlesPackedScene = (PackedScene)ResourceLoader.Load("res://scenes/efeitos/gravity_coins_particles.tscn");

    public override void _Ready()
    {
        rand.Randomize();
    }

    private void _on_CreditosControl_visibility_changed()
    {
        if (Visible)
        {
            int valor_sorteado;
            if (OS.GetName() == "Windows" || OS.GetName() == "Linux")
            {
                valor_sorteado = rand.RandiRange(0, 2);
            }
            else
            {
                valor_sorteado = rand.RandiRange(0, 1);
            }

            switch (valor_sorteado)
            {
                case 0:
                    GetNode<Control>("EfeitoContainer").AddChild((CPUParticles2D)gravityParticlesPackedScene.Instance<CPUParticles2D>());
                    GetNode<Control>("EfeitoContainer").SetAnchorsAndMarginsPreset(LayoutPreset.CenterTop);
                    break;
                case 1:
                    GetNode<Control>("EfeitoContainer").AddChild((CPUParticles2D)orbitParticlesPackedScene.Instance<CPUParticles2D>());
                    GetNode<Control>("EfeitoContainer").SetAnchorsAndMarginsPreset(LayoutPreset.Center);
                    break;
                case 2:
                    GetNode<Control>("EfeitoContainer").AddChild((TextureRect)shaderPackedScene.Instance<TextureRect>());
                    GetNode<Control>("EfeitoContainer").SetAnchorsAndMarginsPreset(LayoutPreset.Wide);
                    break;
            }
        }
        else
        {
            for (int i = 0; i < GetNode<Control>("EfeitoContainer").GetChildCount(); i++)
            {
                GetNode<Control>("EfeitoContainer").RemoveChild(GetNode<Control>("EfeitoContainer").GetChild(i));
            }
        }
    }

}
