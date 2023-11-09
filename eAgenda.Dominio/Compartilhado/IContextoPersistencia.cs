using System.Threading.Tasks;

namespace eAgenda.Dominio
{
    public interface IContextoPersistencia
    {
        void DesfazerAlteracoes();
        void GravarDados();
        Task<bool> GravarDadosAsync();
    }
}
