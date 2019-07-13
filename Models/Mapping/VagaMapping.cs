﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiVagas.Models.Entities;
using System.Data.Entity.ModelConfiguration;    

namespace WebApiVagas.Models.Mapping
{
    public class VagaMapping : EntityTypeConfiguration<Vaga>
    {
        public VagaMapping()
        {
            //Cria tabela no banco
            ToTable("Vagas");

            HasKey(v => v.Id);

            Property(v => v.Titulo).IsRequired().HasMaxLength(100);
            Property(v => v.Descricao).IsRequired().HasMaxLength(200);
            Property(v => v.Empresa).IsRequired().HasMaxLength(50);
            Property(v => v.DataCadastro).IsRequired();
            Property(v => v.SalarioInicial).IsRequired();
            Property(v => v.SalarioMaximo).IsRequired();
            Property(v => v.Requisitos).IsRequired().HasMaxLength(200);
            Property(v => v.EstadoTrabalho).IsRequired().HasMaxLength(2);
            Property(v => v.CidadeTrabalho).IsRequired().HasMaxLength(50);
            Property(v => v.EmailContato).IsRequired().HasMaxLength(50);
        }
    }
}