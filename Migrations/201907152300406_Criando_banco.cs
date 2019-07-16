namespace WebApiVagas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criando_banco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vagas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 100),
                        Descricao = c.String(nullable: false, maxLength: 200),
                        Empresa = c.String(nullable: false, maxLength: 50),
                        Requisitos = c.String(nullable: false, maxLength: 200),
                        SalarioInicial = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalarioMaximo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ativa = c.Boolean(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        EstadoTrabalho = c.String(nullable: false, maxLength: 2),
                        CidadeTrabalho = c.String(nullable: false, maxLength: 50),
                        EmailContato = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vagas");
        }
    }
}
