using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.Models.EF;
using System.Security.Claims;

namespace PubSub.OcppServer.Data
{
    public class FacilityRepository : GenericRepository<Facility>, IFacilityRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _email;
        private readonly bool _isAdmin;
        public FacilityRepository(
            ChargingContext context,
            IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _httpContextAccessor = httpContextAccessor;
            _email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            _isAdmin = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value == "True";

        }
        public List<Facility> GetAuthorizedFacilities()
        {
            var facilities = _context
                .Facilities
                .AsNoTracking()
                .Include(f => f.ChargingPoints)
                .Where(f => f.UserId == _email || _isAdmin);
            return facilities.ToList();
        }
    }
}