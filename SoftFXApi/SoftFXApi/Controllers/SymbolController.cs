using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SoftFXApi.Models;
using System.IO;
using Newtonsoft.Json;

namespace SoftFXApi.Controllers
{
    public class SymbolController : ApiController
    {
        private SoftFXApiContext db = new SoftFXApiContext();

        // GET api/Symbol
        public IHttpActionResult Get()
        {
            HttpWebRequest request =
                    (HttpWebRequest)WebRequest.Create("https://ttdemowebapi.soft-fx.com:8443/api/v1/public/symbol");

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string answer = rd.ReadToEnd();
                    var log = JsonConvert.DeserializeObject<List<Symbol>>(answer);
                    foreach (Symbol symbol in log)
                    {
                        db.Symbols.Add(new Symbol() {Name = symbol.Name });
                    }
                    string jsonResult = JsonConvert.SerializeObject(db.Symbols);
                    return Json(jsonResult);
                }
            }


        }

        // GET api/Symbol/5
        [ResponseType(typeof(Symbol))]
        public IHttpActionResult GetSymbol(int id)
        {
            Symbol symbol = db.Symbols.Find(id);
            if (symbol == null)
            {
                return NotFound();
            }

            return Ok(symbol);
        }

        // PUT api/Symbol/5
        public IHttpActionResult PutSymbol(int id, Symbol symbol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != symbol.SymbolId)
            {
                return BadRequest();
            }

            db.Entry(symbol).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SymbolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Symbol
        [ResponseType(typeof(Symbol))]
        public IHttpActionResult PostSymbol(Symbol symbol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Symbols.Add(symbol);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = symbol.SymbolId }, symbol);
        }

        // DELETE api/Symbol/5
        [ResponseType(typeof(Symbol))]
        public IHttpActionResult DeleteSymbol(int id)
        {
            Symbol symbol = db.Symbols.Find(id);
            if (symbol == null)
            {
                return NotFound();
            }

            db.Symbols.Remove(symbol);
            db.SaveChanges();

            return Ok(symbol);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SymbolExists(int id)
        {
            return db.Symbols.Count(e => e.SymbolId == id) > 0;
        }
    }
}