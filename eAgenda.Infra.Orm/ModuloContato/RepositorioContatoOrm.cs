using eAgenda.Dominio;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eAgenda.Infra.Orm.ModuloContato
{
    public class RepositorioContatoOrm : RepositorioBase<Contato>, IRepositorioContato
    {       

        public RepositorioContatoOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }

        public override Contato SelecionarPorId(Guid id)
        {
            return registros
                .Include(x => x.Compromissos)
                .SingleOrDefault(x => x.Id == id);
        }

        public List<Contato> SelecionarTodos(StatusFavoritoEnum statusFavorito)
        { 
            if (statusFavorito == StatusFavoritoEnum.Todos)
                return registros                                        
                    .ToList();

            else if (statusFavorito == StatusFavoritoEnum.Sim)
                return registros
                    .Where(x => x.Favorito == true)                    
                    .ToList();
            else
                return registros
                   .Where(x => x.Favorito == false)                   
                   .ToList();            
        }

        public async Task<List<Contato>> SelecionarTodosAsync(StatusFavoritoEnum statusFavorito)
        {
            if (statusFavorito == StatusFavoritoEnum.Todos)
                return await registros
                    .ToListAsync();

            else if (statusFavorito == StatusFavoritoEnum.Sim)
                return await registros
                    .Where(x => x.Favorito == true)
                    .ToListAsync();
            else
                return await registros
                   .Where(x => x.Favorito == false)
                   .ToListAsync();
        }
    }
}
