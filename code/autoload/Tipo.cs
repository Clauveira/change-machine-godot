using Godot;
using System;

public class Tipo : Node
{
    public enum VALOR_MOEDA
    {
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
    public enum LADO_MOEDA
    {
        CARA,
        COROA,
    }
}
