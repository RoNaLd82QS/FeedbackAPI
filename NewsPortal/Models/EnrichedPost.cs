using System.Collections.Generic;

using System.Collections.Generic;

namespace NewsPortal.Models
{
    public class EnrichedPost
    {
        public Post? Post { get; set; }
        public Author? Author { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
