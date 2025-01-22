using iPractice.Api.Models.Psychologists;
using iPractice.Api.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.UseCases.Psychologists;

public class CreateNewAvailableTimeSlotCommand(long psychologistId, DateTime from, DateTime to) : IRequest<Psychologist>
{
    public long PsychologistId { get; } = psychologistId;
    public DateTime From { get; } = from;
    public DateTime To { get; } = to;
}

public class CreateNewAvailableTimeSlotHandler(IPsychologistSqlRepository psychologistSqlRepository) : IRequestHandler<CreateNewAvailableTimeSlotCommand, Psychologist>
{
    public async Task<Psychologist> Handle(CreateNewAvailableTimeSlotCommand request, CancellationToken cancellationToken)
    {
        var psychologist = await psychologistSqlRepository.GetPsychologistByIdAsync(request.PsychologistId, cancellationToken);

        psychologist.AddAvailableTimeSlot(new AvailableTimeSlot(request.From, request.To));

        await psychologistSqlRepository.SaveChangesAsync(cancellationToken);

        return psychologist;
    }
}
