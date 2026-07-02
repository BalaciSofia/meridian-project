using backend.DTOs;
using backend.Models;
using Riok.Mapperly.Abstractions;

namespace backend.Mapping
{
    [Mapper]
    public partial class CommentMapper
    {
        [MapperIgnoreTarget(nameof(Comment.Id))]
        [MapperIgnoreTarget(nameof(Comment.PostId))]
        [MapperIgnoreTarget(nameof(Comment.Post))]
        [MapperIgnoreTarget(nameof(Comment.AccountId))]
        [MapperIgnoreTarget(nameof(Comment.Account))]
        [MapperIgnoreTarget(nameof(Comment.CreatedAt))]
        public partial Comment ToEntity(CreateCommentRequest request);

        [MapPropertyFromSource(nameof(CommentResponse.AuthorName), Use = nameof(MapAuthorName))]
        [MapperIgnoreSource(nameof(Comment.Post))]
        public partial CommentResponse ToResponse(Comment comment);

        public partial IEnumerable<CommentResponse> ToResponses(IEnumerable<Comment> comments);

        private static string MapAuthorName(Comment comment)
        {
            return $"{comment.Account.FirstName} {comment.Account.LastName}".Trim();
        }
    }
}
