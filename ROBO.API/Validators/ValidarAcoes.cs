using Microsoft.AspNetCore.Mvc;

namespace ROBO.API.BLL
{
    public class ValidarAcoes
    {
       /// <summary>
       /// Validador da Movimentação do Robo
       /// </summary>
       /// <param name="movimentoAtual"></param>
       /// <param name="movimentoNovo"></param>
       /// <returns></returns>
        public bool ValidarMovimentos(int movimentoAtual, int movimentoNovo)
        {
            if (movimentoAtual - movimentoNovo == 1 || movimentoAtual - movimentoNovo == -1)
            {
                return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Verifação do cotovelo se eleesta fortemente contraido para poder mover o pulso - sttCotovelo tem que ser 4 para estar fortemente contraido
        /// </summary>
        /// <param name="sttCotovelo"></param>
        /// <returns></returns>
        public bool ValidarCotoveloContraido(int sttCotovelo)
        {
           return sttCotovelo == 4 ? true : false;
            
        }

        /// <summary>
        /// Vericação se a cabeça esta iclinada para baixo - sttCabecaBaixo deve ser = 3
        /// </summary>
        /// <param name="sttCabecaBaixo"></param>
        /// <returns></returns>
        public bool ValidarInclinacaoCabecaBaixo(int sttCabecaBaixo)
        {
            return sttCabecaBaixo == 3 ? true : false;

        }

    }
}
