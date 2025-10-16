namespace Minimal_Api.Dominio.Servicos
{
    using Minimal_Api.Dominio.Entidades;
    using Minimal_Api.Infraestrutura.Db;
    using Microsoft.EntityFrameworkCore;
    using Minimal_Api.Dominio.Interfaces;
    using System.Collections.Generic;
    using Minimal_Api.DTOs;
    using Microsoft.VisualBasic;

    public class AdministradorServico : IAdministradorServico
    {
        private readonly DbContexto _contexto;
        public AdministradorServico(DbContexto contexto)
        {
            _contexto = contexto;  
        }
        public Administrador? Login(LoginDTO loginDTO)
        {
            var adm = _contexto.Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
            return adm;
        }
    }
}