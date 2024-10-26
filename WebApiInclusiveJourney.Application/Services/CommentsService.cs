using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiInclusiveJourney.Application.DTO.Response;
using WebApiInclusiveJourney.Application.IServices;
using WebApiInclusiveJourney.Repository;

namespace WebApiInclusiveJourney.Application.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly WebApiInclusiveJourneyContext _ctx;
        public CommentsService(WebApiInclusiveJourneyContext context)
        {
            _ctx = context;
        }

        public List<CommentsResponse> GetComments()
        {
            try
            {
                var comments = _ctx.tabComments.ToList();

                var response = comments.Select(c => new CommentsResponse
                {
                    codigo = c.codigo, 
                    description = c.description,   
                    namePerson = c.namePerson
                                       
                }).ToList();

                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
