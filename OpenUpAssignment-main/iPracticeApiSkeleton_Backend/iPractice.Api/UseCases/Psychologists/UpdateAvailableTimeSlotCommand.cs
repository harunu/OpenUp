using iPractice.Api.Models.Psychologists;
using iPractice.Api.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.UseCases.Psychologists;

public class UpdateAvailableTimeSlotCommand(long psychologistId, string existingAvailableTimeSlotId, DateTime from, DateTime to) : IRequest<Psychologist>
{
    public long PsychologistId { get; } = psychologistId;
    public string ExistingAvailableTimeSlotId { get; } = existingAvailableTimeSlotId;
    public DateTime From { get; } = from;
    public DateTime To { get; } = to;
}

public class UpdateAvailableTimeSlotHandler(IPsychologistSqlRepository psychologistSqlRepository) : IRequestHandler<UpdateAvailableTimeSlotCommand, Psychologist>
{
    public async Task<Psychologist> Handle(UpdateAvailableTimeSlotCommand request, CancellationToken cancellationToken)
    {
        var psychologist = await psychologistSqlRepository.GetPsychologistByIdAsync(request.PsychologistId, cancellationToken);

        psychologist.CancelAvailableTimeSlot(request.ExistingAvailableTimeSlotId);
        psychologist.AddAvailableTimeSlot(new(request.From, request.To));

        await psychologistSqlRepository.SaveChangesAsync(cancellationToken);

        return psychologist;
    }
}
