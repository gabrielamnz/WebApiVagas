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
            //validador.ValidateAndThrow(vaga);

            vaga.Ativa = true;

            db.Vagas.Add(vaga);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vaga.Id }, vaga);
            
        }
    }
}
