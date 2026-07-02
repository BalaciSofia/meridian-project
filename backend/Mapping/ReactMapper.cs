using backend.DTOs;
using backend.Models;
using Riok.Mapperly.Abstractions;

namespace backend.Mapping
{
    [Mapper]
    public partial class ReactMapper
    {
        [MapperIgnoreTarget(nameof(React.Id))]
        [MapperIgnoreTarget(nameof(React.PostId))]
        [MapperIgnoreTarget(nameof(React.Post))]
        [MapperIgnoreTarget(nameof(React.AccountId))]
        [MapperIgnoreTarget(nameof(React.Account))]
        public partial React ToEntity(CreateReactRequest request);

        [MapperIgnoreSource(nameof(React.Post))]
        [MapperIgnoreSource(nameof(React.Account))]
        public partial ReactResponse ToResponse(React react);

        public partial IEnumerable<ReactResponse> ToResponses(IEnumerable<React> reacts);
    }
}
