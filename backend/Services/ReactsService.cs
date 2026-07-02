using backend.DTOs;
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

        public async Task<IEnumerable<ReactResponse>> GetAllReactsForPost(int postId)
        {
            var reacts = await _reactsRepository.GetAllReactsForPost(postId);

            return reacts.Select(MapToReactResponse);
        }

        public async Task AddReact(int postId, CreateReactRequest request)
        {
            var react = new React
            {
                PostId = postId,
                AccountId = request.AccountId,
                ReactType = request.ReactType
            };

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

        private static ReactResponse MapToReactResponse(React react)
        {
            return new ReactResponse
            {
                Id = react.Id,
                PostId = react.PostId,
                AccountId = react.AccountId,
                ReactType = react.ReactType
            };
        }
    }
}
