using backend.DTOs;
using backend.Models;
using Riok.Mapperly.Abstractions;

namespace backend.Mapping
{
    [Mapper]
    public partial class PostMapper
    {
        [MapperIgnoreTarget(nameof(Post.Id))]
        [MapperIgnoreTarget(nameof(Post.CreatedAt))]
        [MapperIgnoreTarget(nameof(Post.CreatedByAccountId))]
        [MapperIgnoreTarget(nameof(Post.CreatedByAccount))]
        public partial Post ToEntity(CreatePostRequest request);

        [MapPropertyFromSource(nameof(PostResponse.CreatedByName), Use = nameof(MapCreatedByName))]
        public partial PostResponse ToResponse(Post post);

        public partial IEnumerable<PostResponse> ToResponses(IEnumerable<Post> posts);

        private static string MapCreatedByName(Post post)
        {
            return $"{post.CreatedByAccount.FirstName} {post.CreatedByAccount.LastName}".Trim();
        }
    }
}
