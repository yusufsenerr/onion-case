using API.Common.Application.Features.Commands.Order.Update;
using MediatR;
using Quartz;

public class UpdateOrderStatusJob : IJob
{
    private readonly IMediator _mediator;

    public UpdateOrderStatusJob(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        
        await _mediator.Send(new UpdateOrdersToCompletedCommand());
    }
}
