using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartCommand : IRequest<CartResult>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public List<CartCartItem> Products { get; set; }

    }
}
