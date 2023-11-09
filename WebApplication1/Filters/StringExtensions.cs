using System.Text.RegularExpressions;

namespace WebApplication1.Filters
{
    public static class StringExtensions
    {
        public static string SepararPalavrasPorMaiusculas(this string nomeMetodo)
        {
            string padraoParaSepararPorMaiusculas = @"([A-Z][a-z]*)";

            MatchCollection matches = Regex.Matches(nomeMetodo, padraoParaSepararPorMaiusculas);

            string nomeMetodoSeparado = "";

            foreach (Match m in matches)
            {
                nomeMetodoSeparado += m.Value + " ";
            }

            return nomeMetodoSeparado;
        }
    }
}
