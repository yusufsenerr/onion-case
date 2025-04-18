using API.Common.Domain.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Common.Application.Features.Commands.Product.Remove
{
    public class RemoveProductCommandRequest:IRequest<BaseResponse>
    {
        public string Id { get; set; }
    }
}
