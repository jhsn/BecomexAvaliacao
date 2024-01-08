namespace ROBO.API.Model
{
    public class Inclinacoes
    {
        /// <summary>
        /// status da inclinação da cabeça
        /// 1 - Para Cima 2 - Em Repouso 3 - Para baixo
        /// </summary>
        public int sttInclinacao { get; set; }

        /// <summary>
        /// situação da inclinação da cabeça 
         /// 1 - Para Cima 2 - Em Repouso 3 - Para baixo
        /// </summary>
        public string situacaoInclinacao
        {
            get
            {
                return sttInclinacao switch
                {
                    1 => "Para Cima",
                    2 => "Em Repouso",
                    3 => "Para baixo"
                };
            }

            private set
            {
                situacaoInclinacao = value;
            }
        }

        public Inclinacoes()
        {
            sttInclinacao = 2;
        }

    }
}
