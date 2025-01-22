using System;
using System.Collections.Generic;

namespace iPractice.Api.Models.Psychologists;

public class Psychologist : Entity
{
    public string Name { get; private set; }
    public Calendar Calendar { get; } = Calendar.Empty();
    public ClientAssignment ClientAssignment { get; }

    private Psychologist()
    {

    }

    private Psychologist(string name, ClientAssignment clientAssignment)
    {
        Name = name;
        ClientAssignment = clientAssignment;
    }

    public static Psychologist Create(string name, List<long> clientIds) =>
        new(name, ClientAssignment.InitializeFrom(clientIds));

    public void AssignNewClient(long clientId) => ClientAssignment.AssignNewClient(clientId);
    public void DeAssignClient(long clientId) => ClientAssignment.DeAssignClient(clientId);

    public void AddAvailableTimeSlot(AvailableTimeSlot timeSlot) => Calendar.AddAvailableTimeSlot(timeSlot);
    public void CancelAvailableTimeSlot(string availabilityId) => Calendar.CancelAvailableTimeSlot(availabilityId);

    public List<AvailableTimeSlot> GetAvailableTimeSlotsFrom(DateTime soonestDate) =>
        Calendar.GetAvailableTimeSlotsFrom(soonestDate);

    public BookedAppointment BookAppointment(string availableTimeSlotId, long clientId)
    {
        return Calendar.BookAppointment(availableTimeSlotId, clientId);
    }

    public CancelledBooking CancelBookedAppointment(string bookedAppointmentId) => Calendar.CancelBookedAppointment(bookedAppointmentId);
}