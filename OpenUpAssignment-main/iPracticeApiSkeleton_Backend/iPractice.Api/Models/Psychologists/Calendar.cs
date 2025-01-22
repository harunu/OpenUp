
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPractice.Api.Models.Psychologists;

public class Calendar
{
    public List<AvailableTimeSlot> AvailableTimeSlots { get; } = [];
    public List<BookedAppointment> BookedAppointments { get; } = [];

    private Calendar()
    {
    }

    public static Calendar Empty() => new();

    public AvailableTimeSlot AddAvailableTimeSlot(AvailableTimeSlot timeSlot)
    {
        AvailableTimeSlots.Add(timeSlot);

        return timeSlot;
    }

    public void CancelAvailableTimeSlot(string availabilityId)
    {
        var bookedAppointment = BookedAppointments.SingleOrDefault(x => x.Id == availabilityId);
        var availability = AvailableTimeSlots.Single(x => x.Id == availabilityId);

        AvailableTimeSlots.Remove(availability);
    }

    public List<AvailableTimeSlot> GetAvailableTimeSlotsFrom(DateTime soonestDate) =>
        AvailableTimeSlots.Where(ats => ats.From >= soonestDate).ToList();

    public BookedAppointment BookAppointment(string availableTimeSlotId, long clientId)
    {
        var availableTimeSlot = AvailableTimeSlots.SingleOrDefault(ats => ats.Id == availableTimeSlotId);

        AvailableTimeSlots.Remove(availableTimeSlot);

        var appointment = new BookedAppointment(availableTimeSlot, clientId);
        BookedAppointments.Add(appointment);

        return appointment;
    }

    public CancelledBooking CancelBookedAppointment(string bookedAppointmentId)
    {
        var appointment = BookedAppointments.SingleOrDefault(x => x.Id == bookedAppointmentId);

        BookedAppointments.Remove(appointment);

        var cancelledAppointment = appointment.ToCancelledBooking();

        AvailableTimeSlots.Add(cancelledAppointment.ToAvailableTimeSlot());

        return cancelledAppointment;
    }
}