using System;

namespace DotnetExample.Models
{
    public abstract class Model
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
