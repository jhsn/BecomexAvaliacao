namespace ROBO.API.Model
{
    public class Rotacoes
    {

        public int sttRotacao { get; set; }


        public string situacaoRotacao
        {
            get

            {
                return sttRotacao switch
                {
                    1 => "Rotação -90º",
                    2 => "Rotação -45º",
                    3 => "Em Repouso",
                    4 => "Rotação 45º",
                    5 => "Rotação 90º"
                };
            }

            private set
            {
                situacaoRotacao = value;
            }
        }

        public Rotacoes()
        {
            sttRotacao = 3;
        }

    }
}
