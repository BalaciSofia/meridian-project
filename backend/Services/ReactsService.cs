using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class ReactsService : IReactsService
    {
        private readonly IReactsRepository _reactsRepository;

        public ReactsService(IReactsRepository reactsRepository)
        {
            _reactsRepository = reactsRepository;
        }

        public async Task<IEnumerable<React>> GetAllReactsForPost(int postId)
        {
            return await _reactsRepository.GetAllReactsForPost(postId);
        }

        public async Task AddReact(React react)
        {
            React? existingReact = await _reactsRepository.FindReactByPostIdAndAccountId(
                react.PostId,
                react.AccountId
            );

            if (existingReact != null)
            {
                if (react.ReactType == existingReact.ReactType)
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
