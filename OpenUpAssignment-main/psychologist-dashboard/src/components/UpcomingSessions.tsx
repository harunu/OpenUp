import React from 'react';

const UpcomingSessions = () => {
    const sessions = [
        { id: 1, time: '10:00 AM', psychologist: 'Dr. A' },
        { id: 2, time: '11:00 AM', psychologist: 'Dr. B' },
    ];

    const removeSession = (id: number) => {
        console.log(`Removing session ID: ${id}`); 
    };

    return (
        <div>
            <h3>Upcoming Sessions</h3>
            <ul>
                {sessions.map((session) => (
                    <li key={session.id}>
                        {session.time} with {session.psychologist}{' '}
                        <button onClick={() => removeSession(session.id)}>Remove</button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default UpcomingSessions;
