import React, { useEffect, useState, useCallback } from 'react';
import { getPsychologistDetails, getClientDetails, deleteBooking } from '../services/api';
import { toast } from 'react-toastify';
import "../styles/app.css";

const UpcomingSessions = ({ psychologistId}) => {
    const [sessions, setSessions] = useState([]);
    const [loading, setLoading] = useState(true);

    const fetchSessions = useCallback(async () => {
        try {
            setLoading(true);

            // Fetch psychologist details
            const psychologistData = await getPsychologistDetails(psychologistId);

            // Fetch client details for booked appointments
            const clientSessions = await Promise.all(
                psychologistData.bookedAppointments.map(async (appointment) => {
                    const clientData = await getClientDetails(appointment.clientId);

                    const date = new Date(appointment.from).toLocaleDateString();
                    const time = new Date(appointment.from).toLocaleTimeString([], {
                        hour: '2-digit',
                        minute: '2-digit',
                        hour12: true,
                    });

                    return {
                        id: appointment.id,
                        date,
                        time,
                        clientName: clientData.name,
                        clientId: appointment.clientId,
                    };
                })
            );
            setSessions(clientSessions);
        } catch (error) {
            console.error('Error fetching sessions:', error);
            toast.error('Failed to load upcoming sessions.');
        } finally {
            setLoading(false);
        }
    }, [psychologistId]);

    useEffect(() => {
        if (psychologistId) {
            fetchSessions();
        }
    }, [psychologistId, fetchSessions]);

    const removeSession = async (sessionId, clientId ) => {
        try {
            await deleteBooking(clientId, sessionId); 
            toast.success('Session deleted successfully!');
            await fetchSessions(); // Refresh sessions after deletion
        } catch (error) {
            console.error('Error removing session:', error);
            toast.error('Failed to delete session.');
        }
    };

    if (loading) {
        return <p>Loading upcoming sessions...</p>;
    }

    return (
        <div className="container">
            <h3>Upcoming Sessions</h3>
            {sessions.length === 0 ? (
                <div className="no-sessions">
                    <p>No upcoming sessions found</p>
                </div>
            ) : (
                <table className="sessions-table">
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>Time</th>
                            <th>Client Name</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {sessions.map((session) => (
                            <tr key={session.id}>
                                <td>{session.date}</td>
                                <td>{session.time}</td>
                                <td>{session.clientName}</td>
                                <td>
                                    <div style={{ textAlign: "center" }}>
                                        <button onClick={() => removeSession(session.id, session.clientId)}>
                                            Remove
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
        </div>
    );
};

export default UpcomingSessions;
