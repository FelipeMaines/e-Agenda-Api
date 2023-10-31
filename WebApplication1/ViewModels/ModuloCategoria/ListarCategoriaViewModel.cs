﻿using WebApplication1.ViewModels.ModuloDespesa;

namespace WebApplication1.ViewModels.ModuloCategoria
{
    public class ListarCategoriaViewModel
    {
        public string Titulo { get; set; }
        public ListarDespesaViewModel despesas { get; set; }

        public Guid Id { get; set; }
    }
}
