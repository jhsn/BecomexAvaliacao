namespace ROBO.API.Model
{
    public class Corpo
    {
        public Cabeca Cabeca { get; set; } = new Cabeca();

        public Bracos BracoEsquerdo { get; set; } = new Bracos();

        public Bracos BracoDireito { get; set; } = new Bracos();
    }
}
