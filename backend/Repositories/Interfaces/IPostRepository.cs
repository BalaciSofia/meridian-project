using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<ActionResult<IEnumerable<Post>>> GetAllPost();
    }
}
