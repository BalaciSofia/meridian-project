using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ReactsService
    {
        private readonly ReactsRepository _reactsRepository;

        public ReactsService(ReactsRepository reactsRepository)
        {
            _reactsRepository = reactsRepository;
        }

        public async Task<ActionResult<IEnumerable<React>>> GetAllReactsForPost(int postId)
        {
            return await _reactsRepository.GetAllReactsForPost(postId);
        }

        public async Task AddReact(React react)
        {
            React? r = await _reactsRepository.FindReactByPostIdAndAccountId(react.PostId, react.AccountId);
            if (r!=null)
            {
                if(react.ReactType == r.ReactType)
                {
                    await _reactsRepository.RemoveReact(react.PostId, react.AccountId);
                }
                else
                {
                    await _reactsRepository.UpdateReact(react);
                }
            }
            else
            {
                await _reactsRepository.AddReact(react);
            }
        }
    }
}
