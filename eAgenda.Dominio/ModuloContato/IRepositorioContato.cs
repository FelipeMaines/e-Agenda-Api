using eAgenda.Dominio.Compartilhado;
using System.Collections.Generic;
using System;
using Microsoft.Win32;
using System.Threading.Tasks;

namespace eAgenda.Dominio.ModuloContato
{
    public interface IRepositorioContato : IRepositorio<Contato>
    {
        List<Contato> SelecionarTodos(StatusFavoritoEnum statusFavorito);

        Task<List<Contato>> SelecionarTodosAsync(StatusFavoritoEnum statusFavorito);
       
    }
}
