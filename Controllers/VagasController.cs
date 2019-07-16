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

        public IHttpActionResult GetVaga()
        {
            var vaga = db.Vagas.OrderBy(v => v.DataCadastro);

            if (vaga == null)
                return NotFound();

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
    }
}
