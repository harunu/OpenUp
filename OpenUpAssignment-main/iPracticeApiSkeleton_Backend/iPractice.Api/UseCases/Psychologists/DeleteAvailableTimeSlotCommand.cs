using MediatR;
using iPractice.Api.Models.Psychologists;
using iPractice.Api.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.UseCases.Psychologists
{
    public class DeleteAvailableTimeSlotCommand : IRequest<Psychologist>
    {
        public long PsychologistId { get; }
        public string ExistingAvailableTimeSlotId { get; }

        public DeleteAvailableTimeSlotCommand(long psychologistId, string existingAvailableTimeSlotId)
        {
            PsychologistId = psychologistId;
            ExistingAvailableTimeSlotId = existingAvailableTimeSlotId;
        }
    }

    public class DeleteAvailableTimeSlotHandler : IRequestHandler<DeleteAvailableTimeSlotCommand, Psychologist>
    {
        private readonly IPsychologistSqlRepository _psychologistSqlRepository;

        public DeleteAvailableTimeSlotHandler(IPsychologistSqlRepository psychologistSqlRepository)
        {
            _psychologistSqlRepository = psychologistSqlRepository;
        }

        public async Task<Psychologist> Handle(DeleteAvailableTimeSlotCommand request, CancellationToken cancellationToken)
        {
            var psychologist = await _psychologistSqlRepository.GetPsychologistByIdAsync(request.PsychologistId, cancellationToken);

            psychologist.CancelAvailableTimeSlot(request.ExistingAvailableTimeSlotId);

            await _psychologistSqlRepository.SaveChangesAsync(cancellationToken);

            return psychologist;
        }
    }
}