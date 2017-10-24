using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using TenMinutePlatformIntegration2.Models;

namespace TenMinutePlatformIntegration2.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using TenMinutePlatformIntegration2.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Request>("Requests");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class RequestsController : ODataController
    {
        private Primary db = new Primary();

        // GET: odata/Requests
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<Request> GetRequests()
        {
            return db.Requests;
        }

        // GET: odata/Requests(5)
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public SingleResult<Request> GetRequest([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.Requests.Where(request => request.Id == key));
        }

        // PUT: odata/Requests(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<Request> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Request request = await db.Requests.FindAsync(key);
            if (request == null)
            {
                return NotFound();
            }

            patch.Put(request);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(request);
        }

        // POST: odata/Requests
        public async Task<IHttpActionResult> Post(Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Requests.Add(request);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RequestExists(request.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(request);
        }

        // PATCH: odata/Requests(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<Request> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Request request = await db.Requests.FindAsync(key);
            if (request == null)
            {
                return NotFound();
            }

            patch.Patch(request);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(request);
        }

        // DELETE: odata/Requests(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            Request request = await db.Requests.FindAsync(key);
            if (request == null)
            {
                return NotFound();
            }

            db.Requests.Remove(request);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestExists(Guid key)
        {
            return db.Requests.Count(e => e.Id == key) > 0;
        }
    }
}
