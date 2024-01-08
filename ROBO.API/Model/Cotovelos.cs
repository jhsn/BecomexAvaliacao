namespace ROBO.API.Model
{
    public class Cotovelos
    {
        /// <summary>
        /// status atual do cotovelo  selecionar uma das opções 
        /// 1 - Em Repouso  2 - Levemente Contraído  3 - Contraído  4 - Fortemente Contraído
        /// </summary>
        public int sttCotovelo { get; set; }
        
        /// <summary>
        ///  define qual lado do cotovelo vai ser alterado Direito ou esquerdo.
        /// </summary>
        public string ladoCotovelo { get; set; }

        public string situacaoSttCotovelo
        {
            get

            {
                return sttCotovelo switch
                {
                    1 => "Em Repouso",
                    2 => "Levemente Contraído",
                    3 => "Contraído",
                    4 => "Fortemente Contraído"
                };
            }
            private set
            {
                situacaoSttCotovelo = value;
            }

        }

        public Cotovelos()
        {
            sttCotovelo = 1;
        }
    }
}
