using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApiVagas.Models.Entities;
using WebApiVagas.Models.Mapping;

namespace WebApiVagas.Models.Context
{
    public class VagasContext : DbContext
    {
        public VagasContext():base("DbVagas")
        {
        }

        //Quando o banco de dados for criado será considerado as regras de validação especificas na classe Mapping:
        //Para isso é necessário sobrescrever o método abaixo que já existe na classe pai
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add<Vaga>(new VagaMapping());
        }

        public DbSet<Vaga> Vagas { get; set; }
    }
}