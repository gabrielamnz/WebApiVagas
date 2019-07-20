using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiVagas.Models.Entities;
using WebApiVagas.Models.Context;
using WebApiVagas.Filters;
using WebApiVagas.Validation;
using FluentValidation;
using System.Data;
using System.Data.Entity;
using System.Web.Http.OData;

namespace WebApiVagas.Controllers
{
    public class VagasController : ApiController
    {
        private VagasContext db = new VagasContext();
        private VagaValidator validador = new VagaValidator();
        
        //Sempre que for feita uma requisição POST o processo de autenticação será verificado
        [BasicAuthentication]
        public IHttpActionResult PostVaga(Vaga vaga)
        {            
            //Para funcionar o validador é necessário importar o FluentValidation;
            validador.ValidateAndThrow(vaga);

            vaga.Ativa = true;

            db.Vagas.Add(vaga);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vaga.Id }, vaga);            
        }

        public IHttpActionResult GetVaga(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            var vaga = db.Vagas.Find(id);

            if (vaga == null)
                return NotFound();

            return Ok(vaga);
        }

        //[EnableQuery(AllowedQueryOptions =)]
        public IHttpActionResult GetVaga(int pagina = 1, int tamanhoPagina = 10)
        {
            if (pagina <= 0 || tamanhoPagina <= 0)
                return BadRequest("Os parâmetros pagina e tamanhoPagina devem ser maiores que zero.");

            if (tamanhoPagina > 10)
                return BadRequest("O tamanho máximo de página permitido é 10.");

            int totalPaginas = (int)Math.Ceiling(db.Vagas.Count() / Convert.ToDecimal(tamanhoPagina));

            if (totalPaginas > 0 && pagina > totalPaginas)
                return BadRequest("A página solicitada não existe;");

            //Adicionar cabeçalho no header do postman
            System.Web.HttpContext.Current.Response.AddHeader("TotalDePaginas", totalPaginas.ToString());

            if (pagina > 1)
                System.Web.HttpContext.Current.Response.AddHeader("PaginaAnterior", Url.Link("DefaultApi", new { pagina = pagina - 1, tamanhoPagina = tamanhoPagina }));

            if (pagina < totalPaginas)
                System.Web.HttpContext.Current.Response.AddHeader("ProximaPagina", Url.Link("DefaultApi", new { pagina = pagina + 1, tamanhoPagina = tamanhoPagina }));

            var vaga = db.Vagas
                .Where(v => v.Ativa)
                .OrderBy(v => v.Id)
                .Skip(tamanhoPagina * (pagina - 1))
                .Take(tamanhoPagina);
                        
            return Ok(vaga);
        }

        [BasicAuthentication]
        public IHttpActionResult PutVaga(int id, Vaga vaga)
        {
            if (id <= 0)
                return BadRequest("O id informado na url deve ser maior que zero.");

            if (id != vaga.Id)
                return BadRequest("O id informado na URL deve ser igual ao id informado no corpo da requisição.");

            //Verifica se existe uma vaga com esse ID
            if (db.Vagas.Count(v => v.Id == id) == 0)
                return NotFound();

            //Fluent Validation
            validador.ValidateAndThrow(vaga);

            db.Entry(vaga).State = EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);

        }

        [BasicAuthentication]
        public IHttpActionResult DeleteVaga(int id)
        {
            if (id <= 0)
                return BadRequest("O id informado na URL deve ser maior que zero.");

            var vaga = db.Vagas.Find(id);

            if (vaga == null)
                return NotFound();

            db.Vagas.Remove(vaga);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
