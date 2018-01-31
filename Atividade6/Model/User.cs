 using System;
using Newtonsoft.Json;

namespace Atividade6.Model
{
    public class User
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
